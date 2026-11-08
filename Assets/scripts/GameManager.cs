using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameState { Playing, Dead, Win,  MainMenu}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Playing;

    [Header("UI Panels")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject staminaBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Time.timeScale = 1f;           
            Cursor.lockState = CursorLockMode.None;  
            Cursor.visible = true;        
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        deathPanel.SetActive(false);
        winPanel.SetActive(false);
        mainMenuPanel.SetActive(true); // show main menu first
        healthBar.SetActive(false); 
        staminaBar.SetActive(false);
        PauseGame(); // freeze game until play is clicked
    }
    public void OnPlayClicked()
    {
        mainMenuPanel.SetActive(false);
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthBar.SetActive(true); 
        staminaBar.SetActive(true);
    }
    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Dead:
                deathPanel.SetActive(true);
                PauseGame();
                break;

            case GameState.Win:
                winPanel.SetActive(true);
                Debug.Log("you win");
                PauseGame();
                break;
        }
    }

    public bool IsPlaying() => currentState == GameState.Playing;

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // --- Button callbacks ---
    public void OnRetryClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}