using System;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    //TODO usar esse script para mostrar quando pegou uma chave, por exemplo
    
    public delegate void CutSceneEvent(bool status);
    public static event CutSceneEvent OnTriggered;
    
    public delegate void CutSceneContent(string text, string charName, string command);
    public static event CutSceneContent OnNext;

    [Serializable]
    private class CutsceneContent
    {
        public string text;
        public string charName;
        public string command;
    }

    [SerializeField] private CutsceneContent[] cutSceneContent;

    [SerializeField] private int counter;
    
    bool gamePaused;
    
    void Awake()
    {
        //TODO Esse objeto precisa salvar que ele já foi tocado uma vez e salvar no prefab
        
        counter = 0;
        
        InputManager.OnPressedAction += BroadcastNext;
    }

    void OnDestroy()
    {
        InputManager.OnPressedAction -= BroadcastNext;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            OnTriggered?.Invoke(true);
            BroadcastNext("NA");
        }
    }

    void BroadcastNext(string buttonPressed)
    {
        //Debug.LogError("Mandou mensagem");
        
        if (counter >= 0 && cutSceneContent.Length > counter)
        {
            OnNext?.Invoke(cutSceneContent[counter].text,
                           cutSceneContent[counter].charName,
                           cutSceneContent[counter].command );

            counter++;
         
            //Debug.LogError(counter);
            
            return;
        }

        if (counter == cutSceneContent.Length)
        {
            //Debug.LogError("eae, desativou?");
            OnTriggered?.Invoke(false);
        }
    }
}