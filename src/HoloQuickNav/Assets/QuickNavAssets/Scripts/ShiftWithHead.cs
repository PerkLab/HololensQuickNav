using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftWithHead : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    public GameObject cam;

    private float currentDistance;
    private float lastDistance;
    private float amount;

    private bool rightLeft = false;
    private bool upDown = false;
    private bool forwardBack = false;

    private bool IsRunning = false;

    // Use this for initialization
    void OnEnable()
    {
        Align();
        Vector3 lookPos = new Vector3(cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
        this.transform.LookAt(lookPos);

        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
    }

    public void RightLeft()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        //select direction of movement
        rightLeft = true;
        upDown = false;
        forwardBack = false;
        IsRunning = true;
    }

    public void UpDown()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        //select direction of movement
        rightLeft = false;
        upDown = true;
        forwardBack = false;
        IsRunning = true;
    }

    public void ForwardBack()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        //select direction of movement
        rightLeft = false;
        upDown = false;
        forwardBack = true;
        IsRunning = true;
    }

    public void Pause()
    {
        IsRunning = false;
        rightLeft = false;
        upDown = false;
        forwardBack = false;

        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);

    }

    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if(IsRunning)
        {
            currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
            amount = Mathf.Abs(currentDistance - lastDistance);

            if (currentDistance > lastDistance) //User moves farther away
            {
                if (rightLeft) //move right
                {
                    Vector3 direction = Quaternion.Euler(0, 90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount;
                }
                else if (upDown) //move up
                { selectedObject.transform.position += new Vector3(0.0f, amount, 0.0f); }

                else if (forwardBack) //move forward
                {
                    Vector3 direction = Quaternion.Euler(0, 180, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount * 0.1f;
                }
            }
            else if (currentDistance < lastDistance) //user moves closer
            {
                if (rightLeft) //move left
                {
                    Vector3 direction = Quaternion.Euler(0, -90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount;
                }
                else if (upDown) //move down
                { selectedObject.transform.position -= new Vector3(0.0f, amount, 0.0f); }

                else if (forwardBack) //move back
                {
                    Vector3 direction = gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount * 0.1f;
                }
            }
            else { } //do nothing if the user hasn't moved their head

            Align();
            lastDistance = currentDistance;
        }
        
    }
}
