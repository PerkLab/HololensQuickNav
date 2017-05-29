using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {


	
	// functions to call from voice commands
	public void Done()
    {
        SetAllInactive();
        BrainOnOff(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(false);
    }
    public void Scale() {
        SetAllInactive();
        ScaleOnOff(true);
    }
    public void RotateGazeA()
    {
        SetAllInactive();
        RotateGazeAOnOff(true);
    }
    public void RotateGazeS()
    {
        SetAllInactive();
        RotateGazeSOnOff(true);
    }
    public void RotateGazeR()
    {
        SetAllInactive();
        RotateGazeROnOff(true);
    }

    public void RotateArrowsA()
    {
        SetAllInactive();
        RotateArrowsAOnOff(true);
    }
    public void RotateArrowsS()
    {
        SetAllInactive();
        RotateArrowsSOnOff(true);
    }
    public void RotateArrowsR()
    {
        SetAllInactive();
        RotateArrowsROnOff(true);
    }

    public void ShiftArrows()
    {
        SetAllInactive();
        ShiftArrowsOnOff(true);
        GameObject.Find("MoveRightArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveLeftArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveUpArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveDownArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveForwardArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveBackArrow").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("MoveRightArrow").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
    }
    public void Align()
    {
        SetAllInactive();
        ShiftArrowsOnOff(true);
        AlignOnOff(true);
        GameObject.Find("MoveRightArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveLeftArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveUpArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveDownArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveForwardArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveBackArrow").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("MoveRightArrow").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
    }
    public void SphereRotate()
    {
        SetAllInactive();
        SphereOnOff(true);
    }

    public void SetAllInactive()
    {
        BrainOnOff(false);
        RotateArrowsAOnOff(false);
        RotateArrowsSOnOff(false);
        RotateArrowsROnOff(false);
        RotateGazeAOnOff(false);
        RotateGazeSOnOff(false);
        RotateGazeROnOff(false);
        ScaleOnOff(false);
        ShiftArrowsOnOff(false);
        AlignOnOff(false);
        SphereOnOff(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(true);
    }


    //turn visibility of components on and off
    void BrainOnOff(bool visible)
    {
        GameObject.Find("Head").transform.FindChild("Brain").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Hemmorhage").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("BurrHole").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("DrainTarget").gameObject.SetActive(visible);
    }

    void RotateGazeAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateGazeA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateGazeSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateGazeS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateGazeROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateGazeR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void RotateArrowsAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateArrowsA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateArrowsSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateArrowsS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateArrowsROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateArrowsR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void ScaleOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("ScaleArrows").gameObject.SetActive(visible);
    }

    void ShiftArrowsOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("ShiftArrows").gameObject.SetActive(visible);
    }
    void AlignOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateShiftArrows").gameObject.SetActive(visible);
    }

    void SphereOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Sphere").gameObject.SetActive(visible);
    }



}
