using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableRegistration : MonoBehaviour {

    [Tooltip("Material for a marker free to move")]
    public Material freeMat;
    [Tooltip("Material for a marker locked in place")]
    public Material lockedMat;
    public GameObject text;

    public UnityEvent DoneEvent;

    //variable used to select current step in registration process
    private int stepCount = 0;

    //text mesh for command instructions 
    private TextMesh CommandName;

    private void Start()
    {
        //assign text mesh
        CommandName = GameObject.Find("RegistrationText").transform.FindChild("CommandName").GetComponent<TextMesh>();

    }

    public void Begin()
    {
        //hide model, show markers, disable voice commands from main program
        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject.SetActive(true);
        GameObject.Find("InputManager").transform.FindChild("VoiceInput").gameObject.SetActive(false);

        //hide command names for model and show them for the registration
        GameObject.Find("CommandText").transform.FindChild("CommandName").gameObject.SetActive(false);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);
        //text.GetComponent<TextMesh>().text = "";
        GameObject.Find("RegistrationText").transform.FindChild("CommandName").gameObject.SetActive(true);
        GameObject.Find("RegistrationText").transform.FindChild("AirTapCommands").gameObject.SetActive(true);

        //initialize at step 0 and call first step
        stepCount = 0;
        Step();

    }

    public void Back()
    {
        //decrease stepcount by 2 to account for stepCount++ in actual step function
        stepCount = stepCount - 2;
        if(stepCount < 0)
        {
            Done();
        }
        else if(stepCount == 1 || stepCount ==3)
        {
            //if the last step was a depth tool, go back to the move tool before it
            Back();
        }
        else
        {
            Step();
        }
        
    }

    public void Step () {

        //hide model and show markers
        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(false);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject.SetActive(true);

        if (stepCount==0) //move nose marker >> move with head
        {
            stepCount++;
            //move marker infront of user
            Vector3 cursorPos = GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").transform.position;
            GameObject.Find("3PointRegistration").transform.FindChild("Frame").transform.position = new Vector3(cursorPos.x, cursorPos.y, cursorPos.z);
            //enable Nose movement, start with MoveWithHead
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/MoveWithHead").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/Depth").gameObject.SetActive(false);

            //disable other commands
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            //hide all markers except the nose
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false);

            //update text
            CommandName.text = "Nose > Move";

        }
        else if(stepCount==1)  //move nose marker >> depth
        {
            stepCount++;
            //disable MoveWithHead, enable Depth
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/MoveWithHead").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Nose/Depth").gameObject.SetActive(true);

            //disable other commands
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            //hide all markers except the nose
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false); 

            //update text
            CommandName.text = "Nose > Depth";
        }
        else if(stepCount==2) //move right eye marker >> move with head
        {
            stepCount++;
            //move RightEye marker to a location in front of the user
            Vector3 noseMarker = GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").transform.position;
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye/Marker").transform.position = new Vector3(noseMarker.x, noseMarker.y, noseMarker.z);
            //enable move command for RightEye
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye/MoveWithHead").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye/Depth").gameObject.SetActive(false);

            //disable Nose, LeftEye commands
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            //hide all markers except the nose, leave right eye hidden for now
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false);

            //centre command name text around marker
            CommandText.ToggleObject(true);

            //update text
            CommandName.text = "Right Eye > Move";
        }
        else if(stepCount==3)  //move right eye marker >> depth
        {
            stepCount++;     
            //enable depth command for RightEye
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye/MoveWithHead").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye/Depth").gameObject.SetActive(true);

            //disable Nose, LeftEye commands
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(false);

            //hide all markers except the nose, leave right eye hidden for now
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(false);

            //change material of nose marker to show it locked in place 
            // GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").gameObject.GetComponent<Renderer>().material = lockedMat;

            //update text
            CommandName.text = "Right Eye > Depth";
        }

        else if(stepCount==4) //move left eye marker
        {
            //update position after step 3 to get proper alignment and spacing between nose and right eye markers
            Vector3 nosePosition = GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").gameObject.transform.position;
            Vector3 REyePosition = GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").gameObject.transform.position;
            Vector3 markerPosition = GameObject.Find("3PointRegistration").transform.FindChild("RightEye/Marker").gameObject.transform.position;
            Vector3 oldDirection = new Vector3(REyePosition.x - nosePosition.x, REyePosition.y - nosePosition.y, REyePosition.z - nosePosition.z);
            Vector3 newDirection = new Vector3(markerPosition.x - nosePosition.x, markerPosition.y - nosePosition.y, markerPosition.z - nosePosition.z);
            //rotate frame to new position
            Quaternion q = Quaternion.FromToRotation(oldDirection, newDirection);
            GameObject frame = GameObject.Find("3PointRegistration").transform.FindChild("Frame").gameObject;
            frame.transform.rotation = q * frame.transform.rotation;
            //shift frame to center between the two markers chosen by the user
            REyePosition = GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").gameObject.transform.position;
            Vector3 distance = new Vector3(REyePosition.x - markerPosition.x, REyePosition.y - markerPosition.y, REyePosition.z - markerPosition.z);
            float distanceMagnitude = Vector3.Magnitude(distance);
            frame.transform.position = frame.transform.position + (distanceMagnitude / 2) * newDirection.normalized;

            //begin next step

            stepCount++;
            //disable Nose, RightEye commands, enable LeftEye command
            GameObject.Find("3PointRegistration").transform.FindChild("Nose").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject.SetActive(false);
            GameObject.Find("3PointRegistration").transform.FindChild("LeftEye").gameObject.SetActive(true);

            //show all markers
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye").gameObject.SetActive(true);
            GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Axis").gameObject.SetActive(true);

            //centre command name text around nose marker
            CommandText.ToggleObject(false);

            //update text
            CommandName.text = "Left Eye";


        }
        else if(stepCount==5) //reached the last step, finish registration
        {
            Done();
        }
	}

    public void Done()
    {
        //reset stepCount for future use
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
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").GetComponent<TextMesh>().gameObject.SetActive(true);
        GameObject.Find("RegistrationText").transform.FindChild("CommandName").gameObject.SetActive(false);
        GameObject.Find("RegistrationText").transform.FindChild("AirTapCommands").gameObject.SetActive(false);

        //turn voice commands from main program back on
        GameObject.Find("InputManager").transform.FindChild("VoiceInput").gameObject.SetActive(true);

        DoneEvent.Invoke();
    }
	
    
}
