using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    //Bool perguntando se precisa de chave
    
    //Enum com as chaves disponíveis

    [SerializeField] private GameObject[] doors;
    
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Verifica se a porta precisa de chave
            //SE PRECISAR
            //Pegar o componente do jogador e ver se tem a chave
            
            //Se a porta precisa de chave, precisa avisar o MovementInfo irmão para ativar a direção proibida

            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorAnimManager>().OpenDoor();
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorAnimManager>().CloseDoor();
            }
        }
    }
}