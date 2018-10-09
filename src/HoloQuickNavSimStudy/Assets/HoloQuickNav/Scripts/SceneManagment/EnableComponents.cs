using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour {

    public Material cursorFaceMat;
    public Material cursorEdgeMat;
    public Material cursorTransparent;
   

    //global variable for text to display current command for user
    private TextMesh CommandName;
    //text to display current command for user while in "home" state

    private void Start()
    {
        //write to log
        WriteLog.WriteData("Program Started");

        //assign text mesh to global variable for use when calling commands
        CommandName = GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>();
        
        SetAllInactive();
        Home(); //start in "home" state before selecting a tool
    }
    
    //
    // functions to call from voice commands
    //
	public void Home()
    {
        SetAllInactive();
        HomeOnOff(true);

        CursorOnOff(false);

        //display command
        CommandName.text = "Home";

        //write to log
        WriteLog.WriteData("Command: Home");
        
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

    public void RotateWithXbox()
    {
        SetAllInactive();
        RotateWithXboxOnOff(true);
        WriteLog.WriteData("Command: Rotate-Xbox");
        CommandName.text = "Rotate";
    }

    public void ShiftWithHead()
    {
        SetAllInactive();
        ShiftWithHeadOnOff(true);

        //display current command
        CommandName.text = "Shift";

        //write to log
        WriteLog.WriteData("Command: Shift");
    }

    public void ShiftWithXbox()
    {
        SetAllInactive();
        ShiftWithXboxOnOff(true);

        //display current command
        CommandName.text = "Shift";

        //write to log
        WriteLog.WriteData("Command: Shift-Xbox");
    }

    public void Move()
    {
        //write to log
        WriteLog.WriteData("Command: MoveFull");
        CommandName.text = "Move";

        SetAllInactive();
        MoveOnOff(true);
        
    }

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        //hide the model entirely, disable skin (layer 0)
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(false);
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
        GameObject.Find("3PointRegistration").transform.Find("Frame").transform.GetComponent<AirTapInput>().enabled = true;
    }

    public void PointRegistration()
    {
        //write to log
        WriteLog.WriteData("Command: PointReg");

        SetAllInactive();
        PointRegOnOff(true);
        CursorOnOff(false);
        GameObject.Find("Model").transform.GetChild(0).gameObject.SetActive(false);
    }

    //
    //turn all tools off before switching to a new tool
    //
    public void SetAllInactive()
    {
        
        //rotate tools
        Rotate3AxisOnOff(false);
        RotateWithXboxOnOff(false);

        //shift tools
        ShiftWithHeadOnOff(false);
        ShiftWithXboxOnOff(false);
        //move tools
        MoveOnOff(false);
        //point registration tool
        PointRegOnOff(false);
        //turn off all layers execpt skin (layer 0)
        foreach (Transform child in GameObject.Find("Model").transform.Find("Layers").transform)
        {
            child.gameObject.SetActive(false);
        }
        GameObject.Find("Model").transform.GetChild(0).gameObject.SetActive(true);
        HomeOnOff(false);
        CursorOnOff(true);
        //visibility of menu
        MenuOnOff(false);
        ResetOnOff(false);

    }

    //
    //enable/disable use of components/tools 
    //

    void ResetOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("Reset").gameObject.SetActive(visible);
    }
    void HomeOnOff(bool visible)
    {

        //close help window if previously open
        GameObject.Find("Controls").transform.Find("Home/HelpWindow").gameObject.SetActive(false);
        //GameObject.Find("Controls").transform.Find("Home/LayerSelector").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("Home").gameObject.SetActive(visible);
        
        GameObject.Find("Controls").transform.Find("Home").transform.GetComponent<AirTapInput>().enabled = visible;
    }

    //Rotate Tool
    void Rotate3AxisOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("Rotate3Axis/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("Rotate3Axis").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("Rotate3Axis").transform.GetComponent<AirTapInput>().enabled = visible;
        //GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        //GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(false);

        GameObject.Find("Model").transform.Find("AxisA").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisS").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisR").gameObject.SetActive(visible);
    }

    void RotateWithXboxOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("RotateWithXbox/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("RotateWithXbox").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("RotateWithXbox").transform.GetComponent<AirTapInput>().enabled = visible;
        //GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        //GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(false);

        GameObject.Find("Model").transform.Find("AxisA").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisS").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisR").gameObject.SetActive(visible);
    }

    //Shift Tool
    void ShiftWithHeadOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.Find("ShiftWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("ShiftWithHead").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("ShiftWithHead").transform.GetComponent<AirTapInput>().enabled = visible;
        //GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        //GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(false);
    }

    //Xbox Shift Tool
    void ShiftWithXboxOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.Find("ShiftWithXbox/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("ShiftWithXbox").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("ShiftWithXbox").transform.GetComponent<AirTapInput>().enabled = visible;
        //GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        //GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(false);

    }

    //Move Tool
    void MoveOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("MoveFull").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("MoveFull/MoveWithHead").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("MoveFull/Depth").gameObject.SetActive(false);

        GameObject.Find("CommandText").transform.Find("MoveHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("MoveFull").transform.GetComponent<AirTapInput>().enabled = visible;

    }

    //Point Registration Tool
    void PointRegOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("PointRegistration").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("PointRegistration").transform.GetComponent<AirTapInput>().enabled = visible;
    }

    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.Find("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.Find("Buttons").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.Find("BackgroundSwap").gameObject.SetActive(false);
        GameObject.Find("Menu").transform.Find("ButtonsSwap").gameObject.SetActive(false);

        //enable/disable skin (layer 0) and command text display if opening or exiting menu
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(!visible);
        //GameObject.Find("Menu").transform.FindChild("Background").transform.GetComponent<AirTapInput>().enabled = visible;
    }

    
    public void CursorOnOff(bool visible)
    {
        GameObject cursor = GameObject.Find("InteractiveMeshCursor").gameObject;
        if (visible)
        { 
            Material[] newMat = new Material[2];
            newMat[0] = cursorFaceMat;
            newMat[1] = cursorEdgeMat;
            cursor.transform.Find("CursorDot").GetComponent<MeshRenderer>().materials = newMat;
            newMat[0] = cursorEdgeMat;
            newMat[1] = cursorFaceMat;
            cursor.transform.Find("CursorRing").GetComponent<MeshRenderer>().materials = newMat;
        }
        else
        {
            Material[] newMat = new Material[2];
            newMat[0] = cursorTransparent;
            newMat[1] = cursorTransparent;
            cursor.transform.Find("CursorDot").GetComponent<MeshRenderer>().materials = newMat;
            cursor.transform.Find("CursorRing").GetComponent<MeshRenderer>().materials = newMat;
        }
    }

    public void ShowNextPatient()
    {
        // figure out which models are visible (and hide them) so we know which to show next
        int highestActiveModelIndex = 0;
        int index = 0;
        foreach (Transform child in GameObject.Find("Model").transform.Find("Layers").transform)
        {
            if (child.gameObject.active == true)
            {
                highestActiveModelIndex = index;
            }
            child.gameObject.SetActive(false);
            index++;
        }
        // show only the desired models (skin / current patient) -- If we are at the end, then set back to first plan.
        if (highestActiveModelIndex == GameObject.Find("Model").transform.Find("Layers").transform.childCount)
        {
            highestActiveModelIndex = 0;
        }
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(highestActiveModelIndex + 1).gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(highestActiveModelIndex + 2).gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(highestActiveModelIndex + 3).gameObject.SetActive(true);
    }
}
