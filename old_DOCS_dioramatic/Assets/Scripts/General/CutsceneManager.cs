using System;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    //TODO na classe, nos inspector, colocar imagem e nome de itens coletáveis
    
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private TextMeshProUGUI charNameField;

    [SerializeField] private GameObject enabler;
    
    [Serializable]
    private class CutscenePortraits
    {
        public Sprite charImage;
        public string charName;
    }

    [SerializeField] private CutscenePortraits[] cutscenePortraits;
    
    void Awake()
    {
        textField.text = "";
        charNameField.text = "";
        
        enabler.SetActive(false);
        
        CutsceneTrigger.OnNext += FillUpText;
        CutsceneTrigger.OnTriggered += EnablerStatus;
    }

    void OnDestroy()
    {
        CutsceneTrigger.OnNext -= FillUpText;
        CutsceneTrigger.OnTriggered -= EnablerStatus;
    }

    private void Start()
    {
        
    }

    void EnablerStatus(bool status)
    {
        enabler.SetActive(status);
    }

    void FillUpText(string text, string charName, string command)
    {
        textField.text = text;
        charNameField.text = charName;
        
        //TODO tem que usar o nome do personagem e colocar a imagem na tela de acordo
    }
}