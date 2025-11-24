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