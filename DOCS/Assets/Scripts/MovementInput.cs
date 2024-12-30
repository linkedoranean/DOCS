using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public delegate void InputEvent();
    public static event InputEvent OnPressedUp;
    public static event InputEvent OnPressedDown;
    public static event InputEvent OnPressedLeft;
    public static event InputEvent OnPressedRight;
    public static event InputEvent OnPressedAction;
    public static event InputEvent OnPressedStart;
    
    
    [SerializeField] private bool allowInput;

    void Awake()
    {
        
    }
    
    void OnDestroy()
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
                OnPressedUp?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnPressedDown?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnPressedLeft?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnPressedRight?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnPressedAction?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnPressedStart?.Invoke();
            }
        }
    }
}