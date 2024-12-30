using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Colocar aqui screenshake no filho da câmera

    [SerializeField] private Transform stalkedObj;
    
    void Awake()
    {
        SelectionManager.ChangeCamera += ChangeCamRef;
    }
    
    void OnDestroy()
    {
        SelectionManager.ChangeCamera -= ChangeCamRef;
    }
    
    void Start()
    {
        
    }

    void ChangeCamRef(Transform objct)
    {
        stalkedObj = objct;
    }

    void Update()
    {
        if (stalkedObj != null)
        {
            transform.position = stalkedObj.position;
        }
    }
}