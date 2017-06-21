using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnObject : MonoBehaviour {
    public GameObject selectedObject;
    private Vector3 v3CenterHead;
    private Bounds arrows;

    // Use this for initialization
    void Start () {
        MeshFilter[] allMeshes = gameObject.GetComponentsInChildren<MeshFilter>();
        arrows = allMeshes[0].sharedMesh.bounds;
        foreach (MeshFilter meshF in allMeshes)
        {
            arrows.Encapsulate(meshF.sharedMesh.bounds);
        }

        MeshFilter[] headMeshes = gameObject.GetComponentsInChildren<MeshFilter>();
        Bounds group = headMeshes[0].sharedMesh.bounds;
        foreach (MeshFilter meshF in allMeshes)
        {
            group.Encapsulate(meshF.sharedMesh.bounds);
        }

        v3CenterHead = group.center;

    }

    public void Align()
    {
        arrows.center = v3CenterHead;
    }
	
}
