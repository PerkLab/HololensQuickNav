using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    public Material cursorFaceMat;
    public Material cursorEdgeMat;
    public Material cursorTransparent;

    //visibility of head and brain
    private bool headOnOff;

    //global variable for text to display current command for user
    private TextMesh CommandName;
    //text to display current command for user while in "done" state
    //displays current visibility of layers 
    private TextMesh DoneCommandName;

    private void Start()
    {
        //write to log
        WriteLog.WriteData("Program Started");

        //assign text mesh to global variable for use when calling commands
        CommandName = GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>();
        DoneCommandName = GameObject.Find("CommandText").transform.FindChild("DoneCommandName").GetComponent<TextMesh>();

        SetAllInactive();
        Done(); //start in "done" state before selecting a tool

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

        if(GameObject.Find("Head").transform.FindChild("Model/Brain").gameObject.activeSelf) //brain on  >> detail must be off
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
            if (GameObject.Find("Head").transform.FindChild("Model/Brain").gameObject.activeSelf) //if brain on detail off
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
            if (GameObject.Find("Head").transform.FindChild("Model/Brain").gameObject.activeSelf) //if brain on detail off
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
        GameObject.Find("Head").transform.FindChild("Model").transform.FindChild("Skin").gameObject.SetActive(false);
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
        Rotate3AxisOnOff(false);

        //shift tools
        ShiftWithHeadOnOff(false);

        //move tools
        MoveWithHeadOnOff(false);
        MoveOnOff(false);
        DepthOnOff(false);

        //visibility tools in "done" state
        HeadOnOff(true);
        BrainOnOff(false);
        DoneOnOff(false);
        CursorOnOff(true);

        //visibility of menu
        MenuOnOff(false);

        ResetOnOff(false);
        GroundTruthOnOff(false);

    }

    //
    //enable/disable use of components/tools 
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
        GameObject.Find("Head").transform.FindChild("Model/Skin").gameObject.SetActive(true);

        if (visible)
        {
            amountVisibleSkin = 0.3f;
            amountSmoothSkin = 0.1f;

            amountVisibleBrain = 0.1f;
            amountSmoothBrain = 0.2f;
        }

        //set visibility of mesh compononets in Brain and Skin
        Transform Skin = GameObject.Find("Head").transform.FindChild("Model/Skin/grp1").transform;
        if(Skin.childCount >0) //mesh has multiple sub sections
        {
            foreach (Transform child in Skin)
            {
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleSkin);
                child.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothSkin);
            }
        }
        else //mesh is only one part
        {
            GameObject.Find("Head").transform.FindChild("Model/Skin/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleSkin);
            GameObject.Find("Head").transform.FindChild("Model/Skin/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothSkin);
        }
        

        Transform Brain = GameObject.Find("Head").transform.FindChild("Model/Brain/grp1").transform;
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
            GameObject.Find("Head").transform.FindChild("Model/Brain/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", amountVisibleBrain);
            GameObject.Find("Head").transform.FindChild("Model/Brain/grp1").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Glossiness", amountSmoothBrain);
        }

    }
    void BrainOnOff(bool visible)
    {
        //toggle visiblity of brain, hemmatoma
        GameObject.Find("Head").transform.FindChild("Model/Skin").gameObject.SetActive(true);
        GameObject.Find("Head").transform.FindChild("Model/Brain").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/SubduralHemmorhage").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/BurrHoleMarker").gameObject.SetActive(visible);
        //GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.SetActive(visible);

    }

    void DetailOnOff(bool visible)
    {
        //view burrhole location as a smaller point
        GameObject.Find("Head").transform.FindChild("Model/BurrHoleMarker").gameObject.SetActive(visible);
        //GameObject.Find("Head").transform.FindChild("Model/Targets").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.FindChild("Model/Skin").gameObject.SetActive(!visible);
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
        GameObject.Find("Head").transform.FindChild("Model/Skin").gameObject.SetActive(!visible);
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
