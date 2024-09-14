using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public InputAction pauseButton;
    
    private bool gamePause;
    private bool gameOver;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void OnEnable()
    {
        pauseButton.Enable();
        pauseButton.performed += PauseGame;
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }


    private void Start()
    {
        gamePause = false;
    }

    void Update()
    {
       
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        UIManager.uI.OpenPlayerHUD();
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(0);
        UIManager.uI.OpenTitleScreen();
    }

    public bool IsGamePause() { return gamePause; }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("pause initiated");
            if (!gamePause)
            {
                UIManager.uI.OpenPauseScreen();
                gamePause = true;
                Debug.Log("pausee");
            }
            else
            {
                UnpauseGame();
            }
        }

    }

    public void UnpauseGame()
    {
        UIManager.uI.OpenPlayerHUD();
        gamePause = false;
        Debug.Log("unpaused");
    }

    //load game over 
    public void GameOver()
    {
        UIManager.uI.OpenGameOverScreen(0);
    }

    public void GameWin(int goal)
    {
        UIManager.uI.OpenGameOverScreen(goal);
    }

    //reset level
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    //exit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
