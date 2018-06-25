using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine;

public class Initialize : MonoBehaviour {
    

	void OnEnable () {

        

        //move loaded models into scene
        while(GameObject.Find("LoadedModels").transform.childCount > 0)
        {
            GameObject.Find("LoadedModels").transform.GetChild(0).transform.position = new Vector3(0f, 0.1f, 1.5f);
            GameObject.Find("LoadedModels").transform.GetChild(0).transform.parent = GameObject.Find("WorldAnchor/Model").transform.Find("Layers");
        }
        //set skin as child[0]
        GameObject.Find("WorldAnchor/Model").transform.Find("Layers/Skin").SetSiblingIndex(0);

        //move model so tip of patient's nose is origin
        Vector3[] fiducials = GameObject.Find("LoadedModels").GetComponent<LocationValues>().getFiducials();
        if(fiducials != null)
        {
            GameObject.Find("WorldAnchor/Model").transform.Find("Layers").transform.position = new Vector3(-fiducials[0].x, -fiducials[0].y + 0.1f, -fiducials[0].z + 1.5f);
        }

        CreateTargets();

    }

    
    private void CreateTargets()
    {
        Vector3[] targets = null;// GameObject.Find("LoadedModels").GetComponent<LocationValues>().getTargets();
        //WriteLog.WriteData("initialize - " + targets.ToString());

        if (targets != null)
        {
            //create correct number of targets
            Transform targetPrefab = GameObject.Find("Model").transform.Find("Layers/Targets/Target").transform;
        
            for (int i = 0; i < (targets.Length-1); i++)
            {
                //duplicate prefab target
                Transform newTarget = Instantiate(targetPrefab, targetPrefab.position, targetPrefab.rotation);
                //make it a child of the targets in the model
                newTarget.parent = GameObject.Find("Model").transform.Find("Layers/Targets").transform;
            }

            //position targets
        
            for (int i = 0; i < targets.Length; i++)
            {
                Transform target = GameObject.Find("Model").transform.Find("Layers/Targets").transform.GetChild(i).transform;
                target.position += new Vector3(targets[i].x, targets[i].y, targets[i].z);
            }
        }
        else
        {
            GameObject.Find("Model").transform.Find("Layers/Targets/Target").gameObject.SetActive(false); 
        }
       
    } 

}
