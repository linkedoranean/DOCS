using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public enum Orientation{ Left, Right, Up, Down }
    public Orientation currentOrientation;
    
    [SerializeField] private bool allowInput;
    [SerializeField] public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (allowInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Desligar o allowInput
                
                if (currentOrientation == Orientation.Up)
                {
                    if (canMoveUp)
                    {
                        //Chamar MoveCharacter();
                    }
                }
                
                if (currentOrientation != Orientation.Up)
                {
                    //Rotacionar pra direção correta
                    
                    //Acho que aqui depende da direção atual
                    //Talvez enviar que a rotação tem que ser zero
                }
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Desligar o allowInput
                
                if (currentOrientation == Orientation.Down)
                {
                    if (canMoveDown)
                    {
                        //Chamar MoveCharacter();
                    }
                }
                
                if (currentOrientation != Orientation.Down)
                {
                    //Rotacionar pra direção correta
                    
                    //Acho que aqui depende da direção atual
                    //Talvez enviar que a rotação tem que ser zero
                }
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Desligar o allowInput
                
                if (currentOrientation == Orientation.Left)
                {
                    if (canMoveLeft)
                    {
                        //Chamar MoveCharacter();
                    }
                }
                
                if (currentOrientation != Orientation.Left)
                {
                    //Rotacionar pra direção correta
                    
                    //Acho que aqui depende da direção atual
                    //Talvez enviar que a rotação tem que ser zero
                }
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Desligar o allowInput
                
                if (currentOrientation == Orientation.Right)
                {
                    if (canMoveRight)
                    {
                        //Chamar MoveCharacter();
                    }
                }
                
                if (currentOrientation != Orientation.Right)
                {
                    //Rotacionar pra direção correta
                    
                    //Acho que aqui depende da direção atual
                    //Talvez enviar que a rotação tem que ser zero
                }
            }
        }
    }

    IEnumerator MoveCharacter()
    {
        //Pegar posição atual em Z
        //Somar +9 e definir como endPos
        //Rigidbody.MovePosition (posAtual, endPos, time.deltaTime)
        
        //Precisa chamar animação do walk
        
        //Quando acabar o Rigidbody.MovePosition, religar allowInput
        
        yield return null;
    }
    
    IEnumerator RotateCharacter()
    {
        //Pegar Rotação atual em Y
        //Somar o valor recebido na chamada e definir como endPos
        //Rigidbody.MoveRotation (https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html)
        
        //Precisa chamar animação de quando rotacionar
        //Talvez nem ter animação de rotação, só rotacionar o modelo mesmo
        
        //Quando acabar o Rigidbody.MovePosition, religar allowInput
        
        yield return null;
    }
}