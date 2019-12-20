using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using MonotypeUnityTextPlugin;
using UnityEngine.SceneManagement;
using System;


public class GameManager1 : MonoBehaviour
{
    public GameObject ScoreComponents;
    public GameObject toDestroy;
    public GameObject questionPanel;
    public MPUITextComponent questionText;
    public MPUITextComponent Result;
    public Question[] questions;
    public List<Question> unansweredQuestions;
    private Question currentQuestion;
    private float Timer;
    public float startingTime = 8f;
    public Text timerText;
    /*-----------------------Answer------------------------------*/
    public string Correct;
    public string Wrong;
    public GameObject answerPanel;
    private bool answered = false;
    private int score = 0;
    public Text Scoretext;
    public MPUITextComponent finalScore;
    /*------------------------Answer------------------------------*/
    private void startTimer()
    {
        Timer = startingTime;
        timerText.text = Timer.ToString("f0");
    }
    public void SetCurrentQuestion()
    {
        if (unansweredQuestions.Count != 0)
        {int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count-1);
        questionPanel.SetActive(true);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        questionText.Text = currentQuestion.Fact.ToString();
        currentQuestion.Answers.SetActive(true);
        startTimer();}
    }
    IEnumerator Start()
    {
        Scoretext.text = "" + score.ToString();
        foreach (Question q in questions)
        {
            unansweredQuestions.Add(q);
        }
        SetCurrentQuestion();
        while (true)
        {
            yield return null;
            if (!answered)
            {
                if (Timer > 0)
                {
                    Timer -= 1 * Time.deltaTime;
                    timerText.text = Timer.ToString("f0");
                }
                else
                  if  (Timer <= 0)
                    { 
                
                        yield return new WaitForSeconds(2f);
                        currentQuestion.Answers.SetActive(false);
                        answerPanel.SetActive(true);
                    if (unansweredQuestions.Count != 0)
                    {
                        Result.Text = Wrong;
                    }
                        yield return new WaitForSeconds(2.5f);
                        selectAnswer(-1);
                        //yield return StartCoroutine(StartNextQuestion());
                    }
            }
            if (unansweredQuestions.Count==0)
            {
                //Debug.Log("no more questions");
                yield return new WaitForSeconds(2.5f);
                answerPanel.SetActive(true);
                ScoreComponents.SetActive(false);
                Result.Text = "";
                finalScore.Text= " "+"النتيجة النهائية " + score.ToString();
                //RestartQuiz.SetActive(true); //Reset Button!
               // Scoretext.text = "Score= " + score.ToString();
                toDestroy.SetActive(false);
            }
        }
       
        
    }
    IEnumerator StartNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(3.5f);
        if (unansweredQuestions.Count!=0)
            answerPanel.SetActive(false);
        SetCurrentQuestion();
        currentQuestion.Answers.SetActive(true);       
        answered = false;
    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        currentQuestion.Answers.SetActive(false);
        answerPanel.SetActive(true);
       
    }
    public void selectAnswer(int index)
    {
        answered = true;
        StartCoroutine(test());

        if (unansweredQuestions.Count != 0)
            if (index == currentQuestion.CorrectAnswer)
            {
                Result.Text = Correct;
                score += 50;
                Scoretext.text = "" + score.ToString();
            }
            else
            {
                Result.Text = Wrong;
                Scoretext.text = "" + score.ToString();
            }
        //if (unansweredQuestions.Count != 0)
        StartCoroutine(StartNextQuestion());
    }
}
