using System;
using System.Collections;
using UnityEngine;

public class RotationSample : MonoBehaviour
{
    public enum Orientation{ Up, Down, Left, Right }
    public Orientation currentOrientation;
    
    [SerializeField] private bool rotating;

    public Vector3 goalRot;

    [SerializeField] private float rotationSpeed;

    
    void Start()
    {
        /*
        if (currentOrientation != Orientation.Up)
        {
            //Talvez enviar que a rotação tem que ser zero
            StartCoroutine(RotateCharacter(1));
        }
        
        if (currentOrientation != Orientation.Down)
        {
            //Talvez enviar que a rotação tem que ser zero
            StartCoroutine(RotateCharacter(180f));
        }
        
        if (currentOrientation != Orientation.Left)
        {
            //Talvez enviar que a rotação tem que ser zero
            StartCoroutine(RotateCharacter(270f));
        }
        
        if (currentOrientation != Orientation.Right)
        {
            StartCoroutine(RotateCharacter(90f));
        }
        */
    }
    
    IEnumerator RotateCharacter(float direction)
    {
        var rotation = transform.eulerAngles;

        goalRot.x = rotation.x;
        goalRot.y = Mathf.Ceil(direction);
        goalRot.z = rotation.z;

        //ChangeRotationDirection();

        //
        
        rotating = true;
        
        yield return null;
    }

    void FixedUpdate()
    {
        if (rotating)
        {
            transform.Rotate(goalRot, rotationSpeed); // ROTATION SPEED MUDA CONFORME DIREÇÃO PARA RODAR

            if (Math.Abs(transform.eulerAngles.y - goalRot.y) > 0.01)
            {
                rotating = false;
                
                goalRot.x = transform.eulerAngles.x;
                goalRot.y = Mathf.Ceil(goalRot.y);
                goalRot.z = transform.eulerAngles.z;

                transform.eulerAngles = goalRot;
                
                switch ((int)goalRot.y)
                {
                    case 270:
                        currentOrientation = Orientation.Left;
                        break;
                    case 180:
                        currentOrientation = Orientation.Down;
                        break;
                    case 90:
                        currentOrientation = Orientation.Right;
                        break;
                    case 1:
                        currentOrientation = Orientation.Up;
                        break;
                    default:
                        print ("Incorrect intelligence level.");
                        break;
                }
            }
        }
    }
    
    void ChangeRotationDirection()
    {
        if (Math.Abs(transform.eulerAngles.y - 0) < 1f ||
            Math.Abs(transform.eulerAngles.y - 1) < 1f ||
            Math.Abs(transform.eulerAngles.y - 360) < 1f)
        {
            if (goalRot.y == 90 ||
                goalRot.y == 180)
            {
                rotationSpeed = Math.Abs(rotationSpeed);
            }
            
            if (goalRot.y == 270)
            {
                rotationSpeed = Math.Abs(rotationSpeed) * -1;;
            }
        }
        
        if (Math.Abs(transform.eulerAngles.y - 90) < 1f)
        {
            if (goalRot.y == 180)
            {
                rotationSpeed = Math.Abs(rotationSpeed);
            }
            
            if (goalRot.y == 1 ||
                goalRot.y == 270)
            {
                rotationSpeed = Math.Abs(rotationSpeed) * -1;
            }
        }
        
        if (Math.Abs(transform.eulerAngles.y - 270) < 1f)
        {
            if (goalRot.y == 1 ||
                goalRot.y == 90)
            {
                rotationSpeed = Math.Abs(rotationSpeed);
            }
            
            if (goalRot.y == 180)
            {
                rotationSpeed = Math.Abs(rotationSpeed) * -1;
            }
        }
        
        if (Math.Abs(transform.eulerAngles.y - 180) < 1f)
        {
            if (goalRot.y == 1 ||
                goalRot.y == 270)
            {
                rotationSpeed = Math.Abs(rotationSpeed);
            }
            
            if (goalRot.y == 90)
            {
                rotationSpeed = Math.Abs(rotationSpeed) * -1;
            }
        }
    }
}
