using System;
using UnityEngine;

public class MoverManager : MonoBehaviour
{
    public delegate void MoverEvent(string direction);
    public static event MoverEvent onCollided;
    
    [SerializeField] private string trigger;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onCollided?.Invoke(trigger);
        }
    }
}