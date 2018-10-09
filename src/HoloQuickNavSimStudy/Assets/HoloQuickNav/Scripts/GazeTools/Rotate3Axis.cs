using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3Axis : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;

    private GameObject TextA;
    private GameObject TextS;
    private GameObject TextR;

    private bool rotateA = false;
    private bool rotateS = false;
    private bool rotateR = false;

    private float currentDistance;
    private float lastDistance;
    private float amount;

    private bool IsRunning = false;

    // Use this for initialization
    void OnEnable () {

        //position around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

        TextA = GameObject.Find("Model").transform.Find("AxisA/TextA").gameObject;
        TextS = GameObject.Find("Model").transform.Find("AxisS/TextS").gameObject;
        TextR = GameObject.Find("Model").transform.Find("AxisR/TextR").gameObject;

        Pause();

    }

    public void RotateA()
    {
        //clear all settings
        Pause();

        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        GameObject.Find("Model").transform.Find("AxisA/rotateArrow1").gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("AxisA/rotateArrow2").gameObject.SetActive(true);
        gameObject.transform.Find("ButtonsRotate/RotateA/Text").GetComponent<TextMesh>().fontStyle = FontStyle.BoldAndItalic;
        gameObject.transform.Find("ButtonsRotate/Pause/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        rotateA = true;
        IsRunning = true;
        //WriteLog.WriteData("Command: Rotate3Axis > RotateA");
        GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>().text = "Rotate3Axis - RotateA";
    }

    public void RotateS()
    {
        //clear all settings
        Pause();

        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        GameObject.Find("Model").transform.Find("AxisS/rotateArrow1").gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("AxisS/rotateArrow2").gameObject.SetActive(true);
        gameObject.transform.Find("ButtonsRotate/RotateS/Text").GetComponent<TextMesh>().fontStyle = FontStyle.BoldAndItalic;
        gameObject.transform.Find("ButtonsRotate/Pause/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        rotateS = true;
        IsRunning = true;
        //WriteLog.WriteData("Command: Rotate3Axis > RotateS");
        GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>().text = "Rotate3Axis - RotateS";
    }

    public void RotateR()
    {
        //clear all settings
        Pause();

        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        GameObject.Find("Model").transform.Find("AxisR/rotateArrow1").gameObject.SetActive(true);
        GameObject.Find("Model").transform.Find("AxisR/rotateArrow2").gameObject.SetActive(true);
        gameObject.transform.Find("ButtonsRotate/RotateR/Text").GetComponent<TextMesh>().fontStyle = FontStyle.BoldAndItalic;
        gameObject.transform.Find("ButtonsRotate/Pause/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        rotateR = true;
        IsRunning = true;
        //WriteLog.WriteData("Command: Rotate3Axis > RotateR");
        GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>().text = "Rotate3Axis - RotateR";
    }

    public void Pause()
    {
        //turn off all rotation
        IsRunning = false;
        rotateA = false;
        rotateS = false;
        rotateR = false;

        //clear all arrows
        GameObject.Find("Model").transform.Find("AxisA/rotateArrow1").gameObject.SetActive(false);
        GameObject.Find("Model").transform.Find("AxisA/rotateArrow2").gameObject.SetActive(false);
        GameObject.Find("Model").transform.Find("AxisS/rotateArrow1").gameObject.SetActive(false);
        GameObject.Find("Model").transform.Find("AxisS/rotateArrow2").gameObject.SetActive(false);
        GameObject.Find("Model").transform.Find("AxisR/rotateArrow1").gameObject.SetActive(false);
        GameObject.Find("Model").transform.Find("AxisR/rotateArrow2").gameObject.SetActive(false);

        //clear buttons
        gameObject.transform.Find("ButtonsRotate/RotateA/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        gameObject.transform.Find("ButtonsRotate/RotateR/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        gameObject.transform.Find("ButtonsRotate/RotateS/Text").GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
        gameObject.transform.Find("ButtonsRotate/Pause/Text").GetComponent<TextMesh>().fontStyle = FontStyle.BoldAndItalic;

        //WriteLog.WriteData("Command: Rotate3Axis > Pause");
    }
	
	// Update is called once per frame
	void Update () {
        
        //have labels and menu face camera
        TextA.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextS.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
        TextR.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);

        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);


        if (IsRunning)
        {
            //calculate new distance
            currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);

            if (rotateA) //rotate about A axis
            {
                if (currentDistance < lastDistance) //if user is closer to the model
                {
                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(0f, amount, 0f);
                }
                else//(currentDistance > lastDistance) //if user is farther from the model
                {

                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(0f, -amount, 0f);
                }
            }
            else if(rotateS) //rotate about S axis
            {
                if (currentDistance < lastDistance) //if user is closer to the model
                {
                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(0f, 0f, amount);
                }
                else//(currentDistance > lastDistance) //if user is farther from the model
                {

                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(0f, 0f, -amount);
                }
            }
            else if(rotateR) //roate about R axis
            {
                if (currentDistance < lastDistance) //if user is closer to the model
                {
                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(amount, 0f, 0f);
                }
                else//(currentDistance > lastDistance) //if user is farther from the model
                {

                    amount = Mathf.Abs(currentDistance - lastDistance) * 100;
                    selectedObject.transform.Rotate(-amount, 0f, 0f);
                }
            }
            //update the distances
            lastDistance = currentDistance;
        }

    }
}
