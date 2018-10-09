using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

/// <summary>
/// Used to control menu and loading of patient data in file browser scene
/// </summary>
public class FileBrowser : MonoBehaviour
{

    ///<summary> Full filepath to access current directory </summary>
    private string filePath;
    ///<summary> Name of current directory </summary>
    private string directoryName;

    ///<summary> File index based on page number and selector location displayed in UI </summary>
    private int fileListIndex = 0;
    ///<summary> Array of all files/directories in current directory </summary>
    private FileSystemInfo[] allFiles;
    ///<summary> Number of files in current directory </summary>
    private int numFilesInDir;
    ///<summary> Current page displayed in UI </summary>
    private int pageIndex = 0;
    ///<summary> Number of pages of files in directory </summary>
    private int numPages = 0;

    ///<summary> Staring location of the selector at the top of the list </summary>
    private float selectorBaseLocation;
    ///<summary> Distance to move the selector up or down the list </summary>
    private float selectorIncrementDistance = 0.03f;
    ///<summary> Current position of selector (0 being the top) </summary>
    private int selectorPosition = 0;

    ///<summary>
    /// Initialize directories and UI
    ///</summary>
    void Start()
    {
        SetMainDirectory();
        CreateFolders();
        GetCurrentFiles();
        //save the selector location for reference to the top of the list
        selectorBaseLocation = GameObject.Find("FileMenu/Button").transform.position.y;
        //set selector to top of list
        selectorPosition = 0;
        SetSelectorLocation();

        //LoadedModels object will contain all the models loaded into the scene
        //ensure that when the procedure scene is loaded, the models will remain in the new scene
        DontDestroyOnLoad(GameObject.Find("LoadedModels"));
    }

    // ------------------------------------------------------
    // update file names

    ///<summary>
    /// Initialize the file browser by opening main directory "PatientData"
    ///</summary>
    private void SetMainDirectory()
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
        //update directory name on UI, unneccesary to display starting portion of filePath so it is removed
        GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
    }

    ///<summary>
    /// Load names of all patient directories in current directory. 
    /// Calculate number of pages required to display files on UI.
    ///</summary>
    private void GetCurrentFiles()
    {
        DirectoryInfo currentDir = new DirectoryInfo(filePath);
        allFiles = currentDir.GetDirectories();

        numFilesInDir = allFiles.Length;

        fileListIndex = 0;
        pageIndex = 0;
        // UI displays 7 files at a time, calculate number of pages required to display all files
        numPages = Mathf.CeilToInt(numFilesInDir / 7.0f);
        //update UI
        SetFileNames();
    }

    ///<summary>
    /// Display all directory information on UI
    ///</summary>
    private void SetFileNames()
    {
        //clear all file names
        for (int i = 0; i <= 6; i++)
        {
            GameObject.Find("FileMenu/Files").transform.GetChild(i).GetComponent<TextMesh>().text = "";
        }

        //set file names
        int childCount = 0;
        //for each of the 7 files in the UI list, or for each of the files on the last page (if less than 7)
        //set text on UI to name of patient directory 
        for (int i = pageIndex * 7; i <= Mathf.Min(((pageIndex * 7) + 6), (numFilesInDir - 1)); i++)
        {
            GameObject.Find("FileMenu/Files").transform.GetChild(childCount).GetComponent<TextMesh>().text = "[" + allFiles[i].Name + "]";
            childCount++;
        }

        //set page numbers and arrows on UI based on current page and total number of pages

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

    ///<summary>
    /// Open file corresponding to selector position and page number. Load patient data or open new directory as needed.
    ///</summary>
    public void SelectFile()
    {
        //set new directory based on name of file at current file index
        DirectoryInfo newDir = new DirectoryInfo(filePath + "/" + allFiles[fileListIndex].Name);
        //directory contains models/files (check for files, not directories)
        //therefore is a patient directory, load patient information into scene
        if (newDir.GetFiles().Length > 0) 
        {
            //load all files and xml information
            GameObject.Find("InputManager/QuickNavManager/FileBrowser").GetComponent<LoadModels>().Load(newDir, (filePath + "/" + allFiles[fileListIndex].Name));
            GameObject.Find("LoadedModels").GetComponent<LocationValues>().readXMLPoints(newDir);
        }
        //directory does not include individual files for patient data
        else 
        {
            //if the directory contains other directories (multiple patients), load the new directory and display on the UI
            if (newDir.GetDirectories().Length > 0)
            {
                //update file path, directory name, and current files
                filePath = filePath + "/" + allFiles[fileListIndex].Name;
                directoryName = "/" + allFiles[fileListIndex].Name;
                GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
                GetCurrentFiles();
                SetFileNames();
                //set selector position to the top of the list 
                selectorPosition = 0;
                SetSelectorLocation();
                
            }
        }
    }

    ///<summary>
    /// Return to the last directory (parent of current directory) and update UI.
    ///</summary>
    public void LastDirectory()
    {
        //if not the main base directory
        if (filePath != Application.persistentDataPath + "/PatientData")
        {
            //remove current directory from file path
            filePath = filePath.Replace(directoryName, "");
            //set directory name to the last string in the file path
            //use "/" to split ".../directoryName/" and store final string in list
            string[] fileNames = filePath.Split('/');
            directoryName = "/" + fileNames[fileNames.Length - 1];

            //update UI and move selector to top of the list
            GetCurrentFiles();
            SetFileNames();
            selectorPosition = 0;
            SetSelectorLocation();
            //update the directory name on the UI
            GameObject.Find("FileMenu").transform.Find("DirectoryName").GetComponent<TextMesh>().text = filePath.Replace(Application.persistentDataPath, "");
        }

    }

    // ------------------------------------------------------
    // navigate list of files

    ///<summary>
    /// Increment file index and move selector down the page on the UI
    ///</summary>
    public void MoveDownList()
    {
        //if selector is not already at the bottom of the page
        if (selectorPosition != 6)
        {
            //if on the last page and reached the last file index on the last page
            if (pageIndex == (numPages - 1) && selectorPosition == (numFilesInDir % 7 - 1))
            {
                //do nothing
            }
            else
            {
                //increase index and move selector down list 
                fileListIndex++;
                selectorPosition++;
                SetSelectorLocation();
            }
        }

    }

    ///<summary>
    /// Increment file index and move selector up the page on the UI
    ///</summary>
    public void MoveUpList()
    {
        //if not already at the top of the page
        if (selectorPosition != 0)
        {
            fileListIndex--;
            selectorPosition--;
            SetSelectorLocation();
        }

    }

    ///<summary>
    /// Display next page of file names on UI and increment file index
    ///</summary>
    public void NextPage()
    {
        //if not already on the last page
        if (pageIndex != (numPages - 1))
        {
            //move selector to top of page
            selectorPosition = 0;
            SetSelectorLocation();

            //increment page index and update files on UI
            pageIndex++;
            SetFileNames();

            //update file index to file at the top of the new page
            fileListIndex = pageIndex * 7;
        }
    }

    ///<summary>
    /// Display previous page of file names on UI and increment file index
    ///</summary>
    public void PreviousPage()
    {
        //if not on the first page
        if (pageIndex != 0)
        {
            //move selector to top of page
            selectorPosition = 0;
            SetSelectorLocation();

            //increment page index and update files on UI
            pageIndex--;
            SetFileNames();

            //update file index to file at the top of the new page
            fileListIndex = pageIndex * 7;
        }
    }

    ///<summary>
    /// Move selector object in space to correspond to position in list
    ///</summary>
    private void SetSelectorLocation()
    {
        //maintain existing x and z componets
        //update y component based on base location, selector position/index and increment distance
        GameObject.Find("FileMenu/Button").transform.position = new Vector3(GameObject.Find("FileMenu/Button").transform.position.x,
                                                                       (selectorBaseLocation - selectorIncrementDistance * selectorPosition),
                                                                        GameObject.Find("FileMenu/Button").transform.position.z);
    }

    // ------------------------------------------------------
    // create folders for testing
    ///<summary>
    /// TODO: this will be removed after establishing connection with Slicer, used currently for testing purposes
    ///</summary>
    private void CreateFolders()
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