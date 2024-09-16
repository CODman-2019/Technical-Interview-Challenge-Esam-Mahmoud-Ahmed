using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sound;

    public AudioClip playerJump;
    public AudioClip playerSwing;
    public AudioClip playerHit;
    public AudioClip playerDash;

    public AudioClip enemySwing;
    public AudioClip enemyCrush;

    public AudioClip secret;
    public AudioClip gameOver;
    public AudioClip gameEnd;

    private AudioSource listener;

    private void Awake()
    {
        sound = this;
        listener = GetComponent<AudioSource>();
    }

    public void TriggerSound(string word)
    {
        switch (word)
        {
            case "Jump":
                listener.PlayOneShot(playerJump);
                break;
            case "Attack":
                listener.PlayOneShot(playerSwing);
                break;
            case "Hit":
                listener.PlayOneShot(playerHit);
                break;
            case "Dash":
                listener.PlayOneShot(playerDash);
                break;
            case "EnemyBasic":
                listener.PlayOneShot(enemySwing);
                break;
            case "EnemyFinisher":
                listener.PlayOneShot(enemyCrush);
                break;
            case "Secret":
                listener.PlayOneShot(secret);
                break;
            case "GameOver":
                listener.PlayOneShot(gameOver);
                break;
            case "GameEnd":
                listener.PlayOneShot(gameEnd);
                break;
        }
    }
}
