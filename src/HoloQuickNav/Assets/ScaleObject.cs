using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{

    [Tooltip("Object you wish to scale")]
    public GameObject selectedObject;
    public GameObject cam;

    // Use this for initialization
    void Start()
    {
        Align();
    }

    void Update()
    {
        Align();
    }

    public void Align()
    {
       
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform);
    }


    public void ScaleUp(float amount)
    {
        selectedObject.transform.localScale += new Vector3(amount, amount, amount);
        Align();
    }

    public void ScaleDown(float amount)
    {
        selectedObject.transform.localScale += new Vector3(-amount, -amount, -amount);
        Align();
    }

   
}
