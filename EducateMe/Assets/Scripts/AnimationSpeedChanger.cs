using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedChanger : MonoBehaviour
{
    public float speed = 1;
    
    
    void Start()
    {
        GetComponent<Animator>().speed = speed; 
    }

}
