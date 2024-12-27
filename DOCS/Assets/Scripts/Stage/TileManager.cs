using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public delegate void TileAction(string name);
    public static event TileAction OnDiscovered;
    
    //[SerializeField] private MeshRenderer tileRenderer;

    [SerializeField] private BoxCollider tileCollider;
    
    [SerializeField] private List<Transform> children;

    //Receber mensagem do TileMasterManager e verificar se é o nome desse tile
    //Se for o nome igual, chamar DisplayTile
    
    void Awake()
    {
        //tileRenderer = GetComponent<MeshRenderer>();
        tileCollider = GetComponent<BoxCollider>();
        //tileRenderer.enabled = false;

        TileMasterManager.OnLoaded += CheckNameReceived;
    }

    void OnDestroy()
    {
        TileMasterManager.OnLoaded -= CheckNameReceived;
    }

    void Start()
    {
        children = new List<Transform>();

        if (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                children.Add(child);
                child.gameObject.SetActive(false);
            }
        }
    }

    private void CheckNameReceived(string nameReceived)
    {
        if (nameReceived == transform.name)
        {
            DisplayTile();
        }
    }

    public void DisplayTile()
    {
        tileCollider.enabled = false;
        //tileRenderer.enabled = true;
        
        if (children.Count != 0)
        {
            foreach (Transform child in children)
            {
                child.gameObject.SetActive(true);
            }
        }
        
        OnDiscovered?.Invoke(transform.name);
        //Debug.LogError(transform.name);
    }
}