using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public static bool SoundMuted = false;

    public void ToggleSound()
    {
        SoundMuted = !SoundMuted;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenURL()
    {
        Application.OpenURL("https://www.facebook.com/3Dwave.tech/");
    }

}
