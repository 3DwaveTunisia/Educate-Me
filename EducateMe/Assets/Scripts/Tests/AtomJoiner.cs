using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomJoiner : MonoBehaviour
{
    public string chainType;

    public Transform parentAtom;
    GameObject newAtom;

    public float posX = 0.131f;
    public float posY = -0.092f;
    public float posZ;

    private Vector3 _newPos;
    private Vector3 _originalPos;

    public float speed = .2f;

    private Collider colRef;
    private bool resetPos = false;

    void Update()
    {
        if(newAtom)
        {
            newAtom.transform.localPosition = Vector3.MoveTowards(newAtom.transform.localPosition, _newPos, speed * Time.deltaTime);
            
            if(newAtom.transform.localPosition == _originalPos)
            {
                newAtom = null;
                resetPos = false;
            }

            if(!colRef.gameObject.activeInHierarchy) resetPos = true;

            if(colRef.gameObject.activeInHierarchy && resetPos)
            {
                newAtom.transform.parent = colRef.transform;
                _newPos = _originalPos;
            }

        }

    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == chainType)
        {   
            colRef = col;

            if(col.transform.childCount > 0)
            {
                newAtom = col.transform.GetChild(0).gameObject;
                _originalPos = newAtom.transform.localPosition;
                newAtom.transform.parent = parentAtom;
                _newPos = new Vector3(posX, posY, posZ);
            }

        }
            
    }

    void OnTriggerExit(Collider col)
    {
        newAtom.transform.parent = col.transform;
        _newPos = _originalPos;
    }

}
