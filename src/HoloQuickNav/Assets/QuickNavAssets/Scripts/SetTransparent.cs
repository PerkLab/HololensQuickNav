using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparent : MonoBehaviour {

    // Use this for initialization

    MeshRenderer rend;

    void Start() {
        rend = GetComponent<MeshRenderer>();

    }

    public void Transparent()
    {
        //rend.sharedMaterial.SetInt("_Mode", 3);
        rend.sharedMaterial.SetFloat("_Metallic", 0.35f);
    }

    public void Opaque()
    {
        //rend.sharedMaterial.SetInt("_Mode", 1);
        rend.sharedMaterial.SetFloat("_Metallic", 1.0f);
    }
	
}
