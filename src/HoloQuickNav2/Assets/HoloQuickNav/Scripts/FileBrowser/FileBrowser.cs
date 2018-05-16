using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileBrowser : MonoBehaviour {

    private string filePath;
    private FileInfo[] currentFiles;
    private DirectoryInfo[] currentDirectories;
    private int fileListIndex = 0;
    private FileSystemInfo[] allFiles;
    private int numFilesInDir;

    private float selectorBaseLocation;
    private float selectorIncrementDistance = 0.03f;
    private int selectorPosition = 0;


    // Use this for initialization
    void Start () {
        //set initial directory
        setMainDirectory();
        createTestFolder();
        getCurrentFiles();
        selectorBaseLocation = GameObject.Find("Menu/Button").transform.position.y;
        selectorPosition = 0;
        setSelectorLocation();
        
	}

    private void setMainDirectory()
    {
        //if main directory doesn't exist create it
        DirectoryInfo patientDataDir = new DirectoryInfo(Application.persistentDataPath + "/PatientData");
        
        if (!patientDataDir.Exists)
        {
            patientDataDir.Create();
        }
        
        //set initial file path to main directory 
        filePath = Application.persistentDataPath + "/PatientData";
    }

    private void getCurrentFiles()
    {
        DirectoryInfo currentDir = new DirectoryInfo(filePath);
        currentDirectories = currentDir.GetDirectories();
        currentFiles = currentDir.GetFiles();

        numFilesInDir = currentDirectories.Length + currentFiles.Length;
        allFiles = new FileSystemInfo[numFilesInDir];

        int index = 0;

        foreach (DirectoryInfo dir in currentDirectories)
        {
            allFiles[index] = dir;
            index++;
        }

        foreach (FileInfo file in currentFiles)
        {
            allFiles[index] = file;
            index++;
        }

        fileListIndex = 0;
        setFileNames();
    }

    private void setFileNames()
    {
        if(numFilesInDir <= 7) //all file names fit in the window
        {
            for (int i = 0; i <= numFilesInDir; i++)
            {
                if ((allFiles[i].Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    GameObject.Find("Menu/Files").transform.GetChild(i).GetComponent<TextMesh>().text = "[" + allFiles[i].Name + "]";
                }
                else
                {
                    GameObject.Find("Menu/Files").transform.GetChild(i).GetComponent<TextMesh>().text = allFiles[i].Name;
                }
            }
        }
        else //display 7 files after the file list index
        {
            int childCount = 0;

            for (int i = fileListIndex; i <= (fileListIndex + 6); i++)
            {
                if ((allFiles[i].Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    GameObject.Find("Menu/Files").transform.GetChild(childCount).GetComponent<TextMesh>().text = "[" + allFiles[i].Name + "]";
                }
                else
                {
                    GameObject.Find("Menu/Files").transform.GetChild(childCount).GetComponent<TextMesh>().text = allFiles[i].Name;
                }
                childCount++;
            }
        }
        
    }

    public void moveDownList()
    {
        fileListIndex++;

        if (fileListIndex >= numFilesInDir) //end of list reached
        {
            //restart at top of list
            fileListIndex = 0;
            setFileNames();
            selectorPosition = 0;
            setSelectorLocation();
        }
        else if(fileListIndex == 7) //start of new page
        {
            setFileNames();
            selectorPosition = 0;
            setSelectorLocation();
        }
        else //move selector down list
        {
            selectorPosition++;
            if (selectorPosition > 7)
                selectorPosition = 0;
            setSelectorLocation();
            
        }
    }

    private void setSelectorLocation()
    {
        GameObject.Find("Menu/Button").transform.position = new Vector3(GameObject.Find("Menu/Button").transform.position.x,
                                                                       (selectorBaseLocation - selectorIncrementDistance * selectorPosition),
                                                                        GameObject.Find("Menu/Button").transform.position.z);
    }

    private void createTestFolder()
    {
        //create 5 folders
        DirectoryInfo testDir;
        for(int i =0;i<=4;i++)
        {
            testDir = new DirectoryInfo(filePath + "/test" + i);
            if (!testDir.Exists)
            {
                testDir.Create();
            }
        }

        // create 6 text files
        for (int i =0;i<=5;i++)
        {
            StreamWriter sw = File.AppendText(Application.persistentDataPath + "/PatientData/text" + i + ".txt");
            sw.AutoFlush = true;
            sw.WriteLine(i);
            sw.Dispose();
        }

    }
}