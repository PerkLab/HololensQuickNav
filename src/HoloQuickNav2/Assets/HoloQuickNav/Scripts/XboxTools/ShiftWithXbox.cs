using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;

/// <summary>
/// Use to translate selected object in space using Xbox controller. Unless otherwise specified in unity editor, selected object is patient model.
/// </summary>
public class ShiftWithXbox : MonoBehaviour
{
    /// <summary> Hololens camera in the scene </summary>
    [Tooltip("HoloLens camera")]
    public GameObject cam;

    /// <summary> Object to translate in space </summary>
    [Tooltip("If null, default object is WorldAnchor/Model.")]
    public GameObject selectedObject;

    /// <summary> Xbox input manager from HoloLensXboxControllerInput plugin </summary>
    private ControllerInput controllerInput;

    /// <summary> Faster speed to translate model quickly into position </summary>
    private float shiftSpeed = 0.005f;
    /// <summary> Slower speed to translate model with higher accuracy for fine adjustments </summary>
    private float shiftSpeedFine = 0.002f;
    /// <summary> Xbox joystick position threshold above which the speed switches from fine adjustment to fast movement </summary>
    private float shiftSpeedThreshold = 0.2f;

    /// <summary>
    /// Initialize tool and selected model
    /// </summary>
    void Start()
    {
        if(selectedObject == null)
        {
            selectedObject = GameObject.Find("WorldAnchor/Model");
        }
        controllerInput = new ControllerInput(0, 0.19f);
        Align();
    }

    /// <summary>
    /// Check status of xbox controller every frame and respond appropriately to user input
    /// </summary>
    void Update()
    {
        controllerInput.Update();
        Shift();
        Align();
    }

    /// <summary>
    /// Translate selected object based on user input 
    /// </summary>
    private void Shift()
    {
        //axis of movement assigned to joysticks arbitrarily after testing
        float shiftAmountR = controllerInput.GetAxisRightThumbstickX();
        float shiftAmountA = controllerInput.GetAxisRightThumbstickY();
        float shiftAmountS = controllerInput.GetAxisLeftThumbstickY();
        //axis of movement must be calculated based on position of displayed axes and model 
        Vector3 axisOfMovement;

        //check for axis of movement with largest xbox joystick value
        if (Mathf.Abs(shiftAmountR)  > Mathf.Abs(shiftAmountA) && Mathf.Abs(shiftAmountR) > Mathf.Abs(shiftAmountS))
        {
            //to calculate axis of movement use origin of axes and location of physical game object located along the R axis game object
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/R").transform.position);
            
            //translate along axis of movement by the amount specified by xbox input and the shift speed at that input
            //square shift amount with absolute value to maintain direction and apply exponential increase in distance translated
            if (Mathf.Abs(shiftAmountR) > shiftSpeedThreshold)
            {
                selectedObject.transform.position += shiftAmountR * Mathf.Abs(shiftAmountR) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position += shiftAmountR * Mathf.Abs(shiftAmountR) * axisOfMovement * shiftSpeedFine;
            }
        }
        else if(Mathf.Abs(shiftAmountA) > Mathf.Abs(shiftAmountR) && Mathf.Abs(shiftAmountA) > Mathf.Abs(shiftAmountS))
        {
            //to calculate axis of movement use origin of axes and location of physical game object located along the R axis game object
            //square shift amount with absolute value to maintain direction and apply exponential increase in distance translated
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/A").transform.position);
            if (Mathf.Abs(shiftAmountA) > shiftSpeedThreshold)
            {
                //negative directions used for more intuitive user input
                selectedObject.transform.position += -shiftAmountA * Mathf.Abs(shiftAmountA) *Mathf.Abs(shiftAmountA) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position += shiftAmountA * Mathf.Abs(shiftAmountA) * axisOfMovement * shiftSpeedFine;
            }

        }
        else if(Mathf.Abs(shiftAmountS) > Mathf.Abs(shiftAmountA) && Mathf.Abs(shiftAmountS) > Mathf.Abs(shiftAmountR))
        {
            //to calculate axis of movement use origin of axes and location of physical game object located along the R axis game object
            //square shift amount with absolute value to maintain direction and apply exponential increase in distance translated
            axisOfMovement = Vector3.Normalize(selectedObject.transform.position - this.transform.Find("AxisPoints/S").transform.position);
            if (Mathf.Abs(shiftAmountS) > shiftSpeedThreshold)
            {
                selectedObject.transform.position += shiftAmountS * Mathf.Abs(shiftAmountS) * axisOfMovement * shiftSpeed;
            }
            else
            {
                selectedObject.transform.position += shiftAmountS * Mathf.Abs(shiftAmountS) * axisOfMovement * shiftSpeedFine;
            }

        }
        
    }

    /// <summary>
    /// Position axes at origin of model (tip of patient's nose) and rotate to correct orientation 
    /// </summary>
    public void Align()
    {
        //position arrows around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        //keep axes level such that A axis is vertical and S and R axes are in horizontal plane
        //calculate amount to rotate about A axis such that S and R are aligned in horizontal plane with the S and R axis of the model
        float rotationAngle = Vector3.Angle(new Vector3(selectedObject.transform.forward.x, 0f, selectedObject.transform.forward.z),
                                                    new Vector3(gameObject.transform.forward.x, 0f, gameObject.transform.forward.z));
        
        //only rotate if noticably needed, otherwise the rotation is glitchy and appears to continuously rotate
        if (rotationAngle > 0.1f)
        {
            gameObject.transform.Rotate(0f, rotationAngle, 0f);
        }
        

        this.transform.Find("AxisA/TextA").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        this.transform.Find("AxisS/TextS").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        this.transform.Find("AxisR/TextR").transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        
    }
}
