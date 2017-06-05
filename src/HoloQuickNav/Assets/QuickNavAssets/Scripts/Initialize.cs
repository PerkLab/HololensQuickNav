using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

    public float Rvalue;
    public float Svalue;
	
    // Use this for initialization
	void Start () {
        gameObject.transform.FindChild("Skin_Reduced").transform.position = new Vector3(Rvalue * 0.001f, 0.1f, -Svalue * 0.001f + 1.5f);
        gameObject.transform.FindChild("Brain_Reduced").transform.position = new Vector3(Rvalue * 0.001f, 0.1f, -Svalue * 0.001f + 1.5f);
        gameObject.transform.FindChild("SubduralHemmorhage").transform.position = new Vector3(Rvalue * 0.001f, 0.1f, -Svalue * 0.001f + 1.5f);
        gameObject.transform.FindChild("DrainTarget").transform.position = new Vector3(Rvalue * 0.001f, 0.1f, -Svalue * 0.001f + 1.5f);
        gameObject.transform.FindChild("BurrHole").transform.position = new Vector3(Rvalue * 0.001f, 0.1f, -Svalue * 0.001f + 1.5f);
    }
	
}
