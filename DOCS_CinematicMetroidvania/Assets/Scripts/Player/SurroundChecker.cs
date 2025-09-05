using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundChecker : MonoBehaviour
{
    public delegate void SurroundCheck(bool up, bool down, bool left, bool right);
    public static event SurroundCheck OnSurroundChecked;
    
    [SerializeField] private float hitDistance;
    
    void Awake()
    {
        EnemyManager.OnFinished += RaycastSurround;
    }

    void OnDestroy()
    {
        EnemyManager.OnFinished += RaycastSurround;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * hitDistance, Color.magenta);
        Debug.DrawRay(transform.position, transform.right * hitDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * hitDistance, Color.yellow);
    }

    void RaycastSurround()
    {
        RaycastHit hit;
        
        bool up = false;
        bool down = false;
        bool left = false;
        bool right = false;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, hitDistance))
        {
            up = true;
            /*
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                up = false;
            }
            */
        }
        
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
        {
            down = true;
            /*
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                down = false;
            }
            */
        }
        
        if (!Physics.Raycast(transform.position, -transform.right, out hit, hitDistance))
        {
            left = true;
            /*
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                left = false;
            }
            */
        }
        
        if (!Physics.Raycast(transform.position, transform.right, out hit, hitDistance))
        {
            right = true;
            /*
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                right = false;
            }
            */
        }
        
        OnSurroundChecked?.Invoke(up, down, left, right);
    }
}