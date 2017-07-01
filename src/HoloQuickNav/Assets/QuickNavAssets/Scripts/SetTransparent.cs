using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransparent : MonoBehaviour {

    
    //empty mesh rendererd for materials in model
    private MeshRenderer[] Renderers;
    //empty variable for current material parameter
    private float current;

    [Tooltip("Object you wish to change material on")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    void OnEnable() {

        Renderers = new MeshRenderer[0]; //empty array initially 
        
        //assign mesh renderers from objects
        GameObject Skin = GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").gameObject;
        if (Skin.transform.childCount > 0) //mesh has multiple sections
        {
            Renderers = Skin.transform.GetComponentsInChildren<MeshRenderer>();
        }
        else //mesh is only one part
        {
            Renderers[0] = Skin.GetComponent<MeshRenderer>();
        }

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
        current = Renderers[0].sharedMaterial.GetFloat("_Metallic");
        //paramaeter cannot be negative, limit at 0
        if((current-amount)<0)
        {
            foreach(MeshRenderer rend in Renderers)
            {
                rend.sharedMaterial.SetFloat("_Metallic", 0f);
            }
        }
        else
        {
            foreach (MeshRenderer rend in Renderers)
            {
                rend.sharedMaterial.SetFloat("_Metallic", (current - amount));
            } 
        }
        
    }

    //decrease transparency by specified amount
    public void Decrease(float amount)
    {
        //check the current material parameter
        current = Renderers[0].sharedMaterial.GetFloat("_Metallic");
        //parameter cannot be greater than 1
        if ((current + amount) > 1)
        {
            foreach (MeshRenderer rend in Renderers)
            {
                rend.sharedMaterial.SetFloat("_Metallic", 1f);
            }
        }
        else
        {
            foreach (MeshRenderer rend in Renderers)
            {
                rend.sharedMaterial.SetFloat("_Metallic", (current + amount));
            }
        }
    }
	
}
