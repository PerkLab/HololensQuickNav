using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;

/// <summary>
/// Use to rotate selected object in space using Xbox controller. Unless otherwise specified in unity editor, selected object is patient model.
/// </summary>
public class RotateWithXbox : MonoBehaviour
{
    ///<summary> Hololens camera in the scene </summary>
    [Tooltip("HoloLens camera")]
    public GameObject cam;

    ///<summary> Object to rotate in space </summary>
    [Tooltip("If null, default object is WorldAnchor/Model.")]
    public GameObject selectedObject;

    ///<summary> Xbox input manager from HoloLensXboxControllerInput plugin </summary>
    private ControllerInput controllerInput;

    ///<summary> Text mesh displayed to label the A axis </summary>
    private GameObject TextA;
    ///<summary> Text mesh displayed to label the S axis </summary>
    private GameObject TextS;
    ///<summary> Text mesh displayed to label the R axis </summary>
    private GameObject TextR;
    
    /// <summary> Faster speed to rotate model quickly into position </summary>
    private float rotateSpeed = 0.5f;
    /// <summary> Slower speed to rotate model with higher accuracy for fine adjustments </summary>
    private float rotateSpeedFine = 0.2f;
    /// <summary> Xbox joystick position threshold above which the speed switches from fine adjustment to fast movement </summary>
    private float rotateSpeedThreshold = 0.2f;

    /// <summary>
    /// Initialize tool and selected model
    /// </summary>
    void Start()
    {
        if (selectedObject == null)
        {
            selectedObject = GameObject.Find("WorldAnchor/Model");
        }

        controllerInput = new ControllerInput(0, 0.19f);

        TextA = GameObject.Find("WorldAnchor/Model").transform.Find("AxisA/TextA").gameObject;
        TextS = GameObject.Find("WorldAnchor/Model").transform.Find("AxisS/TextS").gameObject;
        TextR = GameObject.Find("WorldAnchor/Model").transform.Find("AxisR/TextR").gameObject;

    }

    /// <summary>
    /// Check status of xbox controller every frame and respond appropriately to user input. Update positions of axis labels
    /// </summary>
    void Update()
    {
        //have labels face user
        TextA.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextS.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextR.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);

        controllerInput.Update();
        Rotate();
    }

    /// <summary>
    /// Rotate selected object based on user input
    /// </summary>
    private void Rotate()
    {
        //axis of movement assigned to joysticks arbitrarily after testing
        float rotateAmountR = controllerInput.GetAxisRightThumbstickX(); 
        float rotateAmountA = controllerInput.GetAxisRightThumbstickY();
        float rotateAmountS = controllerInput.GetAxisLeftThumbstickY();

        //check for axis of movement with largest xbox joystick value
        if (Mathf.Abs(rotateAmountR) > Mathf.Abs(rotateAmountA) && Mathf.Abs(rotateAmountR) > Mathf.Abs(rotateAmountS))
        {
            //rotate along axis by the amount specified by xbox input and the rotate speed at that input
            //square rotation amount with absolute value to maintain direction and apply exponential increase in amount rotated
            if (Mathf.Abs(rotateAmountR) > rotateSpeedThreshold)
            {
                //negative directions used for more intuitive user input
                selectedObject.transform.Rotate(-rotateAmountR * Mathf.Abs(rotateAmountR) * rotateSpeed, 0f, 0f);
            }
            else
            {
                selectedObject.transform.Rotate(-rotateAmountR * Mathf.Abs(rotateAmountR) * rotateSpeedFine, 0f, 0f);
            }


        }
        else if (Mathf.Abs(rotateAmountA) > Mathf.Abs(rotateAmountR) && Mathf.Abs(rotateAmountA) > Mathf.Abs(rotateAmountS))
        {
            //rotate along axis by the amount specified by xbox input and the rotate speed at that input
            //square rotation amount with absolute value to maintain direction and apply exponential increase in amount rotated
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
            //rotate along axis by the amount specified by xbox input and the rotate speed at that input
            //square rotation amount with absolute value to maintain direction and apply exponential increase in amount rotated
            if (Mathf.Abs(rotateAmountS) > rotateSpeedThreshold)
            {
                //negative directions used for more intuitive user input
                selectedObject.transform.Rotate(0f, 0f, -rotateAmountS * Mathf.Abs(rotateAmountS) * rotateSpeed);
            }
            else
            {
                selectedObject.transform.Rotate(0f, 0f, -rotateAmountS * Mathf.Abs(rotateAmountS) * rotateSpeedFine);
            }

        }

    }
}
