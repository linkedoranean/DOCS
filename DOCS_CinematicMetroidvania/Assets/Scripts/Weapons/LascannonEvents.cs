using UnityEngine;

public class LascannonEvents : MonoBehaviour
{
    [SerializeField] private GameObject raycastOrigin;

    [SerializeField] private int currentDamage;
    
    [SerializeField] private float resultDistance;

    RaycastHit[] results;

    void Start()
    {
        
    }
    
    private void Update()
    {
        Debug.DrawRay(raycastOrigin.transform.position, -transform.forward * 30f, Color.green);
    }

    public void FireLascannon()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.transform.position, -transform.forward, out hit, 30f))
        {
            //Debug.LogError(hit.transform.name);
            
            resultDistance = Vector3.Distance(raycastOrigin.transform.parent.position, hit.transform.position);
            
            //Debug.LogError(resultDistance);
                
            switch (resultDistance)
            {
                case 11.25f:
                    Debug.LogError("Case A");
                    break;
                case 9f:
                    Debug.LogError("Case B");
                    break;
                case 6.75f:
                    Debug.LogError("Case C");
                    break;
                case 4.5f:
                    Debug.LogError("Case D");
                    break;
                case 2.25f:
                    Debug.LogError("Case E");
                    break;

                default:
                    //
                    break;
            }
            
            if (hit.transform.CompareTag("Enemy"))
            {
                var enemyHealth = hit.transform.GetComponent<EnemyBehavior>();
                enemyHealth.enemyHealth -= currentDamage;
                //enemyHealth.ChangeEnemyBehavior();
            }

            if (hit.transform.CompareTag("Player"))
            {
                var playerHealth = hit.transform.GetComponent<PlayerHealthManager>();
                playerHealth.AlterHealth(currentDamage);
            }
        }
    }
}