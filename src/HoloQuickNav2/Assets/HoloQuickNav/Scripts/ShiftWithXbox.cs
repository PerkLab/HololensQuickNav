using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;

public class ShiftWithXbox : MonoBehaviour
{

    public GameObject cam;
    public GameObject selectedObject;
    private ControllerInput controllerInput;

    private float shiftSpeed = 0.005f;
    private float shiftSpeedFine = 0.002f;
    private float shiftSpeedThreshold = 0.2f;

    // Use this for initialization
    void Start()
    {
        controllerInput = new ControllerInput(0, 0.19f);
        Align();
        
    }

    // Update is called once per frame
    void Update()
    {
        controllerInput.Update();
        Shift();
        Align();

    }

    private void Shift()
    {
       
        float shiftAmountR = controllerInput.GetAxisRightThumbstickX();
        float shiftAmountA = controllerInput.GetAxisRightThumbstickY();
        float shiftAmountS = controllerInput.GetAxisLeftThumbstickY();
        Vector3 axisOfMovement;

        //check for axis with greatest value to determine the user's selection
        if (Mathf.Abs(shiftAmountR)  > Mathf.Abs(shiftAmountA) && Mathf.Abs(shiftAmountR) > Mathf.Abs(shiftAmountS))
        {
            //translate along X axis relative to user (left/right)
            //take cross product of Y axis and vector between user and selectedObject
            //axisOfMovement = Vector3.Normalize(Vector3.Cross(cam.transform.position - selectedObject.transform.position, new Vector3(0f, 1f, 0f)));
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/R").transform.position);
            if (Mathf.Abs(shiftAmountR) > shiftSpeedThreshold)
            {
                selectedObject.transform.position -= shiftAmountR * Mathf.Abs(shiftAmountR) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position -= shiftAmountR * Mathf.Abs(shiftAmountR) * axisOfMovement * shiftSpeedFine;
            }

        }
        else if(Mathf.Abs(shiftAmountA) > Mathf.Abs(shiftAmountR) && Mathf.Abs(shiftAmountA) > Mathf.Abs(shiftAmountS))
        {
            //translate along Y axis relative to user (up/down)
            //axisOfMovement = new Vector3(0f,1f,0f);
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/A").transform.position);
            if (Mathf.Abs(shiftAmountA) > shiftSpeedThreshold)
            {
                selectedObject.transform.position += -shiftAmountA * Mathf.Abs(shiftAmountA) *Mathf.Abs(shiftAmountA) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position += shiftAmountA * Mathf.Abs(shiftAmountA) * axisOfMovement * shiftSpeedFine;
            }

        }
        else if(Mathf.Abs(shiftAmountS) > Mathf.Abs(shiftAmountA) && Mathf.Abs(shiftAmountS) > Mathf.Abs(shiftAmountR))
        {
            //translate along Z axis relative to user (forward/backward)
            //use vector between user and selectedObject
            //axisOfMovement = Vector3.Normalize(new Vector3(cam.transform.position.x,0f, cam.transform.position.z) - new Vector3(selectedObject.transform.position.x, 0f, selectedObject.transform.position.z));
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/S").transform.position);
            if (Mathf.Abs(shiftAmountS) > shiftSpeedThreshold)
            {
                selectedObject.transform.position -= shiftAmountS * Mathf.Abs(shiftAmountS) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position -= shiftAmountS * Mathf.Abs(shiftAmountS) * axisOfMovement * shiftSpeedFine;
            }

        }
        
    }

    public void Align()
    {
        //position arrows around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //gameObject.transform.LookAt(selectedObject.transform);
        //gameObject.transform.rotation = selectedObject.transform.rotation;
        //gameObject.transform.Rotate(0f, -selectedObject.transform.rotation.y, 0f);
        //gameObject.transform.Rotate(-selectedObject.transform.rotation.x, 0f, -selectedObject.transform.rotation.z);

        float rotationAngle = Vector3.Angle(new Vector3(selectedObject.transform.forward.x, 0f, selectedObject.transform.forward.z),
                                                    new Vector3(gameObject.transform.forward.x, 0f, gameObject.transform.forward.z));
        if(rotationAngle > 0.1f)
        {
            gameObject.transform.Rotate(0f, rotationAngle, 0f);
        }
        

        this.transform.Find("AxisA/TextA").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        this.transform.Find("AxisS/TextS").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        this.transform.Find("AxisR/TextR").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);


        //rotate arrows around y axis so forward arrow is facing user
        //Vector3 lookPos = new Vector3(cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
        //this.transform.LookAt(lookPos);
    }
}
