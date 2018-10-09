using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Used to read and store information from xml for each patient
/// </summary>
public class ReadXml : MonoBehaviour {

    ///<summary> Procedure type which corresponds to which scene will be loaded for registration tools </summary>
    private string procedureType;
    ///<summary> Patient ID for creating project folder in the file browser </summary>
    private string patientID;
    ///<summary> List of model names (.obj files identified in xml) </summary>
    private List<string> models;
    ///<summary> Vector of point coordinates listed in xml </summary>
    private List<Vector3> points;
    ///<summary> String of point name, corresponds to matching index in points list </summary>
    private List<string> pointNames;
    ///<summary> Vector of target coordinates listed in xml </summary>
    private List<Vector3> targets;
    ///<summary> String of target name, corresponds to matching index in targets list </summary>
    private List<string> targetNames;

    private void Start() //for testing
    {
        //TODO: removes this once Slicer connection established, only used for testing in OpenIGTLink Scene to test UI
        if (Application.loadedLevelName == "OpenIGTLink")
        {
            DirectoryInfo newDir = new DirectoryInfo(Application.persistentDataPath + "/PatientData/SDH Demo");
            readXML(newDir);
            GameObject.Find("FileMenu").GetComponent<ModelLoaderMenu>().displayXmlData();
        }
        
    }

    ///<summary>
    /// Read information from xml file sent from Slicer and stored in patient folder
    /// TODO: this method will need to be changed and used to create patient folders based on data in xml
    ///       this will need to be done once the connection with Slicer is established 
    ///</summary>
    ///<param name="patientDir"> Directory containing the .xml file and all other .obj files </param>
    public void readXML(DirectoryInfo patientDir)
    {
        StreamReader sr;
        XmlDocument xmlDoc;

        //lists of all assets that should be provided by Slicer
        models = new List<string>();
        points = new List<Vector3>(); 
        pointNames = new List<string>(); 
        targets = new List<Vector3>(); 
        targetNames = new List<string>(); 

        //for each .xml file in the directory
        foreach (FileInfo xmlFile in patientDir.GetFiles("*.xml"))
        {
            sr = xmlFile.OpenText();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sr.ReadToEnd());
            //get array of patient nodes - should only be one per xml file
            XmlNodeList procedureList = xmlDoc.GetElementsByTagName("QuickNavProcedure");
            //get procedure type and patient id
            //TODO: in future, this data will be used to create a patient folder in the proper location to store models
            procedureType = procedureList[0].Attributes["Type"].Value;
            patientID = procedureList[0].Attributes["StudyId"].Value;

            //get array of assets - should only be one set per xml file
            XmlNodeList allAssetsList = xmlDoc.GetElementsByTagName("Assets");
            foreach (XmlNode assets in allAssetsList)
            {
                //access each individual asset and sort by type
                XmlNodeList assetList = assets.ChildNodes;
                foreach (XmlNode asset in assetList)
                {
                    if (asset.Name == "Model")
                    {
                        //save file name to list of models
                        models.Add(asset.Attributes["File"].Value);
                    }

                    if (asset.Name == "Point")
                    {
                        //create vector from point values
                        Vector3 point = new Vector3(float.Parse(asset.Attributes["x"].Value), float.Parse(asset.Attributes["y"].Value), float.Parse(asset.Attributes["z"].Value));
                        //save name of point and vector in list of points and point names
                        points.Add(point);
                        pointNames.Add((asset.Attributes["Id"].Value));
                    }

                    if (asset.Name == "Target")
                    {
                        //create vector from point values
                        Vector3 target = new Vector3(float.Parse(asset.Attributes["x"].Value), float.Parse(asset.Attributes["y"].Value), float.Parse(asset.Attributes["z"].Value));
                        //save name of point and vector in list of targets and target names
                        targets.Add(target);
                        targetNames.Add((asset.Attributes["Id"].Value));
                    }
                }
            }

        }
    }
    

    //--------------------------------------------------------------------
    //acessors
    
    public string getProcedureType()
    {
        return procedureType;
    }

    public string getPatientID()
    {
        return patientID;
    }

    public List<string> getListOfModels()
    {
        return models;
    }

    public List<Vector3> getListOfPoints()
    {
        return points;
    }

    public List<string> getListOfPointNames()
    {
        return pointNames;
    }

    public List<Vector3> getListOfTargets()
    {
        return targets;
    }

    public List<string> getListOfTargetNames()
    {
        return targetNames;
    }
}
