using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotor : MonoBehaviour
{
    public float YaxeRotation = 0;
    public float ZaxeRotation = 0;
    public float speed = 120;
    public Transform stal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stal.transform.Rotate(0, YaxeRotation*Time.deltaTime*speed, ZaxeRotation* Time.deltaTime * speed);
    }
}
