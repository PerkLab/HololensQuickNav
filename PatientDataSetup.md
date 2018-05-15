# From DICOM series to HoloLens

## MR Image Segmentation and Export

1. Load Images in by creating a DICOM series and turning it into a volume.
2. In Segment Editor, create a skin and ROI segment.
3. Select the DICOM series's volume as the Master Volume.
4. Add a new segment, called Skin.
5. Threshold segment the majority of the scan, invert, keep largest island, median smoothing, invert; finally remove any excess areas manually.
6. Add a new segment, called ROI.
7. Manually segment or use level tracing and then manually clean the ROI (tumour / SDH, etc).
8. Import the atlasMask (as LabelMap) and atlasImage into Slicer as volumes.
9. Open Swiss Skull Stripper, select the DICOM series's volume as your input, create your output volume and mask as LabelMapVolumes, set the atlasMash and atlmasImage as the Atlas.
10. Click apply and run the Skull Stripper. 
11. In Data, Convert Brain LabelMapVolume to Segmentation, then to a model. Then export ROI and Skin to models.
12. In Surface Toolbox, decimate Skin, Brain and ROI to 0.6.
13. Place a set of fiducial markers on tip of nose, and corners of each eye.
14. Export models as OBJ and markers as CSV.

## CT Image Segmentation and Export

TBD When we have CT images to confirm workflow.

## Setting up and deploying from Unity

### Setting up the Unity Scene

1. Drag and drop the exported models and markers into HoloQuickNav > Models > Anatomy > (New Folder Name)
2. In WorldAnchor > Model > Layers, delete previous models, replace with the exported models (Make sure to rename Skin model "Skin" if not already).
3. Change the scale of all models to 0.001 in X Y and Z.
4. In HoloQuickNav > Models > Anatomy > Materials, and place the materials for each model onto its respective model.
5. In Controls > Home, drag and drop ROI and Brain into the Missing Objects of the Turn On Layers Script. Change the function for each of the objects as GameObject > SetActive(bool)
6. In  InputManager > QuickNavManager > LocationValues, drag and drop the exported markers into Location Values 'CSV Markers' field.

### Build and Deploy

1. In File > Build Settings, click Build and make sure that the build directory is UWP.
2. Once the vs solution is built, open it and make sure that the build is in Release and on x86 architecture..
3. Click Build > Deploy Solution.
4. If the Hololens IP address changes, you can modify it in Project > HoloQuickNav2 Properties > Debug > Remote Machine.
