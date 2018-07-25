using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Used to call OBJ Loader and create game objects from .obj files stored in selected directory
/// </summary>
public class LoadModels : MonoBehaviour {

    ///<summary> Display object in UI to tell user the model is being loaded </summary>
    GameObject loadingScreen;

    private void Start()
    {
        //store object location of loadingScreen
        loadingScreen = GameObject.Find("Loading").transform.Find("LoadingLogo").gameObject;
    }

    ///<summary>
    /// Starts coroutine for process to load .obj files
    ///</summary>
    ///<param name="directory"> Directory containing the .obj files </param>
    ///<param name="directoryFilePath"> Full file path to directory containing .obj files </param>
    public void Load(DirectoryInfo directory, string directoryFilePath)
    {
        StartCoroutine(StartLoadingOBJs(directory, directoryFilePath));
    }


    //----------------------------------------------------------------------------------------------------------------
    //object loading

    ///<summary>
    /// Controls display of loading screen and calls method to load .obj files
    ///</summary>
    ///<param name="directory"> Directory containing the .obj files </param>
    ///<param name="directoryFilePath"> Full file path to directory containing .obj files </param>
    IEnumerator StartLoadingOBJs(DirectoryInfo directory, string directoryFilePath)
    {
        //destory any existing objects in the scene from previously loaded .obj files
        foreach (Transform child in GameObject.Find("LoadedModels").transform)
        {
            Destroy(child.gameObject);
        }
        //wait until all objects are destroyed 
        yield return new WaitUntil(() => GameObject.Find("LoadedModels").transform.childCount == 0);

        //display loading screen and wait until visible 
        loadingScreen.SetActive(true);
        yield return new WaitUntil(() => loadingScreen.activeSelf == true);

        //initialize variable to count number of .obj files to load
        int numObjects = 0; 

        //check all files in directory for .obj files
        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name.Contains(".obj")) //if an obj file
            {
                //call method to load .obj into scene and apply material 
                LoadOBJ(directoryFilePath + "/" + file.Name, file.Name);
                //increase object count
                numObjects++;
            }
        }

        //wait until all models in the directory are loaded into the scene
        //then hide loading screen
        GameObject loadedModels = GameObject.Find("LoadedModels");
        yield return new WaitUntil(() => loadedModels.transform.childCount == numObjects);
        loadingScreen.SetActive(false);
    }

    ///<summary>
    /// Calls method to parse .obj file and applies transforms and material to loaded mesh
    ///</summary>
    ///<param name="filePath"> File path to .obj file </param>
    ///<param name="fileName"> Name of .obj file and object to be created </param>
    private void LoadOBJ(string filePath, string fileName)
    {
        /* 
         * Note that there was difficulty using the OBJImport asset with long file paths
         * To fix this, a temporary copy of the .obj file is created in the LocalState file directory (persistentDataPath)
         * Once parsed by OBJImport the file is destroyed
        */

        //delete existing tempFile file if not properly deleted before
        if (File.Exists(Application.persistentDataPath + "/tempFile.obj"))
            File.Delete(Application.persistentDataPath + "/tempFile.obj");
        //copy .obj file to temporary file location
        File.Copy(filePath, Application.persistentDataPath + "/tempFile.obj");

        //use OBJImport asset to parse .obj file and create a new gameObject in the scene
        GameObject newObj;
        newObj = OBJLoader.LoadOBJFile(Application.persistentDataPath + "/tempFile.obj");
        File.Delete(Application.persistentDataPath + "/tempFile.obj");
        //scale mesh down to adjust for 3DSlicer units 
        newObj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        //position in the centre of the file browser frame
        newObj.transform.position = new Vector3(0.16f, 0.1f, 1.5f);
        //make a child of LoadedModels to be passed into the next scene
        newObj.transform.parent = GameObject.Find("LoadedModels").transform;
        //change object name to the file name
        newObj.name = fileName.Replace(".obj", "");
        //change model material based on type
        /*
         * Note in future more materials can be added to the material manager to accomodate for more models  
        */
        Material[] mat = new Material[1];
        if (newObj.name == "Skin")
            mat[0] = GameObject.Find("InputManager/QuickNavManager/MaterialManager").GetComponent<MaterialManager>().Skin;
        else if (newObj.name == "Brain")
            mat[0] = GameObject.Find("InputManager/QuickNavManager/MaterialManager").GetComponent<MaterialManager>().Brain;
        else
            mat[0] = GameObject.Find("InputManager/QuickNavManager/MaterialManager").GetComponent<MaterialManager>().ROI;

        //depending on the size of the mesh, it may be broken up into parts and loaded as multiple child objects 
        //must access each child mesh renderer to apply material
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
        
    }
}
