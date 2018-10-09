using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ReadXml : MonoBehaviour {

    private string procedureType;
    private string patientID;
    private List<string> models;
    private List<Vector3> points;
    private List<string> pointNames;
    private List<Vector3> targets;
    private List<string> targetNames;

    private void Start() //for testing
    {
        if(Application.loadedLevelName == "OpenIGTLink")
        {
            DirectoryInfo newDir = new DirectoryInfo(Application.persistentDataPath + "/PatientData/SDH Demo");
            readXML(newDir);
            GameObject.Find("FileMenu").GetComponent<ModelLoaderMenu>().displayXmlData();
        }
        
    }

    public void readXML(DirectoryInfo newDir)
    {
        StreamReader sr;
        XmlDocument xmlDoc;
        models = new List<string>();
        points = new List<Vector3>();
        pointNames = new List<string>();

        foreach (FileInfo xmlFile in newDir.GetFiles("*.xml"))
        {
            sr = xmlFile.OpenText();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sr.ReadToEnd());
            //get array of patient nodes - should only be one per xml file
            XmlNodeList procedureList = xmlDoc.GetElementsByTagName("QuickNavProcedure");
            //get procedure type and patient id
            procedureType = procedureList[0].Attributes["Type"].Value;
            patientID = procedureList[0].Attributes["StudyId"].Value;

            //get array of assets - should only be one set per xml file
            XmlNodeList allAssetsList = xmlDoc.GetElementsByTagName("Assets");
            foreach (XmlNode assets in allAssetsList)
            {
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
                        //save name of point and vector in list of points
                        points.Add(point);
                        pointNames.Add((asset.Attributes["Id"].Value));
                    }

                    if (asset.Name == "Target")
                    {
                        //create vector from point values
                        Vector3 target = new Vector3(float.Parse(asset.Attributes["x"].Value), float.Parse(asset.Attributes["y"].Value), float.Parse(asset.Attributes["z"].Value));
                        //save name of point and vector in list of points
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
