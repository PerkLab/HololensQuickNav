using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  General class to allow user to navigate basic menus with xbox controller
/// </summary>

public class XboxMenuController : MonoBehaviour {

    /// <summary> Selector object used to highlight option on menu </summary>
    [Tooltip("Selector object used to highlight option on menu.")]
    public GameObject selector;
    
    /// <summary> Original position of the selector object at the top of the list </summary>
    private float selectorBaseLocation;
    
    /// <summary> Distance for selector to move between buttons. </summary>
    [Tooltip("Distance for selector to move between buttons.")]
    public float selectorIncrementDistance = 0.03f;
    
    /// <summary> Position of selector object on the list (0 being the top of the list) </summary>
    private int selectorPosition = 0;
    
    /// <summary> Numer of options displayed on the menu </summary>
    private int numOfButtons;

    ///<summary> List of actions to perform, order corresponds to order and number of buttons on menu. </summary>
    [Tooltip("List of actions to perform, order corresponds to order and number of buttons on menu.")]
    public UnityEvent[] Actions;

    /// <summary>
    /// Initialize menu and move selector to top of list 
    /// </summary>
    void Start () {
        //number of buttons determined in unity editor by adding actions
        numOfButtons = Actions.Length;
        selectorBaseLocation = selector.transform.position.y;
        //move selector to top of list
        selectorPosition = 0;
        setSelectorLocation();
    }

    /// <summary>
    /// Move selector object to location based on current selector position in list
    /// </summary>
    private void setSelectorLocation()
    {
        //calculate position using selector position, increment distance, and base location
        selector.transform.position = new Vector3(selector.transform.position.x, (selectorBaseLocation - selectorIncrementDistance * selectorPosition), selector.transform.position.z);
    }

    /// <summary>
    /// Move selector object down list 
    /// </summary>
    public void moveDownList()
    {
        //if not already at the bottom of the list
        if (selectorPosition != (numOfButtons - 1))
        {
                selectorPosition++;
                setSelectorLocation();
        }
    }

    /// <summary>
    /// Move selector object up list
    /// </summary>
    public void moveUpList()
    {
        //if not already at the top of the list 
        if (selectorPosition != 0)
        {
            selectorPosition--;
            setSelectorLocation();
        }

    }

    /// <summary>
    /// Perform action corresponding to current location of selector in the list
    /// </summary>
    public void selectButton()
    {
        //perform action specified in the unity editor
        Actions[selectorPosition].Invoke();
    }

    /// <summary>
    /// Use to set base location at runtime if variable
    /// </summary>
    public void SetSelectorBaseLocation(Vector3 baseLocation)
    {
        selectorBaseLocation = baseLocation.y;
    }

    /// <summary>
    /// Use to set number of buttons at runtime if variable
    /// </summary>
    public void SetNumberOfButtons(int numButtons)
    {
        numOfButtons = numButtons;
    }

    public int GetSelectorPosition()
    {
        return selectorPosition;
    }
}
