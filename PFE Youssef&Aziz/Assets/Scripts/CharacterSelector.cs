using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    private GameObject[] Characters;
    private int index = 0;

    public Slider levelProgress;
    
    void Start()
    {
        updateList();
    }

    public void updateList()
    {
        index = PlayerPrefs.GetInt("selecetdPlayer");

        Characters = new GameObject[transform.childCount];

        for(int i=0; i < transform.childCount; i++)
        {
            Characters[i] = transform.GetChild(i).gameObject;
            Characters[i].SetActive(false);
        }

        if(Characters[index]) Characters[index].SetActive(true);

    }

    public void ToggleLeft()
    {
        Characters[index--].SetActive(false);

        if(index < 0) index= Characters.Length - 1;

        Characters[index].SetActive(true);

    }

    public void ToggleRight()
    {
        Characters[index++].SetActive(false);

        if(index == Characters.Length) index= 0;

        Characters[index].SetActive(true);

    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("selecetdPlayer", index);
        loadLevel("Game");
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
    
}
