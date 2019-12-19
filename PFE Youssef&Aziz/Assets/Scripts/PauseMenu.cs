using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool drop = false;

    Animator anim;

    public Slider levelProgress;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void dropMenu()
    {
        if (!(anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime))
        {
            drop = !drop;
            anim.SetBool("Drop", drop);
        }

    }

    public void loadLevel(string sceneName)
    {
        StartCoroutine( LoadAsync(sceneName) );
    }

    IEnumerator LoadAsync (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            levelProgress.value = progress;

            yield return null;

        }

    }

    public void ToggleSound()
    {
        MainMenu.SoundMuted = !MainMenu.SoundMuted;

    }

}
