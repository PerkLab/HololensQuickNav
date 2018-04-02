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

    private float rotateSpeed = 0.1f;

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

        float rotateAmountX = controllerInput.GetAxisRightThumbstickY(); 
        float rotateAmountY = controllerInput.GetAxisLeftThumbstickX();
        float rotateAmountZ = controllerInput.GetAxisRightThumbstickX(); 
        
        //check for axis with greatest value to determine the user's selection
        if (Mathf.Abs(rotateAmountX) > Mathf.Abs(rotateAmountY) && Mathf.Abs(rotateAmountX) > Mathf.Abs(rotateAmountZ))
        {
            //rotate along X axis
            selectedObject.transform.Rotate(rotateAmountX * rotateSpeed,0f,0f);
            
        }
        else if (Mathf.Abs(rotateAmountY) > Mathf.Abs(rotateAmountX) && Mathf.Abs(rotateAmountY) > Mathf.Abs(rotateAmountZ))
        {
            //rotate along Y axis 
            selectedObject.transform.Rotate(0f, rotateAmountY * rotateSpeed, 0f);

        }
        else if (Mathf.Abs(rotateAmountZ) > Mathf.Abs(rotateAmountY) && Mathf.Abs(rotateAmountZ) > Mathf.Abs(rotateAmountX))
        {
            //rotate along Z axis 
            selectedObject.transform.Rotate(0f, 0f, -rotateAmountZ * rotateSpeed);

        }

    }
}
