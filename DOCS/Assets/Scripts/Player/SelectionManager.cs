using System.Collections;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public delegate void SelectionAction(Transform character);
    public static event SelectionAction OnSelected;
    public static event SelectionAction ChangeCamera;

    //TODO Tem que mudar isso por turno
    [SerializeField] private bool canSelect;

    [SerializeField] private int selectionCounter;
    [SerializeField] private Transform[] listChars;
    [SerializeField] private Transform marker;

    [SerializeField] private GameObject markerObj;

    void Awake()
    {
        MovementInput.OnPressedAction += ActionButton;
        MovementInput.OnPressedUp += ListUpward;
        MovementInput.OnPressedDown += ListDownward;
        
        GameStatusManager.onChanged += AllowSelection;
    }
    
    void OnDestroy()
    {
        MovementInput.OnPressedAction -= ActionButton;
        MovementInput.OnPressedUp -= ListUpward;
        MovementInput.OnPressedDown -= ListDownward;
        
        GameStatusManager.onChanged -= AllowSelection;
    }

    void ActionButton()
    {
        if (canSelect)
        {
            OnSelected?.Invoke(listChars[selectionCounter]);

            markerObj.SetActive(false);
            canSelect = false;
        }
    }

    void ListUpward()
    {
        if (canSelect)
        {
            selectionCounter--;
        
            if (selectionCounter < 0)
            {
                selectionCounter = 2;
            }

            marker.transform.position = listChars[selectionCounter].transform.position;
        }
    }
    
    void ListDownward()
    {
        if (canSelect)
        {
            selectionCounter++;
        
            if (selectionCounter > 2)
            {
                selectionCounter = 0;
            }
        
            marker.transform.position = listChars[selectionCounter].transform.position;
        }
    }
    
    void Start()
    {
        
    }

    void AllowSelection(string status, Transform character)
    {
        if (status == "SelectChar")
        {
            marker.transform.position = listChars[selectionCounter].transform.position;
            
            canSelect = true;
            markerObj.SetActive(true);
            
            ChangeCamera?.Invoke(transform);
            
            //StartCoroutine(TurnSelectionOn());
        }
    }
}