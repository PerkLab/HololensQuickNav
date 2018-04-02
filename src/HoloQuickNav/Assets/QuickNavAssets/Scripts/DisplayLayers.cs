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

    void Start () {
        
        numberOfLayers = GameObject.Find("Model").transform.Find("Layers").transform.childCount;

        //position tool around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        CreateButtons();
        PlaceMenu();
        ResetButtons();
        
    }

    private void CreateButtons()
    {
        Transform buttonPrefab = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons/ButtonPrefab").transform;
        
        //create buttons in list
        for(int i = 0; i< (numberOfLayers-1); i++)
        {
            //duplicate prefab button
            Transform newButton = Instantiate(buttonPrefab, (buttonPrefab.position - new Vector3(0f, buttonCentreToCentre* (i+1), 0f)), buttonPrefab.rotation);
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

    public void ToggleLayer(GameObject pressedButton)
    {
        Material[] matList = new Material[1]; 

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

    private void ResetButtons()
    {
        GameObject button;
        Material[] matList = new Material[1];
                      
        for (int i= 0;i< numberOfLayers;i++) 
        {
            button = GameObject.Find("Controls").transform.Find("Home/LayerSelector/Buttons").transform.GetChild(i).transform.GetChild(0).gameObject;

            if (GameObject.Find("Model").transform.Find("Layers").transform.GetChild(i).gameObject.activeSelf) //if layer visible
            {
                matList[0] = buttonOn;
                button.GetComponent<MeshRenderer>().materials = matList; //set button material to show layer on
            }
            else
            {
                matList[0] = buttonOff;
                button.GetComponent<MeshRenderer>().materials = matList; //set button material to show layer off
            }
            
        }
    }




}
