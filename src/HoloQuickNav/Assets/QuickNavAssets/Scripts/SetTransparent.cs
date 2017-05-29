using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparent : MonoBehaviour {

    // Use this for initialization

    private MeshRenderer rend;
    private float current;
    public GameObject selectedObject;
    public GameObject cam;

    void Start() {
        rend = selectedObject.transform.FindChild("Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>();

    }

    void Update()
    {
        Align();
    }

    public void Align()
    {

        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform);
    }

    public void Increase(float amount)
    {
        current = rend.sharedMaterial.GetFloat("_Metallic");
        if((current-amount)<0)
        {
            rend.sharedMaterial.SetFloat("_Metallic", 0f);
        }
        else
        {
            rend.sharedMaterial.SetFloat("_Metallic", (current - amount));
        }
        
    }

    public void Decrease(float amount)
    {
        current = rend.sharedMaterial.GetFloat("_Metallic");
        if ((current + amount) > 1)
        {
            rend.sharedMaterial.SetFloat("_Metallic", 1f);
        }
        else
        {
            rend.sharedMaterial.SetFloat("_Metallic", (current + amount));
        }
    }
	
}
