using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Vector3 _originalPos;
    private Vector3 _newPos;

    public float _zoomSpeed = .2f;

    public List<Image> girlCustomBtns = new List<Image>();
    public List<Image> boyCustomBtns = new List<Image>();

    
    void Start()
    {
        _originalPos = transform.position;
        _newPos = _originalPos;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _newPos, _zoomSpeed * Time.deltaTime);
    }

    void swapColor(int index)
    {
        girlCustomBtns[index].color= new Color32(231, 142, 12, 255);
        boyCustomBtns[index].color= new Color32(231, 142, 12, 255);
    }

    void resetColor()
    {
        foreach(Image img in girlCustomBtns)
        {
            img.color = new Color32(254, 235, 159, 255);
        }

        foreach(Image img in boyCustomBtns)
        {
            img.color = new Color32(254, 235, 159, 255);
        }

    }

    public void zoomToFace(int index)
    {
        Vector3 facePos= new Vector3(-.09f, 1.83f, -8.34f);
        _newPos = facePos;
        resetColor();
        swapColor(index);
    }

    public void zoomToBody(int index)
    {
        Vector3 bodyPos= new Vector3(-.09f, 1.26f, -8.34f);
        _newPos = bodyPos;
        resetColor();
        swapColor(index);
    }

    public void zoomToLegs(int index)
    {
        Vector3 legsPos= new Vector3(-.09f, 0.69f, -8.34f);
        _newPos = legsPos;
        resetColor();
        swapColor(index);
    }

    public void resetCam(int index)
    {
        _newPos = _originalPos;
        resetColor();
        if (index == 6) swapColor(6);
        
    }

}
