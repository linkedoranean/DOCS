using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public delegate void CharAction(string movementReached);
    public static event CharAction OnMoved;
    public static event CharAction OnFinished;
    
    [SerializeField] public bool moving;
    [SerializeField] public Vector3 newPos;
    [SerializeField] private float playerSpeed;

    [SerializeField] private string direction;

    [SerializeField] private BoxCollider playerCollider;
    
    void Awake()
    {
        SurroundChecker.OnSurroundChecked += SetupCharacterMove_Initial;
        RelocatorManager.OnRelocated += SetupCharacterMove_Relocated;
    }
    
    void OnDestroy()
    {
        SurroundChecker.OnSurroundChecked -= SetupCharacterMove_Initial;
        RelocatorManager.OnRelocated -= SetupCharacterMove_Relocated;
    }
    
    void FixedUpdate()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, playerSpeed);
            
            if (Math.Abs(transform.localPosition.z - newPos.z) < 0.001f &&
                Math.Abs(transform.localPosition.x - newPos.x) < 0.001f)
            {
                playerCollider.enabled = true;
                
                moving = false;
                
                OnFinished?.Invoke("AllowInput");
            }
        }
    }
    
    void SetupCharacterMove_Initial(Vector3 receivedPos, string receivedDirection)
    {
        direction = receivedDirection;

        newPos = receivedPos;

        moving = true;

    }
    
    void SetupCharacterMove_Relocated(Vector3 receivedPos)
    {
        transform.position = receivedPos;

        newPos = Vector3.zero;

        moving = true;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mover"))
        {
            playerCollider.enabled = false;
            
            moving = false;

            OnMoved?.Invoke(direction);
        }
    }
}