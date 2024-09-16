using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uI;
    [SerializeField] private GameObject     titleScreen;
    
    [SerializeField] private GameObject     playerScreen;
    [SerializeField] private Slider         playerHealthBar;
    [SerializeField] private TMP_Text       timer;
    [SerializeField] private Image          dashIcon;
    [SerializeField] private Color          dashEnable;
    [SerializeField] private Color          dashDisable;

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

        dashIcon.color = dashEnable;

        playerScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);

    }

    private void Update()
    {
        if (!GameManager.gameManager.IsGamePause() && !GameManager.gameManager.IsGameOver())
        {   
            if(Time.timeScale == 0) { Time.timeScale = 1; }
            float timeCount = Mathf.Round(Time.timeSinceLevelLoad);
            timer.text = timeCount.ToString();
        }
        else { Time.timeScale = 0f; }
    }

    //TITLE UI
    public void OpenTitleScreen()
    {
        currentScreen.SetActive(false);
        currentScreen = titleScreen;
        currentScreen.SetActive(true);
    }

    //HUD UI
    public void OpenPlayerHUD()
    {
        currentScreen.SetActive(false);
        currentScreen = playerScreen;
        currentScreen.SetActive(true);
    }

    public void UpdatePlayerHealthBar(float value)
    {
        playerHealthBar.value = value;
    }

    public void ChangeDashIconColor(int state)
    {
        switch (state)
        {
            case 0:
                dashIcon.color = dashEnable;
                break;
            case 1:
                dashIcon.color = dashDisable;
                break;
            default:
                dashIcon.color = dashDisable;
                break;
        }
    }

    //PAUSE UI
    public void OpenPauseScreen()
    {
        currentScreen.SetActive(false);
        currentScreen = pauseScreen;
        currentScreen.SetActive(true);
    }

    //GAME OVER UI
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
                gameOverText.text = "You died, try again?";
                break;
            case 1:
                gameOverText.text = "You survived, NICE!";
                break;
            case 2:
                gameOverText.text = "You survived, but your late!";
                break;
        }
    }
}
