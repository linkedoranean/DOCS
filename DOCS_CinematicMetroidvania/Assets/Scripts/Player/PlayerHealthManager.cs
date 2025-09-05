using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float health;

    void Start()
    {
        
    }

    public void AlterHealth(int modifier)
    {
        health -= modifier;
    }
}