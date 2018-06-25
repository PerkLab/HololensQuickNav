using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelLoaderMenu : MonoBehaviour {

    private string filePath;
    private List<string> assets;
    private ReadXml xml;

    private void Awake()
    {


        //initialize filePath
        filePath = Application.persistentDataPath + "/PatientData";

        //clear list of assets
        GameObject.Find("PatientID").GetComponent<TextMesh>().text = "PatientID:";
        GameObject.Find("Procedure").GetComponent<TextMesh>().text = "Procedure:";
        foreach(TextMesh textMesh in GameObject.Find("Assets").GetComponentsInChildren<TextMesh>())
        {
            textMesh.text = "";
        }
        foreach (TextMesh textMesh in GameObject.Find("Status").GetComponentsInChildren<TextMesh>())
        {
            textMesh.text = "";
        }
        //display patient id and procedure as not loaded
        GameObject.Find("Status").transform.GetChild(0).GetComponent<TextMesh>().text = "---";
        GameObject.Find("Status").transform.GetChild(1).GetComponent<TextMesh>().text = "---";

    }

    public void displayXmlData()
    {
        //access xml file
        xml = GameObject.Find("OpenIGTLink").GetComponent<ReadXml>();
        assets = new List<string>();

        //update filePath
        filePath = filePath + "/" + xml.getProcedureType() + "/" + xml.getPatientID();

        //get patient id and procedure, show that values are loaded
        GameObject.Find("PatientID").GetComponent<TextMesh>().text = "PatientID: " + xml.getPatientID();
        GameObject.Find("Procedure").GetComponent<TextMesh>().text = "Procedure: " + xml.getProcedureType();
        GameObject.Find("Status").transform.GetChild(0).GetComponent<TextMesh>().text = "yes";
        GameObject.Find("Status").transform.GetChild(1).GetComponent<TextMesh>().text = "yes";

        //get full list of assets
        foreach (string model in xml.getListOfModels())
        {
            assets.Add(model);
        }

        foreach (string point in xml.getListOfPointNames())
        {
            assets.Add(point);
        }

        foreach (string target in xml.getListOfTargetNames())
        {
            assets.Add(target);
        }

        for (int i=0;i<assets.Count;i++)
        {
            //set asset names and show if loaded
            GameObject.Find("Assets").transform.GetChild(i).GetComponent<TextMesh>().text = assets[i];
            //if an obj file, show not loaded until recieved over connection
            if (assets[i].Contains(".obj"))
                GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "---";   //first two children in status object display status for patient id and procedure type
            else
                GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "yes";
        }
    }

    public void updateLoadStatus()
    {
        FileInfo objFile;

        for(int i=0;i<assets.Count;i++)
        {
            //if the asset is an obj file, check if it has been loaded into the patient directory
            if (assets[i].Contains(".obj"))
            {
                objFile = new FileInfo(filePath + "/" + assets[i]);
                //update the load status on the menu
                if (objFile.Exists)
                    GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "yes";
                else
                    GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "---";
            }
        }
    }
}
