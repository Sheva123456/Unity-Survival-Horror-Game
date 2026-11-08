using StarterAssets;
using System;
using System.Collections; 
using UnityEngine;
using UnityEngine.InputSystem;

public class CandlePickUP : MonoBehaviour
{
    [SerializeField] bool canPickUp;
    [SerializeField] GameObject textOnScreen;
    [SerializeField] AudioSource lockedDoor;
    [SerializeField] GameObject tableCandle;
    [SerializeField] GameObject handCandle;

    void Update()
    {
        if (canPickUp == true)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                this.GetComponent<BoxCollider>().enabled = false; 
                tableCandle.SetActive(false);
                handCandle.SetActive(true);
            }
            
        }
    }
    void OnMouseOver()
    {   
        if (PlayerCasting.distanceFromTarget <= 5)
        {
            canPickUp = true; 
            UIController.actionText = "candle";
            UIController.commandText = "Pick up";
            UIController.uiActive = true;
        }
        else
        {
            canPickUp = false; 
            UIController.actionText = "";
            UIController.commandText = "";
            UIController.uiActive = false;
        }
    }

    void OnMouseExit()
    {
        canPickUp = false;
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }

    
}
