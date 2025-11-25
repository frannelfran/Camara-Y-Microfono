using UnityEngine;
using System.IO;

public class CamaraEnPantalla : MonoBehaviour
{
    private WebCamTexture camaraTexture;
    private string nombreCamara;

    void Start()
    {
        Renderer rendererPantalla = GetComponent<Renderer>();

        if (WebCamTexture.devices.Length > 0)
        {
            nombreCamara = WebCamTexture.devices[0].name;
            Debug.Log("Nombre de la cámara: " + nombreCamara);

            camaraTexture = new WebCamTexture(nombreCamara);
            rendererPantalla.material.mainTexture = camaraTexture;
            camaraTexture.Play();
        }
        else
        {
            Debug.LogWarning("No se detectó ninguna cámara.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CapturarFotograma();
        }
    }

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
}