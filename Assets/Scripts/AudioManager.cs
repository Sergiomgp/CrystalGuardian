using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gameAudio;
    public AudioClip[] gameSongs;

    private void Awake()
    {
        gameAudio = GetComponent<AudioSource>();
        gameAudio.clip = gameSongs[0];
        gameAudio.Play();
    }

    private void Update()
    {
        if (BossController.engaged && !PauseMenu.isPaused)
        {
            gameAudio.clip = gameSongs[1];
            if (!gameAudio.isPlaying)
            {
                gameAudio.Play();
                Debug.Log("boss engaged, playing boss song" + gameAudio.clip.name);
            }
        }

        if (!BossStats.bossAlive)
        {
            gameAudio.clip = gameSongs[0];
            if (!gameAudio.isPlaying && !PauseMenu.isPaused)
            {
                gameAudio.Play();
                Debug.Log("boss dead, playing boss song" + gameAudio.clip.name);
            }
        }

        PauseMusic();
    }


    void PauseMusic()
    {
        if (PauseMenu.isPaused)
        {
            gameAudio.Pause();
        }
        else if (!PauseMenu.isPaused)
        {
            if (!gameAudio.isPlaying)
            {
                gameAudio.Play();
            }
        }
    }
}
