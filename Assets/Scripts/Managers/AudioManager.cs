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
            case "jump":
                listener.PlayOneShot(playerJump);
                break;
            case "attack":
                listener.PlayOneShot(playerSwing);
                break;
            case "hit":
                listener.PlayOneShot(playerJump);
                break;
            case "dash":
                listener.PlayOneShot(playerDash);
                break;
            case "enemyBasic":
                listener.PlayOneShot(enemySwing);
                break;
            case "enemyFinisher":
                listener.PlayOneShot(enemyCrush);
                break;
            case "game_over":
                listener.PlayOneShot(gameOver);
                break;
            case "game_end":
                listener.PlayOneShot(gameEnd);
                break;
        }
    }
}
