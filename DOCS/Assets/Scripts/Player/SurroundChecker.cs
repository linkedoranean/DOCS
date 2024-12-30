using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundChecker : MonoBehaviour
{
    public delegate void SurroundCheck(bool up, bool down, bool left, bool right);
    public static event SurroundCheck OnChecked;
    
    [SerializeField] private float hitDistance;
    
    void Awake()
    {
        MovementManager.OnMoved += RaycastSurround;
    }

    void OnDestroy()
    {
        MovementManager.OnMoved -= RaycastSurround;
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
        
        bool up = true;
        bool down = true;
        bool left = true;
        bool right = true;

        if (Physics.Raycast(transform.position, transform.forward, out hit, hitDistance))
        {
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                up = false;
            }
        }
        
        if (Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
        {
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                down = false;
            }
        }
        
        if (Physics.Raycast(transform.position, -transform.right, out hit, hitDistance))
        {
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                left = false;
            }
        }
        
        if (Physics.Raycast(transform.position, transform.right, out hit, hitDistance))
        {
            if (!hit.transform.CompareTag("AreaRevealer"))
            {
                right = false;
            }
        }
        
        OnChecked?.Invoke(up, down, left, right);
    }
}