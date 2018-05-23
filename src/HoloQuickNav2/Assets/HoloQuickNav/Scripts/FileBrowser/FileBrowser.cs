using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileBrowser : MonoBehaviour
{

    private string filePath;
    private string directoryName;

    private int fileListIndex = 0;
    private FileSystemInfo[] allFiles;
    private int numFilesInDir;
    private int pageIndex = 0;
    private int numPages = 0;

    private float selectorBaseLocation;
    private float selectorIncrementDistance = 0.03f;
    private int selectorPosition = 0;

    // Use this for initialization
    void Start()
    {
        //set initial directory
        setMainDirectory();
        createTestFolder();
        getCurrentFiles();
        selectorBaseLocation = GameObject.Find("FileMenu/Button").transform.position.y;
        selectorPosition = 0;
        setSelectorLocation();

    }

    // ------------------------------------------------------
    // update file names

    private void setMainDirectory()
    {
        //if main directory doesn't exist create it
        DirectoryInfo patientDataDir = new DirectoryInfo(Application.persistentDataPath + "/PatientData");
        directoryName = "/PatientData";

        if (!patientDataDir.Exists)
        {
            patientDataDir.Create();
        }

        //set initial file path to main directory 
        filePath = Application.persistentDataPath + "/PatientData";

        GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
    }

    private void getCurrentFiles()
    {
        DirectoryInfo currentDir = new DirectoryInfo(filePath);
        allFiles = currentDir.GetDirectories();

        numFilesInDir = allFiles.Length;

        fileListIndex = 0;
        pageIndex = 0;
        numPages = Mathf.CeilToInt(numFilesInDir / 7.0f);
        setFileNames();
    }

    private void setFileNames()
    {
        //clear all file names
        for (int i = 0; i <= 6; i++)
        {
            GameObject.Find("FileMenu/Files").transform.GetChild(i).GetComponent<TextMesh>().text = "";
        }

        //set file names
        int childCount = 0;
        for (int i = pageIndex * 7; i <= Mathf.Min(((pageIndex * 7) + 6), (numFilesInDir - 1)); i++)
        {
            GameObject.Find("FileMenu/Files").transform.GetChild(childCount).GetComponent<TextMesh>().text = "[" + allFiles[i].Name + "]";
            childCount++;
        }

        //set page numbers and arrows

        GameObject.Find("FileMenu/PageNumber").GetComponent<TextMesh>().text = (pageIndex + 1) + "/" + numPages;


        if (pageIndex == 0)
            GameObject.Find("FileMenu").transform.Find("LeftArrow").gameObject.SetActive(false);
        else
            GameObject.Find("FileMenu").transform.Find("LeftArrow").gameObject.SetActive(true);
        if (pageIndex == (numPages - 1))
            GameObject.Find("FileMenu").transform.Find("RightArrow").gameObject.SetActive(false);
        else
            GameObject.Find("FileMenu").transform.Find("RightArrow").gameObject.SetActive(true);

    }

    // ------------------------------------------------------
    // open directories/create models

    public void selectFile()
    {
        DirectoryInfo newDir = new DirectoryInfo(filePath + "/" + allFiles[fileListIndex].Name);

        if (newDir.GetDirectories().Length > 0) //directory contains more than one set of models
        {
            filePath = filePath + "/" + allFiles[fileListIndex].Name;
            directoryName = "/" + allFiles[fileListIndex].Name;
            getCurrentFiles();
            setFileNames();
            selectorPosition = 0;
            setSelectorLocation();
            GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
        }
        else //directory contains models for one patient
        {
            if (newDir.GetFiles().Length > 0) //if directory contains files
            {
                //show loading screen
                //GameObject.Find("LoadingLogo").transform.position += new Vector3(-5f, 0f, 0f);

                //check if the patient already has meshes
                bool hasMesh = false;
                foreach (FileInfo file in newDir.GetFiles())
                {
                    if (file.Name.Contains("mesh_"))
                        hasMesh = true;
                }

                if (hasMesh)
                {
                    foreach (FileInfo file in newDir.GetFiles())
                    {
                        if (file.Name.Contains("mesh_")) //if a mesh
                        {
                            loadMeshes(filePath + "/" + allFiles[fileListIndex].Name + "/" + file.Name, file.Name);
                        }
                    }
                }
                else
                {
                    foreach (FileInfo file in newDir.GetFiles())
                    {
                        if (file.Name.Contains(".obj")) //if an obj file
                        {
                            loadModel(filePath + "/" + allFiles[fileListIndex].Name + "/" + file.Name, file.Name);
                        }
                    }
                    //saveMeshes();
                }


                //hide loading screen
                //GameObject.Find("LoadingLogo").transform.position += new Vector3(5f, 0f, 0f);
            }
        }
    }

    public void lastDirectory()
    {
        if (filePath != Application.persistentDataPath + "/PatientData")
        {
            filePath = filePath.Replace(directoryName, "");
            string[] fileNames = filePath.Split('/');
            directoryName = "/" + fileNames[fileNames.Length - 1];

            getCurrentFiles();
            setFileNames();
            selectorPosition = 0;
            setSelectorLocation();

            GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
        }

    }

    private void loadModel(string path, string name)
    {


        //copy file and load object 
        if (File.Exists(Application.persistentDataPath + "tempFile.obj"))
            File.Delete(Application.persistentDataPath + "tempFile.obj");
        File.Copy(path, Application.persistentDataPath + "tempFile.obj");
        GameObject newObj;
        newObj = OBJLoader.LoadOBJFile(Application.persistentDataPath + "tempFile.obj");
        File.Delete(Application.persistentDataPath + "tempFile.obj");

        //change model location
        newObj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        newObj.transform.position = new Vector3(0.2f, 0.1f, 1.5f);
        newObj.transform.parent = GameObject.Find("WorldAnchor/Model/Layers").transform;
        newObj.name = name.Replace(".obj", "");

        //change model material based on type
        Material[] mat = new Material[1];
        if (newObj.name == "Skin")
            mat[0] = GameObject.Find("InputManager/MaterialManager").GetComponent<MaterialManager>().Skin;
        else if (newObj.name == "Brain")
            mat[0] = GameObject.Find("InputManager/MaterialManager").GetComponent<MaterialManager>().Brain;
        else
            mat[0] = GameObject.Find("InputManager/MaterialManager").GetComponent<MaterialManager>().ROI;

        if (newObj.transform.childCount > 0)
        {
            foreach (Transform child in newObj.transform)
            {
                child.GetComponent<MeshRenderer>().materials = mat;
            }
        }
        else
        {
            newObj.GetComponent<MeshRenderer>().materials = mat;
        }

        //save mesh to binary file

    }

    private void saveMeshes()
    {
        /*
        foreach(Transform meshParent in GameObject.Find("WorldAnchor/Model/Layers").transform.GetComponentsInChildren<Transform>())
        {
            foreach(Transform meshChild in meshParent.GetComponentInChildren<Transform>())
            {
                Mesh meshToSave = meshChild.GetComponent<MeshFilter>().mesh;
                SaveMeshes.SaveMesh(filePath + "/mesh_" + meshParent.name + "_" + meshChild.name, meshToSave);
            }
        }
        */

    }

    private void loadMeshes(string path, string name)
    {
        //meshes saved as "mesh_Skin_grp1"
        //parse name to create objects
        /*
        string[] meshNames = name.Split('_');

        if(GameObject.Find("WorldAnchor/Model/Layers").transform.Find(meshNames[1]) == null) //parent object doesn't exist
        {
            GameObject newParent = new GameObject(meshNames[1]);
            newParent.transform.parent = GameObject.Find("WorldAnchor/Model/Layers").transform;
        }
        
        GameObject meshObject = new GameObject(meshNames[2]);
        meshObject.transform.parent = GameObject.Find("WorldAnchor/Model/Layers/" + meshNames[1]).transform;
        meshObject.AddComponent<MeshFilter>();
        meshObject.AddComponent<MeshRenderer>();
        meshObject.GetComponent<MeshFilter>().mesh = SaveMeshes.LoadMesh(path);
        */
    }

    // ------------------------------------------------------
    // navigate list of files

    public void moveDownList()
    {

        if (selectorPosition != 6)//&& selectorPosition != (numFilesInDir % 7))
        {
            if (pageIndex == (numPages - 1) && selectorPosition == (numFilesInDir % 7 - 1))
            {
                //do nothing
            }
            else
            {
                fileListIndex++;
                selectorPosition++;
                setSelectorLocation();
            }
        }

    }

    public void moveUpList()
    {
        if (selectorPosition != 0)
        {
            fileListIndex--;
            selectorPosition--;
            setSelectorLocation();
        }


    }

    public void nextPage()
    {
        if (pageIndex != (numPages - 1))
        {
            selectorPosition = 0;
            setSelectorLocation();

            pageIndex++;
            setFileNames();

            fileListIndex = pageIndex * 7;
        }
    }

    public void lastPage()
    {
        if (pageIndex != 0)
        {
            selectorPosition = 0;
            setSelectorLocation();

            pageIndex--;
            setFileNames();

            fileListIndex = pageIndex * 7;
        }
    }

    private void setSelectorLocation()
    {
        GameObject.Find("FileMenu/Button").transform.position = new Vector3(GameObject.Find("FileMenu/Button").transform.position.x,
                                                                       (selectorBaseLocation - selectorIncrementDistance * selectorPosition),
                                                                        GameObject.Find("FileMenu/Button").transform.position.z);
    }

    // ------------------------------------------------------
    // create folders for testing

    private void createTestFolder()
    {
        //create 5 folders
        DirectoryInfo testDir;
        for (int i = 0; i <= 4; i++)
        {
            testDir = new DirectoryInfo(filePath + "/test" + i);
            if (!testDir.Exists)
            {
                testDir.Create();
            }
        }

        for (int i = 0; i <= 5; i++)
        {
            testDir = new DirectoryInfo(filePath + "/test1/test1-" + i);
            if (!testDir.Exists)
            {
                testDir.Create();
            }
        }


        /*
        // create 6 text files
        for (int i = 0; i <= 12; i++)
        {
            StreamWriter sw = File.AppendText(Application.persistentDataPath + "/PatientData/text" + i + ".txt");
            sw.AutoFlush = true;
            sw.WriteLine(i);
            sw.Dispose();
        }

        
    */

    }


}