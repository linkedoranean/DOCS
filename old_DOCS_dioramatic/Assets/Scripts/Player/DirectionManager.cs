using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    void Awake()
    {
        InputManager.OnPressedDirection += RotateCharacter;
    }
    
    void OnDestroy()
    {
        InputManager.OnPressedDirection -= RotateCharacter;
    }

    void RotateCharacter(string direction)
    {
        //Debug.LogError(direction);
        
        switch (direction)
        {
            case "Up":
                transform.eulerAngles = Vector3.zero;
                break;
            case "Down":
                transform.eulerAngles = new Vector3 (0f, 180f, 0f);
                break;
            case "Left":
                transform.eulerAngles = new Vector3 (0f, -90f, 0f);
                break;
            case "Right":
                transform.eulerAngles = new Vector3 (0f, 90f, 0f);
                break;
            default:
                print ("Incorrect intelligence level.");
                break;
        }
    }
}