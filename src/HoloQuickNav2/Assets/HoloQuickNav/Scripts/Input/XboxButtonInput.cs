using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;
using UnityEngine.Events;
/// <summary>
/// Attach to tools or menus, use to specify unique xbox controls and their corresponding actions in the scene
/// </summary>
public class XboxButtonInput : MonoBehaviour {

    ///<summary> Xbox input manager from HoloLensXboxControllerInput plugin </summary>
    private ControllerInput controllerInput;

    //use Unity editor to assign specific actions in the scene to each event
    public UnityEvent A;
    public UnityEvent B;
    public UnityEvent X;
    public UnityEvent Y;
    public UnityEvent RightButton;
    public UnityEvent LeftButton;
    public UnityEvent RightTrigger;
    public UnityEvent LeftTrigger;
    public UnityEvent DPadDown;
    public UnityEvent DPadUp;
    public UnityEvent DPadRight;
    public UnityEvent DPadLeft;
    public UnityEvent Menu;
    public UnityEvent View;

    // Use this for initialization
    void Start () {
        controllerInput = new ControllerInput(0, 0.19f);
    }
	
	// Update is called once per frame
	void Update () {
        controllerInput.Update();
        checkButtons();
    }

    /// <summary>
    /// Check for any pressed buttons on most recent update and invoke corresponding event
    /// </summary>
    void checkButtons()
    {
        if (controllerInput.GetButtonDown(ControllerButton.A))
            A.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.B))
            B.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.X))
            X.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.Y))
            Y.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.RightShoulder))
            RightButton.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.LeftShoulder))
            LeftButton.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.DPadDown))
            DPadDown.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.DPadUp))
            DPadUp.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.DPadLeft))
            DPadLeft.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.DPadRight))
            DPadRight.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.Menu))
            Menu.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.View))
            View.Invoke();
        else if (controllerInput.GetAxisRightTrigger() > 0)
            RightTrigger.Invoke();
        else if (controllerInput.GetAxisLeftTrigger() > 0)
            LeftTrigger.Invoke();

    }
}
