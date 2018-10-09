using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attach to menus and help windows to ensure they are positioned properly infront of the user
/// </summary>
public class MenuBehaviour : MonoBehaviour {

    [Tooltip("Hololens camera")]
    public GameObject cam;

    [Tooltip("Help windows open to the user's right")]
    /// <summary> Select if help window, positions menu to the user's right </summary>
    public bool HelpWindow;
    [Tooltip("Menus open directly in front of the user")]
    /// <summary> Select if help window, positions menu to the user's right </summary>
    public bool Menu;


	void OnEnable () {

        if (Menu) //place menu 1.5 meters infront of user
        {
            gameObject.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        }
        else if(HelpWindow) //place help window infront of user but slightly to the side so as to not cover model
        {
            Vector3 direction = Quaternion.Euler(0f, 20f, 0f) * cam.transform.forward;
            gameObject.transform.position = cam.transform.position + direction * 1.5f;
        }
        //turn window to face user
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
	}
	
	void Update () {
        //always have window face user as they move around
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }

}
