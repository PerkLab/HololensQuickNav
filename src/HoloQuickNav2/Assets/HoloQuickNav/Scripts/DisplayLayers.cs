using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLayers : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;
    public Material buttonOn;
    public Material buttonOff;

    private List<GameObject> layers;
    private int numberOfLayers = 0;
    private float buttonDistance = 0.01f;
    private float buttonCentreToCentre = 0.04f;
    private float buttonHeight = 0.03f;

    private float timer = 0; //timer for toggling layers

    private bool MenuOpen = false;
    private int SelectorLocation = 0;

    void Start () {
        
        numberOfLayers = GameObject.Find("Model").transform.Find("Layers").transform.childCount;

        //position tool around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        CreateButtons();
        PlaceMenu();
        //OpenMenu();
        
    }

    private void CreateButtons()
    {
        Transform buttonPrefab = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons/ButtonPrefab").transform;
        
        //create buttons in list
        for(int i = 0; i< (numberOfLayers-1); i++)
        {
            //duplicate prefab button
            Transform newButton = Instantiate(buttonPrefab, (buttonPrefab.position - new Vector3(0f, buttonCentreToCentre*(i+1), 0f)), buttonPrefab.rotation);
            //make it a child of the buttons in layer selector
            newButton.parent = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform;
        }

        string layerName;
        GameObject button;

        //rename buttons
        for (int i = 0; i < numberOfLayers; i++)
        {
            layerName = GameObject.Find("Model").transform.Find("Layers").transform.GetChild(i).name;
            //shorten name if needed to fit on button
            if(layerName.Length > 12)
            {
                layerName = layerName.Substring(0, 12) + "..";
            }
            button = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).gameObject;
            button.transform.GetChild(1).GetComponent<TextMesh>().text = layerName;
        }

        //move selector to top button
        Vector3 position = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(0).transform.position;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Selector").transform.position = new Vector3(position.x, position.y, position.z);
        SelectorLocation = 0;

    }

    private void PlaceMenu()
    {
        //shift buttons to centre on menu
        float shiftDistance = (numberOfLayers * buttonDistance + (numberOfLayers - 1) * buttonHeight) / 2;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.position += new Vector3(0f, shiftDistance, 0f);

        //scale menu background
        float backgroundHeight = (numberOfLayers * buttonDistance + (numberOfLayers - 1) * buttonHeight) + 0.06f;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Background").transform.localScale = new Vector3(0.25f, backgroundHeight, 0.01f);
    }

    private void Update()
    {
        //have menus face the user
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);

        timer += Time.deltaTime; //increment time since last layer was toggled
    }

    /*
    public void ToggleLayer(GameObject pressedButton)
    {
        Material[] matList = new Material[1];

        if(MenuOpen)
        {
            if (timer > 2) //if 2 seconds have past since you last turned a layer on
            {
                timer = 0;
                //determine layer corresponding to button
                int layerIndex = pressedButton.transform.GetSiblingIndex();

                //if layer is visible, hide it
                //if not visible, show it
                if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(layerIndex).gameObject.activeSelf)
                {
                    GameObject.Find("Model").transform.Find("Layers").transform.GetChild(layerIndex).gameObject.SetActive(false);
                    matList[0] = buttonOff;
                    pressedButton.transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
                }
                else
                {
                    GameObject.Find("Model").transform.Find("Layers").transform.GetChild(layerIndex).gameObject.SetActive(true);
                    matList[0] = buttonOn;
                    pressedButton.transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
                }
            }
        }
      

    }

    public void OpenMenu()
    {
        MenuOpen = true;
        
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Background").gameObject.SetActive(true);
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Buttons").gameObject.SetActive(true);
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Selector").gameObject.SetActive(true);
        

        //set colour of buttons base on current visibility of layers
        Material[] matList = new Material[1];
        GameObject button;
        for (int i = 0; i < numberOfLayers; i++)
        {
            button = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).gameObject;
            if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(i).gameObject.activeSelf)
            {
                matList[0] = buttonOn;
                button.transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
            }
            else
            {
                matList[0] = buttonOff;
                button.transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
            }
        }
    }
    public void CloseMenu()
    {
        MenuOpen = false;
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Background").gameObject.SetActive(false);
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Buttons").gameObject.SetActive(false);
        //GameObject.Find("Controls").transform.Find("Home").transform.Find("LayerSelector").Find("Selector").gameObject.SetActive(false);
    }
    public void ToggleMenu()
    {
        if (MenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    public void ToggleLayer()
    {
        if(MenuOpen)
        {
            Material[] matList = new Material[1];

            //if layer is visible, hide it
            //if not visible, show it
            if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(SelectorLocation).gameObject.activeSelf)
            {
                GameObject.Find("Model").transform.Find("Layers").transform.GetChild(SelectorLocation).gameObject.SetActive(false);
                matList[0] = buttonOff;
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(SelectorLocation).transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
            }
            else
            {
                GameObject.Find("Model").transform.Find("Layers").transform.GetChild(SelectorLocation).gameObject.SetActive(true);
                matList[0] = buttonOn;
                GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(SelectorLocation).transform.GetChild(0).GetComponent<MeshRenderer>().materials = matList;
            }
        }
        
    }

    public void MoveSelector(bool Up)
    {
        Vector3 position;
        if (Up)
        {
            if(SelectorLocation == GameObject.Find("Model").transform.Find("Layers").transform.childCount - 1)
                //move to top of list
                SelectorLocation = 0;
            else
                SelectorLocation += 1;
        }
        else
        {
            if (SelectorLocation == 0)
                //move to bottom  of list
                SelectorLocation = GameObject.Find("Model").transform.Find("Layers").transform.childCount - 1;
            else
                SelectorLocation -= 1;
        }
        position = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(SelectorLocation).transform.position;
        GameObject.Find("Controls").transform.Find("Home/LayerSelector/Selector").transform.position = new Vector3(position.x, position.y, position.z);
    }
    */
}

    
     
      