using System.Collections;
using System;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public delegate void CharAction();
    public static event CharAction OnMoved;

    [SerializeField] public bool canMove;
    [SerializeField] public bool startMoving;
    [SerializeField] public Vector3 newPos;
    [SerializeField] private float playerSpeed;

    void Awake()
    {
        GameStatusManager.onChanged += AllowMoveCharacter;
    }
    
    void OnDestroy()
    {
        GameStatusManager.onChanged -= AllowMoveCharacter;
    }
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (startMoving)
            {
                //TODO Fazer o boneco ir step by step
                //Receber o caminho do MovementManager
            
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, playerSpeed);

                if (Math.Abs(transform.localPosition.z - newPos.z) < 0.001f &&
                    Math.Abs(transform.localPosition.x - newPos.x) < 0.001f)
                {
                    canMove = false;
                    startMoving = false;

                    OnMoved?.Invoke();
                }
            }
        }
    }
    
    void AllowMoveCharacter(string gameStatus, Transform receivedDoctor)
    {
        if (gameStatus == "MoveChar")
        {
            startMoving = true;
        }
    }
}