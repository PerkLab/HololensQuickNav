## Workflow

### Med Student / Resident / Neurosurgeon Training

* Med Students rotate every few weeks - get their numbers and ask for 10 minutes to go through software and get their consent (before any cases with them assisting) (Zac/Sarah)
* Residents rotate every month - same as above (Zac/Sarah)
* Neurosurgeons will all be trained near start of the Summer individually, consent to be obtained at Neuro Division Meeting on May 7th @ 5pm (Zac/Sarah)

### Patient Recruitment & HoloLens Prep

* Speak to Cheryl weekly or biweekly to identify any upcoming cranial cases (Zac/Sarah)
* Look up patient phone number in chart and call from KGH to gain verbal consent after talking to them about the study (Zac/Sarah)
* Search for patient images in PACS or Xero regional viewer:
  * If only on regional viewer email PACSXRayRequest@kgh.kari.net with CR#, name, imaging type, date and location
  * If no recent imaging exists, return to look for it at a date closer to the procedure 
* Bring laptop and USB stick to OR planning centre to export images if patient consents (Zac/Sarah)
* Export anonymized images on USB, check them on the laptop, and update spreadsheet with anonymized patient ID (Zac/Sarah)
* Segment [CT](https://github.com/PerkLab/HololensQuickNav/blob/master/PatientDataSetup.md#ct-image-segmentation-and-export) or [MR](https://github.com/PerkLab/HololensQuickNav/blob/master/PatientDataSetup.md#mr-image-segmentation-and-export) skin surface, brain and relevant lesion in 3D slicer (Zac/Sarah)
* [Load segmentation and patient head surface into Unity as head and lesion models, build software and deploy to HoloLens](https://github.com/PerkLab/HololensQuickNav/blob/master/PatientDataSetup.md#setting-up-and-deploying-from-unity) (Zac)

### Pre & Intra-Op

* Meet patient before procedure to get written consent (Zac/Sarah)
* Enter OR (with scrubs and surgical cap/mask) and follow procedures - Nurses, etc. will give run through on this first few times through (Zac/Sarah)
* Create approximate registration with HoloLens to hand off to trainee (Zac/Sarah)
* Have Med Student identify entry point and trajectory conventionally, scan with Camera (Zac/Sarah)
* Have Med Student identify entry point and trajectory with HoloLens, scan with Camera (Zac/Sarah)
* Have Resident identify entry point and trajectory conventionally, scan with Camera (Zac/Sarah)
* Have Resident identify entry point and trajectory with HoloLens, scan with Camera (Zac/Sarah)
* Have Neurosurgeon identify entry point and trajectory conventionally, scan with Camera (Zac/Sarah)
* Have Neurosurgeon identify entry point and trajectory with HoloLens, scan with Camera (Zac/Sarah)
* When neuronav system is setup and registered by the Neurosurgeon, scan entry point and trajectory with Camera (Zac/Sarah)

### Post-Op

* Update new row of patient spreadsheet with P_id, age, date of procedure, procedure type, and all results from 7 scans above (Zac/Sarah)
* Take consent form for the patient with information for Ron and slide under his office door for him to update master study spreadsheet (Zac/Sarah)
