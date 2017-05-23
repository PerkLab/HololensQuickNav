using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftDirection : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    private Vector3 v3CenterHead;
    private Bounds arrows;
    private Transform childTransform;

    // Use this for initialization
    void Start()
    {
        Align();
    }

    public void Align()
    {
        childTransform = selectedObject.transform.GetChild(0);
        gameObject.transform.position = new Vector3(childTransform.position.x,childTransform.position.y+0.1f,childTransform.position.z+1.5f);
        //MeshFilter[] allMeshes = gameObject.GetComponentsInChildren<MeshFilter>();
        //arrows = allMeshes[0].sharedMesh.bounds;
        //foreach (MeshFilter meshF in allMeshes)
        //{
          //  arrows.Encapsulate(meshF.sharedMesh.bounds);
        //}

        //MeshFilter[] headMeshes = gameObject.GetComponentsInChildren<MeshFilter>();
        //Bounds group = headMeshes[0].sharedMesh.bounds;
        //foreach (MeshFilter meshF in allMeshes)
        //{
          //  group.Encapsulate(meshF.sharedMesh.bounds);
        //}

        //v3CenterHead = group.center;
        //arrows.center = v3CenterHead;
    }



    public void ArrowRight() {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position += new Vector3(0.01f, 0.0f, 0.0f);
        Align();
	}

    public void ArrowLeft()
    {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position -= new Vector3(0.01f, 0.0f, 0.0f);
        Align();
    }

    public void ArrowUp()
    {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position += new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }

    public void ArrowDown()
    {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position -= new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }
    public void ArrowForward()
    {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position -= new Vector3(0.0f, 0.0f, 0.01f);
        Align();
    }

    public void ArrowBack()
    {
        //Transform objectTrans = selectedObject.tr;
        selectedObject.transform.position += new Vector3(0.0f, 0.0f, 0.01f);
        Align();
    }

}
