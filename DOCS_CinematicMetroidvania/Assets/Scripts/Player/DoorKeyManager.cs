using UnityEngine;

public class DoorKeyManager : MonoBehaviour
{
    [SerializeField] public int
        keycardBlue,
        keycardRed,
        keycardYellow,
        keycardGreen,
        keycardPurple,
        overrideAlpha,
        overrideBeta,
        overrideGamma,
        overrideOmega,
        torch;

    void Awake()
    {
        //Ler o playerpref e alterar os valores dos int relacionados;
    }

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ao coletar uma chave. mudar o valor do int relacionado
        //Salvar o SaveCollectibleInfo para salvar informações
    }

    void SaveCollectibleInfo()
    {
        //Aqui precisa ser chamar no ontriggerenter
        //Toda vez que coletar um tipo de chave, mudar o int relacionado para 1 e salvar no playerpref
    }
}