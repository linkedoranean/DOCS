using System;
using Unity.VisualScripting;
using UnityEngine;

public class MachineGunEvents : MonoBehaviour
{
    public delegate void MachineGunEvent();
    //public static event MachineGunEvent OnFired;
    public static event MachineGunEvent OnFinished;

    [SerializeField] private GameObject raycastOrigin;
    [SerializeField] private GameObject playerManager;

    [SerializeField] private GameObject[] damagers;
    
    [SerializeField] private float resultDistance;
    
    [SerializeField] private int currentDamage;
    [SerializeField] private int damageModifier;

    [SerializeField] private bool armorPiercing;
    
    void Start()
    {
        
    }

    /*
    private void Update()
    {
        Debug.DrawRay(raycastOrigin.transform.position, -transform.up * 5f, Color.green);
    }
    */

    public void MachineGunFired()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(raycastOrigin.transform.position, -transform.up, out hit, 30f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                resultDistance = Vector3.Distance(playerManager.transform.position, hit.transform.position);
                
                switch (resultDistance)
                {
                    case 11.25f:
                        currentDamage = 1 + damageModifier;
                        //damagers[0].SetActive(true);
                        break;
                    case 9f:
                        currentDamage = 2 + damageModifier;
                        break;
                    case 6.75f:
                        currentDamage = 3 + damageModifier;
                        break;
                    case 4.5f:
                        currentDamage = 4 + damageModifier;
                        break;
                    case 2.25f:
                        currentDamage = 5 + damageModifier;
                        break;

                    default:
                        //
                        break;
                }

                var enemyHealth = hit.transform.GetComponent<EnemyBehavior>();
                enemyHealth.enemyHealth -= currentDamage;
                enemyHealth.ChangeEnemyBehavior();

                foreach (var damager in damagers)
                {
                    //damager.SetActive(false);
                }
            }
        }
    }
    
    public void MachineGunFinished()
    {
        OnFinished?.Invoke();
    }
    
    void TriggerEvent()
    {
        
    }
}