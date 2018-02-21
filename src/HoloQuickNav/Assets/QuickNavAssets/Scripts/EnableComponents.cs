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
        CommandName = GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>();
        
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

    public void Menu()
    {
        SetAllInactive();
        MenuOnOff(true);
        //hide the model entirely, disable skin (layer 0)
        GameObject.Find("Model").transform.FindChild("Layers").transform.GetChild(0).gameObject.SetActive(false);
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
        MoveOnOff(false);

        //turn off all layers execpt skin (layer 0)
        foreach(Transform child in GameObject.Find("Model").transform.FindChild("Layers").transform)
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
        GameObject.Find("Controls").transform.FindChild("Reset").gameObject.SetActive(visible);
    }
    void HomeOnOff(bool visible)
    {

        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("Home/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Home").gameObject.SetActive(visible);
        
        GameObject.Find("Controls").transform.FindChild("Home").transform.GetComponent<AirTapInput>().enabled = visible;
    }

    //Rotate Tool
    void Rotate3AxisOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("Rotate3Axis").transform.GetComponent<AirTapInput>().enabled = visible;
        GameObject.Find("CommandText").transform.FindChild("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);

        GameObject.Find("Model").transform.FindChild("AxisA").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.FindChild("AxisS").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.FindChild("AxisR").gameObject.SetActive(visible);
    }

    //Shift Tool
    void ShiftWithHeadOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead/HelpWindow").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("ShiftWithHead").transform.GetComponent<AirTapInput>().enabled = visible;
        GameObject.Find("CommandText").transform.FindChild("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(false);
    }

    //Move Tool
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

        //enable/disable skin (layer 0) and command text display if opening or exiting menu
        GameObject.Find("Model").transform.FindChild("Layers").transform.GetChild(0).gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(!visible);
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
