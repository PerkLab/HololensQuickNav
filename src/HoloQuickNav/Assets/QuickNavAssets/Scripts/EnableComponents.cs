using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    //visibility of head and brain
    private bool headOnOff;
    //level of transparency of skin
    private float skinVisibility;

    //variables for use in RotateFree to enable script after delay
    private bool SphereOn = false;
    private float DelayTimer = 0;
    private float WaitTime = 1;

    //global variable for text to display current command for user
    private TextMesh CommandName;

    private void Start()
    {
        //assign text mesh to global variable for use when calling commands
        CommandName = GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>();

        SetAllInactive();
        Done();
        
    }

    //if sphere visible, update every frame
    private void Update()
    {
        //enable script for RotateFree after delay if the sphere is visible (command has been called)
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
    
    //
    // functions to call from voice commands
    //
	public void Done()
    {
        SetAllInactive();
        DoneOnOff(true);

        //disable cursor on user's gaze for easier visibility of model
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(false);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(false);

        //display command
        CommandName.text = "Done > Brain:Off > Head:On";
        
    }
    public void Brain()
    {
        //if visible, turn brain off
        //if not visible, turn brain on

        if(GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf) //brain on
        {
            BrainOnOff(false);
            //display proper current states of head and brain visibility 
            if(headOnOff)
            { CommandName.text = "Done > Brain:Off > Head:On";  } 
            else if(!headOnOff)
            { CommandName.text = "Done > Brain:Off > Head:Off"; }
        }
        else //brain off
        {
            BrainOnOff(true);
            //display proper current states of head and brain visibility 
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
        if(headOnOff) //head on
        {
            HeadOnOff(false);
            //display proper current states of head and brain visibility
            if (GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf)
            { CommandName.text = "Done > Brain:On > Head:Off"; }
            else
            { CommandName.text = "Done > Brain:Off > Head:Off"; }
        }
        else //head off
        {
            HeadOnOff(true);
            //display proper current states of head and brain visibility
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
        //display current command
        CommandName.text = "Reset";
    }

    public void Scale(int option)
    {
        SetAllInactive();
        ScaleOnOff(true);
        //different voice commands call different tools by selecting option
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

        //different voice commands call different tools by selecting option
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

        //different voice commands call different tools by selecting option
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

        //different voice commands call different tools by selecting option
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
        //set arrows to full visibility if previously using "Align" command
        GameObject.Find("ShiftArrows").transform.FindChild("MoveRightArrow/Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);

    }
    public void Align()
    {
        SetAllInactive();
        ShiftArrowsOnOff(true);
        AlignOnOff(true);
        //disable use of shift arrows while aligning directions
        EnableShiftArrows(false);
        //reduce visibility of arrows to show they are disabled
        GameObject.Find("ShiftArrows").transform.FindChild("MoveRightArrow/Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
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

        //display current command
        CommandName.text = "Shift";
    }

    public void SphereRotate()
    {
        SetAllInactive();
        SphereOnOff(true);
        //declare the sphere on to call delay function in Update() to enable the script
        SphereOn = true;
        //display initial command as pause, until user starts tool with voice command
        CommandName.text = "Rotate Free > Pause";
    }
    public void Orientation()
    {
        SetAllInactive();
        OrientationOnOff(true);
        //display current command
        CommandName.text = "Orientation";
    }
    public void Transparent()
    {
        SetAllInactive();
        TransparentOnOff(true);
        //display current command
        CommandName.text = "Transparent";
    }
    public void MoveWithHead()
    {
        SetAllInactive();
        MoveWithHeadOnOff(true);
        //display current command
        CommandName.text = "Move";
    }
    public void Depth()
    {
        SetAllInactive();
        DepthOnOff(true);
        //display current command
        CommandName.text = "Depth";
    }

    public void Copy()
    {
        SetAllInactive();
        CopyOnOff(true);
        //display current command
        CommandName.text = "Copy";
    }

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        //hide the model entirely from view
        GameObject.Find("Head").transform.FindChild("Model").transform.FindChild("Skin_Reduced").gameObject.SetActive(false);
        //clear current command text
        CommandName.text = "";
    }

    public void Begin()
    {
        SetAllInactive();
        DoneOnOff(false);
    }

    //
    //turn all tools off before switching to a new tool
    //
    public void SetAllInactive()
    {
        
        //rotate tools
        RotateAOnOff(false);
        RotateSOnOff(false);
        RotateROnOff(false);
        SphereOnOff(false);
        //disable RotateFree script until enabled by voice command
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

        //make cursor visible if previously using Done command, in which case it is hidden
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(true);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("Spotlight").gameObject.SetActive(true);
    }

    //
    //enable/disable use of components 
    //
    void HeadOnOff(bool visible)
    {
        //declare values for material parameters
        float amountVisible = 0f;
        float amountSmooth = 0f;
        //declare state of headOnOff
        headOnOff = visible;

        if (visible)
        {
            //if making head visible set amountVisible as skinVisility, the previous value of the material last time it was set by Transparent command
            amountVisible = skinVisibility;
            amountSmooth = 0.2f;
        }

        //set visibility of mesh compononets in Brain and Skin
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
        //toggle visiblity of brain, hemmatoma, and target locations
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
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Done/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Done").gameObject.SetActive(visible);
    }

    //Rotate Tools
    void RotateAOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("RotateA/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("RotateA").gameObject.SetActive(visible);
        //if disabling component, disable all subcomponents for next use
        if(!visible)
        {
            RotateArrowsAOnOff(visible);
            RotateGazeAOnOff(visible);
            RotateHeadAOnOff(visible);
        }
    }
    void RotateSOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("RotateS/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("RotateS").gameObject.SetActive(visible);
        //if disabling component, disable all subcomponents for next use
        if (!visible)
        {
            RotateArrowsSOnOff(visible);
            RotateGazeSOnOff(visible);
            RotateHeadSOnOff(visible);
        }
    }
    void RotateROnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("RotateR/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("RotateR").gameObject.SetActive(visible);
        //if disabling component, disable all subcomponents for next use
        if (!visible)
        {
            RotateArrowsROnOff(visible);
            RotateGazeROnOff(visible);
            RotateHeadROnOff(visible);
        }
    }

    void RotateGazeAOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateGazeA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateGazeSOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateGazeS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateGazeROnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateR").transform.FindChild("RotateGazeR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void RotateArrowsAOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateArrowsA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateArrowsSOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateArrowsS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateArrowsROnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateR").transform.FindChild("RotateArrowsR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    void RotateHeadAOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateA").transform.FindChild("RotateHeadA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisA").gameObject.SetActive(visible);
    }
    void RotateHeadSOnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
        GameObject.Find("Controls").transform.FindChild("RotateS").transform.FindChild("RotateHeadS").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("AxisS").gameObject.SetActive(visible);
    }
    void RotateHeadROnOff(bool visible)
    {
        //enable rotation tool and corresponding axis
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
        //enable/disable use of arrows but not visibility by toggling mesh collider
        GameObject shiftArrows = GameObject.Find("ShiftArrows");
        shiftArrows.transform.FindChild("MoveRightArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveRightArrow/Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveLeftArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveLeftArrow/Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveUpArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveUpArrow/Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveDownArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveDownArrow/Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveForwardArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveForwardArrow/Box3").GetComponent<MeshCollider>().enabled = visible;

        shiftArrows.transform.FindChild("MoveBackArrow/Box").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow/Box1").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow/Box2").GetComponent<MeshCollider>().enabled = visible;
        shiftArrows.transform.FindChild("MoveBackArrow/Box3").GetComponent<MeshCollider>().enabled = visible;
    }
    void ShiftWithHeadOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").gameObject.SetActive(visible);
    }

    //Scale Tools
    void ScaleOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Scale/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Scale").gameObject.SetActive(visible);
        //if disabling component also disable subcomponents for future use
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
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Sphere/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Sphere").gameObject.SetActive(visible);
    }
    void OrientationOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Orientation/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Orientation").gameObject.SetActive(visible);
    }
    void TransparentOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Transparent/HelpWindow").gameObject.SetActive(false);

        //if disabling component update skinVisibility variable with new material parameter
        if (!visible)
        {
            GameObject Head = GameObject.Find("Head");
            skinVisibility = Head.transform.FindChild("Model/Skin_Reduced/grp1/grp1_MeshPart0").GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
        }
        GameObject.Find("Controls").transform.FindChild("Transparent").gameObject.SetActive(visible);
    }
    void MoveWithHeadOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("MoveWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("MoveWithHead").gameObject.SetActive(visible);
    }
    void DepthOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Depth/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Depth").gameObject.SetActive(visible);
    }
    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.FindChild("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("Buttons").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("BackgroundSwap").gameObject.SetActive(false);
        GameObject.Find("Menu").transform.FindChild("ButtonsSwap").gameObject.SetActive(false);
        //enable/disable model and command text display if opening or exiting menu
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(!visible);
    }

    void CopyOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("CopyRotation").gameObject.SetActive(visible);
    }
    


}
