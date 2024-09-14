using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager uI;
    [SerializeField] private GameObject     titleScreen;
    [SerializeField] private GameObject     playerScreen;
    [SerializeField] private GameObject     pauseScreen;
    [SerializeField] private GameObject     gameOverScreen;
    [SerializeField] private TMP_Text       gameOverText;

    private GameObject currentScreen;

    private void Awake()
    {
        if(uI == null)
        {
            uI = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        currentScreen = titleScreen;
        playerScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }


    public void OpenTitleScreen()
    {
        currentScreen.SetActive(false);
        currentScreen = titleScreen;
        currentScreen.SetActive(true);
    }

    public void OpenPlayerHUD()
    {
        currentScreen.SetActive(false);
        currentScreen = playerScreen;
        currentScreen.SetActive(true);
    }

    public void OpenPauseScreen()
    {
        currentScreen.SetActive(false);
        currentScreen = pauseScreen;
        currentScreen.SetActive(true);
    }

    public void OpenGameOverScreen(int ending)
    {
        currentScreen.SetActive(false);
        ChangeGameOverText(ending);
        currentScreen = gameOverScreen;
        currentScreen.SetActive(true);
    }

    private void ChangeGameOverText(int outcome)
    {
        switch(outcome)
        {
            case 0:
                gameOverText.text = "You Lose, try again?";
                break;
        }
    }
}
