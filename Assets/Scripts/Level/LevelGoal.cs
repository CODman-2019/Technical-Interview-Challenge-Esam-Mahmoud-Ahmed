using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public int goalIndex;
    public bool hasTimer;
    public bool isWin;
    public int timeLimit;
    public int goalAlternative;

    private int indexReturn;

    private void Start()
    {
        indexReturn = goalIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTimer)
        {
            if(Time.timeSinceLevelLoad >= timeLimit)
            {
                //Debug.Log("ending change");
                indexReturn = goalAlternative;
                hasTimer = false;
                isWin = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isWin)
                GameManager.gameManager.GameWin(indexReturn);
            else
                GameManager.gameManager.GameOver(indexReturn);
        }
    }
}
