using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public delegate void WeaponsBroadcast();
    public static event WeaponsBroadcast OnFinished;
    
    void Awake()
    {
        MachineGunEvents.OnFinished += FinishedFiring;
    }

    void OnDestroy()
    {
        MachineGunEvents.OnFinished -= FinishedFiring;
    }
    
    void Start()
    {
        
    }

    void FinishedFiring()
    {
        //Debug.LogError("Terminou de atirar");
        OnFinished?.Invoke();
    }
}