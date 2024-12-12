using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMasterManager : MonoBehaviour
{
    public delegate void TileMasterAction(string name);
    public static event TileMasterAction OnLoaded;
    
    [SerializeField] private List<string> tileList;
    
    void Awake()
    {
        tileList = new List<string>();
        
        //Aqui precisa carregar a lista de nomes de tiles do json
        //foreach item da lista, invocar Onloaded e passar o nome como mensagem

        TileManager.OnDiscovered += RegisterTileName;
    }

    void OnDestroy()
    {
        TileManager.OnDiscovered -= RegisterTileName;
    }

    void RegisterTileName(string name)
    {
        tileList.Add(name);
        //essa lista precisa ser salva como json
    }
    
    //Criar uma função para zerar a lista/apagar tudo e salvar
}