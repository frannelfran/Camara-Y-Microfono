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