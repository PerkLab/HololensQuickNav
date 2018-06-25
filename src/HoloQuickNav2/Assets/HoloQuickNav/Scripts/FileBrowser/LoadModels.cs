using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class LoadModels : MonoBehaviour {

    float time;
    bool Waiting;
    GameObject loadingScreen;


    private void Start()
    {
        loadingScreen = GameObject.Find("Loading").transform.Find("LoadingLogo").gameObject;
    }

    public void Load(DirectoryInfo newDir, string filePath)
    {
        //clear all existing children

        StartCoroutine(loadOBJ(newDir, filePath));
    }
   

    //----------------------------------------------------------------------------------------------------------------
    //object loading

    IEnumerator loadOBJ(DirectoryInfo directory, string directoryFilePath)
    {
        foreach(Transform child in GameObject.Find("LoadedModels").transform)
        {
            Destroy(child.gameObject);
        }
        /*
        while (GameObject.Find("LoadedModels").transform.childCount > 0)
        {
            Destroy(GameObject.Find("LoadedModels").transform.GetChild(0).gameObject);
        }*/
        yield return new WaitUntil(() => GameObject.Find("LoadedModels").transform.childCount == 0);
        
        loadingScreen.SetActive(true);
        yield return new WaitUntil(() => loadingScreen.activeSelf == true);

        int numObjects = 0; 

        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name.Contains(".obj")) //if an obj file
            {
                loadOBJ(directoryFilePath + "/" + file.Name, file.Name);
                numObjects++;
            }
        }
        GameObject loadedModels = GameObject.Find("LoadedModels");
        yield return new WaitUntil(() => loadedModels.transform.childCount == numObjects);
        loadingScreen.SetActive(false);
    }

    private void loadOBJ(string path, string name)
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
        newObj.transform.position = new Vector3(0.31f, 0.1f, 1.5f);
        newObj.transform.parent = GameObject.Find("LoadedModels").transform;
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

        //loadingScreen.SetActive(false);
        
    }



    private void Wait(int seconds)
    {
        time = 0;
        Waiting = true;
        while (time < 5)
        {
            //wait
        }
        Waiting = false;
    }

    private void Update()
    {
        if (!Waiting)
            return;
        else
            time += Time.deltaTime;
    }
}
