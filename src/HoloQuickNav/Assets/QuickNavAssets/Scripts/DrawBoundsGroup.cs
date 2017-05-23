
using UnityEngine;
using System.Collections;


[ExecuteInEditMode()]

public class DrawBoundsGroup : MonoBehaviour
{
    public Color color;
    public GameObject selectedObject;
    public GameObject selectedLine;
   // public Material mat;

    private Vector3 v3FrontTopLeft;
    private Vector3 v3FrontTopRight;
    private Vector3 v3FrontBottomLeft;
    private Vector3 v3FrontBottomRight;
    private Vector3 v3BackTopLeft;
    private Vector3 v3BackTopRight;
    private Vector3 v3BackBottomLeft;
    private Vector3 v3BackBottomRight;

    public void DrawFrame()
    {
        CalcPositons();
        DrawBox();
    }

    void CalcPositons()
    {
        MeshFilter[] allMeshes = selectedObject.GetComponentsInChildren<MeshFilter>();
        Bounds group = allMeshes[0].sharedMesh.bounds;
        foreach (MeshFilter meshF in allMeshes)
        {
            group.Encapsulate(meshF.sharedMesh.bounds);
        }

        Vector3 v3Center = group.center;
        Vector3 v3Extents = group.extents;


        v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
        v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
        v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
        v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
        v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
        v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
        v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
        v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

        v3FrontTopLeft = transform.TransformPoint(v3FrontTopLeft);
        v3FrontTopRight = transform.TransformPoint(v3FrontTopRight);
        v3FrontBottomLeft = transform.TransformPoint(v3FrontBottomLeft);
        v3FrontBottomRight = transform.TransformPoint(v3FrontBottomRight);
        v3BackTopLeft = transform.TransformPoint(v3BackTopLeft);
        v3BackTopRight = transform.TransformPoint(v3BackTopRight);
        v3BackBottomLeft = transform.TransformPoint(v3BackBottomLeft);
        v3BackBottomRight = transform.TransformPoint(v3BackBottomRight);
    }

    void DrawBox()
    {

        LineRenderer lineRender = selectedLine.GetComponent<LineRenderer>();
        lineRender.SetPosition(0, new Vector3(0.0f,-0.1f,1.5f));
        lineRender.SetPosition(1, new Vector3(0.0f, 0.5f, 1.5f));
        //lineRender.SetPosition(0, new Vector3(v3FrontTopRight.x, v3FrontTopRight.y, v3FrontTopRight.z));
        //lineRender.SetPosition(1, v3FrontTopLeft);






        //Debug.DrawLine(v3FrontTopLeft, v3FrontTopRight, color);
        //Debug.DrawLine(v3FrontTopRight, v3FrontBottomRight, color);
        //Debug.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color);
        //Debug.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color);

        //Debug.DrawLine(v3BackTopLeft, v3BackTopRight, color);
        //Debug.DrawLine(v3BackTopRight, v3BackBottomRight, color);
        //Debug.DrawLine(v3BackBottomRight, v3BackBottomLeft, color);
        //Debug.DrawLine(v3BackBottomLeft, v3BackTopLeft, color);

        //Debug.DrawLine(v3FrontTopLeft, v3BackTopLeft, color);
        //Debug.DrawLine(v3FrontTopRight, v3BackTopRight, color);
        //Debug.DrawLine(v3FrontBottomRight, v3BackBottomRight, color);
        //Debug.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color);
        //}
    }

}