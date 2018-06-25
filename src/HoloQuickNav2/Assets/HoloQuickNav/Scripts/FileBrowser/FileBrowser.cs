using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

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
        createFolders();
        getCurrentFiles();
        selectorBaseLocation = GameObject.Find("FileMenu/Button").transform.position.y;
        selectorPosition = 0;
        setSelectorLocation();

        DontDestroyOnLoad(GameObject.Find("LoadedModels"));

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
    // open directories

    public void selectFile()
    {

        DirectoryInfo newDir = new DirectoryInfo(filePath + "/" + allFiles[fileListIndex].Name);

        if (newDir.GetFiles().Length > 0) //directory contains models/files
        {
            //load all files and xml information
            GameObject.Find("InputManager/FileBrowser").GetComponent<LoadModels>().Load(newDir, (filePath + "/" + allFiles[fileListIndex].Name));
            GameObject.Find("LoadedModels").GetComponent<LocationValues>().readXMLPoints(newDir);
        }
        else //directory contains other directories
        {
            if(newDir.GetDirectories().Length > 0)
            {
                filePath = filePath + "/" + allFiles[fileListIndex].Name;
                directoryName = "/" + allFiles[fileListIndex].Name;
                getCurrentFiles();
                setFileNames();
                selectorPosition = 0;
                setSelectorLocation();
                GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
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

    // ------------------------------------------------------
    // load location values

    private void loadFiducials(DirectoryInfo newDir)
    {
        //clear past values
        GameObject.Find("LoadedModels").GetComponent<LocationValues>().clearValues();
        GameObject.Find("LoadedModels").GetComponent<LocationValues>().readXMLPoints(newDir);
        /*
        foreach (FileInfo file in newDir.GetFiles())
        {
            if (file.Name.Contains("fiducials.csv")) //if fiducials for registration
            {
                GameObject.Find("LoadedModels").GetComponent<LocationValues>().readFiducials(file);
            }
            else if (file.Name.Contains("targets.csv")) //if targets
            {
                GameObject.Find("LoadedModels").GetComponent<LocationValues>().readTargets(file);
            }

        }*/
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

    private void createFolders()
    {
        //create 5 folders
        DirectoryInfo testDir;
        testDir = new DirectoryInfo(filePath + "/SDH Demo");
        if (!testDir.Exists)
        {
            testDir.Create();
        }

        testDir = new DirectoryInfo(filePath + "/ClinicalTrials");
        if (!testDir.Exists)
        {
            testDir.Create();
        }

        for (int i = 1;i<=5;i++)
        {
            testDir = new DirectoryInfo(filePath + "/ClinicalTrials/Patient00" + i);
            if (!testDir.Exists)
            {
                testDir.Create();
            }
        }

        testDir = new DirectoryInfo(filePath + "/ClinicalTrials/BTUM_001");
        if (!testDir.Exists)
        {
            testDir.Create();
        }

        testDir = new DirectoryInfo(filePath + "/MRImageSlicing");
        if (!testDir.Exists)
        {
            testDir.Create();
        }

    }


}