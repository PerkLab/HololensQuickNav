using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandText : MonoBehaviour {

    [Tooltip("Hololens Camera")]
    public GameObject cam;
    [Tooltip("Object to position text around")]
    public GameObject selectedObject;
    public GameObject selectedObject2;
    static private bool toggleObject = false;

    void Update () {
        //position around selected object and face camera
        if(toggleObject) //center on object 2
        {
            gameObject.transform.position = new Vector3(selectedObject2.transform.position.x, selectedObject2.transform.position.y, selectedObject2.transform.position.z);
        }
        else //center on object 1
        {
            gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        }
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }

    static public void ToggleObject(bool secondObject)
    {
        toggleObject = secondObject;
    }
}
