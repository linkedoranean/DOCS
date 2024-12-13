using System;
using System.Collections;
using UnityEngine;

public class DoorAnimManager : MonoBehaviour
{
    public enum Direction{ Left, Right, Up, Down }
    public Direction doorSide;
    
    public enum Orientation{ Idle, Opening, Closing }
    public Orientation currentDoorStatus;

    [SerializeField] private float doorSpeed;
    [SerializeField] private float endingPos;

    [SerializeField] private Vector3 goalPos;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (currentDoorStatus == Orientation.Opening)
        {
            goalPos.x = endingPos;
            goalPos.y = transform.localPosition.y;
            goalPos.z = transform.localPosition.z;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, doorSpeed);
            
            if (Math.Abs(transform.localPosition.x - endingPos) < 0.01f)
            {
                currentDoorStatus = Orientation.Idle;
            }
        }

        if (currentDoorStatus == Orientation.Closing)
        {
            goalPos.x = 0;
            goalPos.y = transform.localPosition.y;
            goalPos.z = transform.localPosition.z;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, doorSpeed);
            
            if (Math.Abs(transform.localPosition.x - 0) < 0.01f)
            {
                currentDoorStatus = Orientation.Idle;
            }
        }
    }

    public void OpenDoor()
    {
        if (doorSide == Direction.Left)
        {
            doorSpeed = Math.Abs(doorSpeed);
        }

        currentDoorStatus = Orientation.Opening;
    }
    
    public void CloseDoor()
    {
        if (doorSide == Direction.Left)
        {
            doorSpeed = Math.Abs(doorSpeed);
        }
        
        currentDoorStatus = Orientation.Closing;
    }
}