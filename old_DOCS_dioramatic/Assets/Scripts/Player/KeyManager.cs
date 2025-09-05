using UnityEngine;

public class KeyManager : MonoBehaviour
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

    void Awake()
    {
        if (PlayerPrefs.HasKey(keyDesignation.ToString()))
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(keyDesignation.ToString(), 1);
        }
        
        gameObject.SetActive(false);
    }
}