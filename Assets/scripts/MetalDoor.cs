using StarterAssets;
using System.Collections; 
using UnityEngine;
using UnityEngine.InputSystem;

public class MetalDoor : MonoBehaviour
{
    [SerializeField] bool canOpen;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject theCam;
    [SerializeField] GameObject textOnScreen;
    [SerializeField] AudioSource lockedDoor;
    [SerializeField] private Animator animator; 


    void Update()
    {
        if (canOpen == true)
        {
            
            
        }
    }
    void OnMouseOver()
    {   
        if (PlayerCasting.distanceFromTarget <= 5 && canOpen ==false)
        {
            
            UIController.actionText = "Door is locked";
      
            UIController.uiActive = true;
        }
        else
        {
            canOpen = false; 
            UIController.actionText = "";
            UIController.commandText = "";
            UIController.uiActive = false;
        }
    }

    void OnMouseExit()
    {
        canOpen = false;
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }

    public void UnlockDoor()
    {
        canOpen = true;
        animator.SetTrigger("OpenDoor");
        Debug.Log("heyho");
    }
    
}
