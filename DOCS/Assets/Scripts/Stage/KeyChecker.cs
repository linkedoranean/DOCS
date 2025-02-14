using System;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    public delegate void KeyCheckerEvent(string necessaryKey);
    public static event KeyCheckerEvent OnRequested;
    
    public enum DoorType
    {
        regular,
        keycardBlue,
        keycardRed,
        keycardYellow,
        keycardGreen,
        keycardPurple,
        overrideAlpha,
        overrideBeta,
        overrideGamma,
        overrideOmega,
        torch
    }
    public DoorType RequiredKey;

    [SerializeField] private bool isDoorLocked;

    [SerializeField] private GameObject mover;
    
    [Space]
    [SerializeField] private bool openingDoor;
    [SerializeField] private float doorSpeed;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private Vector3[] doorGoalPos;

    void Awake()
    {
        mover.SetActive(false);
    }
    
    void OnDestroy()
    {
        
    }
    
    void Start()
    {
        if (PlayerPrefs.HasKey(RequiredKey.ToString()))
        {
            OpenDoors(true);
        }
    }

    void FixedUpdate()
    {
        if (openingDoor)
        {
            doors[0].transform.localPosition = Vector3.MoveTowards(doors[0].transform.localPosition, doorGoalPos[0], doorSpeed);
            doors[1].transform.localPosition = Vector3.MoveTowards(doors[1].transform.localPosition, doorGoalPos[1], doorSpeed);

            if (Math.Abs(doors[0].transform.localPosition.y - doorGoalPos[0].y) < 0.01f)
            {
                openingDoor = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDoorLocked)
            {
                if (PlayerPrefs.HasKey(RequiredKey.ToString()))
                {
                    OpenDoors(true);
                }
            }

            if (!isDoorLocked)
            {
                DoorOpeningAnim();
            }
        }
    }

    [ContextMenu("Open Door")]
    void ManuallyOpenDoors()
    {
        OpenDoors(true);
    }

    void OpenDoors(bool hasKey)
    {
        if (hasKey)
        {
            DoorOpeningAnim();

            mover.SetActive(true);
            isDoorLocked = false;
        }

        if (!hasKey)
        {
            //Mostrar a mensagem indicando que precisa da chave
            //Tocar som de porta trancada
        }
    }

    void DoorOpeningAnim()
    {
        openingDoor = true;
    }
}