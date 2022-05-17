---
layout: default
title: Buttons
parent: Scripts and Methods
nav_order: 1
---

# `ExTK` Base Scripts and Methods
The `Base` scripts and methods for `ExTK` are found within **ExTK/Assets/ExTK/Base/Scripts** directory path, see [this link](https://github.com/sandialabs/ExTK/tree/main/ExTK/Assets/ExTK/Base/Scripts) to open the code directory and follow along. 

## `ExplorationToolKit (Script Object)`
The `ExplorationToolKit` script will be used to build the `ExplorationToolKit` system.  Apply this script to a `GameObject` in the hierarchy. 
To build, apply a selected button prefab for the menu system.

>Note: Prefabs are included in:
>  - The base [ExTK Package](https://github.com/sandialabs/ExTK/blob/main/ExTK_V1.unitypackage)
>    - [Project/Assets/ExTK/Base/Prefabs](https://github.com/sandialabs/ExTK/tree/main/ExTK/Assets/ExTK/Base/Prefabs)
>  - As well as from [`MenuFramework`](https://github.com/sandialabs/MenuFramework) in the [ExTK with Plugins Package](https://github.com/sandialabs/ExTK/blob/ExTK-V1-with-Plugins/ExTK_V1_with_Plugins.unitypackage)
>    - [Project/Assets/ExTK/AddPlugins/MenuFrameworkPrefabs](https://github.com/sandialabs/ExTK/tree/ExTK-V1-with-Plugins/ExTK/Assets/ExTK/AddPlugins/MenuFramework/Prefabs)


## Functionality Scripts

### `Animations (Script Object)`
The `Animations` script controls the animations the builder throws into the list of model animations.
  - `AnimButtomClick()`: This method sets the on click event for the main animation canvas button on the menu system.
  - `BeenClicked(AnimationClip)`: This method takes in an `AnimationClip` variable in order to call and run the animations on the specific animation button click.
  - `PlayAnim()`: This method sets the animation speed back to it original speed to the animation continues.
  - `PauseAnim()`: This method sets the animation speed to zero which pauses the current animation at its current state.
  - `ResetAnimation()`: This method sets the animation back to its Idle state.
   
### `Background (Script Object)`
The `Background` script sets a background for the selected model. `ExTK` has a default clean room model that can be used.
  - `BeenClicked()`: This method turns the background `gameObject` **on** and **off**.
  - `ResetBackground()`: This method sets the background object to inactive and set the background toggle back to **false**.

### `Exit (Script Object)`
The `Exit` script exits the application; this works for both the Unity Editor play mode and the device play/run the application on.
  - `BeenClicked()`: This method enables the yes/no panel to exit
  - `YesBeenClicked()`: This method calls Unity default `Application.Quit()` method to exit the application that is currently running.
  - `NoBeenClicked()`: This mehtod sets the exit canvas to false so the user can cancel their application exit sequence.
  - `BeenClickedDefault()`: This method resets everything having to do with exiting the application.

### `ExplodeContract (Script Object)`
The `ExplodeContract` script takes the selected model and create a list of original model positions and exploded model positions.
  - `ToggleExplodedView()`: This method changes the exploded view of the model by setting a toggled exploded variable.
  - `ChangePosition()`: This method changes the position of the subitems of the model called from Unity's default `Update()` method.
  - `ExplodeAndContract(Vector3,Vector3,int)`: ExplodeAndContract takes in three parameters a `Vector3` of the current model position, a `Vector3` of the move position to create the distance the model will move, and an `integer` for the total number of submodels that need to be moved. The method is used to create the move distance for each sub-model item.

### `HidePart (Script Object)`
The `HidePart` script will be automatically applied to the model when the `IntegrateUnityUI` script is added to the `ExplorationToolKit` object with script.
  - `BeenClicked()`: This method toggles the `HidePart` boolean **true** or **false**.
  
### `Move (Script Object)`
The `Move` script moves the selected `gameObject` **Up**, **Down**, **Left**, **Right**, **Forward**, or **Back**.
   - `BeenClicked()`: This method moves the canvas to active allowing for the move buttons to be visible for the user.
   - `BeenClicked(string)`: This method is called to start the Unity default `Update()` method which begins the model movement.  Additionally, it is an overloaded method that passes in a string parameter of **Up**, **Down**, **Left**, **Right**, **Forward**, and **Back**. 
   - `Movement(Vector3)`: This method changes the `Vector3` position of the model based on the `Vector3` passed in.
   - `StopMove()`: This method stops any movement on the model selected.
   - `ResetClicked()`: This method resets everything back to **false** including the move canvas active and the move buttons boolean variables.

### `Reset (Script Object)`
The `Reset` script sets everything back to its original state including the menu system and the model.
  - `BeenClicked()`: This mehtod initiates the reset of everything on the menu system and the model.

### `Rotate (Script Object)`
The `Rotate` script controls the selected `gameObject` rotation on the X, Y, and Z axis.
   - `BeenClicked()`: This method sets the rotate canvas to active so the user can see the rotate **X**,**Y**, and **Z** buttons.
   - `BeenClicked(string)`: This method is an overloaded method that rotates the model based on the passing paramters: `"X"`,`"Y"`, or `"Z"`.
   - `ResetBeenClicked()`: This method resets the rotate canvas to **false** and rotate variables to **false**.

### `Scale (Script Object)`
The `Scale` script scales the selected `gameObject` **Up** (larger) or **Down** (smaller).
  - `BeenClicked()`: This method opens the scale canvas with the **Scale Up** and **Scale Down** buttons on the menu system.
  - `ResetClicked()`: This method resets: the scale to default settings, including the menu scale canvas; and the variables to **false** in the script.
  - `BeenClicked(string)`: This method starts the **Scale Up** or **Scale Down** of the object. Additionally, it is an overloaded method called with a string parameter of `"Up"` or `"Down"`.

### `Stop (Script Object)`
The `Stop` script stops all parts of the model: move, scale, rotate, animations
  - `BeenClicked()`: This method stops all parts of the model: move, scale, rotate, and animations.


### `Subsystems (Script Object)`
The `Subsystems` script has a list of sub-model items the builder adds. This script will allow for specific parts of the model to become visible only.
   - `BeenClicked(string)`: This method takes in a string parameter of the button name that is clicked. This method uses the button name to find the sub-model item by setting all model items inactive.
   - `SetModelActive()`: This method sets all model sub-items to **true** and is used to reset the model back to visible.

## Utilities Scripts

### `AddMaterials (Script Object)`
The `AddMaterials` script can be added to any model. Once the script has been attached in the inspector, add the model, or parts of the model, to the `gameObject` box and then add a material to the material box. Once these are added, click the "Add Material" button; this will apply the material to the selected `gameObject`.

### `CircleData (Script Object)`
The `CircleData` script builds up a circular menu system of buttons. Apply this script to a `GameObject` in the hierarchy and add the buttons you will be using to the `GameObject` list.

### `IntegrateUnityUI (Script Object)`
The `IntegrateUnityUI` script is applied to the `ExplorationToolKit` object so the Unity button click events can be applied to the default Unity buttons

### `MenuLayout (Script Object)`
The `MenuLayout` script controls the layout of the selected menu system component, from vertical to horizontal useable menu.

### `Util (Script Object)`
The `Util` script provides utility control for the selected menu system component.
