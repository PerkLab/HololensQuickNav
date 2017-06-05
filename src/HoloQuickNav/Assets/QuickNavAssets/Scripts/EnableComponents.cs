﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    private bool SphereOn = false;
    private float DelayTimer = 0;
    private float WaitTime = 1;
    private GameObject shiftArrows;
	
	// functions to call from voice commands
	public void Done()
    {
        SetAllInactive();
        DoneOnOff(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(false);
    }
    public void Brain()
    {
        //if visible, turn off
        //if not visible, turn on
        if(GameObject.Find("Head").transform.FindChild("Brain_Reduced").gameObject.activeSelf)
        {
            BrainOnOff(false);
        }
        else
        {
            BrainOnOff(true);
        }
    }
    public void ResetModel()
    {
        SetAllInactive();
        ResetOnOff(true);

    }

    public void Scale(int option)
    {
        SetAllInactive();
        ScaleOnOff(true);
        if(option == 0)
        { ScaleWithHeadOnOff(true); }
        else
        { ScaleArrowsOnOff(true); }
    }

    public void RotateA(int option)
    {
        SetAllInactive();
        RotateAOnOff(true);
        if (option == 0)
        { RotateGazeAOnOff(true); }
        else if (option ==1)
        { RotateArrowsAOnOff(true); }
        else
        { RotateHeadAOnOff(true); }
    }
    public void RotateS(int option)
    {
        SetAllInactive();
        RotateSOnOff(true);
        if (option == 0)
        { RotateGazeSOnOff(true); }
        else if (option == 1)
        { RotateArrowsSOnOff(true); }
        else
        { RotateHeadSOnOff(true); }
    }
    public void RotateR(int option)
    {
        SetAllInactive();
        RotateROnOff(true);
        if (option == 0)
        { RotateGazeROnOff(true); }
        else if (option == 1)
        { RotateArrowsROnOff(true); }
        else
        { RotateHeadROnOff(true); }
    }

    public void ShiftArrows()
    {
        SetAllInactive();
        ShiftArrowsOnOff(true);
        EnableShiftArrows(true);
        shiftArrows = GameObject.Find("ShiftArrows");
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
    }
    public void Align()
    {
        SetAllInactive();
        ShiftArrowsOnOff(true);
        AlignOnOff(true);
        EnableShiftArrows(false);
        shiftArrows = GameObject.Find("ShiftArrows");
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
    }
    public void SphereRotate()
    {
        SetAllInactive();
        SphereOnOff(true);
        SphereOn = true;
    }
    public void Options()
    {
        SetAllInactive();
        OptionsOnOff(true);
    }
    public void Transparent()
    {
        SetAllInactive();
        TransparentOnOff(true);
    }
    public void MoveWithHead()
    {
        SetAllInactive();
        MoveWithHeadOnOff(true);
    }
    public void Depth()
    {
        SetAllInactive();
        DepthOnOff(true);
    }

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        GameObject.Find("Head").transform.FindChild("Skin_Reduced").gameObject.SetActive(false);
    }


    public void SetAllInactive()
    {
        BrainOnOff(false);
        RotateAOnOff(false);
        RotateSOnOff(false);
        RotateROnOff(false);
        ScaleOnOff(false);
        ShiftArrowsOnOff(false);
        AlignOnOff(false);
        SphereOnOff(false);
        (GameObject.Find("Controls").transform.FindChild("Sphere").GetComponent("Interactive") as MonoBehaviour).enabled = false;
        OptionsOnOff(false);
        TransparentOnOff(false);
        MoveWithHeadOnOff(false);
        DepthOnOff(false);
        MenuOnOff(false);
        DoneOnOff(false);
        ResetOnOff(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(true);
    }

    //Updates every frame
    private void Update()
    {
        // delay interactive component on Sphere Rotation
        if (SphereOn)
        {
            if(DelayTimer < WaitTime)
            {
                DelayTimer += Time.deltaTime;
            }
            else
            {
                (GameObject.Find("Controls").transform.FindChild("Sphere").GetComponent("Interactive") as MonoBehaviour).enabled = true;
                SphereOn = false;
                DelayTimer = 0;
            }
        }
    }


    //turn visibility of components on and off
    void BrainOnOff(bool visible)
    {
        GameObject.Find("Head").transform.FindChild("Brain_Reduced").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("SubduralHemmorhage").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("BurrHole").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("DrainTarget").gameObject.SetActive(visible);
    }
    void ResetOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Reset").gameObject.SetActive(visible);
    }
    void DoneOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Done").gameObject.SetActive(visible);
    }

    //Rotate Tools
    void RotateAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateA").gameObject.SetActive(visible);
        if(!visible)
        {
            RotateArrowsAOnOff(visible);
            RotateGazeAOnOff(visible);
            RotateHeadAOnOff(visible);
        }
    }
    void RotateSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateS").gameObject.SetActive(visible);
        if (!visible)
        {
            RotateArrowsSOnOff(visible);
            RotateGazeSOnOff(visible);
            RotateHeadSOnOff(visible);
        }
    }
    void RotateROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateR").gameObject.SetActive(visible);
        if (!visible)
        {
            RotateArrowsROnOff(visible);
            RotateGazeROnOff(visible);
            RotateHeadROnOff(visible);
        }
    }

    void RotateGazeAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateGazeA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateGazeSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateGazeS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateGazeROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateR").transform.FindChild("RotateGazeR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void RotateArrowsAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateArrowsA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateArrowsSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateArrowsS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateArrowsROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateR").transform.FindChild("RotateArrowsR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void RotateHeadAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateHeadA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateHeadSOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateHeadS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateHeadROnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateR").transform.FindChild("RotateHeadR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    //Shift Tools
    void ShiftArrowsOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("ShiftArrows").gameObject.SetActive(visible);
    }
    void AlignOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateShiftArrows").gameObject.SetActive(visible);
    }
    void EnableShiftArrows(bool visible)
    {
        shiftArrows = GameObject.Find("ShiftArrows");
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveLeftArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveDownArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveBackArrow").transform.FindChild("Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow").transform.FindChild("Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow").transform.FindChild("Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow").transform.FindChild("Box3").GetComponent<MeshCollider>().enabled = visible;
    }

    //Scale Tools
    void ScaleOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Scale").gameObject.SetActive(visible);
        if (!visible)
        {
            ScaleArrowsOnOff(visible);
            ScaleWithHeadOnOff(visible);
        }
    }
    void ScaleArrowsOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Scale").transform.FindChild("ScaleArrows").gameObject.SetActive(visible);
    }
    void ScaleWithHeadOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Scale").transform.FindChild("ScaleWithHead").gameObject.SetActive(visible);
    }

    void SphereOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Sphere").gameObject.SetActive(visible);
    }
    void OptionsOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Options").gameObject.SetActive(visible);
    }
    void TransparentOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Transparent").gameObject.SetActive(visible);
    }
    void MoveWithHeadOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("MoveWithHead").gameObject.SetActive(visible);
    }
    void DepthOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Depth").gameObject.SetActive(visible);
    }
    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.FindChild("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("Buttons").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Skin_Reduced").gameObject.SetActive(!visible);
    }



}