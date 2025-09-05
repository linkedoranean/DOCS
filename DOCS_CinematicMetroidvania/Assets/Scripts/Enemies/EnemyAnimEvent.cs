using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    [SerializeField] private EnemyBehavior parentEnemyBehavior;
    [SerializeField] private LascannonEvents lascannonBehavior;
    
    void Start()
    {
        
    }

    void FinishedAction()
    {
        parentEnemyBehavior.FinishedAction();
    }

    void FireLascannon()
    {
        lascannonBehavior.FireLascannon();
    }
}