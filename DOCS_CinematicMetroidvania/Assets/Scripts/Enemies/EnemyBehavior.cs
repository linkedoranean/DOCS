using System;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public delegate void EnemyBroadcast(int pos);
    public static event EnemyBroadcast OnFinished;
    
    public enum Orientation{ Down, Up, Left, Right }
    public Orientation currentOrientation;
    
    public enum Behavior{ Turn, Move, Aiming }
    public Behavior currentBehavior;
    
    [SerializeField] private bool enemyDeactivated;
    [SerializeField] private bool moving;
    
    [SerializeField] private float hitDistance;
    [SerializeField] private float playerSpeed;

    [SerializeField] private Vector3 currentPlayerPos;
    [SerializeField] private Vector3 goalPos;

    [SerializeField] public int currentPos;
    [SerializeField] public int enemyHealth;
    
    public Animator enemyAnim;

    void Awake()
    {
        //MovementManager.PlayerPos += UpdatePlayerPos;
    }

    void OnDestroy()
    {
        //MovementManager.PlayerPos -= UpdatePlayerPos;
    }
    
    void Start()
    {
        enemyDeactivated = false;
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
                currentBehavior = Behavior.Turn;
                //InvokeTheBroadcast();
            }
        }
    }

    void UpdatePlayerPos(Vector3 broadcastedPlayerPos)
    {
        currentPlayerPos = broadcastedPlayerPos;
    }
    
    /*
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 21f, Color.black);
    }
    */

    public void ChangeEnemyBehavior()
    {
        if (currentBehavior != Behavior.Aiming)
        {
            RotateEnemy();
        }
    }

    public void TriggerEnemy()
    {
        RaycastHit hit;
        
        if (enemyDeactivated)
        {
            InvokeTheBroadcast();
            return;
        }
        
        if (currentBehavior == Behavior.Aiming)
        {
            enemyAnim.SetTrigger("shoot");
            //Ativando um filho que tem as colisões de ataque
            currentBehavior = Behavior.Turn;
            //InvokeTheBroadcast();
            return;
        }
        
        if (currentBehavior != Behavior.Aiming)
        {
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 21f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    currentBehavior = Behavior.Aiming;
                    enemyAnim.SetTrigger("aim");
                    InvokeTheBroadcast();
                    return;
                }
            }
            //InvokeTheBroadcast();
        }
        
        //Então o jogador não está diretamente a frente do inimigo
        if (currentBehavior == Behavior.Turn)
        {
            RotateEnemy();
            return;
            //InvokeTheBroadcast();
        }
        
        if (currentBehavior == Behavior.Move)
        {
            //Se o inimigo estiver olhando para cima
            if (currentOrientation == Orientation.Up)
            {
                //Atira um raio para cima
                if (!Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
                {
                    Debug.LogError("Colidiu com jogador, ele está acima");
                    //Se não pegar em nada, anda pra cima
                    goalPos = transform.localPosition;
                    goalPos.z += 2.25f;
                    moving = true;
                    enemyAnim.SetTrigger("walk");
                    //return;
                }
                else
                {
                    //Se pegar no jogador, inimigo vai mirar
                    if (hit.transform.CompareTag("Player"))
                    {
                        currentBehavior = Behavior.Aiming;
                        enemyAnim.SetTrigger("aim");
                        InvokeTheBroadcast();
                        return;
                    }
                }
                //Se pegar em algo, que não é o jogador, muda Behavior para Turn
                //Debug.LogError(hit.transform.name);
                currentBehavior = Behavior.Turn;
                //InvokeTheBroadcast();
            }
            
            //Se o inimigo estiver olhando para cima
            if (currentOrientation == Orientation.Down)
            {
                if (!Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
                {
                    goalPos = transform.localPosition;
                    goalPos.z -= 2.25f;
                    moving = true;
                    enemyAnim.SetTrigger("walk");
                    //return;
                }
                else
                {
                    //Se pegar no jogador, inimigo vai mirar
                    if (hit.transform.CompareTag("Player"))
                    {
                        currentBehavior = Behavior.Aiming;
                        enemyAnim.SetTrigger("aim");
                        InvokeTheBroadcast();
                        return;
                    }
                }
                //Se pegar em algo, que não é o jogador, muda Behavior para Turn
                //Debug.LogError(hit.transform.name);
                currentBehavior = Behavior.Turn;
                //InvokeTheBroadcast();
            }
                
            //Se o inimigo estiver olhando para direita
            if (currentOrientation == Orientation.Right)
            {
                //Atira um raio para direita
                if (!Physics.Raycast(transform.position,-transform.forward, out hit, hitDistance))
                {
                    //Se não pegar em nada, anda pra direita
                    goalPos = transform.localPosition;
                    goalPos.x += 2.25f;
                    moving = true;
                    enemyAnim.SetTrigger("walk");
                    //return;
                }
                else
                {
                    //Se pegar no jogador, inimigo vai mirar
                    if (hit.transform.CompareTag("Player"))
                    {
                        currentBehavior = Behavior.Aiming;
                        enemyAnim.SetTrigger("aim");
                        InvokeTheBroadcast();
                        return;
                    }
                }
                //Se pegar em algo, que não é o jogador, muda Behavior para Turn
                //Debug.LogError(hit.transform.name);
                currentBehavior = Behavior.Turn;
                //InvokeTheBroadcast();
            }
                
            //Se o inimigo estiver olhando para direita
            if (currentOrientation == Orientation.Left)
            {
                //Atira um raio para direita
                if (!Physics.Raycast(transform.position, -transform.forward, out hit, hitDistance))
                {
                    //Se não pegar em nada, anda pra direita
                    goalPos = transform.localPosition;
                    goalPos.x -= 2.25f;
                    moving = true;
                    enemyAnim.SetTrigger("walk");
                    //return;
                }
                else
                {
                    //Se pegar no jogador, inimigo vai mirar
                    if (hit.transform.CompareTag("Player"))
                    {
                        currentBehavior = Behavior.Aiming;
                        enemyAnim.SetTrigger("aim");
                        InvokeTheBroadcast();
                        return;
                    }
                }
                //Se pegar em algo, que não é o jogador, muda Behavior para Turn
                //Debug.LogError(hit.transform.name);
                currentBehavior = Behavior.Turn;
                //InvokeTheBroadcast();
            }
        }
    }

    public void FinishedAction()
    {
        InvokeTheBroadcast();
    }

    void InvokeTheBroadcast()
    {
        currentPos++;
        OnFinished?.Invoke(currentPos);
    }

    void RotateEnemy()
    {
        enemyAnim.Play("idle");

        //O jogador está acima do inimigo?
        if (transform.position.z < currentPlayerPos.z)
        {
            //Debug.LogError(transform.name + ": Jogador está acima do inimigo");
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            currentOrientation = Orientation.Up;
            currentBehavior = Behavior.Move;
            InvokeTheBroadcast();
            return;
        }
            
        if (transform.position.z > currentPlayerPos.z)
        {
            //Debug.LogError(transform.name + ": Jogador está abaixo do inimigo");

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            currentOrientation = Orientation.Down;
            currentBehavior = Behavior.Move;
            InvokeTheBroadcast();
            return;
        }
            
        if (transform.position.x < currentPlayerPos.x)
        {
            //Debug.LogError(transform.name + ": Jogador está a direita do inimigo");

            transform.rotation = Quaternion.Euler(0, -90f, 0f);
            currentOrientation = Orientation.Right;
            currentBehavior = Behavior.Move;
            InvokeTheBroadcast();
            return;
        }
            
        if (transform.position.x > currentPlayerPos.x)
        {
            //Debug.LogError(transform.name + ": Jogador está a esquerda do inimigo");

            transform.rotation = Quaternion.Euler(0, 90f, 0f);
            currentOrientation = Orientation.Left;
            currentBehavior = Behavior.Move;
            InvokeTheBroadcast();
        }
    }
}