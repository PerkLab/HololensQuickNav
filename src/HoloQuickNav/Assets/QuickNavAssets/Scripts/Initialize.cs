using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {
    // Use this for initialization

    public GameObject axis;

	void OnEnable () {
        GameObject.Find("Head").transform.FindChild("Model").transform.position = new Vector3(-LocationValues.NoseR, -LocationValues.NoseA + 0.1f, -LocationValues.NoseS + 1.5f);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame").transform.position = new Vector3(-LocationValues.NoseR, -LocationValues.NoseA + 0.1f, -LocationValues.NoseS + 1.5f);
        AlignAxis();
        ScaleSphere();
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/Nose/Marker").transform.position += new Vector3(LocationValues.NoseR, LocationValues.NoseA, LocationValues.NoseS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightEye/Marker").transform.position += new Vector3(LocationValues.REyeR, LocationValues.REyeA, LocationValues.REyeS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/LeftEye/Marker").transform.position += new Vector3(LocationValues.LEyeR, LocationValues.LEyeA, LocationValues.LEyeS);
    }

    void AlignAxis()
    {
        Vector3 direction = new Vector3(LocationValues.REyeR - LocationValues.NoseR, LocationValues.REyeA - LocationValues.NoseA, LocationValues.REyeS - LocationValues.NoseS);
        float distance = direction.magnitude;
        Quaternion q = Quaternion.FromToRotation(axis.transform.up, direction);
        axis.transform.rotation = q * axis.transform.rotation;
        axis.transform.position = new Vector3(0f,0.1f,1.5f) + (distance/2) * direction.normalized;

        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/NoseMarker").transform.position += new Vector3(LocationValues.NoseR, LocationValues.NoseA, LocationValues.NoseS);
        GameObject.Find("3PointRegistration").transform.FindChild("Frame/SubFrame/RightMarker").transform.position += new Vector3(LocationValues.REyeR, LocationValues.REyeA, LocationValues.REyeS);

    }

    void ScaleSphere()
    {
        float radius = Vector3.Magnitude(new Vector3(LocationValues.REyeR - LocationValues.NoseR, LocationValues.REyeA - LocationValues.NoseA, LocationValues.REyeS - LocationValues.NoseS));
        GameObject sphere = GameObject.Find("3PointRegistration").transform.FindChild("RightEye").gameObject;
        sphere.transform.localScale += new Vector3(2f * radius - 1f, 2f * radius - 1f, 2f * radius - 1f);
    }
	
}
