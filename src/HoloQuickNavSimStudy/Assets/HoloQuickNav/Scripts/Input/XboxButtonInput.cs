using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;
using UnityEngine.Events;

public class XboxButtonInput : MonoBehaviour {

    private ControllerInput controllerInput;
    public UnityEvent A;
    public UnityEvent B;
    public UnityEvent X;
    public UnityEvent Y;
    public UnityEvent RightShoulder;
    public UnityEvent LeftShoulder;
    public UnityEvent DPadDown;
    public UnityEvent DPadUp;
    public UnityEvent DPadRight;
    public UnityEvent DPadLeft;
    public UnityEvent Menu;
    public UnityEvent View;

    //public UnityEvent RightTrigger;
    //public UnityEvent LeftTrigger;


    // Use this for initialization
    void Start () {
        controllerInput = new ControllerInput(0, 0.19f);
    }
	
	// Update is called once per frame
	void Update () {
        controllerInput.Update();
        checkButtons();
    }

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
            RightShoulder.Invoke();
        else if (controllerInput.GetButtonDown(ControllerButton.LeftShoulder))
            LeftShoulder.Invoke();
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


    }
}
