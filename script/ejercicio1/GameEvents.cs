using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> OnGuerreroAlcanzaObjetivo;

    public static void GuerreroAlcanzaObjetivo(GameObject guerrero)
    {
        OnGuerreroAlcanzaObjetivo?.Invoke(guerrero);
    }
}