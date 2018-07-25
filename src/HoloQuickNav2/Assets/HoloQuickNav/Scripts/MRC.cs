using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;
using System.Linq;

public class MRC : MonoBehaviour {

    private PhotoCapture photoCaptureObject = null;

	// Use this for initialization
	public void TakePhoto() {

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        PhotoCapture.CreateAsync(true, delegate (PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 1.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result)
            {
                //GameObject.Find("Main Camera/MRCCamera").transform.position = GameObject.Find("Main Camera").transform.position;
                GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
                GameObject.Find("Main Camera/MRCCamera").GetComponent<Camera>().enabled = true;

                string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
                string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
                
                photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);

            });
        });
        

    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if(result.success)
        {
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
            GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
            GameObject.Find("Main Camera/MRCCamera").GetComponent<Camera>().enabled = false;
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    
}
