using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Dicom;
//using Dicom.Log;

public class MRISliceControl : MonoBehaviour {

    private int currentSlice = 0;

	// Use this for initialization
	void Start () {

        for(int i=0;i< GameObject.Find("MRImages").transform.childCount; i++)
        {
            GameObject.Find("MRImages").transform.GetChild(i).position += new Vector3(0f, 0f, i * 0.05f);
        }

        currentSlice = 0;
        updateSlices();






    }
	
	public void nextSlice()
    {
        if (currentSlice != GameObject.Find("MRImages").transform.childCount - 1)
            currentSlice++;
        updateSlices();
    }

    public void lastSlice()
    {
        if (currentSlice != 0)
            currentSlice--;
        updateSlices();
    }

    private void updateSlices()
    {
        /*
        for (int i = 0; i < Scans.Length; i++)
        {
            if (i == currentSlice)
                Scans[i].gameObject.SetActive(true);
            else
                Scans[i].gameObject.SetActive(false);
        }
        */

        for (int i = 0; i < GameObject.Find("MRImages").transform.childCount; i++)
        {
            if(i==currentSlice)
                GameObject.Find("MRImages").transform.GetChild(i).gameObject.SetActive(true);
            else
                GameObject.Find("MRImages").transform.GetChild(i).gameObject.SetActive(false);

        }

        /*
        foreach (Transform scan in Scans)
        {
            scan.gameObject.SetActive(false);
            if(currentSlice == scan.GetSiblingIndex())
                scan.gameObject.SetActive(true);
        }
        */
    }
}
