using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private bool isDoorLocked;

    public enum DoorType
    {
        Regular,
        CardedBlue,
        CardedRed,
        CardedYellow,
        CardedGreen,
        CardedPurple,
        HackableAlpha,
        HackableBeta,
        HackableGamma,
        HackableOmega,
        Soldered
    }
    public DoorType doorType;

    [SerializeField] private GameObject managerBrother;
    
    [SerializeField] private GameObject[] doors;

    [SerializeField] private bool unlockedUp, unlockedDown, unlockedLeft, unlockedRight;
    [Space(10)]
    [SerializeField] private bool lockedUp;
    [SerializeField] private bool lockedDown;
    [SerializeField] private bool lockedLeft;
    [SerializeField] private bool lockedRight;

    void Awake()
    {
        managerBrother = transform.parent.Find("MovementInfo").gameObject;

        if (isDoorLocked)
        {
            managerBrother.SetActive(false);
        }
    }
    
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorType != DoorType.Regular)
            {
                isDoorLocked = true;
                
                other.GetComponent<MovementInput>().canMoveUp = lockedUp;
                other.GetComponent<MovementInput>().canMoveDown = lockedDown;
                other.GetComponent<MovementInput>().canMoveLeft = lockedLeft;
                other.GetComponent<MovementInput>().canMoveRight = lockedRight;
                
                var tempKey = other.GetComponent<DoorKeyManager>();

                if (doorType == DoorType.CardedBlue &&
                    tempKey.keycardBlue == 1 ||
                    
                    doorType == DoorType.CardedRed &&
                    tempKey.keycardRed == 1 ||
                    
                    doorType == DoorType.CardedYellow &&
                    tempKey.keycardYellow == 1 ||
                    
                    doorType == DoorType.CardedGreen &&
                    tempKey.keycardGreen == 1 ||
                    
                    doorType == DoorType.CardedPurple &&
                    tempKey.keycardPurple == 1 ||
                    
                    doorType == DoorType.HackableAlpha &&
                    tempKey.overrideAlpha == 1 ||
                    
                    doorType == DoorType.HackableBeta &&
                    tempKey.overrideBeta == 1 ||
                    
                    doorType == DoorType.HackableGamma &&
                    tempKey.overrideGamma == 1 ||
                    
                    doorType == DoorType.HackableOmega &&
                    tempKey.overrideOmega == 1 ||
                    
                    doorType == DoorType.Soldered &&
                    tempKey.torch == 1)
                {
                    isDoorLocked = false;
                }
            }

            if (!isDoorLocked ||
                doorType == DoorType.Regular)
            {
                foreach (GameObject door in doors)
                {
                    door.GetComponent<DoorAnimManager>().OpenDoor();
                }

                other.GetComponent<MovementInput>().canMoveUp = unlockedUp;
                other.GetComponent<MovementInput>().canMoveDown = unlockedDown;
                other.GetComponent<MovementInput>().canMoveLeft = unlockedLeft;
                other.GetComponent<MovementInput>().canMoveRight = unlockedRight;
                
                //managerBrother.SetActive(true);
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