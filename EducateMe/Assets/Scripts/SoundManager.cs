using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    void Start()
    {
        if ( SceneManager.GetActiveScene().name == "Game" ) MainMenu.SoundMuted = false;
        if ( SceneManager.GetActiveScene().name == "Quiz" ) MainMenu.SoundMuted = false;
    }
    
    void Update()
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource src in sources)
            src.mute = MainMenu.SoundMuted;
    }

}
