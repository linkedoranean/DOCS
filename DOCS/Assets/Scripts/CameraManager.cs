using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Colocar aqui screenshake no filho da câmera

    [SerializeField] private Transform playerParent;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = playerParent.position;
    }
}