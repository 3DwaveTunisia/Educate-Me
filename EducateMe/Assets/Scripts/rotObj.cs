using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotObj : MonoBehaviour
{

    public float rotSpeed= 800;
    public Transform parent;


    public void Dragging()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -rotX);
    }

    public void SelectDrag()
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeInHierarchy)
            {
                float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                child.Rotate(Vector3.up, -rotX);
            }

        }

    }

}
