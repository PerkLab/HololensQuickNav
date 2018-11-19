﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;



public class PlaceWorldAnchor : MonoBehaviour {

    WorldAnchor anchor;
    WorldAnchorStore store;
    private bool StoreReady = false;
    public GameObject selectedObject;

	// Use this for initialization
	void Start () {
        WorldAnchorStore.GetAsync(AnchorStoreLoaded);
    }

    private void OnEnable()
    {
        if(StoreReady)
        {
            SaveAnchor();
        }
    }

    private void OnDisable()
    {
        if(StoreReady)
        {
            ClearAnchor();
        }
    }

    private void AnchorStoreLoaded(UnityEngine.XR.WSA.Persistence.WorldAnchorStore store)
    {
        this.store = store;
        LoadAnchors();
        StoreReady = true; //for future OnEnable calls when opening "home"
        SaveAnchor();
    }

    private void LoadAnchors()
    {
        //load world anchor and check if loaded
        bool retTrue = this.store.Load("model", selectedObject);
        if (!retTrue)
        {
            // Until the gameObjectIWantAnchored has an anchor saved at least once it will not be in the AnchorStore
        }
    }

    private void SaveAnchor()
    {
        bool retTrue;
        //create anchor for head
        anchor = GameObject.Find("model").AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
        // Remove any previous worldanchor saved with the same name so we can save new one
        this.store.Delete("model");
        //save anchor and check if saved
        retTrue = this.store.Save("model", anchor);
        if (!retTrue)
        {
            Debug.Log("Anchor save failed.");
        }
    }

    private void ClearAnchor()
    {
        anchor = selectedObject.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
        if (anchor)
        {
            // remove any world anchor component from the game object so that it can be moved
            DestroyImmediate(anchor);
        }
    }

}