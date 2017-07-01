using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    [Tooltip("Hololens camera")]
    public GameObject cam;

    //developer can choose which window they're displaying
    public bool HelpWindow;
    public bool Menu;
    public bool PrimaryMenu = false;
    public bool SecondaryMenu = false;
    public bool PrimaryFixed;

    public static Vector3 menuPos;

	void OnEnable () {

        if (Menu) //place menu 1.5 meters infront of user
        {
            if(PrimaryMenu) 
            {
                if (PrimaryFixed) //don't update with users gaze, keep in same location as last secondary menu
                {
                    gameObject.transform.position = new Vector3(menuPos.x, menuPos.y, menuPos.z);
                    PrimaryFixed = false;
                }
                else //place in front of user's gaze
                {
                    gameObject.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
                    menuPos = gameObject.transform.position;
                }
            }
            else if(SecondaryMenu) //don't update with users gaze, keep in same location as primary menu
            {
                gameObject.transform.position = new Vector3(menuPos.x, menuPos.y, menuPos.z);
            }
            
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
        //always have window face user
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }

    public void Fixed()
    {
       PrimaryFixed = true;
    }
}
