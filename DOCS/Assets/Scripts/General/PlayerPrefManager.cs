using System;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public enum KeyType
    {
        keycardBlue,
        keycardRed,
        keycardYellow,
        keycardGreen,
        keycardPurple,
        overrideAlpha,
        overrideBeta,
        overrideGamma,
        overrideOmega,
        torch
    }
    public KeyType keyDesignation;

    [ContextMenu("Reset All PlayerPrefs")]
    void ResetCollectedKeys()
    {
        foreach (var values in Enum.GetValues(typeof(KeyType)))
        {
            PlayerPrefs.DeleteKey(values.ToString());
        }
    }
}
