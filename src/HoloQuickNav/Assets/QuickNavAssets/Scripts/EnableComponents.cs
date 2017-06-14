using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    private GameObject shiftArrows;
    private bool headOnOff;
    private float skinVisibility;

    private bool SphereOn = false;
    private float DelayTimer = 0;
    private float WaitTime = 1;

    private TextMesh CommandName;

    private void Start()
    {
        CommandName = GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>();
        
    }

    //if sphere visible, update every frame
    private void Update()
    {
        if(SphereOn)
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
    
    // functions to call from voice commands

	public void Done()
    {
        SetAllInactive();
        DoneOnOff(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(false);

        CommandName.text = "Done > Brain:Off > Head:On";
        
    }
    public void Brain()
    {
        //if visible, turn off
        //if not visible, turn on
        if(GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf)
        {
            BrainOnOff(false);
            if(headOnOff)
            { CommandName.text = "Done > Brain:Off > Head:On";  } 
            else if(!headOnOff)
            { CommandName.text = "Done > Brain:Off > Head:Off"; }
        }
        else
        {
            BrainOnOff(true);
            if (headOnOff)
            { CommandName.text = "Done > Brain:On > Head:On"; }
            else if (!headOnOff)
            { CommandName.text = "Done > Brain:On > Head:Off"; }
        }
    }
    public void Head()
    {
        //if visible, turn off
        //if not visible, turn on
        if(headOnOff)
        {
            HeadOnOff(false);
            if (GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf)
            { CommandName.text = "Done > Brain:On > Head:Off"; }
            else
            { CommandName.text = "Done > Brain:Off > Head:Off"; }
        }
        else
        {
            HeadOnOff(true);
            if (GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf)
            { CommandName.text = "Done > Brain:On > Head:On"; }
            else
            { CommandName.text = "Done > Brain:Off > Head:On"; }
        }
    }
    public void ResetModel()
    {
        SetAllInactive();
        ResetOnOff(true);
        CommandName.text = "Reset";
    }

    public void Scale(int option)
    {
        SetAllInactive();
        ScaleOnOff(true);
        if(option == 0)
        {
            ScaleWithHeadOnOff(true);
            CommandName.text = "Scale > Head";
        }
        else
        {
            ScaleArrowsOnOff(true);
            CommandName.text = "Scale > Gaze";
        }
    }

    public void RotateA(int option)
    {
        SetAllInactive();
        RotateAOnOff(true);
        if (option == 0)
        {
            RotateGazeAOnOff(true);
            CommandName.text = "Rotate A > Gaze";
        }
        else if (option ==1)
        {
            RotateArrowsAOnOff(true);
            CommandName.text = "Rotate A > Tap";
        }
        else
        {
            RotateHeadAOnOff(true);
            CommandName.text = "Rotate A > Head";
        }
    }
    public void RotateS(int option)
    {
        SetAllInactive();
        RotateSOnOff(true);
        if (option == 0)
        {
            RotateGazeSOnOff(true);
            CommandName.text = "Rotate S > Gaze";
        }
        else if (option == 1)
        {
            RotateArrowsSOnOff(true);
            CommandName.text = "Rotate S > Tap";
        }
        else
        {
            RotateHeadSOnOff(true);
            CommandName.text = "Rotate S > Head";
        }
    }
    public void RotateR(int option)
    {
        SetAllInactive();
        RotateROnOff(true);
        if (option == 0)
        {
            RotateGazeROnOff(true);
            CommandName.text = "Rotate R > Gaze";
        }
        else if (option == 1)
        {
            RotateArrowsROnOff(true);
            CommandName.text = "Rotate R > Tap";
        }
        else
        {
            RotateHeadROnOff(true);
            CommandName.text = "Rotate R > Head";
        }
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
    public void ShiftWithHead()
    {
        SetAllInactive();
        ShiftWithHeadOnOff(true);
        //turn on visiblity of all arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);

        CommandName.text = "Shift";
    }

    public void SphereRotate()
    {
        SetAllInactive();
        SphereOnOff(true);
        SphereOn = true;
        CommandName.text = "Rotate Free > Pause";
    }
    public void Orientation()
    {
        SetAllInactive();
        OrientationOnOff(true);
        CommandName.text = "Orientation";
    }
    public void Transparent()
    {
        SetAllInactive();
        TransparentOnOff(true);
        CommandName.text = "Transparent";
    }
    public void MoveWithHead()
    {
        SetAllInactive();
        MoveWithHeadOnOff(true);
        CommandName.text = "Move";
    }
    public void Depth()
    {
        SetAllInactive();
        DepthOnOff(true);
        CommandName.text = "Depth";
    }

    public void Copy()
    {
        SetAllInactive();
        CopyOnOff(true);
        CommandName.text = "Copy";
    }

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        GameObject.Find("Head").transform.FindChild("Model").transform.FindChild("Skin_Reduced").gameObject.SetActive(false);
        CommandName.text = "";
        
    }

    //turn all tools off before switching to a new tool

    public void SetAllInactive()
    {
        
        //rotate tools
        RotateAOnOff(false);
        RotateSOnOff(false);
        RotateROnOff(false);
        SphereOnOff(false);
        (GameObject.Find("Controls").transform.FindChild("Sphere").GetComponent("Interactive") as MonoBehaviour).enabled = false;

        //shift tools
        ShiftArrowsOnOff(false);
        AlignOnOff(false);
        ShiftWithHeadOnOff(false);

        ScaleOnOff(false);
        OrientationOnOff(false);
        TransparentOnOff(false);
        MoveWithHeadOnOff(false);
        DepthOnOff(false);
        CopyOnOff(false);

        MenuOnOff(false);

        HeadOnOff(true);
        BrainOnOff(false);
        DoneOnOff(false);
        ResetOnOff(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(true);
    }


    //turn visibility of components on and off


    void HeadOnOff(bool visible)
    {
        float amountVisible = 0f;
        float amountSmooth = 0f;
        headOnOff = visible;

        if (visible)
        {
            amountVisible = skinVisibility;
            amountSmooth = 0.2f;
        }
        
        GameObject Head = GameObject.Find("Head");
        Head.transform.FindChild("Model/Brain_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisible);
        Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisible);
        Head.transform.FindChild("Model/Brain_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmooth);
        Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmooth);

        Head.transform.FindChild("Model/Brain_Reduced/grp1/grp1_MeshPart1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisible);
        Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisible);
        Head.transform.FindChild("Model/Brain_Reduced/grp1/grp1_MeshPart1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmooth);
        Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmooth);
    }
    void BrainOnOff(bool visible)
    {
        GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/SubduralHemmorhage").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/BurrHole").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/DrainTarget").gameObject.SetActive(visible);
    }
    void ResetOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Reset").gameObject.SetActive(visible);
    }
    void DoneOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Done/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Done").gameObject.SetActive(visible);
    }

    //Rotate Tools
    void RotateAOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("RotateA/HelpWindow").gameObject.SetActive(false);
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
        GameObject.Find("Controls").transform.FindChild("RotateS/HelpWindow").gameObject.SetActive(false);
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
        GameObject.Find("Controls").transform.FindChild("RotateR/HelpWindow").gameObject.SetActive(false);
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
    void ShiftWithHeadOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").gameObject.SetActive(visible);
    }

    //Scale Tools
    void ScaleOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Scale/HelpWindow").gameObject.SetActive(false);
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
        GameObject.Find("Controls").transform.FindChild("Sphere/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Sphere").gameObject.SetActive(visible);
    }
    void OrientationOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Orientation/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Orientation").gameObject.SetActive(visible);
    }
    void TransparentOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Transparent/HelpWindow").gameObject.SetActive(false);
        if (!visible)
        {
            GameObject Head = GameObject.Find("Head");
            skinVisibility = Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
        }
        GameObject.Find("Controls").transform.FindChild("Transparent").gameObject.SetActive(visible);
    }
    void MoveWithHeadOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("MoveWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("MoveWithHead").gameObject.SetActive(visible);
    }
    void DepthOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Depth/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Depth").gameObject.SetActive(visible);
    }
    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.FindChild("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("Buttons").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("Help-Menu").gameObject.SetActive(!visible);
    }

    void CopyOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("CopyRotation").gameObject.SetActive(visible);
    }



}
