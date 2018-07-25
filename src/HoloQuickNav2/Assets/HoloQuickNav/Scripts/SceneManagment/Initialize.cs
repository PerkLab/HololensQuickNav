using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine;

/// <summary>
/// Prepare models loaded in filebrowser to be manipulated by tools in the scene
/// </summary>
public class Initialize : MonoBehaviour {
    

	void OnEnable () {
        //move loaded models under proper parent object in scene
        while(GameObject.Find("LoadedModels").transform.childCount > 0)
        {
            //position each model in front of the user 
            GameObject.Find("LoadedModels").transform.GetChild(0).transform.position = new Vector3(0f, 0.1f, 1.5f);
            GameObject.Find("LoadedModels").transform.GetChild(0).transform.parent = GameObject.Find("WorldAnchor/Model").transform.Find("Layers");
        }
        //set skin as child[0] to ensure it is visible while manipulating the models 
        GameObject.Find("WorldAnchor/Model").transform.Find("Layers/Skin").SetSiblingIndex(0);

        //move model so tip of patient's nose is the origin for rotation
        //fiducial[0] will always be used, must be specified in proper order in .xml from Slicer
        Vector3[] fiducials = GameObject.Find("LoadedModels").GetComponent<LocationValues>().getFiducials();
        if(fiducials != null)
        {
            //shift by each x,y,z value but also maintain original (0, 0.1, 1.5) shift so model is infront of user
            GameObject.Find("WorldAnchor/Model").transform.Find("Layers").transform.position = new Vector3(-fiducials[0].x, -fiducials[0].y + 0.1f, -fiducials[0].z + 1.5f);
        }
        
        //if any targets must be placed, create them
        CreateTargets();

    }

    /// <summary>
    /// Create required number of objects to represent targets and position according to .xml data
    /// </summary>
    private void CreateTargets()
    {
        //load target values from the location values component loaded in the file browser
        Vector3[] targets = GameObject.Find("LoadedModels").GetComponent<LocationValues>().getTargets();

        //if there is at least one target
        if (targets.Length > 0)
        {
            //create correct number of targets by copying the existing prefab
            Transform targetPrefab = GameObject.Find("Model").transform.Find("Layers/Targets/Target").transform;
            for (int i = 0; i < (targets.Length-1); i++)
            {
                //duplicate prefab target
                Transform newTarget = Instantiate(targetPrefab, targetPrefab.position, targetPrefab.rotation);
                //make it a child of the targets in the model
                newTarget.parent = GameObject.Find("Model").transform.Find("Layers/Targets").transform;
            }

            //position targets using values from .xml
            for (int i = 0; i < targets.Length; i++)
            {
                Transform target = GameObject.Find("Model").transform.Find("Layers/Targets").transform.GetChild(i).transform;
                target.position += new Vector3(targets[i].x, targets[i].y, targets[i].z);
            }
        }
        //there are no targets so hide the prefab object from view
        else
        {
            GameObject.Find("Model").transform.Find("Layers/Targets/Target").gameObject.SetActive(false);
            GameObject.Find("Model").transform.Find("Layers/Targets").gameObject.SetActive(false);
        }
       
    } 

}
