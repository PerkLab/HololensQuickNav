using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRegistration : MonoBehaviour {

    public Material currentMarker;
    public Material lockedMarker;
    private int markerCount = 0;
    private Vector3[] fiducials;

	// Use this for initialization
	void Start () {
        CreateLockedMarkers();
        MoveMarker();
        fiducials = GameObject.Find("LoadedModels").GetComponent<LocationValues>().getFiducials();
	}
	
    private void CreateLockedMarkers()
    {
        //create correct number of locked markers
        Transform lockedMarkerPrefab = GameObject.Find("Controls").transform.Find("PointRegistration/LockedMarkers/LockedMarkerPrefab").transform;
        
        for (int i = 0; i < (fiducials.Length - 1); i++)
        {
            //duplicate prefab target
            Transform newMarker = Instantiate(lockedMarkerPrefab, lockedMarkerPrefab.position, lockedMarkerPrefab.rotation);
            //make it a child of the targets in the model
            newMarker.parent = GameObject.Find("Controls").transform.Find("PointRegistration/LockedMarkers").transform;
            //set all markers inactive until placed
            newMarker.gameObject.SetActive(false);
        }

    }

    public void PlaceCurrentMarker()
    {
        PlaceLockedMarker(markerCount);
        
        markerCount++;
        if (markerCount > (fiducials.Length-1))
        {
            GameObject.Find("Controls").transform.Find("PointRegistration/Marker").gameObject.SetActive(false);
            Register();
        }
        else
        {
            GameObject.Find("Controls").transform.Find("PointRegistration/Marker").gameObject.SetActive(true);
            
            MoveMarker();
        }
        
    }

    public void Back()
    {
        markerCount--;
        //hide last locked marker
        GameObject.Find("Controls").transform.Find("PointRegistration/LockedMarkers").GetChild(markerCount).gameObject.SetActive(false);
        MoveMarker();
    }

    public void MoveMarker()
    {
        GameObject.Find("Controls").transform.Find("PointRegistration/Depth").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("PointRegistration/MoveWithHead").gameObject.SetActive(true);
    }
    public void Depth()
    {
        GameObject.Find("Controls").transform.Find("PointRegistration/MoveWithHead").gameObject.SetActive(false);
        GameObject.Find("Controls").transform.Find("PointRegistration/Depth").gameObject.SetActive(true);
    }

    public void toggleTools()
    {
        if (GameObject.Find("Controls").transform.Find("PointRegistration/MoveWithHead").gameObject.activeSelf)
        {
            Depth();
        }
        else
        {
            GameObject.Find("Controls").transform.Find("PointRegistration/Depth").gameObject.SetActive(false);
            PlaceCurrentMarker();
        }
    }

    private void PlaceLockedMarker(int markerIndex)
    {
        //place locked marker at the location of the current marker
        Transform lockedMarker = GameObject.Find("Controls").transform.Find("PointRegistration/LockedMarkers").GetChild(markerIndex).transform;
        lockedMarker.position = GameObject.Find("Controls").transform.Find("PointRegistration/Marker").transform.position;
        //make locked marker visible
        lockedMarker.gameObject.SetActive(true);
    }

    private void Register()
    {
        //complete point-point registration

        

    }


}
