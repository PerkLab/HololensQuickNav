using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to position test displaying current command and available tools 
/// </summary>
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
    /// <summary>
    /// Toggle object was used in the case of 3point registration to switch the selected object so the text would appear around each point as it was placed
    /// Can be removed if old scripts for 3point registration are removed entirely 
    /// </summary>
    /// <param name="secondObject"></param>
    static public void ToggleObject(bool secondObject)
    {
        toggleObject = secondObject;
    }
}
