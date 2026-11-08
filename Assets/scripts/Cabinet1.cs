using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class Cabinet1 : MonoBehaviour
{
    [SerializeField] Animator ani;

    private bool open;

    //  shared reference for ALL cabinets
    private static Cabinet1 currentTarget;

    public Paper paper;
    public GameObject PaperUI;
    public TMP_Text contentText;

    void Start()
    {
        open = false;
        ani.SetBool("open", false);
        ani.SetBool("close", false);
       
    }

    void Update()
    {
        // Only the selected cabinet can be interacted with
        if (currentTarget != this) return;
        AutoCloseIfTooFar(); 
        if (PlayerCasting.distanceFromTarget <= 5f)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                ToggleCabinet();
            }
        }
        if (PaperUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) )
            {
                PaperUI.SetActive(false);
            }
        }

        if (ani.GetBool("close"))
        {
            PaperUI.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        currentTarget = this; 
        if (PlayerCasting.distanceFromTarget <= 5f)
        {
            

            UIController.uiActive = true;
            UIController.actionText = open ? "Close" : "Open";
        }
        else
        {
            UIController.uiActive = false;
            UIController.actionText = ""; 
        }
        
    }

    void OnMouseExit()
    {
        if (currentTarget == this)
            currentTarget = null;

        UIController.uiActive = false;
        UIController.actionText = "";
    }

    void ToggleCabinet()
    {
        if (!open)
        {
            ani.SetBool("open", true);
            ani.SetBool("close", false);
            open = true;

            if (paper != null)
            {
                Debug.Log(paper.content);
                contentText.text = paper.content;
                PaperUI.SetActive(true); 

            }
           
        }
        else
        {
            ani.SetBool("close", true);
            ani.SetBool("open", false);
            open = false;
        }
    }
    void AutoCloseIfTooFar()
    {
        if (open && PlayerCasting.distanceFromTarget > 5f)
        {
            ani.SetBool("close", true);
            ani.SetBool("open", false);
            open = false;

            
        }
    }
}