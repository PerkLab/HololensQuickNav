using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRegistration : MonoBehaviour {

    public Material freeMat;
    public Material lockedMat;

    private int stepCount = 0;

    private TextMesh CommandName;

    private void Start()
    {
        CommandName = GameObject.Find("RegistrationText").transform.FindChild("CommandName").GetComponent<TextMesh>();

    }

    public void Begin()
    {
        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject.SetActive(true);
        GameObject.Find("InputManager").transform.FindChild("VoiceInput").gameObject.SetActive(false);

        GameObject.Find("CommandText").transform.FindChild("CommandName").gameObject.SetActive(false);
        GameObject.Find("CommandText").transform.FindChild("Help-Menu").gameObject.SetActive(false);
        //GameObject.Find("RegistrationText").transform.FindChild("CommandName").gameObject.SetActive(true);
        //GameObject.Find("RegistrationText").transform.FindChild("Help-Menu").gameObject.SetActive(true);
        stepCount = 0;
        Step();
    }

    public void Back()
    {
        stepCount = stepCount - 2;
        if(stepCount < 0)
        {
            Done();
        }
        else
        {
            Step();
        }
        
    }

    public void Step () {

        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject.SetActive(true);

        CommandName.text = "Test";

        if (stepCount==0) //move nose marker >> move with head
        {
            stepCount++;
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/MoveWithHead").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/Depth").gameObject.SetActive(false);

            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false);

            CommandName.text = "Nose > Move";

        }
        else if(stepCount==1)  //move nose marker >> depth
        {
            stepCount++;
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/MoveWithHead").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/Depth").gameObject.SetActive(true);

            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false); 

            CommandName.text = "Nose > Depth";
        }
        else if(stepCount==2) //move right eye marker
        {
            stepCount++;
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false);

           // GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").gameObject.GetComponent<Renderer>().material = lockedMat;

            CommandName.text = "Right Eye";
        }
        else if(stepCount==3) //move left eye marker
        {
            stepCount++;
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(true);

            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(true);

            CommandName.text = "Left Eye";

            //GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").gameObject.GetComponent<Renderer>().material = lockedMat;


        }
        else if(stepCount==4)
        {
            Done();
        }
	}

    public void Done()
    {
        stepCount = 0;

        //hide markers and all registration tools
        GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject.SetActive(false);

        //set material of locked markers back to free
        //GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").gameObject.GetComponent<Renderer>().material = freeMat;
        //GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").gameObject.GetComponent<Renderer>().material = freeMat;

        //align model of head with the markers
        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(true);
        GameObject Frame = GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject;
        GameObject.Find("Head").transform.position = new Vector3(Frame.transform.position.x, Frame.transform.position.y, Frame.transform.position.z);
        GameObject.Find("Head").transform.rotation = Frame.transform.rotation;

        //set the appropriate text as visible
        GameObject.Find("CommandText").transform.FindChild("CommandName").gameObject.SetActive(true);
        GameObject.Find("CommandText").transform.FindChild("Help-Menu").gameObject.SetActive(true);
        GameObject.Find("RegistrationText").transform.FindChild("CommandName").gameObject.SetActive(false);
        GameObject.Find("RegistrationText").transform.FindChild("Help-Menu").gameObject.SetActive(false);

        //turn voice input back on
        GameObject.Find("InputManager").transform.FindChild("VoiceInput").gameObject.SetActive(false);
    }
	
    
}
