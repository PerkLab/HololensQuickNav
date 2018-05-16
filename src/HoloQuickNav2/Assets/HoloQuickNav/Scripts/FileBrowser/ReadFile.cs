using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ReadFile : MonoBehaviour {


    public GameObject cam;
    private GameObject objectNew;
    public Material skinMat;
    private static StreamWriter sw;
   
    // Use this for initialization
    void Start () {

        //WriteObj();
        ReadObj("/Objects/testWrite.obj");
    }
	
	
    public void ReadObj(string filePath)
    {
        if (File.Exists(Application.persistentDataPath + filePath))
        {
            WriteLog.WriteData("file found");

            //create temp copy of file and read 
            CopyObj(Application.persistentDataPath + filePath);
            objectNew = OBJLoader.LoadOBJFile(Application.persistentDataPath + "/tempFile");

            objectNew.transform.position = GameObject.Find("Cube").transform.position;
            objectNew.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            Material[] newMats = new Material[1];
            newMats[0] = skinMat;

            if (objectNew.transform.childCount > 0)
            {
                foreach (Transform child in objectNew.transform)
                {
                    child.GetComponent<MeshRenderer>().materials = newMats;
                }
            }
            else
            {
                objectNew.GetComponent<MeshRenderer>().materials = newMats;
            }

            //delete temp file
            File.Delete(Application.persistentDataPath + "/tempFile");

        }

        else
        {
            WriteLog.WriteData("file not found");
        }

    }

    private void CopyObj(string filePath)
    {
        if (File.Exists(filePath))
        {
            //copy obj file to temp location
            sw = File.AppendText(Application.persistentDataPath + "/tempFile");
            sw.AutoFlush = true;
            foreach (String str in File.ReadAllLines(filePath))
            {
                sw.WriteLine(str);
            }
        }
        else
        {
            WriteLog.WriteData("file not found: " + filePath);
        }
        sw.Dispose();
    }

    public void WriteObj()
    {
        DirectoryInfo objectDir = new DirectoryInfo(Application.persistentDataPath + "/Objects");
        if(!objectDir.Exists)
        {
            objectDir.Create();
        }
        sw = File.AppendText(Application.persistentDataPath + "/testWrite.obj");
        sw.AutoFlush = true;

        //------------------------------------------------------------------------------------------------------

        //String[] objText = File.ReadAllLines(Application.persistentDataPath + "test.obj");
        WriteLog.WriteData("test2");
        foreach (String str in File.ReadAllLines(Application.persistentDataPath + "/test.obj"))
        {
            sw.WriteLine(str);
            
        }

        sw.Dispose();

        WriteLog.WriteData("test3");
    }
}
