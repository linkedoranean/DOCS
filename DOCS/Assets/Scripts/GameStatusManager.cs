using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameStatusManager : MonoBehaviour
{
    public enum GameStatus{
        SelectChar = 0,
        MovePosMarker = 1,
        MoveChar = 2,
        RotateChar = 3,
        InitialOption = 4,
        Paused = 10 }
    public GameStatus CurrentGameStatus;
    
    public delegate void GameStatusBroadcast(string status, Transform selectedDoc);
    public static event GameStatusBroadcast onChanged;

    [SerializeField] private int statusRef;

    void Awake()
    {
        SelectionManager.OnSelected += CharacterSelected;
        MovementInput.OnPressedStart += StartSelection;
        MovementManager.OnMoveFinished += MarkerMoved;
        //CharactersManager.OnMoved += CharMoved;
        CharactersManager.OnMoved += ResetLoop;
    }
    
    void OnDestroy()
    {
        SelectionManager.OnSelected -= CharacterSelected;
        MovementInput.OnPressedStart -= StartSelection;
        MovementManager.OnMoveFinished -= MarkerMoved;
        //CharactersManager.OnMoved -= CharMoved;
        CharactersManager.OnMoved -= ResetLoop;
    }
    
    void Start()
    {
        
    }

    void StartSelection()
    {
        if (CurrentGameStatus == GameStatus.InitialOption)
        {
            StartCoroutine(BroadcastSelectChar());
        }
    }

    IEnumerator BroadcastSelectChar()
    {
        yield return new WaitForSeconds(0.5f);

        CurrentGameStatus = GameStatus.SelectChar;
        onChanged?.Invoke("SelectChar", null);
    }

    void CharacterSelected(Transform selectedDoctor)
    {
        StartCoroutine(BroadcastCharSelected(selectedDoctor));
    }
    
    IEnumerator BroadcastCharSelected(Transform character)
    {
        yield return new WaitForSeconds(0.5f);
        
        CurrentGameStatus = GameStatus.MovePosMarker;
        onChanged?.Invoke("MovePosMarker", character);
    }
    
    void MarkerMoved()
    {
        StartCoroutine(BroadcastToMoveChar());
    }
    
    IEnumerator BroadcastToMoveChar()
    {
        yield return new WaitForSeconds(0.5f);

        CurrentGameStatus = GameStatus.MoveChar;
        onChanged?.Invoke("MoveChar", null);
    }
    
    void CharMoved()
    {
        StartCoroutine(BroadcastToRotateChar());
    }
    
    IEnumerator BroadcastToRotateChar()
    {
        yield return new WaitForSeconds(0.5f);

        CurrentGameStatus = GameStatus.RotateChar;
        onChanged?.Invoke("RotateChar", null);
    }

    void ResetLoop()
    {
        CurrentGameStatus = GameStatus.InitialOption;
    }
}