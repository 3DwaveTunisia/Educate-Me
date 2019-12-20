using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonotypeUnityTextPlugin;

public class InfoDialog : MonoBehaviour
{
    public MPUITextComponent DiagBox;
    public string[] Sentences;
    public AudioClip[] AudioSenences;

    public AudioSource Src;
    
    private int index;

    public float typingSpeed = .02f;

    public Button prevBtn;
    public Button nextBtn;
    public Button repBtn;

    public GameObject nextSequence;

    public Transform Player;

    Animator anim;
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController talk;


    void Start()
    {
        //Src.clip = AudioSenences[index];
        //Src.Play();
        playAudio(index);
        StartCoroutine(Type());
        enableButtons(false);
        anim = Player.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (DiagBox.Text == Sentences[index]) enableButtons(true);

        if(Src.isPlaying) anim.runtimeAnimatorController = talk;
        else anim.runtimeAnimatorController = idle;

    }

    void playAudio(int index)
    {
        if (AudioSenences.Length != 0)
        {
            if (AudioSenences[index])
            {
                Src.clip = AudioSenences[index];
                Src.Play();
            }

        }

    }

    void enableButtons(bool param) 
    {
        prevBtn.interactable = param;
        nextBtn.interactable = param;
        repBtn.interactable = param;
    }

    IEnumerator Type()
    {
        foreach(char letter in Sentences[index].ToCharArray())
        {
            DiagBox.Text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void nextSentence()
    {
        if (index < Sentences.Length - 1)
        {
            enableButtons(false);
            index++;
            DiagBox.Text = "";
            StartCoroutine(Type());
            playAudio(index);
            //Src.clip = AudioSenences[index];
            //Src.Play();
        }
        else nextSequence.SetActive(true);

    }

    public void replaySentence()
    {
        enableButtons(false);
        DiagBox.Text = "";
        StartCoroutine(Type());
        playAudio(index);
        //Src.clip = AudioSenences[index];
        //Src.Play();
    }

    public void prevSentence()
    {
        if (index > 0)
        {
            enableButtons(false);
            index--;
            DiagBox.Text = "";
            StartCoroutine(Type());
            playAudio(index);
            //Src.clip = AudioSenences[index];
            //Src.Play();
        }

    }

}
