# QuickNav Assets
## HoloLensXboxControllerInput

   <https://assetstore.unity.com/packages/tools/input-management/xbox-controller-input-for-hololens-70068>  
   Contains plugins that allow you to connect to the Xbox controller.  
   Use the example script as a reference for using the plugin in your own script.

## HoloToolKit

   <https://github.com/Microsoft/MixedRealityToolkit-Unity>  
   Contains prefabs for cursors and the HoloLens camera, as well as the input manager which is used for  
   AirTap and Gaze control and background stability control.

## OBJImport

   <https://assetstore.unity.com/packages/tools/modeling/runtime-obj-importer-49547>  
   Contains script used to load .obj files as meshes into the scene.

## HoloQuickNav (assets created in the lab)
### Models
#### Anatomy
   Contains models and .csv files containing fiducial points for old phantom models used in previous demos  
   and testing. Can be removed if not needed, models are on the P:\ drive if ever needed in the future. Also  
   contains material assets for skin, brain, and hematoma.

#### Menus
   Contains material assets for menus such as backgrounds, buttons, and menu selectors. Also contains sprite  
   assets for Xbox button symbols and the Perk Lab logo.
   
#### MRI
   Contains MRI slices from testing for volume rendering. Can be removed or used for future testing. 
   
#### Tools
   Contains models for arrows used in various translating and rotating tools. Also contains materials for  
   these models as well as the axes shown during rotation and translation.
   
### Scripts
#### 3PointReg_old
   Contains all scripts related to original 3 point registration. Can be deleted if not needed anymore. 

#### FileBrowser
   Contains all scripts used in the PatientManager to access patient data stored in QuickNav’s LocalState file.  
   These scripts are used to load the models into the scene, rename them and apply the appropriate materials.
   
#### GazeTools
   Contains scripts for old tools that relied on the user’s gaze and head movement, including translation,  
   rotation, and a newer version of the point registration. MoveWithHead and DepthWithHead were used in  
   combination to move the model freely and have control over its position in all 3 dimensions.
   
#### IGTLink
   Contains script to control the UI that displays the information that has been loaded from Slicer through an .xml.  
   Also contains ReadXml script which is used in both the PatientManager and OpenIGTLink scenes to read the patient’s .xml file.

#### Input
   Contains scripts for handling AirTap, Gaze, and Xbox controller input. Attach these objects to the tools/objects  
   that you want to manipulate and add Unity Events to each input in the Unity Editor.
   
#### MenuControl
   Contains scripts to be attached to menus and text in the scene so that they are positioned correctly and facing the user.

#### MRI
   Currently only contains a test script for scrolling through a set of MR images. This will likely not be needed if we  
   intend on using 3DVolumes instead.
   
#### SceneManagement
   Contains scripts for initializing scenes, swapping between scenes, and controlling various components or tools  
   in a scene. Also contains WriteLog which is a script that can be used to track a user’s process while they use  
   the program, and also as a debugger during testing.
   
#### XboxTools
   Contains scripts for translation and rotation tools using the Xbox controller.
   
### Scenes



## Scene Structures
### Opening
* Displays Perk Lab logo for 3 seconds before swapping scenes to MainMenu
* InputManager game object contains QuickNavManager, which scene management scripts are attached to
* WriteLog script is initialized in this scene and DoNotDestroy() is applied so that it continues to exist during all future scene changes, therefore can be accessed at any point for debugging purposes

