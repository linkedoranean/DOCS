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
        if (doorSide == Direction.Left ||
            doorSide == Direction.Right)
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
        
        if (doorSide == Direction.Up ||
            doorSide == Direction.Down)
        {
            if (currentDoorStatus == Orientation.Opening)
            {
                goalPos.x = transform.localPosition.x;
                goalPos.y = endingPos;
                goalPos.z = transform.localPosition.z;

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, doorSpeed);
            
                if (Math.Abs(transform.localPosition.y - endingPos) < 0.01f)
                {
                    currentDoorStatus = Orientation.Idle;
                }
            }

            if (currentDoorStatus == Orientation.Closing)
            {
                goalPos.x = transform.localPosition.x;
                goalPos.y = 0;
                goalPos.z = transform.localPosition.z;

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, doorSpeed);
            
                if (Math.Abs(transform.localPosition.y - 0) < 0.01f)
                {
                    currentDoorStatus = Orientation.Idle;
                }
            }
        }
    }

    public void OpenDoor()
    {
        currentDoorStatus = Orientation.Opening;
    }
    
    public void CloseDoor()
    {
        currentDoorStatus = Orientation.Closing;
    }
}