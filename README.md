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
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        micDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (micDevice == null)
        {
            Debug.LogWarning("No microphone detected.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && micDevice != null)
        {
            if (!isRecording)
            {
                audioSource.clip = Microphone.Start(micDevice, false, 10, 44100);
                audioSource.loop = false;
                isRecording = true;
                Debug.Log("Recording started...");
            }
            else
            {
                Microphone.End(micDevice);
                audioSource.Play();
                isRecording = false;
                Debug.Log("Playback started...");
            }
        }
    }
}
```

En la condición me aseguro de que esa funcionalidad sólo se utilice si se pulsa la tecla R y luego hago todo el procedimiento de almacenar el audio y luego mostrarlo.

### Resultado

https://github.com/user-attachments/assets/439e88bd-2584-4f00-8956-aa46f9f8140d