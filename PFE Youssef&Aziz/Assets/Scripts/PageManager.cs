using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PageManager : MonoBehaviour
{

    public float startDelay = 2f;

    void Start()
    {
        StartCoroutine( LateStart(startDelay) );
    }

    public void disableTrackers()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ImageTargetBehaviour>().enabled = false;
        }

    }

    IEnumerator LateStart(float waitTime)
     {
         yield return new WaitForSeconds(waitTime);
         disableTrackers();
     }

}
