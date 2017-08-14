using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine;

public class Initialize : MonoBehaviour {
    
    //axis for 3 point registration
    public GameObject axis;

	void OnEnable () {

        //align burrhole marker 
        GameObject.Find("Head").transform.FindChild("Model/BurrHoleMarker").transform.position += new Vector3(LocationValues.targetR, LocationValues.targetA, LocationValues.targetS);

        //align BB markers
        GameObject.Find("Head").transform.FindChild("Model/Targets/1BB").transform.position += new Vector3(LocationValues.BB1R, LocationValues.BB1A, LocationValues.BB1S);
        GameObject.Find("Head").transform.FindChild("Model/Targets/2BB").transform.position += new Vector3(LocationValues.BB2R, LocationValues.BB2A, LocationValues.BB2S);
        GameObject.Find("Head").transform.FindChild("Model/Targets/3BB").transform.position += new Vector3(LocationValues.BB3R, LocationValues.BB3A, LocationValues.BB3S);
        GameObject.Find("Head").transform.FindChild("Model/Targets/4BB").transform.position += new Vector3(LocationValues.BB4R, LocationValues.BB4A, LocationValues.BB4S);
        GameObject.Find("Head").transform.FindChild("Model/Targets/5BB").transform.position += new Vector3(LocationValues.BB5R, LocationValues.BB5A, LocationValues.BB5S);
        GameObject.Find("Head").transform.FindChild("Model/Targets/6BB").transform.position += new Vector3(LocationValues.BB6R, LocationValues.BB6A, LocationValues.BB6S);

        //shift 3 point registration frame and model so tip of the nose is at the origin
        GameObject.Find("Head").transform.FindChild("Model").transform.position = new Vector3(-LocationValues.NoseR, -LocationValues.NoseA + 0.1f, -LocationValues.NoseS + 1.5f);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame").transform.position = new Vector3(-LocationValues.NoseR, -LocationValues.NoseA + 0.1f, -LocationValues.NoseS + 1.5f);

        //complete setup for 3 point registration tools
        AlignAxis();
        //ScaleSphere();

        //align markers with points
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").transform.position += new Vector3(LocationValues.NoseR, LocationValues.NoseA, LocationValues.NoseS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").transform.position += new Vector3(LocationValues.REyeR, LocationValues.REyeA, LocationValues.REyeS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye/Marker").transform.position += new Vector3(LocationValues.LEyeR, LocationValues.LEyeA, LocationValues.LEyeS);

        
        //uncomment the following lines for rotate3axis simulation
        //GameObject.Find("Head").transform.position += new Vector3(0f, -0.6f, 1.5f);
        //GameObject.Find("Head").transform.position += new Vector3(0f, LocationValues.NoseA, 0f);

    }

    void AlignAxis()
    {
        //calculate vector between nose and right eye marker
        Vector3 direction = new Vector3(LocationValues.REyeR - LocationValues.NoseR, LocationValues.REyeA - LocationValues.NoseA, LocationValues.REyeS - LocationValues.NoseS);
        float distance = direction.magnitude;

        //rotate axis to align with vector between markers
        Quaternion q = Quaternion.FromToRotation(axis.transform.up, direction);
        axis.transform.rotation = q * axis.transform.rotation;
        
        //center axis between markers
        axis.transform.position = new Vector3(0f,0.1f,1.5f) + (distance/2) * direction.normalized;

        //align empty object markers to assign axis of rotation
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/NoseMarker").transform.position += new Vector3(LocationValues.NoseR, LocationValues.NoseA, LocationValues.NoseS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightMarker").transform.position += new Vector3(LocationValues.REyeR, LocationValues.REyeA, LocationValues.REyeS);

    }

    void ScaleSphere()
    {
        //determine distance between nose and right eye markers as radius for sphere
        float radius = Vector3.Magnitude(new Vector3(LocationValues.REyeR - LocationValues.NoseR, LocationValues.REyeA - LocationValues.NoseA, LocationValues.REyeS - LocationValues.NoseS));

        //update scale of sphere to match new radius
        GameObject sphere = GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject;
        
        //scale of sphere can be calculated using diameter 
        //subtract 1 to account for original scale of sphere in Unity
        sphere.transform.localScale += new Vector3(2f * radius - 1f, 2f * radius - 1f, 2f * radius - 1f);
    }
	
}
