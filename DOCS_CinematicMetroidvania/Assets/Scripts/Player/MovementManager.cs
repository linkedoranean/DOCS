using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    /*
    //Esse evento é usado quando termina de andar e atirar raycast e saber se pode mover para frente
    //TODO Mover esse evento para o script MovementManager
    public delegate void MovementAction(int pos);
    public static event MovementAction OnMoved;
    //public static event MovementAction OnMoveFinished;
    
    public delegate void BroadcastPlayerPos(Vector3 playerPos);
    public static event BroadcastPlayerPos PlayerPos;
    
    public enum Orientation{ Up, Down, Left, Right }
    public Orientation currentOrientation;
    
    [SerializeField] private bool allowInput;
    [SerializeField] public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;
    [SerializeField] private bool moving;

    [SerializeField] private float playerSpeed;
    
    public Vector3 goalPos;
    
    public GameObject character;

    public Animator charAnim;
    
    void Awake()
    {
        InputManager.OnKeyPressed += MovementLoop;
        SurroundChecker.OnSurroundChecked += CheckSurrounding;
        EnemyManager.OnFinished += AllowInput;
        WeaponsManager.OnFinished += FinishedMoving;

        currentOrientation = Orientation.Up;
    }
    
    void OnDestroy()
    {
        InputManager.OnKeyPressed -= MovementLoop;
        SurroundChecker.OnSurroundChecked -= CheckSurrounding;
        EnemyManager.OnFinished -= AllowInput; 
        WeaponsManager.OnFinished -= FinishedMoving;
    }
    
    void Start()
    {
        
    }

    void CheckSurrounding(bool up, bool down, bool left, bool right)
    {
        canMoveUp = up;
        canMoveDown = down;
        canMoveLeft = left;
        canMoveRight = right;
    }
    
    void AllowInput()
    {
        if (Math.Abs(transform.localPosition.z - goalPos.z) < 0.001f &&
            Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
        {
            allowInput = true;
        }
    }

    void MovementLoop(string buttonPressed)
    {
        if (allowInput)
        {
            if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                //Check the current orientation
                //Based on the current orientation and pressed input, check if will move or rotate the character
                if (buttonPressed == "UpArrow")
                {
                    allowInput = false;
                    
                    character.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    currentOrientation = Orientation.Up;
                    if (canMoveUp)
                    {
                        charAnim.SetTrigger("walk");
                        SetGoalPos("up");
                        return;
                        //character.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    FinishedMoving();
                    return;
                }
                
                if (buttonPressed == "DownArrow")
                {
                    allowInput = false;
                    
                    character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    currentOrientation = Orientation.Down;
                    if (canMoveDown)
                    {
                        charAnim.SetTrigger("walk");
                        SetGoalPos("down");
                        return;
                        //character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    FinishedMoving();
                    return;
                }
                
                if (buttonPressed == "LeftArrow")
                {
                    allowInput = false;
                    
                    character.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                    currentOrientation = Orientation.Left;
                    if (canMoveLeft)
                    {
                        charAnim.SetTrigger("walk");
                        SetGoalPos("left");
                        return;
                    }
                    FinishedMoving();
                    return;
                }
                
                if (buttonPressed == "RightArrow")
                {
                    allowInput = false;
                    
                    character.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    currentOrientation = Orientation.Right;
                    if (canMoveRight)
                    {
                        charAnim.SetTrigger("walk");
                        SetGoalPos("right");
                        return;
                        //character.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    }
                    FinishedMoving();
                    return;
                }
                
                if (buttonPressed == "key_W_pressed")
                {
                    //allowInput = false;

                    charAnim.SetTrigger("aim");
                }
            }

            if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
            {
                if (buttonPressed == "key_W_released")
                {
                    //allowInput = false;

                    charAnim.SetTrigger("cancelAim");
                }
                
                if (buttonPressed == "UpArrow")
                {
                    character.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    currentOrientation = Orientation.Up;
                }
                
                if (buttonPressed == "DownArrow")
                {
                    character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    currentOrientation = Orientation.Down;
                }
                
                if (buttonPressed == "LeftArrow")
                {
                    character.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                    currentOrientation = Orientation.Left;
                }
                
                if (buttonPressed == "RightArrow")
                {
                    character.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    currentOrientation = Orientation.Right;
                }
                
                if (buttonPressed == "key_Space")
                {
                    allowInput = false;
                    charAnim.SetTrigger("shoot");
                }
            }
        }
    }

    void FinishedMoving()
    {
        //Essa mensagem precisa alertar o EnemyManager para ativar o turno do inimigo 
        Debug.LogError("Chegou aqui?");
        PlayerPos?.Invoke(transform.position);
        OnMoved?.Invoke(0);
    }


    void SetGoalPos(string direction)
    {
        goalPos = transform.localPosition;

        if (direction == "up")
        {
            goalPos.z += 2.25f;
        }
        
        if (direction == "down")
        {
            goalPos.z -= 2.25f;
        }

        if (direction == "right")
        {
            goalPos.x += 2.25f;
        }
        
        if (direction == "left")
        {
            goalPos.x -= 2.25f;
        }

        moving = true;
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

                FinishedMoving();
            }
        }
    }
    */
    
    public enum Orientation{ Up, Down, Left, Right }
    public Orientation currentOrientation;
    
    public enum FloorLevel{ Mid, Upper, Bottom }
    public FloorLevel currentFloorLevel;
    
    public delegate void ElevatorEvent(string inputID);
    public static event ElevatorEvent OnKeyPressed;
    
    public enum PlayerState{
        Idle,
        WalkingRight,
        WalkingLeft,
        Aiming,
        CoverWall,
        TakeElevatorUp,
        TakeElevatorDown,
        OnElevator,
        ExitingElevator,
    }
    public PlayerState currentState;

    public Vector3 goalPos;
    
    [SerializeField] private float playerSpeed;
    [SerializeField] private float hitDistance;
    [SerializeField] private float elevatorTravel;
    
    [SerializeField] private bool canWalk;
    [SerializeField] private bool canTakeElevatorUp;
    //[SerializeField] private bool canTakeElevatorDown;
    [SerializeField] private bool takeElevatorUp;
    //[SerializeField] private bool takeElevatorDown;
    [SerializeField] private bool canExitElevator;
    [SerializeField] private bool exitElevator;
    [SerializeField] private bool pressedToGoUp;
    [SerializeField] private bool pressedToGoDown;
    [SerializeField] private bool elevatorGoingUp;
    [SerializeField] private bool elevatorGoingDown;
    
    [SerializeField] private Animator charAnim;
    
    [SerializeField] private GameObject charRotation;

    [SerializeField] private string elevatorMessage_A;
    [SerializeField] private string elevatorMessage_B;
    [SerializeField] private string elevatorMessage_C;
    
    void Awake()
    {
        currentOrientation = Orientation.Right;
        
        InputManager.OnKeyPressed += ManageInput;
    }
    
    void OnDestroy()
    {
        InputManager.OnKeyPressed -= ManageInput;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (canWalk)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.z - goalPos.z) < 0.001f)// &&
                //Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
            {
                canWalk = false;

                ManageStatus();
            }
        }
        
        if (takeElevatorUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)// &&
                //Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
            {
                currentState = PlayerState.OnElevator;
                currentOrientation = Orientation.Down;
                charRotation.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                takeElevatorUp = false;

                ManageStatus();
            }
        }
        
        /*
        if (takeElevator_Down)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)// &&
                //Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
            {
                currentState = PlayerState.OnElevator;
                currentOrientation = Orientation.Up;
                charRotation.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                takeElevator_Down = false;

                ManageStatus();
            }
        }
        */
        
        if (exitElevator)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)// &&
                //Math.Abs(transform.localPosition.x - goalPos.x) < 0.001f)
            {
                currentState = PlayerState.Idle;
                currentOrientation = Orientation.Right;
                charRotation.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                exitElevator = false;

                ManageStatus();
            }
        }
        
        if (elevatorGoingUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.y - goalPos.y) < 0.001f)
            {
                if (currentFloorLevel == FloorLevel.Mid)
                {
                    currentFloorLevel = FloorLevel.Upper;
                    OnKeyPressed?.Invoke(elevatorMessage_A);
                }
                
                if (currentFloorLevel == FloorLevel.Bottom)
                {
                    currentFloorLevel = FloorLevel.Mid;
                    OnKeyPressed?.Invoke(elevatorMessage_B);
                }
                
                currentState = PlayerState.OnElevator;
                //currentOrientation = Orientation.Down;
                //charRotation.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                elevatorGoingUp = false;

                ManageStatus();
            }
        }
        
        if (elevatorGoingDown)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, playerSpeed);

            if (Math.Abs(transform.localPosition.y - goalPos.y) < 0.001f)
            {
                if (currentFloorLevel == FloorLevel.Mid)
                {
                    currentFloorLevel = FloorLevel.Bottom;
                    OnKeyPressed?.Invoke(elevatorMessage_C);
                }
                
                if (currentFloorLevel == FloorLevel.Upper)
                {
                    currentFloorLevel = FloorLevel.Mid;
                    OnKeyPressed?.Invoke(elevatorMessage_B);
                }
                
                currentState = PlayerState.OnElevator;
                //currentOrientation = Orientation.Down;
                //charRotation.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                elevatorGoingDown = false;

                ManageStatus();
            }
        }
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * hitDistance, Color.magenta);
    }

    void ManageInput(string receivedInput)
    {
        //Debug.LogError(receivedInput);
        if (currentState == PlayerState.Idle)
        {
            if (currentOrientation == Orientation.Right)
            {
                if (receivedInput == "RightArrow_Pressed")
                {
                    if (!charAnim.GetCurrentAnimatorStateInfo(0).IsName("walking"))
                    {
                        currentState = PlayerState.WalkingRight;
                        
                        charAnim.SetTrigger("walk");
                        
                        ManageStatus();
                    }

                    return;
                }
            
                if (receivedInput == "LeftArrow_Pressed")
                {
                    if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        currentOrientation = Orientation.Left;
                
                        charRotation.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }

                    return;
                }
            }
            
            if (currentOrientation == Orientation.Left)
            {
                if (receivedInput == "LeftArrow_Pressed")
                {
                    if (!charAnim.GetCurrentAnimatorStateInfo(0).IsName("walking"))
                    {
                        currentState = PlayerState.WalkingLeft;

                        charAnim.SetTrigger("walk");

                        ManageStatus();
                    }

                    return;
                }
            
                if (receivedInput == "RightArrow_Pressed")
                {
                    if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        currentOrientation = Orientation.Right;

                        charRotation.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }

                    return;
                }
            }
            
            if (receivedInput == "key_S_pressed")
            {
                if (currentState != PlayerState.Aiming)
                {
                    if (!canWalk)
                    {
                        currentState = PlayerState.Aiming;
                        charAnim.Play("aiming");
                        //charAnim.SetTrigger("aim");
                    }
                }
            }
            
            if (receivedInput == "UpArrow_Pressed")
            {
                if (!canWalk)
                {
                    if (currentOrientation == Orientation.Right)
                    {
                        currentState = PlayerState.CoverWall;
                        charAnim.Play("cover_Wall_Right");
                        //charAnim.SetTrigger("aim");
                    }
                    
                    if (currentOrientation == Orientation.Left)
                    {
                        currentState = PlayerState.CoverWall;
                        charAnim.Play("cover_Wall_Left");
                        //charAnim.SetTrigger("aim");
                    }
                }
            }
            
            if (receivedInput == "key_W_pressed")
            {
                if (!canWalk)
                {
                    if (canTakeElevatorUp)
                    {
                        currentState = PlayerState.TakeElevatorUp;
                        charAnim.SetTrigger("walk");
                        //charAnim.Play("enterElevatorUp");
                        ManageStatus();
                        
                        return;
                    }
                    
                    /*
                    if (canTakeElevator_Down)
                    {
                        currentState = PlayerState.TakeElevatorDown;
                        charAnim.SetTrigger("walk");
                        //charAnim.Play("enterElevatorUp");
                        ManageStatus();
                        
                        return;
                    }
                    */
                }
            }
        }
        
        if (currentState == PlayerState.OnElevator)
        {
            if (receivedInput == "UpArrow_Pressed")
            {
                if (currentFloorLevel == FloorLevel.Mid ||
                    currentFloorLevel == FloorLevel.Bottom)
                {
                    if (!elevatorGoingUp)
                    {
                        pressedToGoUp = true;
                        ManageStatus();
                        return;
                    }
                }
            }
            
            if (receivedInput == "DownArrow_Pressed")
            {
                if (currentFloorLevel == FloorLevel.Mid ||
                    currentFloorLevel == FloorLevel.Upper)
                {
                    if (!elevatorGoingDown)
                    {
                        pressedToGoDown = true;
                        ManageStatus();
                        return;
                    }
                }
            }
            
            if (receivedInput == "key_W_pressed")
            {
                if (!elevatorGoingUp &&
                    !elevatorGoingDown)
                {
                    currentState = PlayerState.ExitingElevator;
                    charAnim.SetTrigger("walk");
                    //charAnim.Play("enterElevatorUp");
                    canExitElevator = true;
                    ManageStatus();
                        
                    return;
                }
            }
        }
        
        if (currentState == PlayerState.Aiming)
        {
            if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("aiming")||
                charAnim.GetCurrentAnimatorStateInfo(0).IsName("firing"))
            {
                if (receivedInput == "key_Space")
                {
                    charAnim.Play("firing");
                    //charAnim.SetTrigger("shoot");
                }
            
                if (receivedInput == "key_S_released")
                {
                    //charAnim.SetTrigger("cancelAim");
                    charAnim.Play("idle");
                    currentState = PlayerState.Idle;
                }
            }
        }

        if (currentState == PlayerState.CoverWall)
        {
            if (receivedInput == "UpArrow_Released")
            {
                currentState = PlayerState.Idle;
                charAnim.Play("idle");
                //charAnim.SetTrigger("aim");
            }
        }

        if (currentState == PlayerState.WalkingLeft ||
            currentState == PlayerState.WalkingRight)
        {
            if (receivedInput == "RightArrow_Released" ||
                receivedInput == "LeftArrow_Released")
            {
                currentState = PlayerState.Idle;

                ManageStatus();
            
                return;
            }
        }
        
        if (currentState == PlayerState.TakeElevatorUp)
        {
            if (receivedInput == "key_W_released")
            {
                canTakeElevatorUp = false;
                //charAnim.Play("enterElevatorUp");

                return;
            }
        }
        
        /*
        if (currentState == PlayerState.TakeElevatorDown)
        {
            if (receivedInput == "key_W_released")
            {
                canTakeElevator_Down = false;
                //charAnim.Play("enterElevatorUp");

                return;
            }
        }
        */
    }

    void ManageStatus()
    {
        RaycastHit hit;
        
        if (currentState == PlayerState.WalkingLeft)
        {
            if (!Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
            {
                var currentPos = transform.position;
                currentPos.z -= 2.25f;

                goalPos = currentPos;
            
                canWalk = true;
            }
        }
        
        if (currentState == PlayerState.WalkingRight)
        {
            if (!Physics.Raycast(transform.position, transform.forward, out hit, hitDistance))
            {
                var currentPos = transform.position;
                currentPos.z += 2.25f;

                goalPos = currentPos;
            
                canWalk = true;
            }
        }
        
        if (currentState == PlayerState.TakeElevatorUp)
        {
            if (canTakeElevatorUp)
            {

                var currentPos = transform.position;
                currentPos.x -= 6.75f;
                
                currentOrientation = Orientation.Up;
                charRotation.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

                goalPos = currentPos;
            
                OnKeyPressed?.Invoke("ElevatorMoving");
                
                takeElevatorUp = true;
                
                canTakeElevatorUp = false;
            }
        }
        
        /*
        if (currentState == PlayerState.TakeElevatorDown)
        {
            if (canTakeElevator_Down)
            {
                var currentPos = transform.position;
                currentPos.x += 6.75f;
                
                currentOrientation = Orientation.Down;
                charRotation.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

                goalPos = currentPos;
            
                takeElevator_Down = true;
                
                canTakeElevator_Down = false;
            }
        }
        */
        
        if (currentState == PlayerState.ExitingElevator)
        {
            if (canExitElevator)
            {
                var currentPos = transform.position;
                currentPos.x = 0f;

                bool changeOrientation = true;

                if (changeOrientation)
                {
                    if (currentOrientation == Orientation.Up)
                    {
                        currentOrientation = Orientation.Down;
                        charRotation.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                        changeOrientation = false;
                    }
                }
                
                if (changeOrientation)
                {
                    if (currentOrientation == Orientation.Down)
                    {
                        currentOrientation = Orientation.Up;
                        charRotation.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        //changeOrientation = false;
                    }
                }

                goalPos = currentPos;
            
                exitElevator = true;
                
                canExitElevator = false;
            }
        }
        
        if (currentState == PlayerState.Idle)
        {
            if (!canWalk)
            {
                charAnim.SetTrigger("cancelWalk");
            }
            
            if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                charAnim.ResetTrigger("cancelWalk");
            }
        }
        
        if (currentState == PlayerState.OnElevator)
        {
            if (!takeElevatorUp) //&&
                //!takeElevator_Down)
            {
                charAnim.SetTrigger("cancelWalk");
            }
            
            if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                charAnim.ResetTrigger("cancelWalk");
            }

            if (pressedToGoUp)
            {
                if (currentFloorLevel == FloorLevel.Mid)
                {
                    OnKeyPressed?.Invoke(elevatorMessage_B);
                }
                
                if (currentFloorLevel == FloorLevel.Bottom)
                {
                    OnKeyPressed?.Invoke(elevatorMessage_C);
                }
                
                pressedToGoUp = false;
                
                var currentPos = transform.position;
                currentPos.y += elevatorTravel;

                goalPos = currentPos;
            
                elevatorGoingUp = true;
            }
            
            if (pressedToGoDown)
            {
                if (currentFloorLevel == FloorLevel.Upper)
                {
                    OnKeyPressed?.Invoke(elevatorMessage_A);
                }
                
                if (currentFloorLevel == FloorLevel.Mid)
                {
                    OnKeyPressed?.Invoke(elevatorMessage_B);
                }

                pressedToGoDown = false;
                
                var currentPos = transform.position;
                currentPos.y -= elevatorTravel;

                goalPos = currentPos;
            
                elevatorGoingDown = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ElevatorUp"))
        {
            elevatorMessage_A = other.GetComponent<ElevatorManager>().upperID;
            elevatorMessage_B = other.GetComponent<ElevatorManager>().midID;
            elevatorMessage_C = other.GetComponent<ElevatorManager>().bottomID;
            
            elevatorTravel = other.GetComponent<ElevatorManager>().elevatorTravel;
            
            //Debug.LogError("Elevador");
            canTakeElevatorUp = true;
            //Aqui ativar UI para aavisar que jogador pode entrar no elevador
        }
        
        /*
        if (other.CompareTag("ElevatorDown"))
        {
            //Debug.LogError("Elevador");
            canTakeElevator_Down = true;
            //Aqui ativar UI para aavisar que jogador pode entrar no elevador
        }
        */
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ElevatorUp"))
        {
            //elevatorMessage_A = null;
            //elevatorMessage_B= null;
            //elevatorMessage_C = null;
            
            //Debug.LogError("Saiu Elevador");
            canTakeElevatorUp = false;
            //Aqui ativar UI para aavisar que jogador pode entrar no elevador
        }
        
        /*
        if (other.CompareTag("ElevatorDown"))
        {
            //Debug.LogError("Saiu Elevador");
            canTakeElevator_Down = false;
            //Aqui ativar UI para aavisar que jogador pode entrar no elevador
        }
        */
    }
}