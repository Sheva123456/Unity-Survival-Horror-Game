using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections; 

public class HealthStamina : MonoBehaviour
{
    public static int health;
    public static int stamina;
    private float nextRegenTime;
    public float currentStamina = 100f;
    private float maxStamina = 100f;
    public bool exhausted = false;
    private float exhaustedUntil;

    [SerializeField] private float staminaDrainRate = 50f;
    [SerializeField] private float staminaRegenRate = 15f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.value = 100;
        staminaBar.value = 100;
        
    }
    // Update is called once per frame
    void Update()
    {
       if (healthBar.value == 0)
        {
            GameManager.Instance.SetState(GameState.Dead);
            return; 
        }

       
        if (healthBar.value < 100)
        {
            regenHP(); 
        }
            
    }
    public void getdamaged(int dmg)
    {
        healthBar.value -= dmg;
        nextRegenTime = Time.time + 2f; 
    }

    private void regenHP()
    {  
            if (Time.time >= nextRegenTime)
            {
                healthBar.value += 2;
                nextRegenTime = Time.time + 2f; 
            }    
    }

    public bool CanSprint()
    {
        return currentStamina > 0;
    }

    public void DrainStamina()
    {
        currentStamina -= staminaDrainRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina <= 0 && !exhausted)
        {
            exhausted = true;
            exhaustedUntil = Time.time + 3f; // exhausted for 3 seconds
        }

        staminaBar.value = currentStamina;
    }

    public void RegenStamina()
    {
        if (exhausted)
        {
            if (Time.time >= exhaustedUntil)
            {
                exhausted = false;

                currentStamina = 10f;
                staminaBar.value = currentStamina;
            }
            else
            {
                return;
            }
        }

        currentStamina += staminaRegenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        staminaBar.value = currentStamina;
    }

}
