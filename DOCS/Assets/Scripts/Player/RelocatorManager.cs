using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class RelocatorManager : MonoBehaviour
{
    public delegate void RelocatorAction(Vector3 relocationPos);
    public static event RelocatorAction OnRelocated;
    
    [SerializeField] private Vector3 newPos;
    
    void Awake()
    {
        MovementManager.OnMoved += ChangeStages;
    }
    
    void OnDestroy()
    {
        MovementManager.OnMoved -= ChangeStages;
    }

    void ChangeStages(string transferDirection)
    {
        switch (transferDirection)
        {
            case "Up":
                newPos = new Vector3(0, 0, -6);
                break;
            case "Down":
                newPos = new Vector3(0, 0, 6);
                break;
            case "Left":
                newPos = new Vector3(6, 0, 0);
                break;
            case "Right":
                newPos = new Vector3(-6, 0, 0);
                break;
            default:
                print ("Incorrect intelligence level.");
                break;
        }

        RelocatePlayer(newPos);
    }

    void RelocatePlayer(Vector3 relocationPos)
    {
        OnRelocated?.Invoke(relocationPos);
    }
}