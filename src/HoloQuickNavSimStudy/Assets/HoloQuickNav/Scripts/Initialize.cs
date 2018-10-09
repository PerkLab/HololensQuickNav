using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine;

public class Initialize : MonoBehaviour {
    
    //axis for 3 point registration
    public GameObject axis;

	void OnEnable () {

        CreateTargets();

        //shift 3 point registration frame and model so tip of the nose is at the origin
        // TODO: this will need to be changed for more general procedures
            //use a csv for an origin point specifcally instead of the first registration point
        GameObject.Find("Model").transform.Find("Layers").transform.position = new Vector3(-LocationValues.fiducials[0].x, -LocationValues.fiducials[0].y + 0.1f, -LocationValues.fiducials[0].z + 1.5f);
        
    }


    private void CreateTargets()
    {
        if (LocationValues.numTargets > 0)
        {
            //create correct number of targets
            Transform targetPrefab = GameObject.Find("Model").transform.Find("Layers/Targets/Target").transform;
        
            for (int i = 0; i < (LocationValues.numTargets-1); i++)
            {
                //duplicate prefab target
                Transform newTarget = Instantiate(targetPrefab, targetPrefab.position, targetPrefab.rotation);
                //make it a child of the targets in the model
                newTarget.parent = GameObject.Find("Model").transform.Find("Layers/Targets").transform;
            }

            //position targets
        
            for (int i = 0; i < LocationValues.numTargets; i++)
            {
                Transform target = GameObject.Find("Model").transform.Find("Layers/Targets").transform.GetChild(i).transform;
                target.position += new Vector3(LocationValues.targets[i].x, LocationValues.targets[i].y, LocationValues.targets[i].z);
            }
        }
        else
        {
            GameObject.Find("Model").transform.Find("Layers/Targets/Target").gameObject.SetActive(false); 
        }
       
    } 

}
