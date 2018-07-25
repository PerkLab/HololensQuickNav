using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class in the scene to control all registration tools 
/// </summary>
public class ComponentManager : MonoBehaviour
{

    /// <summary> Text mesh displayed to user to indicate current registration tool </summary>
    private TextMesh CommandName;

    /// <summary>
    /// Initialize registration tools 
    /// </summary>
    private void Start()
    {
        //write to log
        WriteLog.WriteData("Program Started");

        //assign text mesh to global variable
        CommandName = GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>();

        //disable all tools and start user in a locked "home" state
        SetAllInactive();
        Home(); 
    }

    //----------------------------------------------------------------------------------------
    // functions to call from voice commands
    //

    /// <summary>
    /// Enable "home" state for viewing models
    /// </summary>
    public void Home()
    {
        //disable any active tools first
        SetAllInactive();
        HomeOnOff(true);
        CommandName.text = "Home";
        WriteLog.WriteData("Command: Home");

    }

    /// <summary>
    /// Enable xbox rotation tool
    /// </summary>
    public void RotateWithXbox()
    {
        //disable any active tools first
        SetAllInactive();
        RotateWithXboxOnOff(true);
        WriteLog.WriteData("Command: Rotate-Xbox");
        CommandName.text = "Rotate";
    }
    
    /// <summary>
    /// Enable xbox translation tool
    /// </summary>
    public void ShiftWithXbox()
    {
        //disable any active tools first
        SetAllInactive();
        ShiftWithXboxOnOff(true);
        CommandName.text = "Shift";
        WriteLog.WriteData("Command: Shift-Xbox");
    }

    /// <summary>
    /// Hide models and display main menu
    /// </summary>
    public void Menu()
    {
        //disable any active tools first
        SetAllInactive();
        MenuOnOff(true);
        //SetAllInactive() hides all layers of the model except the skin
        //To completely hide the model, disable skin (layer 0)
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(false);
        //clear current command text to hide the text mesh
        CommandName.text = "";
        //write to log
        WriteLog.WriteData("Command: Menu");
    }

    //----------------------------------------------------------------------------------------
    //turn all tools off before switching to a new tool
    //

    /// <summary>
    /// Disables all tools and menus in the scene to ensure nothing is active when calling another tool
    /// </summary>
    public void SetAllInactive()
    {
        //use toggle methods to disable all tools and menu
        RotateWithXboxOnOff(false);
        ShiftWithXboxOnOff(false);
        HomeOnOff(false);
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
    /// <summary>
    /// Toggle active state of Home tool 
    /// </summary>
    /// <param name="visible"> Active/inactive state of tool </param>
    void HomeOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("Home").gameObject.SetActive(visible);
    }

    /// <summary>
    /// Toggle active state of xbox rotate tool 
    /// </summary>
    /// <param name="visible"> Active/inactive state of tool </param>
    void RotateWithXboxOnOff(bool visible)
    {
        //set active/inactive status of tool and rotation axes 
        GameObject.Find("Controls").transform.Find("RotateWithXbox").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisA").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisS").gameObject.SetActive(visible);
        GameObject.Find("Model").transform.Find("AxisR").gameObject.SetActive(visible);
    }

    //Xbox Shift Tool
    void ShiftWithXboxOnOff(bool visible)
    {
        GameObject.Find("Controls").transform.Find("ShiftWithXbox").gameObject.SetActive(visible);

    }

    void MenuOnOff(bool visible)
    {
        GameObject.Find("Menu").transform.Find("Background").gameObject.SetActive(visible);
        GameObject.Find("Menu").transform.Find("Buttons").gameObject.SetActive(visible);

        //enable/disable skin (layer 0) and command text display if opening or exiting menu
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(!visible);
        GameObject.Find("CommandText").transform.Find("Tools").gameObject.SetActive(!visible);
    }

}
