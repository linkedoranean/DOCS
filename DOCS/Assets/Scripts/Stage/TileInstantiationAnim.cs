using UnityEngine;

public class TileInstantiationAnim : MonoBehaviour
{
    [SerializeField] private Animator tileAnim;
    
    void Awake()
    {
        MoverManager.onCollided += TriggerAnim;
    }
        
    void OnDestroy()
    {
        MoverManager.onCollided -= TriggerAnim;
    }

    void TriggerAnim(string trigger)
    {
        tileAnim.SetTrigger(trigger);
    }
}