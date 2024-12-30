using System;
using System.Collections;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //Esse evento é usado quando termina de andar e atirar raycast e saber se pode mover para frente
    //TODO Mover esse evento para o script MovementManager
    public delegate void MovementAction();
    public static event MovementAction OnMoved;
    public static event MovementAction OnMoveFinished;
    
    public enum Orientation{ Up, Down, Left, Right }
    public Orientation currentOrientation;
    
    [SerializeField] private bool receiveInput;
    [SerializeField] public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;
    [SerializeField] private bool moving;
    
    [SerializeField] private GameObject movMarker;

    [SerializeField] private float distanceWalk;
    [SerializeField] private float playerSpeed;
    
    public Vector3 goalPos;

    //TODO Implementar contagem de passos
    [SerializeField] private int numberSteps;
    
    //TODO Listar os vectors do caminho que o marcador passa e quando apertar B, volta os passos e apagar os últimos


    [SerializeField] private Transform currentSelectedCharacter;
    
    void Awake()
    {
        MovementInput.OnPressedUp += MovementUp;
        MovementInput.OnPressedDown += MovementDown;
        MovementInput.OnPressedLeft += MovementLeft;
        MovementInput.OnPressedRight += MovementRight;
        MovementInput.OnPressedAction += ActionButton;

        SurroundChecker.OnChecked += SurroundReturn;
        
        GameStatusManager.onChanged += AllowMoveMarker;
    }
    
    void OnDestroy()
    {
        MovementInput.OnPressedUp -= MovementUp;
        MovementInput.OnPressedDown -= MovementDown;
        MovementInput.OnPressedLeft -= MovementLeft;
        MovementInput.OnPressedRight -= MovementRight;
        MovementInput.OnPressedAction -= ActionButton;

        SurroundChecker.OnChecked -= SurroundReturn;
        
        GameStatusManager.onChanged -= AllowMoveMarker;
    }
    
    void Start()
    {
        
    }

    void MovementUp()
    {
        if (receiveInput)
        {
            receiveInput = false;
                
            if (canMoveUp)
            {
                ChangeGoalPos("canMoveUp");
            }
                    
            if (!canMoveUp)
            {
                receiveInput = true;
            }
        }
    }
    
    void MovementDown()
    {
        if (receiveInput)
        {
            receiveInput = false;
                
            if (canMoveDown)
            {
                ChangeGoalPos("canMoveDown");
            }
                    
            if (!canMoveDown)
            {
                receiveInput = true;
            }
        }
    }
    
    void MovementLeft()
    {
        if (receiveInput)
        {
            receiveInput = false;
                
            if (canMoveLeft)
            {
                ChangeGoalPos("canMoveLeft");
            }
                    
            if (!canMoveLeft)
            {
                receiveInput = true;
            }
        }
    }
    
    void MovementRight()
    {
        if (receiveInput)
        {
            receiveInput = false;
            
            if (canMoveRight)
            {
                ChangeGoalPos("canMoveRight");
            }
                    
            if (!canMoveRight)
            {
                receiveInput = true;
            }
        }
    }
    
    void ActionButton()
    {
        if (receiveInput)
        {
            receiveInput = false;
            
            if (currentSelectedCharacter != null)
            {
                currentSelectedCharacter.GetComponent<CharactersManager>().newPos = goalPos;
                currentSelectedCharacter.GetComponent<CharactersManager>().canMove = true;
                    
                currentSelectedCharacter = null;
                movMarker.SetActive(false);
                OnMoveFinished?.Invoke();
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
                moving = false;

                OnMoved?.Invoke();
            }
        }
    }

    void AllowMoveMarker(string gameStatus, Transform receivedDoctor)
    {
        if (gameStatus == "MovePosMarker")
        {
            if (receivedDoctor != null)
            {
                currentSelectedCharacter = receivedDoctor;
                transform.position = receivedDoctor.position;
            }
            
            movMarker.SetActive(true);
        
            //OnMoved é usado pelo SurroundChecker
            OnMoved?.Invoke();
        
            receiveInput = true;
        }
    }

    void ChangeGoalPos(string direction)
    {
        moving = true;
        
        if (direction == "canMoveUp")
        {
            var position = transform.localPosition;

            goalPos.x = position.x;
            goalPos.y = position.y;
            goalPos.z = Mathf.Ceil(position.z + distanceWalk);
        }
        
        if (direction == "canMoveDown")
        {
            var position = transform.localPosition;

            goalPos.x = position.x;
            goalPos.y = position.y;
            goalPos.z = Mathf.Ceil(position.z - distanceWalk);
        }
        
        if (direction == "canMoveLeft")
        {
            var position = transform.localPosition;

            goalPos.x = Mathf.Ceil(position.x - distanceWalk);
            goalPos.y = position.y;
            goalPos.z = position.z;
        }
        
        if (direction == "canMoveRight")
        {
            var position = transform.localPosition;

            goalPos.x = Mathf.Ceil(position.x + distanceWalk);
            goalPos.y = position.y;
            goalPos.z = position.z;
        }
    }

    void SurroundReturn(bool up, bool down, bool left, bool right)
    {
        canMoveUp = up;
        canMoveDown = down;
        canMoveLeft = left;
        canMoveRight = right;
        
        receiveInput = true;
    }
}