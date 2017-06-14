using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSphereTest : MonoBehaviour
{

    public float NoseR;
    public float NoseA;
    public float NoseS;
    public float REyeR;
    public float REyeA;
    public float REyeS;

    public GameObject sphere;

    // Use this for initialization
    void OnEnable()
    {
        //sphere.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        NoseR = NoseR * 0.001f;
        NoseA = NoseA * 0.001f;
        NoseS = NoseS * 0.001f;
        REyeR = REyeR * 0.001f;
        REyeA = REyeA * 0.001f;
        REyeS = REyeS * 0.001f;

        float radius = Vector3.Magnitude(new Vector3(REyeR - NoseR, REyeA - NoseA, REyeS - NoseS));
        sphere.transform.localScale += new Vector3(2f * radius - 1f, 2f * radius - 1f, 2f * radius - 1f);

    }
}
