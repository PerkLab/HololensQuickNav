using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSphere : MonoBehaviour {

    public GameObject sphere;
    public Material lockedMat;
    public Material freeMat;

    // Use this for initialization
    void OnEnable()
    {
        float radius = Vector3.Magnitude(new Vector3(LocationValues.REyeR - LocationValues.NoseR, LocationValues.REyeA - LocationValues.NoseA, LocationValues.REyeS - LocationValues.NoseS));
        sphere.transform.localScale += new Vector3(2f * radius - 1f, 2f * radius - 1f, 2f * radius - 1f);

        //GameObject.Find("3PointRegistration").transform.FindChild("Frame/Nose/grp1").GetComponent<Renderer>().materials[0] = lockedMat;
        //GameObject.Find("3PointRegistration").transform.FindChild("Frame/RightEye/grp1").GetComponent<Renderer>().materials[0] = freeMat;

    }
}
