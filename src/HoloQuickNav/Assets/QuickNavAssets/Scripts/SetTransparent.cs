using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparent : MonoBehaviour {

    
    //empty mesh rendererd for materials in model
    private MeshRenderer rend0;
    private MeshRenderer rend1;
    //empty variable for current material parameter
    private float current;

    [Tooltip("Object you wish to change material on")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    void Start() {
        //assign mesh renderers from objects
        rend0 = selectedObject.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>();
        rend1 = selectedObject.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart1").GetComponent<MeshRenderer>();

    }

    void Update()
    {
        Align();
    }

    public void Align()
    {
        // poition arrows around selected object and face the user
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform);
    }

    //increase transparency by specified amount
    public void Increase(float amount)
    {
        //check the current material parameter
        current = rend0.sharedMaterial.GetFloat("_Metallic");
        //paramaeter cannot be negative, limit at 0
        if((current-amount)<0)
        {
            rend0.sharedMaterial.SetFloat("_Metallic", 0f);
            rend1.sharedMaterial.SetFloat("_Metallic", 0f);
        }
        else
        {
            rend0.sharedMaterial.SetFloat("_Metallic", (current - amount));
            rend1.sharedMaterial.SetFloat("_Metallic", (current - amount));
        }
        
    }

    //decrease transparency by specified amount
    public void Decrease(float amount)
    {
        //check the current material parameter
        current = rend0.sharedMaterial.GetFloat("_Metallic");
        //parameter cannot be greater than 1
        if ((current + amount) > 1)
        {
            rend0.sharedMaterial.SetFloat("_Metallic", 1f);
            rend1.sharedMaterial.SetFloat("_Metallic", 1f);
        }
        else
        {
            rend0.sharedMaterial.SetFloat("_Metallic", (current + amount));
            rend1.sharedMaterial.SetFloat("_Metallic", (current + amount));
        }
    }
	
}
