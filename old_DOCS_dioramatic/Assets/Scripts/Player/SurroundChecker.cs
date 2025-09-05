
using System;
using System.Collections;
using UnityEngine;

public class SurroundChecker : MonoBehaviour
{
    public delegate void SurroundEvent(Vector3 position, string direction);
    public static event SurroundEvent OnSurroundChecked;

    public delegate void SurroundReturn(string message);
    public static event SurroundReturn OnNothingFound;

    [SerializeField] private float hitDistance;

    [SerializeField] private Vector3 raycastDirection;
    
    void Awake()
    {
        InputManager.OnPressedDirection += SetDirection;
    }

    void OnDestroy()
    {
        InputManager.OnPressedDirection -= SetDirection;
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * hitDistance, Color.magenta);
        Debug.DrawRay(transform.position, transform.right * hitDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * hitDistance, Color.yellow);
    }

    void SetDirection(string direction)
    {
        switch (direction)
        {
            case "Up":
                raycastDirection = transform.forward;
                break;
            case "Down":
                raycastDirection = -transform.forward;
                break;
            case "Left":
                raycastDirection = -transform.right;
                break;
            case "Right":
                raycastDirection = transform.right;
                break;
            default:
                print ("Incorrect intelligence level.");
                break;
        }

        RaycastSurround(direction);
    }

    void RaycastSurround(string registeredDirection)
    {
        RaycastHit hit;
        
        if (!Physics.Raycast(transform.position, raycastDirection, out hit, hitDistance))
        {
            StartCoroutine(WaitToReset());
            
            return;
        }

        if (Physics.Raycast(transform.position, raycastDirection, out hit, hitDistance))
        {
            if (hit.transform.CompareTag("Mover"))
            {
                Vector3 hitPos;

                //hitPos = hit.transform.position;
                hitPos = transform.position;
                hitPos.y = 0f;

                if (Math.Abs(raycastDirection.z - 1) < 0.1f)
                {
                    hitPos.z = hitPos.z + 10f;
                }
                
                if (Math.Abs(raycastDirection.z - (-1)) < 0.1f)
                {
                    hitPos.z = hitPos.z - 10f;
                }
                
                if (Math.Abs(raycastDirection.x - 1) < 0.1f)
                {
                    hitPos.x = hitPos.x + 10f;
                }
                
                if (Math.Abs(raycastDirection.x - (-1)) < 0.1f)
                {
                    hitPos.x = hitPos.x - 10f;
                }

                OnSurroundChecked?.Invoke(hitPos, registeredDirection);
            }
        }
    }

    IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(0.25f);
        
        OnNothingFound?.Invoke("NoPath");
    }
}