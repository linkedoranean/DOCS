using System;
using UnityEngine;

public class DoorAnimManager : MonoBehaviour
{
    public enum Level{ Upper, Mid, Bottom }
    public Level elevatorLevel;
    
    public enum Type{ Elevator, Hallway, Room }
    public Type doorType;
    
    public enum Status{ Closed, Open, Opening, Closing, Locked }
    public Status doorStatus;

    [SerializeField] private float doorSpeed;
    
    [SerializeField] private Vector3 LeftDoorEndingPos;
    [SerializeField] private Vector3 RightDoorEndingPos;

    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    [SerializeField] public string upperID;
    [SerializeField] public string midID;
    [SerializeField] public string bottomID;
    
    void Awake()
    {
        MovementManager.OnKeyPressed += CheckMessage;
    }
    
    void OnDestroy()
    {
        MovementManager.OnKeyPressed -= CheckMessage;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        leftDoor.transform.localPosition = Vector3.MoveTowards(
            leftDoor.transform.localPosition, LeftDoorEndingPos, doorSpeed);
        rightDoor.transform.localPosition = Vector3.MoveTowards(
            rightDoor.transform.localPosition, RightDoorEndingPos, doorSpeed);
        
        if (Math.Abs(leftDoor.transform.localPosition.z - LeftDoorEndingPos.z) < 0.01f ||
            Math.Abs(rightDoor.transform.localPosition.z - RightDoorEndingPos.z) < 0.01f)
        {
            if (doorStatus == Status.Opening)
            {
                doorStatus = Status.Open;
            }
            
            if (doorStatus == Status.Closing)
            {
                doorStatus = Status.Closed;
            }
        }
    }

    void CheckMessage(string receivedID)
    {
        if (receivedID == upperID && elevatorLevel == Level.Upper ||
            receivedID == midID && elevatorLevel == Level.Mid  ||
            receivedID == bottomID && elevatorLevel == Level.Bottom)
        {
            if (doorStatus == Status.Open)
            {
                CloseDoor();

                return;
            }

            if (doorStatus == Status.Closed)
            {
                OpenDoor();
                
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorStatus == Status.Closed)
        {
            if (other.transform.position.z > transform.position.z ||
                other.transform.position.z < transform.position.z)
            {
                if (elevatorLevel == Level.Upper)
                {
                    CheckMessage(upperID);
                }
                
                if (elevatorLevel == Level.Mid)
                {
                    CheckMessage(midID);
                }
                
                if (elevatorLevel == Level.Bottom)
                {
                    CheckMessage(bottomID);
                }
            }
        }

        if (Math.Abs(transform.position.x - other.transform.position.x) < 4.5f)
        {
            Debug.LogError("Jogador entrou vindo de cima");
        }
        
        if (Math.Abs(transform.position.x - other.transform.position.x) > 4.5f)
        {
            Debug.LogError("Jogador entrou vindo de baixo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (doorStatus == Status.Open)
        {
            if (other.transform.position.z > transform.position.z ||
                other.transform.position.z < transform.position.z)
            {
                if (elevatorLevel == Level.Upper)
                {
                    CheckMessage(upperID);
                }
                
                if (elevatorLevel == Level.Mid)
                {
                    CheckMessage(midID);
                }
                
                if (elevatorLevel == Level.Bottom)
                {
                    CheckMessage(bottomID);
                }
            }
        }
    }

    void OpenDoor()
    {
        LeftDoorEndingPos = leftDoor.transform.localPosition;
        RightDoorEndingPos = rightDoor.transform.localPosition;
            
        LeftDoorEndingPos.z -= 1.25f;
        RightDoorEndingPos.z += 1.25f;
            
        doorStatus = Status.Opening;
    }
    
    void CloseDoor()
    {
        LeftDoorEndingPos = leftDoor.transform.localPosition;
        RightDoorEndingPos = rightDoor.transform.localPosition;
            
        LeftDoorEndingPos.z = 0f;
        RightDoorEndingPos.z = 0f;
            
        doorStatus = Status.Closing;
    }
}