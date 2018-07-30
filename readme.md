# HoloLensQuickNav

_Large scene files and images can be found at on the perkdata server: p:\data\PerkTutor\HololensQuickNav._

## Workflow and Clinical Study Protocol

For the latest workflow information and process, see [Workflow](https://github.com/PerkLab/HololensQuickNav/blob/master/Workflow.md).

## Segmentation and Model Generation

For the latest segmentation and model creation workflow process, see [Patient Data Setup](https://github.com/PerkLab/HololensQuickNav/blob/master/PatientDataSetup.md).

## Developer Guide

For descriptions of the current assets and scene structure of the application, see [DeveloperGuide](https://github.com/PerkLab/HololensQuickNav/blob/master/DeveloperGuide.md)

## Additional Software Installation Requirements and Notes (*[From Microsoft](https://developer.microsoft.com/en-us/windows/mixed-reality/install_the_tools#installation_checklist_for_hololens)*)

### Visual Studio 2017
* Select the Universal Windows Platform development workload
* Select the Game Development with Unity workload
* You may deselect the Unity Editor optional component since you'll be installing a newer version of Unity from the instructions below.
 * All editions of Visual Studio 2017 are supported (including Community). While Visual Studio 2015 Update 3 is still supported, we recommend Visual Studio 2017 for the best experience.
* You can download this for free through Queen's.

### HoloLens Emulator and Holographic Templates (build 10.0.14393.1358)	
* The emulator allows you to run apps on Windows Holographic in a virtual machine without a physical HoloLens. It includes a virtual HoloLens image that runs the latest version of the Windows Holographic OS. If you have already installed a previous build of the emulator, this build will install side-by-side. This package also includes holographic DirectX project templates for Visual Studio. If desired, you can select to install only the templates without the emulator.
* Your system must support Hyper-V for the Emulator installation to succeed (To enable go to Control Panel > Programs > Programs and Features > Turn Windows features on or off > Ensure Hyper-V is checked).

### Unity 2018.1.5 (*[Download link](https://store.unity.com/download?ref=personal)*)
* The Unity engine is an easy way to get started building a holographic app. 
* Make sure to select the Windows Store .NET Scripting Backend (you may install the docs as well if you want them available offline).
