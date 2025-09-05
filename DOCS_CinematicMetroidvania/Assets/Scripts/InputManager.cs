using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputEvent(string inputID);
    public static event InputEvent OnKeyPressed;
    
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputSender("UpArrow_Pressed");
        }
        
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            InputSender("UpArrow_Released");
        }
            
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputSender("DownArrow_Pressed");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            InputSender("DownArrow_Released");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputSender("LeftArrow_Pressed");
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            InputSender("LeftArrow_Released");
        }
            
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputSender("RightArrow_Pressed");
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            InputSender("RightArrow_Released");
        }
            
        if (Input.GetKeyDown(KeyCode.W))
        {
            InputSender("key_W_pressed");
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            InputSender("key_W_released");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            InputSender("key_S_pressed");
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            InputSender("key_S_released");
        }
            
        if (Input.GetKeyDown(KeyCode.A))
        {
            InputSender("key_A");
        }
            
        if (Input.GetKeyDown(KeyCode.D))
        {
            InputSender("key_D");
        }
            
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputSender("key_Space");
        }
            
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputSender("key_Return");
        }
    }

    void InputSender(string pressedID)
    {
        OnKeyPressed?.Invoke(pressedID);
    }
}