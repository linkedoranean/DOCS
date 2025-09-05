using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputEvent(string info);
    public static event InputEvent OnPressedDirection;
    public static event InputEvent OnPressedAction;
    public static event InputEvent OnPressedStart;

    [SerializeField] private bool isInCutscene;
    [SerializeField] private bool isInMap;
    [SerializeField] private bool allowInput;

    void Awake()
    {
        MovementManager.OnFinished += ChangeInputStatus;
        SurroundChecker.OnNothingFound += ChangeInputStatus;
        CutsceneTrigger.OnTriggered += StartCutscene;
    }
    
    void OnDestroy()
    {
        MovementManager.OnFinished -= ChangeInputStatus;
        SurroundChecker.OnNothingFound -= ChangeInputStatus;
        CutsceneTrigger.OnTriggered -= StartCutscene;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!isInMap)
        {
            if (!isInCutscene)
            {
                if (allowInput)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        PlayerPressedUp();
                        allowInput = false;

                        return;
                    }
            
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        PlayerPressedDown();
                        allowInput = false;

                        return;
                    }
            
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        PlayerPressedLeft();
                        allowInput = false;

                        return;
                    }
            
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        PlayerPressedRight();
                        allowInput = false;

                        return;
                    }
            
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        PlayerPressedAction();
                        allowInput = false;

                        return;
                    }
            
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        PlayerPressedStart();
                        allowInput = false;
                    }
                }
            }

            if (isInCutscene)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PlayerPressedAction();
                }
            }
        }
    }

    void PlayerPressedUp()
    {
        OnPressedDirection?.Invoke("Up");
    }
    
    void PlayerPressedDown()
    {
        OnPressedDirection?.Invoke("Down");
    }
    
    void PlayerPressedLeft()
    {
        OnPressedDirection?.Invoke("Left");
    }
    
    void PlayerPressedRight()
    {
        OnPressedDirection?.Invoke("Right");
    }
    
    void PlayerPressedAction()
    {
        OnPressedAction?.Invoke("Space");
    }
    
    void PlayerPressedStart()
    {
        OnPressedStart?.Invoke("Enter");
    }

    void ChangeInputStatus(string message)
    {
        //Debug.LogError(message);
        
        if (message is "AllowInput" or "NoPath")
        {
            allowInput = true;
        }
    }

    void StartCutscene(bool broadcastedStatus)
    {
        isInCutscene = broadcastedStatus;
        allowInput = !broadcastedStatus;
    }
}