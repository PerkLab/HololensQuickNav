using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    public Material cursorFaceMat;
    public Material cursorEdgeMat;
    public Material cursorTransparent;

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
    private TextMesh DoneCommandName;

    private void Start()
    {
        //write to log
        WriteLog.WriteData("Program Started");

        //assign text mesh to global variable for use when calling commands
        CommandName = GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>();
        DoneCommandName = GameObject.Find("CommandText").transform.FindChild("DoneCommandName").GetComponent<TextMesh>();

        //get skinVisiblity value of initial material
        GameObject Skin = GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").gameObject;
        if (Skin.transform.childCount > 0) //mesh has multiple parts
        {
            skinVisibility = Skin.transform.FindChild("grp1_MeshPart0").GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
        }
        else //mesh is one part
        {
            skinVisibility = Skin.GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
        }

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

        CursorOnOff(false);

        //display command
        CommandName.text = "Done";

        //write to log
        WriteLog.WriteData("Command: Done");
        
    }
    public void Brain()
    {
        //if visible, turn brain off
        //if not visible, turn brain on

        if(GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf) //brain on  >> detail must be off
        {
            BrainOnOff(false);
            //display proper current states of head and brain visibility 
            if(headOnOff)
            {
                DoneCommandName.text = "Head:On - Brain:Off - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Brain (Done - Head:On - Brain:Off - Detail:Off)");
            } 
            else if(!headOnOff)
            {
                DoneCommandName.text = "Head:Off - Brain:Off - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Brain (Done - Head:Off - Brain:Off - Detail:Off)");
            }
        }
        else //brain off >> turn brain on, detail off
        {
            BrainOnOff(true);
            //display proper current states of head and brain visibility 
            if (headOnOff)
            {
                DoneCommandName.text = "Head:On - Brain:On - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Brain (Done - Head:On - Brain:On - Detail:Off)");
            }
            else if (!headOnOff)
            {
                DoneCommandName.text = "Head:Off - Brain:On - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Brain (Done - Head:Off - Brain:On - Detail:Off)");
            }
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
            if (GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf) //if brain on detail off
            {
                DoneCommandName.text = "Head:Off - Brain:On - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Head (Done - Head:Off - Brain:On - Detail:Off)");
            }
            else //if brain off, detail may be on
            {
                if(GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.activeSelf)
                {
                    DoneCommandName.text = "Head:Off - Brain:Off - Detail:On";
                    //write to log
                    WriteLog.WriteData("Command: Head (Done - Head:Off - Brain:Off - Detail:On)");
                }
                else //brain and detail off
                {
                    DoneCommandName.text = "Head:Off - Brain:Off - Detail:Off";
                    //write to log
                    WriteLog.WriteData("Command: Head (Done - Head:Off - Brain:Off - Detail:Off)");
                }
                
            }
        }
        else //head off
        {
            HeadOnOff(true);
            //display proper current states of head and brain visibility
            if (GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.activeSelf) //if brain on detail off
            {
                DoneCommandName.text = "Head:On - Brain:On - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Head (Done - Head:On - Brain:On - Detail:Off)");
            }
            else //if brain off, detail may be on
            {
                if (GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.activeSelf)
                {
                    DoneCommandName.text = "Head:On - Brain:Off - Detail:On";
                    //write to log
                    WriteLog.WriteData("Command: Head (Done - Head:On - Brain:Off - Detail:On)");
                }
                else //brain and detail off
                {
                    DoneCommandName.text = "Head:On - Brain:Off - Detail:Off";
                    //write to log
                    WriteLog.WriteData("Command: Head (Done - Head:On - Brain:Off - Detail:Off)");
                }

            }
        }
    }
    public void Detail()
    {
        if (GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.activeSelf)
        {
            BrainOnOff(false);
            DetailOnOff(false);

            if (headOnOff)
            {
                DoneCommandName.text = "Head:On - Brain:Off - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Detail (Done - Head:On - Brain:Off - Detail:Off)");
            }
            else if (!headOnOff)
            {
                DoneCommandName.text = "Head:Off - Brain:Off - Detail:Off";
                //write to log
                WriteLog.WriteData("Command: Detail (Done - Head:Off - Brain:Off - Detail:Off)");
            }
        }
        else
        {
            BrainOnOff(false);
            DetailOnOff(true);

            if (headOnOff)
            {
                DoneCommandName.text = "Head:On - Brain:Off - Detail:On";
                //write to log
                WriteLog.WriteData("Command: Detail (Done - Head:On - Brain:Off - Detail:On)");
            }
            else if (!headOnOff)
            {
                DoneCommandName.text = "Head:Off - Brain:Off - Detail:On";
                //write to log
                WriteLog.WriteData("Command: Detail (Done - Head:Off - Brain:Off - Detail:On)");
            }
        }
    }
    public void ResetModel()
    {
        SetAllInactive();
        ResetOnOff(true);
        //display current command
        CommandName.text = "Reset";
        //write to log
        WriteLog.WriteData("Command: Reset");
    }

    public void Scale(int option)
    {
        SetAllInactive();
        ScaleOnOff(true);
        //different voice commands call different tools by selecting option
        if(option == 0)
        {
            ScaleWithHeadOnOff(true);
            CommandName.text = "Scale - Head";
            //write to log
            WriteLog.WriteData("Command: Scale - Head");
        }
        else
        {
            ScaleArrowsOnOff(true);
            CommandName.text = "Scale - Gaze";
            //write to log
            WriteLog.WriteData("Command: Scale - Gaze");
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
            CommandName.text = "Rotate A - Gaze";
            //write to log
            WriteLog.WriteData("Command: Rotate A - Gaze");
        }
        else if (option ==1)
        {
            RotateArrowsAOnOff(true);
            CommandName.text = "Rotate A - Tap";
            //write to log
            WriteLog.WriteData("Command: Rotate A - Tap");
        }
        else
        {
            RotateHeadAOnOff(true);
            CommandName.text = "Rotate A - Head";
            //write to log
            WriteLog.WriteData("Command: Rotate A - Head");
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
            CommandName.text = "Rotate S - Gaze";
            //write to log
            WriteLog.WriteData("Command: Rotate S - Gaze");
        }
        else if (option == 1)
        {
            RotateArrowsSOnOff(true);
            CommandName.text = "Rotate S - Tap";
            //write to log
            WriteLog.WriteData("Command: Rotate S - Tap");
        }
        else
        {
            RotateHeadSOnOff(true);
            CommandName.text = "Rotate S - Head";
            //write to log
            WriteLog.WriteData("Command: Rotate S - Head");
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
            CommandName.text = "Rotate R - Gaze";
            //write to log
            WriteLog.WriteData("Command: Rotate R - Gaze");
        }
        else if (option == 1)
        {
            RotateArrowsROnOff(true);
            CommandName.text = "Rotate R - Tap";
            //write to log
            WriteLog.WriteData("Command: Rotate R - Tap");
        }
        else
        {
            RotateHeadROnOff(true);
            CommandName.text = "Rotate R - Head";
            //write to log
            WriteLog.WriteData("Command: Rotate R - Head");
        }
    }
    public void Rotate3Axis()
    {
        SetAllInactive();
        Rotate3AxisOnOff(true);
        WriteLog.WriteData("Command: Rotate3Axis");
        CommandName.text = "Rotate3Axis";
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

        //write to log
        WriteLog.WriteData("Command: Shift");
    }

    public void SphereRotate()
    {
        SetAllInactive();
        SphereOnOff(true);
        //declare the sphere on to call delay function in Update() to enable the script
        SphereOn = true;
        //display initial command as pause, until user starts tool with voice command
        CommandName.text = "Rotate Free - Pause";
        //write to log
        WriteLog.WriteData("Command: Rotate Free");
    }
    public void Orientation()
    {
        SetAllInactive();
        OrientationOnOff(true);
        //display current command
        CommandName.text = "Orientation";
        //write to log
        WriteLog.WriteData("Command: Orientation");
    }
    public void Transparent()
    {
        SetAllInactive();
        TransparentOnOff(true);
        //display current command
        CommandName.text = "Transparent";
        //write to log
        WriteLog.WriteData("Command: Transparent");
    }

    public void Move()
    {
        //write to log
        WriteLog.WriteData("Command: MoveFull");

        SetAllInactive();
        MoveOnOff(true);
        
    }

    public void MoveWithHead()
    {
        SetAllInactive();
        MoveWithHeadOnOff(true);
        //display current command
        CommandName.text = "Move";
        //write to log
        WriteLog.WriteData("Command: Move");
    }
    public void Depth()
    {
        SetAllInactive();
        DepthOnOff(true);
        //display current command
        CommandName.text = "Depth";
        //write to log
        WriteLog.WriteData("Command: Depth");
    }

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        //hide the model entirely from view
        GameObject.Find("Head").transform.FindChild("Model").transform.FindChild("Skin_Reduced").gameObject.SetActive(false);
        //clear current command text
        CommandName.text = "";
        //write to log
        WriteLog.WriteData("Command: Menu");
    }

    public void ThreePointReg()
    {
        SetAllInactive();
        CursorOnOff(false);
        //write to log
        WriteLog.WriteData("Command: 3 Pt Registration");
        GameObject.Find("3PointRegistration").transform.FindChild("Frame").transform.GetComponent<AirTapInput>().enabled = true;
    }

    public void GroundTruth()
    {
        SetAllInactive();
        GroundTruthOnOff(true);
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
        Rotate3AxisOnOff(false);
        SphereOnOff(false);
        //disable RotateFree script until enabled by voice command
        (GameObject.Find("Controls").transform.FindChild("Sphere").GetComponent("Interactive") as MonoBehaviour).enabled = false;

        //shift tools
        ShiftWithHeadOnOff(false);

        ScaleOnOff(false);
        OrientationOnOff(false);
        TransparentOnOff(false);
        MoveWithHeadOnOff(false);
        MoveOnOff(false);
        DepthOnOff(false);

        HeadOnOff(true);
        BrainOnOff(false);
        DoneOnOff(false);

        ResetOnOff(false);

        GroundTruthOnOff(false);
        MenuOnOff(false);

        CursorOnOff(true);

    }

    //
    //enable/disable use of components 
    //
    void HeadOnOff(bool visible)
    {
        //declare values for material parameters
        float amountVisibleSkin = 0f;
        float amountVisibleBrain = 0f;
        float amountSmoothSkin = 0f;
        float amountSmoothBrain = 0f;
        //declare state of headOnOff
        headOnOff = visible;
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(true);

        if (visible)
        {
            //if making head visible set amountVisible as skinVisility, the previous value of the material last time it was set by Transparent command
            //amountVisibleSkin = skinVisibility;
            amountVisibleSkin = 0.3f;
            amountSmoothSkin = 0.1f;

            amountVisibleBrain = 0.1f;
            amountSmoothBrain = 0.2f;
        }

        //set visibility of mesh compononets in Brain and Skin
        Transform Skin = GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").transform;
        if(Skin.childCount >0) //mesh has multiple sections
        {
            foreach (Transform child in Skin)
            {
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleSkin);
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothSkin);
            }
        }
        else //mesh is only one part
        {
            GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleSkin);
            GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothSkin);
        }
        

        Transform Brain = GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced/grp1").transform;
        if (Brain.childCount > 0) //mesh has multiple sections
        {
            foreach (Transform child in Brain)
            {
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleBrain);
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothBrain);
            }
        }
        else //mesh is only one part
        {
            GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleBrain);
            GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothBrain);
        }

    }
    void BrainOnOff(bool visible)
    {
        //toggle visiblity of brain, hemmatoma
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(true);
        GameObject.Find("Head").transform.FindChild("Model/Brain_Reduced").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/SubduralHemmorhage").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/BurrHoleMarker").gameObject.SetActive(visible);
        //GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.SetActive(visible);

    }

    void DetailOnOff(bool visible)
    {
        //view burrhole location as a smaller point
        GameObject.Find("Head").transform.FindChild("Model/BurrHoleMarker").gameObject.SetActive(visible);
        //GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(!visible);
        headOnOff = !visible;
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
        GameObject.Find("CommandText").transform.FindChild("DoneCommandName").gameObject.SetActive(visible);
        DoneCommandName.text = "Head:On - Brain:Off - Detail:Off";

        //if turning off, set head back to full colour
        if (!visible)
        {
            HeadOnOff(true);
        }
        GameObject.Find("Controls").transform.FindChild("Done").transform.GetComponent<AirTapInput>().enabled = visible;
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

    void Rotate3AxisOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis").transform.GetComponent<AirTapInput>().enabled = visible;
        GameObject.Find("CommandText").transform.FindChild("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);

    }

    //Shift Tools
    void ShiftWithHeadOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").transform.GetComponent<AirTapInput>().enabled = visible;
        GameObject.Find("CommandText").transform.FindChild("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);
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
            GameObject Skin = GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced/grp1").gameObject;
            if(Skin.transform.childCount > 0) //mesh has multiple parts
            {
                skinVisibility = Skin.transform.FindChild("grp1_MeshPart0").GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
            }
            else //mesh is one part
            {
                skinVisibility = Skin.GetComponent<MeshRenderer>().material.GetFloat("_Metallic");
            }
            
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
    void MoveOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("MoveFull").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("MoveFull/MoveWithHead").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("MoveFull/Depth").gameObject.SetActive(false);

        GameObject.Find("CommandText").transform.FindChild("MoveHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("MoveFull").transform.GetComponent<AirTapInput>().enabled = visible;

    }

    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.FindChild("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("Buttons").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.FindChild("BackgroundSwap").gameObject.SetActive(false);
        GameObject.Find("Menu").transform.FindChild("ButtonsSwap").gameObject.SetActive(false);
        GameObject.Find("Menu").transform.FindChild("BackgroundOther").gameObject.SetActive(false);
        GameObject.Find("Menu").transform.FindChild("ButtonsOther").gameObject.SetActive(false);
        //enable/disable model and command text display if opening or exiting menu
        GameObject.Find("Head").transform.FindChild("Model/Skin_Reduced").gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(!visible);
        //GameObject.Find("Menu").transform.FindChild("Background").transform.GetComponent<AirTapInput>().enabled = visible;
    }


    void GroundTruthOnOff(bool visible)
    {
        if(visible)
        {
            //move marker infront of user
            Vector3 cursorPos = GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").transform.position;
            GameObject.Find("GroundTruth").transform.FindChild("Marker").transform.position = new Vector3(cursorPos.x, cursorPos.y, cursorPos.z);
        }

        //hide model and text
        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("CommandName").gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(!visible);

        GameObject.Find("GroundTruth").transform.FindChild("Marker").gameObject.SetActive(visible);
        GameObject.Find("GroundTruth").transform.FindChild("MoveWithHead").gameObject.SetActive(visible);
        GameObject.Find("GroundTruth").transform.FindChild("Depth").gameObject.SetActive(false);
        
    }
    
    public void CursorOnOff(bool visible)
    {
        GameObject cursor = GameObject.Find("InteractiveMeshCursor").gameObject;
        if (visible)
        { 
            Material[] newMat = new Material[2];
            newMat[0] = cursorFaceMat;
            newMat[1] = cursorEdgeMat;
            cursor.transform.FindChild("CursorDot").GetComponent<MeshRenderer>().materials = newMat;
            newMat[0] = cursorEdgeMat;
            newMat[1] = cursorFaceMat;
            cursor.transform.FindChild("CursorRing").GetComponent<MeshRenderer>().materials = newMat;
        }
        else
        {
            Material[] newMat = new Material[2];
            newMat[0] = cursorTransparent;
            newMat[1] = cursorTransparent;
            cursor.transform.FindChild("CursorDot").GetComponent<MeshRenderer>().materials = newMat;
            cursor.transform.FindChild("CursorRing").GetComponent<MeshRenderer>().materials = newMat;
        }
    }


}
