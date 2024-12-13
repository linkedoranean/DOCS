using System;
using System.Collections;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public delegate void MovementAction();
    public static event MovementAction OnMoved;
    
    public enum Orientation{ Up, Down, Left, Right }
    public Orientation currentOrientation;
    
    [SerializeField] private bool allowInput;
    [SerializeField] public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;
    [SerializeField] private bool moving;
    [SerializeField] private bool rotating;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float rotationSpeed;
    
    public Vector3 goalPos;
    public Vector3 goalRot;

    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (allowInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                allowInput = false;
                
                if (currentOrientation == Orientation.Up)
                {
                    if (canMoveUp)
                    {
                        StartCoroutine(MoveCharacter());
                    }
                    
                    if (!canMoveUp)
                    {
                        allowInput = true;
                    }
                }
                
                if (currentOrientation != Orientation.Up)
                {
                    //Talvez enviar que a rotação tem que ser zero
                    StartCoroutine(RotateCharacter(1));
                }
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                allowInput = false;
                
                if (currentOrientation == Orientation.Down)
                {
                    if (canMoveDown)
                    {
                        StartCoroutine(MoveCharacter());
                    }
                    
                    if (!canMoveDown)
                    {
                        allowInput = true;
                    }
                }
                
                if (currentOrientation != Orientation.Down)
                {
                    //Talvez enviar que a rotação tem que ser zero
                    StartCoroutine(RotateCharacter(180f));
                }
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                allowInput = false;
                
                if (currentOrientation == Orientation.Left)
                {
                    if (canMoveLeft)
                    {
                        StartCoroutine(MoveCharacter());
                    }
                    
                    if (!canMoveLeft)
                    {
                        allowInput = true;
                    }
                }
                
                if (currentOrientation != Orientation.Left)
                {
                    //Talvez enviar que a rotação tem que ser zero
                    StartCoroutine(RotateCharacter(270f));
                }
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                allowInput = false;
                
                if (currentOrientation == Orientation.Right)
                {
                    if (canMoveRight)
                    {
                        StartCoroutine(MoveCharacter());
                    }
                    
                    if (!canMoveRight)
                    {
                        allowInput = true;
                    }
                }
                
                if (currentOrientation != Orientation.Right)
                {
                    StartCoroutine(RotateCharacter(90f));
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.z - goalPos.z) < 0.001f &&
                Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
            {
                allowInput = true;
                moving = false;
                rotating = false;

                OnMoved?.Invoke();
            }
        }

        if (rotating)
        {
            transform.Rotate(goalRot, rotationSpeed); // ROTATION SPEED MUDA CONFORME DIREÇÃO PARA RODAR

            if (Math.Abs(Math.Abs(transform.eulerAngles.y) - goalRot.y) < 10f)
            {
                allowInput = true;
                moving = false;
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
                
                OnMoved?.Invoke();
            }
        }
    }

    IEnumerator MoveCharacter()
    {
        ChangeGoalPos();
     
        //
        //Precisa chamar animação do walk
        //
        
        //
        rotating = false;
        moving = true;

        yield return null;
    }
    
    IEnumerator RotateCharacter(float direction)
    {
        var rotation = transform.eulerAngles;

        goalRot.x = rotation.x;
        goalRot.y = Mathf.Ceil(direction);
        goalRot.z = rotation.z;

        ChangeRotationDirection();

        //Precisa chamar animação de quando rotacionar
        //Talvez nem ter animação de rotação, só rotacionar o modelo mesmo
        
        //
        
        rotating = true;
        moving = false;
        
        yield return null;
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

    void ChangeGoalPos()
    {
        if (currentOrientation == Orientation.Up)
        {
            var position = transform.localPosition;

            goalPos.x = position.x;
            goalPos.y = position.y;
            goalPos.z = Mathf.Ceil(position.z + 9);
        }
        
        if (currentOrientation == Orientation.Down)
        {
            var position = transform.localPosition;

            goalPos.x = position.x;
            goalPos.y = position.y;
            goalPos.z = Mathf.Ceil(position.z - 9);
        }
        
        if (currentOrientation == Orientation.Left)
        {
            var position = transform.localPosition;

            goalPos.x = Mathf.Ceil(position.x - 9);
            goalPos.y = position.y;
            goalPos.z = position.z;
        }
        
        if (currentOrientation == Orientation.Right)
        {
            var position = transform.localPosition;

            goalPos.x = Mathf.Ceil(position.x + 9);
            goalPos.y = position.y;
            goalPos.z = position.z;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirectionInfo"))
        {
            canMoveUp = other.GetComponent<DirectionInfoManager>().canGoUp;
            canMoveDown = other.GetComponent<DirectionInfoManager>().canGoDown;
            canMoveLeft = other.GetComponent<DirectionInfoManager>().canGoLeft;
            canMoveRight = other.GetComponent<DirectionInfoManager>().canGoRight;
        }
    }
}