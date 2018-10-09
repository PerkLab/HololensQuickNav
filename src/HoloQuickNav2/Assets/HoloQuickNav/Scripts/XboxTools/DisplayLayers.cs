using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Use to toggle the visibility of each model in the scene. Attach to menu with prefab buttons to create menu of loaded models at runtime. 
/// </summary>
public class DisplayLayers : MonoBehaviour {

    /// <summary> Hololens camera in scene /// </summary>
    public GameObject cam;
    /// <summary> Model to align menu around /// </summary>
    public GameObject selectedObject;
    /// <summary> List of models in the scene /// </summary>
    private List<GameObject> layers;
    /// <summary> Number of models in the scene /// </summary>
    private int numberOfLayers = 0;
    /// <summary> Distance between bottom of one button and top of the next. Use to determine size of menu background /// </summary>
    private float buttonDistance = 0.01f;
    /// <summary> Distance between two buttons centre points. Use to position new buttons. /// </summary>
    private float buttonCentreToCentre = 0.04f;
    /// <summary> Height of each button. Use to determine size of menu background. /// </summary>
    private float buttonHeight = 0.03f;

    void Start () {
        
        //get number of models currently existing in the scene
        numberOfLayers = GameObject.Find("Model").transform.Find("Layers").transform.childCount;

        //if there are no targets listed in the .xml, don't display a button to toggle the target visibility
        if (GameObject.Find("LoadedModels").GetComponent<LocationValues>().getTargets().Length == 0)
        {
            //one less layer if not displaying targets
            numberOfLayers--;
            //set as last sibiling so that it's index is larger than the number of layers and won't be accessed
            GameObject.Find("Model").transform.Find("Layers/Targets").transform.SetAsLastSibling();
        }

        //position menu around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        //create and name all required buttons
        CreateButtons();
        //position the buttons and menu based on number of buttons
        OrganizeMenu();
        //initialized xbox control component for use with a dynamic menu
        SetupXboxMenuContol();
        //load initial visibility status of all models
        OpenMenu();
        
    }

    /// <summary>
    /// Create buttons and display names of each model based on models loaded in the scene
    /// </summary>
    private void CreateButtons()
    {
        //access prefab
        Transform buttonPrefab = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons/ButtonPrefab").transform;
        
        //create buttons in list
        for(int i = 0; i< (numberOfLayers-1); i++)
        {
            //duplicate prefab button and position based on distance between two buttons
            Transform newButton = Instantiate(buttonPrefab, (buttonPrefab.position - new Vector3(0f, buttonCentreToCentre*(i+1), 0f)), buttonPrefab.rotation);
            //make it a child of the buttons in layer selector
            newButton.parent = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform;
        }

        string layerName;
        GameObject button;

        //display name of each layer on buttons and visibility status
        for (int i = 0; i < numberOfLayers; i++)
        {
            layerName = GameObject.Find("Model").transform.Find("Layers").transform.GetChild(i).name;
            //shorten name if needed to fit on button
            if(layerName.Length > 12)
            {
                layerName = layerName.Substring(0, 12) + "..";
            }
            button = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).gameObject;
            button.transform.GetChild(1).GetComponent<TextMesh>().text = layerName + ": On";
        }
    }

    /// <summary>
    /// Scale menu based on number of buttons and properly centre buttons on the background.
    /// </summary>
    private void OrganizeMenu()
    {
        //shift buttons to centre on menu 
        float shiftDistance = (numberOfLayers * buttonDistance + (numberOfLayers - 1) * buttonHeight) / 2;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.position += new Vector3(0f, shiftDistance, 0f);

        //scale menu background
        float backgroundHeight = (numberOfLayers * buttonDistance + (numberOfLayers - 1) * buttonHeight) + 0.06f;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Background").transform.localScale = new Vector3(0.25f, backgroundHeight, 0.01f);

        //place command text under menu
        Vector3 lastButton = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").GetChild(numberOfLayers - 1).position;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/CommandText").position = new Vector3(GameObject.Find("Controls").transform.Find("Home/LayerSelector/CommandText").position.x,
                                                                                                             lastButton.y - 0.05f,
                                                                                                            GameObject.Find("Controls").transform.Find("Home/LayerSelector/CommandText").position.z);
    }

    private void Update()
    {
        //have menu face the user
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }

    /// <summary>
    /// Every time the menu is opened, display current visibility status of each layer
    /// </summary>
    public void OpenMenu()
    {
        GameObject button;
        for (int i = 0; i < numberOfLayers; i++)
        {
            //check visibility and change button text
            button = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).gameObject;
            if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(i).gameObject.activeSelf)
            {
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).transform.GetChild(1).GetComponent<TextMesh>().text =
                    GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).transform.GetChild(1).GetComponent<TextMesh>().text.Replace("Off", "On");
            }
            else
            {
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).transform.GetChild(1).GetComponent<TextMesh>().text =
                    GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).transform.GetChild(1).GetComponent<TextMesh>().text.Replace("On", "Off");
            }
        }
    }

    /// <summary>
    /// Change visibility of model based on the button index corresponding to the current location of the selector object
    /// </summary>
    public void ToggleLayer()
    {
        //access selector position from xbox controller 
        XboxMenuController controller = GameObject.Find("Controls").transform.Find("Home/LayerSelector").GetComponent<XboxMenuController>();
        int selectorPosition = controller.GetSelectorPosition();
        
        //if layer is visible, hide it
        //if not visible, show it
        if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(selectorPosition).gameObject.activeSelf)
        {
            GameObject.Find("Model").transform.Find("Layers").transform.GetChild(selectorPosition).gameObject.SetActive(false);
            //change text on corresponding button to show that the layer is off
            GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(selectorPosition).transform.GetChild(1).GetComponent<TextMesh>().text =
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(selectorPosition).transform.GetChild(1).GetComponent<TextMesh>().text.Replace("On", "Off");
        }
        else
        {
            GameObject.Find("Model").transform.Find("Layers").transform.GetChild(selectorPosition).gameObject.SetActive(true);
            //change text on corresponding button to show that the layer is on
            GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(selectorPosition).transform.GetChild(1).GetComponent<TextMesh>().text =
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(selectorPosition).transform.GetChild(1).GetComponent<TextMesh>().text.Replace("Off", "On");
        }
        
    }

    /// <summary>
    /// Setup variables in XboxMenuControl component based on layers loaded into scene and the resulting menu that is created at runtime
    /// </summary>
    private void SetupXboxMenuContol()
    {
        XboxMenuController controller = GameObject.Find("Controls").transform.Find("Home/LayerSelector").GetComponent<XboxMenuController>();

        //move selector to top button and set base location in controller
        Vector3 position = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(0).transform.position;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Selector").transform.position = new Vector3(position.x, position.y, position.z);
        controller.SetSelectorBaseLocation(position);
        
        //set number of buttons in controller
        controller.SetNumberOfButtons(numberOfLayers);
        
        //create an array of unity events, one for each button
        //each event in the array calls ToggleLayer()
        UnityEvent toggleEvent = new UnityEvent();
        toggleEvent.AddListener(ToggleLayer);

        UnityEvent[] toggleEvents = new UnityEvent[numberOfLayers];
        for (int i = 0; i < numberOfLayers; i++) 
        {
            toggleEvents[i] = toggleEvent;
        }
        controller.Actions = toggleEvents;
    }


}

    
     
      