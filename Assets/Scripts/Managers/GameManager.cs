using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Load level
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    //load game over 
    public void GameOver()
    {
        UIManager.uI.OpenGameOverScreen();
    }
    //reset level

    public void Reset()
    {
        
    }
    //exit game

}
