using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{

    //global variable for text to display current command for user
    private TextMesh CommandName;

    private void Start()
    {
        //write to log
        WriteLog.WriteData("Program Started");

        //assign text mesh to global variable for use when calling commands
        CommandName = GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>();

        SetAllInactive();
        Home(); //start in "home" state before selecting a tool
    }

    //----------------------------------------------------------------------------------------
    // functions to call from voice commands
    //
    public void Home()
    {
        SetAllInactive();
        HomeOnOff(true);

        //display command
        CommandName.text = "Home";

        //write to log
        WriteLog.WriteData("Command: Home");

    }

    public void RotateWithXbox()
    {
        SetAllInactive();
        RotateWithXboxOnOff(true);
        WriteLog.WriteData("Command: Rotate-Xbox");
        CommandName.text = "Rotate";
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

    //----------------------------------------------------------------------------------------
    //turn all tools off before switching to a new tool
    //
    public void SetAllInactive()
    {

        //disable all tools
        RotateWithXboxOnOff(false);
        ShiftWithXboxOnOff(false);
        HomeOnOff(false);
        //disable menu
        MenuOnOff(false);

        //turn off all layers execpt skin (layer 0)
        foreach (Transform child in GameObject.Find("WorldAnchor/Model").transform.Find("Layers").transform)
        {
            child.gameObject.SetActive(false);
        }
        GameObject.Find("WorldAnchor/Model/Layers").transform.GetChild(0).gameObject.SetActive(true);


    }

    //----------------------------------------------------------------------------------------
    //enable/disable use of components/tools 
    //
    void HomeOnOff(bool visible)
    {
        //close help window if previously open
        GameObject.Find("Controls").transform.Find("Home/HelpWindow").gameObject.SetActive(false);
        //GameObject.Find("Controls").transform.Find("Home/LayerSelector").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("Home").gameObject.SetActive(visible);
    }

    //Xbox Rotate Tool
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

    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.Find("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.Find("Buttons").gameObject.SetActive(visible);

        //enable/disable skin (layer 0) and command text display if opening or exiting menu
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(!visible);
    }



}
