---
layout: default
title: AddPlugins
parent: Scripts and Methods
nav_order: 2
---

# `ExTK` AddPlugins Scripts and Methods
The `AddPlugins` scripts for `ExTK`, manage third party plugins/packages, and custom scripts with accompanying methods.  The `AddPlugins` scripts and methods for `ExTK` are to be placed within **ExTK/Assets/ExTK/AddPlugins/** directory path.  


## MenuFramework Scripts
Please see our provided [ExTK with Plugins Package](https://github.com/sandialabs/ExTK/blob/ExTK-V1-with-Plugins/ExTK_V1_with_Plugins.unitypackage) which provides our [`MenuFramework`](https://github.com/sandialabs/MenuFramework) "plugged" into `ExTK` for an efficient, ready-to-use menu system; `MenuFramework` relies on the [Microsoft Mixed Reality Toolkit (MRTK)](https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/?view=mrtkunity-2021-05). 

### `ApplySliderMRTK (Script Object)`
The `ApplySliderMRTK` script adds a slider to the selected button to allow the end-user "sliding" control over the speeds of move, rotate, scale, and animations.

### `GazeMRTK (Script Object)`
The `GazeMRTK` script allows for the `gameObject` to have gaze selection. `GazeMRTK` utilizes
[`IMixedRealityPointerHandler`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealitypointerhandler?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0), and [`IMixedRealityFocusHandler`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealityfocushandler?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0) for [`OnFocusEnter()`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealityfocushandler.onfocusenter?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0) and [`OnFocusExit()`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealityfocushandler.onfocusexit?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0#microsoft-mixedreality-toolkit-input-imixedrealityfocushandler-onfocusexit(microsoft-mixedreality-toolkit-input-focuseventdata)) events to allow the user the ability to look at a model and change parts of it.

### `HelpMenuEnable (Script Object)`
The `HelpMenuEnable` script provides an audio feedback of "Help Commands" to navigate through the menu system.

### `HidePartMRTK (Script Object)`
The `HidePartMRTK` script will be automatically applied to the model when the `IntegrateMRTK` script is added to the `ExplorationToolKit` object with script.
  - `HidePartMRTK` inherits [`IMixedRealityPointerHandler`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealitypointerhandler?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0), and [`IMixedRealityFocusHandler`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.mixedreality.toolkit.input.imixedrealityfocushandler?view=mixed-reality-toolkit-unity-2020-dotnet-2.7.0). Implementing these interfaces allows for this script to check for any pointer focus collisions, which supports the `HidePartMRTK` script and allows the user to focus an object and set it to inactive.

### `IntegrateMRTK (Script Object)`
The `IntegrateMRTK` script is used to add [`OnClick()`](https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html) events to the selected buttons within `ExTK`. The idea is to call the interactable scripts that are attached to these buttons to allow for the buttons to work when the project is built or when it is tested inside the Unity Editor.
  - Add this script onto the `ExplorationToolKit` object to allow the `MRTK` menu system to work properly. Applying this script will allow the `MRTK` interactable script button click events.

### `MenuController (Script Object)`
The `MenuController`script applies menu controls to an object that is being built up as the useable menu with `ExTK`.
