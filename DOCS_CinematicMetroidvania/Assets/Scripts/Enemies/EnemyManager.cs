using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemiesArray
{
    [SerializeField] public GameObject enemies;
}

public class EnemyManager : MonoBehaviour
{
    public delegate void EnemyManagerBroadcast();
    public static event EnemyManagerBroadcast OnFinished;
    
    [SerializeField] private List<EnemiesArray> enemiesList;
    
    [SerializeField] private int listCounter;

    [SerializeField] private bool triggerEnemy;

    void Awake()
    {
        //MovementManager.OnMoved += ActivateEnemies;
        EnemyBehavior.OnFinished += ActivateEnemies;
    }

    void OnDestroy()
    {
        //MovementManager.OnMoved -= ActivateEnemies;
        EnemyBehavior.OnFinished -= ActivateEnemies;
    }
    
    void Start()
    {
        listCounter = 0;
    }

    void ActivateEnemies(int pos)
    {
        listCounter = pos;
        triggerEnemy = true;
        
        if (enemiesList.Count == 0)
        {
            OnFinished?.Invoke();

            return;
        }
    
        
        if (listCounter < enemiesList.Count)
        {
            if (triggerEnemy)
            {
                triggerEnemy = false;
                enemiesList[listCounter].enemies.GetComponent<EnemyBehavior>().currentPos = listCounter;
                enemiesList[listCounter].enemies.GetComponent<EnemyBehavior>().TriggerEnemy();
                //return;
            }
        }
        
        /*
        if (triggerEnemy)
        {
            triggerEnemy = false;
            enemiesList[listCounter].enemies.GetComponent<EnemyBehavior>().currentPos = listCounter;
            enemiesList[listCounter].enemies.GetComponent<EnemyBehavior>().TriggerEnemy();
            return;
        }
        */
        
        if (listCounter >= enemiesList.Count)
        {
            //Debug.LogError("Ta tentando entrar aqui?");
            OnFinished?.Invoke();
        }
    }
}