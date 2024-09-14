using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager gameManager;

    private bool gamePause;
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

    private void Start()
    {
        gamePause = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }
        }
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

    public void PauseGame()
    {
        UIManager.uI.OpenPauseScreen();
        gamePause = true;
        //Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        UIManager.uI.OpenPlayerHUD();
        gamePause = false;
        //Time.timeScale = 1f;
    }

    //load game over 
    public void GameOver()
    {
        UIManager.uI.OpenGameOverScreen();
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
