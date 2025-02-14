using System;
using UnityEngine;

public class MoverManager : MonoBehaviour
{
    //public delegate void MoverEvent(string direction);
    //public static event MoverEvent onCollided;
    
    [SerializeField] private string trigger;
    [SerializeField] private string hideTrigger;

    [SerializeField] private Animator thisAnim;
    [SerializeField] private Animator nextRoomAnim;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (thisAnim != null)
            {
                thisAnim.SetTrigger(hideTrigger);
            }

            if (nextRoomAnim != null)
            {
                nextRoomAnim.SetTrigger(trigger);
            }
            
            //onCollided?.Invoke(trigger);
        }
    }
}