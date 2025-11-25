# Camara-Y-Microfono

1. Utilizar la escena de los guerreros y activa la reproducción de alguno de los sonidos incluidos en la carpeta adjunta cuando un guerrero alcanza algún objetivo. Para reproducir sonido en una aplicación Unity es necesario utilizar un objeto AudioSource. El objeto AudioSource reproduce el sonido que contiene un AudioClip, que podemos instanciar arrastrando desde el editor el asset con el clip de audio que esté importado en la escena.

Para este problema he utilizado los eventos para su resolución, el codigo es el siguiente.

```csharp
using UnityEngine;

public class GuerreroDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Guerrero"))
        {
            GameEvents.GuerreroAlcanzaObjetivo(other.gameObject);
        }
    }
}
```
Y luego el guerrero que será considerado como el objetivo, tiene el siguiente código:

```csharp
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnEnable()
    {
        GameEvents.OnGuerreroAlcanzaObjetivo += ReproducirSonido;
    }

    private void OnDisable()
    {
        GameEvents.OnGuerreroAlcanzaObjetivo -= ReproducirSonido;
    }

    private void ReproducirSonido(GameObject guerrero)
    {
        audioSource.Play();
    }
}
```

### Resultado
https://github.com/user-attachments/assets/409dc0ec-00ec-40b3-a5e9-7fcb374bd7b6

2. Del mismo modo que podemos reproducir un sonido previamente grabado, podemos hacer que se reproduzca el AudioClip que genera el micrófono utilizando la función Microphone.Start. Crea una escena en la que estés en un espacio abierto en el que habrá una pantalla central con altavoces que, al pulsar la tecla R, reproduzcan el sonido que se obtenga por el micrófono del dispositivo.

Para este ejercicio he hecho una especie de tele con altavoces juntando un plano y 2 cubos y luego he hecho el siguiente script donde escucho el audio de entrada gracias al micrófono y luego lo suelto.

```csharp
using UnityEngine;

public class CapturadorAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private string micDevice;
    private bool isRecording = false;

    void Start()
**Resumen (breve) — qué hace `CapturarFotograma`**

- Comprueba que la `WebCamTexture` está disponible y reproduciéndose.
- Crea una `Texture2D` con las mismas dimensiones, copia los píxeles del frame actual y aplica los cambios.
- Convierte la textura a PNG (`EncodeToPNG`) y guarda el archivo en `Application.persistentDataPath` usando `File.WriteAllBytes`.
- Logea en consola la ruta del fichero guardado.

Notas: uso puntual (coste en rendimiento al obtener píxeles), requiere `using System.IO;` para escribir el fichero y debe ejecutarse en el hilo principal de Unity.

                isRecording = true;
                Debug.Log("Recording started...");
            }
             ```csharp
             void CapturarFotograma()
                 {
                     if (camaraTexture != null && camaraTexture.isPlaying)
                     {
                         Texture2D imagenCapturada = new Texture2D(camaraTexture.width, camaraTexture.height);
                         imagenCapturada.SetPixels(camaraTexture.GetPixels());
                         imagenCapturada.Apply();

                         byte[] datosImagen = imagenCapturada.EncodeToPNG();
                         string ruta = Application.persistentDataPath + "/captura_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                         File.WriteAllBytes(ruta, datosImagen);

                         Debug.Log("Fotograma capturado y guardado en: " + ruta);
                     }
                 }
             ```

            **Resumen (muy breve):** captura el frame actual de la `WebCamTexture`, lo convierte a PNG y lo guarda en `Application.persistentDataPath` (requiere `using System.IO;`).
```csharp
camaraTexture = new WebCamTexture(nombreCamara);
            rendererPantalla.material.mainTexture = camaraTexture;
            camaraTexture.Play();
```

### Resultado

https://github.com/user-attachments/assets/8f01a129-44f3-46b1-a699-7183f6fd1868

4. Debe ser posible capturar fotogramas aislados y conservarlos en memoria como imágenes fijas. Se debe crear un objeto para la textura, `Texture2D`, y pasarle la imagen-frame de la cámara como un bloque de píxeles y, finalmente, almacenar en un fichero dicha textura.

Para esto, yo lo que hice fue si el usuario pulsa la tecla C se realiza lo siguiente:

```csharp
void CapturarFotograma()
    {
        if (camaraTexture != null && camaraTexture.isPlaying)
        {
            Texture2D imagenCapturada = new Texture2D(camaraTexture.width, camaraTexture.height);
            imagenCapturada.SetPixels(camaraTexture.GetPixels());
            imagenCapturada.Apply();

            byte[] datosImagen = imagenCapturada.EncodeToPNG();
            string ruta = Application.persistentDataPath + "/captura_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            File.WriteAllBytes(ruta, datosImagen);

            Debug.Log("Fotograma capturado y guardado en: " + ruta);
        }
    }
```

Primero compruebo que la camara este funcionando correctamente, luego el `new Texture2D` crea una textura en memoria con las dimensiones del plano donde se encuentra alojada la cámara, luego copio los pixeles del frame acgtual y por último almaceno el .png en los datos del sistema.

### Resultado

