using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private bool gamePause;
    private bool gameOver;

    private MenuControls menuControls;
    private InputAction pause;
    private InputAction reset;
    private InputAction title;

    private void Awake()
    {
        if (gameManager != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            gameManager = this;
            if(menuControls == null)
            menuControls = new MenuControls();

            //DontDestroyOnLoad(this);
        }

    }
    private void OnEnable()
    {
        pause = menuControls.Menus.pause;
        reset = menuControls.Menus.reset;
        title = menuControls.Menus.title;

        if(!pause.enabled)  pause.Enable();
        if(!reset.enabled)  reset.Enable();
        if(!title.enabled) title.Enable();

        pause.performed += PauseGame;
        reset.performed += ResetGame;
        title.performed += LoadTitleScreen;
    }

    private void OnDisable()
    {
        pause.Disable();
        reset.Disable();
        title.Disable();
    }


    private void Start()
    {
        gamePause = false;
        gameOver = false;
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        UIManager.uI.OpenPlayerHUD();
    }

    public void LoadTitleScreen(InputAction.CallbackContext context)
    {
        if (gamePause || gameOver)
        {
            SceneManager.LoadScene(0);
            UIManager.uI.OpenTitleScreen();
        }
    }

    public bool IsGamePause() { return gamePause; }
    public bool IsGameOver()  {  return gameOver; }

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
    public void GameOver(int index)
    {
        UIManager.uI.OpenGameOverScreen(index);
        gameOver = true;
        AudioManager.sound.TriggerSound("GameOver");
    }

    public void GameWin(int goal)
    {
        UIManager.uI.OpenGameOverScreen(goal);
        AudioManager.sound.TriggerSound("GameEnd");
        gameOver = true;
    }

    //reset level
    public void ResetGame(InputAction.CallbackContext context)
    {
        if(gamePause || gameOver)
        {
            SceneManager.LoadScene(1);
            UIManager.uI.OpenPlayerHUD();
        }
    }

    //exit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
