using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisOfRotation : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject selectedLine;

    public void DrawLines () {
		
	}
	
    void CalculatePoints()
    {
        MeshFilter[] allMeshes = selectedObject.GetComponentsInChildren<MeshFilter>();
        Bounds group = allMeshes[0].sharedMesh.bounds;
        foreach (MeshFilter meshF in allMeshes)
        {
            group.Encapsulate(meshF.sharedMesh.bounds);
        }

        Vector3 v3Center = group.center;

    }
}
