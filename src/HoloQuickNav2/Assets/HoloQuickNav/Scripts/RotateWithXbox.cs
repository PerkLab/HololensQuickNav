using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;

public class RotateWithXbox : MonoBehaviour
{

    public GameObject cam;
    public GameObject selectedObject;
    private ControllerInput controllerInput;

    private GameObject TextA;
    private GameObject TextS;
    private GameObject TextR;

    private float rotateSpeed = 0.5f;
    private float rotateSpeedFine = 0.2f;
    private float rotateSpeedThreshold = 0.2f;

    // Use this for initialization
    void Start()
    {
        controllerInput = new ControllerInput(0, 0.19f);

        TextA = GameObject.Find("Model").transform.Find("AxisA/TextA").gameObject;
        TextS = GameObject.Find("Model").transform.Find("AxisS/TextS").gameObject;
        TextR = GameObject.Find("Model").transform.Find("AxisR/TextR").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //have labels face user
        TextA.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextS.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextR.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);

        controllerInput.Update();
        Rotate();
    }

    private void Rotate()
    {

        float rotateAmountR = controllerInput.GetAxisRightThumbstickX(); 
        float rotateAmountA = controllerInput.GetAxisRightThumbstickY();
        float rotateAmountS = controllerInput.GetAxisLeftThumbstickY(); 
        
        //check for axis with greatest value to determine the user's selection
        if (Mathf.Abs(rotateAmountR) > Mathf.Abs(rotateAmountA) && Mathf.Abs(rotateAmountR) > Mathf.Abs(rotateAmountS))
        {
            //rotate along X axis
            if (Mathf.Abs(rotateAmountR) > rotateSpeedThreshold)
            {
                selectedObject.transform.Rotate(rotateAmountR * Mathf.Abs(rotateAmountR) * rotateSpeed, 0f, 0f);
            }
            else
            {
                selectedObject.transform.Rotate(rotateAmountR * Mathf.Abs(rotateAmountR) * rotateSpeedFine, 0f, 0f);
            }


        }
        else if (Mathf.Abs(rotateAmountA) > Mathf.Abs(rotateAmountR) && Mathf.Abs(rotateAmountA) > Mathf.Abs(rotateAmountS))
        {
            //rotate along Y axis 
            if (Mathf.Abs(rotateAmountA) > rotateSpeedThreshold)
            {
                selectedObject.transform.Rotate(0f, rotateAmountA * Mathf.Abs(rotateAmountA) * rotateSpeed, 0f);
            }
            else
            {
                selectedObject.transform.Rotate(0f, rotateAmountA * Mathf.Abs(rotateAmountA) * rotateSpeedFine, 0f);
            }

        }
        else if (Mathf.Abs(rotateAmountS) > Mathf.Abs(rotateAmountA) && Mathf.Abs(rotateAmountS) > Mathf.Abs(rotateAmountR))
        {
            //rotate along Z axis 
            if (Mathf.Abs(rotateAmountS) > rotateSpeedThreshold)
            {
                selectedObject.transform.Rotate(0f, 0f, rotateAmountS * Mathf.Abs(rotateAmountS) * rotateSpeed);
            }
            else
            {
                selectedObject.transform.Rotate(0f, 0f, rotateAmountS * Mathf.Abs(rotateAmountS) * rotateSpeedFine);
            }

        }

    }
}
