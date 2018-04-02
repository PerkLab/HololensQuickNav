using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;

public class ShiftWithXbox : MonoBehaviour
{

    public GameObject cam;
    public GameObject selectedObject;
    private ControllerInput controllerInput;

    private float shiftSpeed = 0.001f;

    // Use this for initialization
    void Start()
    {
        controllerInput = new ControllerInput(0, 0.19f);
        
    }

    // Update is called once per frame
    void Update()
    {
        controllerInput.Update();
        Shift();
        
    }

    private void Shift()
    {
       
        float shiftAmountX = controllerInput.GetAxisRightThumbstickX();
        float shiftAmountY = controllerInput.GetAxisRightThumbstickY();
        float shiftAmountZ = controllerInput.GetAxisLeftThumbstickY();
        Vector3 axisOfMovement;

        //check for axis with greatest value to determine the user's selection
        if (Mathf.Abs(shiftAmountX)  > Mathf.Abs(shiftAmountY) && Mathf.Abs(shiftAmountX) > Mathf.Abs(shiftAmountZ))
        {
            //translate along X axis relative to user (left/right)
            //take cross product of Y axis and vector between user and selectedObject
            axisOfMovement = Vector3.Normalize(Vector3.Cross(cam.transform.position - selectedObject.transform.position, new Vector3(0f, 1f, 0f)));
            selectedObject.transform.position += shiftAmountX * axisOfMovement*shiftSpeed;
            
        }
        else if(Mathf.Abs(shiftAmountY) > Mathf.Abs(shiftAmountX) && Mathf.Abs(shiftAmountY) > Mathf.Abs(shiftAmountZ))
        {
            //translate along Y axis relative to user (up/down)
            axisOfMovement = new Vector3(0f,1f,0f);
            selectedObject.transform.position += shiftAmountY * axisOfMovement *shiftSpeed;
            
        }
        else if(Mathf.Abs(shiftAmountZ) > Mathf.Abs(shiftAmountY) && Mathf.Abs(shiftAmountZ) > Mathf.Abs(shiftAmountX))
        {
            //translate along Z axis relative to user (forward/backward)
            //use vector between user and selectedObject
            axisOfMovement = Vector3.Normalize(cam.transform.position - selectedObject.transform.position);
            selectedObject.transform.position += (-shiftAmountZ) * axisOfMovement* shiftSpeed;
            
        }
        
    }
}
