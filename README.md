# `Exploration ToolKit` (`ExTK`)
The `Exploration ToolKit` (`ExTK`) is a reusable Extended Reality (XR) system developed for incorporating and exploring 3-Dimenstional (3-D) computer aided design (CAD) models in XR, with a primary focus on Augmented Reality (AR).  `ExTK` is developed using the [Unity Development Platform](https://unity.com/). 


## Overview
This project serves the development, implementation and deployment of the `Exploration ToolKit` (`ExTK`). This toolkit is desinged for Unity and allows users to easily set up a project to explore their CAD models, incorporating basic exploration functionality. 


## Structure
`ExTK` consists of two modes: **Developer Mode** and **User Mode**. 


### Developer Mode
In **Developer Mode**, `ExTK` provides developers with the ability to easily import 3-D CAD models and activate desired exploration functionality and layout. Multiple models can be added to a single instantiation of the `ExTK` by use of Unity’s Scene capability. Exploration functions include scaling, rotating, explode/contract, animations, hiding parts, submodules, and measurement functions. 


### User Mode
In **User Mode**, `ExTK` provides a menu system that allows users to select models and initiate exploration functions. `ExTK` is architected for reusability and developers can customize the `ExTK` layout and functions according to application needs. `ExTK` is designed to be hardware agnostic and currently supports HoloLens v1, HoloLens v2, and standalone PC.


## Implementation
`ExTK` is a Unity Package that can be imported in then added to the scene to allow for the manipulation of models with this scene.
1. Start New Unity Project
1. Download the `ExTK_V*.unitypackage` file
    >- **Note:** `*` indicates placeholder for version number`
1. Import in `ExTK` Unity Package
1. Go to `Toolbar` and select `ExTK->Add Component->Add Exploration ToolKit`
1. Select `Exploration ToolKit` in Hierarchy
   - Look in the inspector on the right
      - Add your model
      - **[Optional]** Add your runtime animator controller if you have animations
      - Add preferred button prefab for User Interface (UI) appearance
         >- **Note:** If you don't have one a default will be given to you 
      - Select which model abilities you would like to add by selecting the enable check mark on the right and click the name to drop down the settings for each


## Deploying to Devices
>Note: The following devices supported as of `August 2021` are:
> - HoloLens V1
> - HoloLens V2
> - Standalone PC

The following deployment steps use Unity, `ExTK`, and [Microsoft's Mixed Reality Toolkit (MRTK)](https://github.com/microsoft/MixedRealityToolkit) for deployment to the HoloLens 1 and 2.


### Deploying to HoloLens V1
1. Open Build Window (`Mixed Reality -> Toolkit -> Utilities -> Build Window`)
1. Select `Unity Build Window` tab
   - Target Device: `HoloLens`
   - Add your scene to the `Scenes in Build`
   - Click `Build Unity Project` button
   - Wait for build to finish
   - Click `Cancel` when the AppX Build window pops up
1. Select `AppX Build Options`
   - Build Configuration: `Release` or preferred
   - Build Platform: `x86` for HoloLens 1
   - Platform Toolset: `Solution`
   - Click `Build AppX`
     - Wait for it to finish
1. Select `Deploy Options` tab
   - Target Type: `Local` or preferred
   - **Username** and **Password** are tied to your Windows Deployment login
   - Connect HoloLens 1 
   - Click `Test Connection` 
     >- Note: It will say `Successful` or `Failure`
   - Click `Install App` button in Builds


### Deploying to HoloLens V2
1. Open Build Window (`Mixed Reality -> Toolkit -> Utilities -> Build Window`)
1. Select `Unity Build Window` tab
   - Target Device: `HoloLens`
   - Add your scene to the `Scenes in Build`
   - Click `Build Unity Project` button
   - Wait for build to finish
   - Click `Cancel` when the AppX Build window pops up
1. Select `AppX Build Options`
   - Build Configuration: `Release` or preferred
   - Build Platform: `ARM64` for HoloLens 2
   - Platform Toolset: `Solution`
   - Click `Build AppX`
     - Wait for it to finish
1. Select `Deploy Options` tab
   - Target Type: `Local` or preferred
   - **Username** and **Password** are tied to your Windows Deployment login
   - Connect HoloLens 2 
   - Click `Test Connection` 
     >- Note: It will say `Successful` or `Failure`
   - Click `Install App` button in Builds


