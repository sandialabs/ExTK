using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExTKSerialization : Editor
{
    public SerializedProperty soFunctionalityList;
    public SerializedProperty soMenuData;
    
    //Tab 1: 
    public SerializedProperty soMenuLayout;
    public SerializedProperty soBackgroundData;
    public SerializedProperty soScaleData;
    public SerializedProperty soRotateData;
    public SerializedProperty soMoveData;
    public SerializedProperty soAdditionalsData;

    //Tab 2: Models
    public SerializedProperty soExplodeContractData;
    public SerializedProperty soAnimations, soAnimationsEnabled, soAnimationSpeed, soAnimationClip, soAnimationButton, soAnimMenu, soAnimObject, soAnimCanvas;

    public SerializedProperty soSubsystems, soSubsystemsEnable, soSubSysbuttonState, soSubsystembutton, soSubsystemmenu, soSubsystemObject, soSubSysCanvas, soSubsystemButton, soSubSystemsScript, soCircleData;
    public SerializedProperty soHidePartsData;
    public SerializedProperty soModelManipulation;

    /// <summary>
    /// AssignSerializedFields(), used to setup the serialized fields and associate them to the fields in the class
    /// </summary>
    /// <param name="soExTK"></param>
    public void AssignSerializedFields(SerializedObject soExTK)
    {

        soFunctionalityList = soExTK.FindProperty("functionalityList");
        soMenuData = soExTK.FindProperty("menuData");
        soCircleData = soExTK.FindProperty("circleData");

        //Tab 1: Defaults
        soBackgroundData = soExTK.FindProperty("backgroundData");
        soScaleData = soExTK.FindProperty("scaleData");
        soRotateData = soExTK.FindProperty("rotateData");
        soMoveData = soExTK.FindProperty("moveData");
        soAdditionalsData = soExTK.FindProperty("additionalsData");

        //Tab 2: Models
        soExplodeContractData = soExTK.FindProperty("explodeContractData");
        soAnimations = soExTK.FindProperty("animations");
        soAnimationsEnabled = soExTK.FindProperty("animationsEnable");
        soAnimationSpeed = soExTK.FindProperty("animationspeed");
        soAnimationClip = soExTK.FindProperty("animationclip");
        soAnimationButton = soExTK.FindProperty("animationbutton");
        soAnimMenu = soExTK.FindProperty("animmenu");
        soAnimObject = soExTK.FindProperty("animObject");
        soAnimCanvas = soExTK.FindProperty("animCanvas");

        soHidePartsData = soExTK.FindProperty("hidePartsData");
        soSubsystems = soExTK.FindProperty("subsystems");
        soSubsystemsEnable = soExTK.FindProperty("subsystemsEnable");
        soSubSysbuttonState = soExTK.FindProperty("subSysbuttonState");
        soSubsystembutton = soExTK.FindProperty("subsystembutton");
        soSubsystemmenu = soExTK.FindProperty("subsystemmenu");
        soSubsystemObject = soExTK.FindProperty("subsystemObject");
        soSubSysCanvas = soExTK.FindProperty("subSysCanvas");
        soSubsystemButton = soExTK.FindProperty("SubsystemButton");
        soSubSystemsScript = soExTK.FindProperty("subSystemsScript");
    }

    public void SetEnabledValues(ExplorationToolKit ExTK)
    {
        if (ExTK.moveData.moveList == null)
            ExTK.moveData.moveList = new List<string>();
        if (ExTK.moveData.moveList.Count == 0)
        {
            ExTK.moveData.moveList.Add("Up");
            ExTK.moveData.moveList.Add("Down");
            ExTK.moveData.moveList.Add("Left");
            ExTK.moveData.moveList.Add("Right");
            ExTK.moveData.moveList.Add("Forward");
            ExTK.moveData.moveList.Add("Back");
        }
        if (ExTK.moveData.moveListBool == null)
            ExTK.moveData.moveListBool = new List<bool>();
        if (ExTK.moveData.moveListBool.Count == 0)
        {
            ExTK.moveData.moveListBool.Add(ExTK.moveData.moveup = true);
            ExTK.moveData.moveListBool.Add(ExTK.moveData.movedown = true);
            ExTK.moveData.moveListBool.Add(ExTK.moveData.moveleft = true);
            ExTK.moveData.moveListBool.Add(ExTK.moveData.moveright = true);
            ExTK.moveData.moveListBool.Add(ExTK.moveData.moveforward = true);
            ExTK.moveData.moveListBool.Add(ExTK.moveData.moveback = true);
        }
        ExTK.moveData.onOffSpeeds = new List<bool>();

        if (ExTK.rotateData.rotateList == null)
            ExTK.rotateData.rotateList = new List<string>();
        if (ExTK.rotateData.rotateList.Count == 0)
        {
            ExTK.rotateData.rotateList.Add("X");
            ExTK.rotateData.rotateList.Add("Y");
            ExTK.rotateData.rotateList.Add("Z");
        }
        if (ExTK.rotateData.rotateListBool == null)
            ExTK.rotateData.rotateListBool = new List<bool>();
        if (ExTK.rotateData.rotateListBool.Count == 0)
        {
            ExTK.rotateData.rotateListBool.Add(ExTK.rotateData.xaxis);
            ExTK.rotateData.rotateListBool.Add(ExTK.rotateData.yaxis);
            ExTK.rotateData.rotateListBool.Add(ExTK.rotateData.zaxis);
        }

        if (ExTK.scaleData.scaleList == null)
            ExTK.scaleData.scaleList = new List<string>();
        if (ExTK.scaleData.scaleList.Count == 0)
        {
            ExTK.scaleData.scaleList.Add("Up");
            ExTK.scaleData.scaleList.Add("Down");
        }
        if (ExTK.scaleData.scaleListBool == null)
            ExTK.scaleData.scaleListBool = new List<bool>();
        if (ExTK.scaleData.scaleListBool.Count == 0)
        {
            ExTK.scaleData.scaleListBool.Add(ExTK.scaleData.scaleUpBool);
            ExTK.scaleData.scaleListBool.Add(ExTK.scaleData.scaleDownBool);
        }

        if (ExTK.menuData.canvasObjObjects == null)
            ExTK.menuData.canvasObjObjects = new List<string>();
        if (ExTK.menuData.canvasObjObjects.Count == 0)
        {
            ExTK.menuData.canvasObjObjects.Add("Obj");
            ExTK.menuData.canvasObjObjects.Add("");
            ExTK.menuData.canvasObjObjects.Add("Canvas");
        }

        if (ExTK.additionalsData.additionalsStrings == null)
            ExTK.additionalsData.additionalsStrings = new List<string>();
        if (ExTK.additionalsData.additionalsStrings.Count == 0)
        {
            ExTK.additionalsData.additionalsStrings.Add("Yes");
            ExTK.additionalsData.additionalsStrings.Add("No");
            ExTK.additionalsData.additionalsStrings.Add("Reset");
            ExTK.additionalsData.additionalsStrings.Add("Minimize");
            ExTK.additionalsData.additionalsStrings.Add("Palm");
            ExTK.additionalsData.additionalsStrings.Add("Follow");
        }

        if (ExTK.circleData == null)
            ExTK.circleData = new List<CircleData>();
        if (ExTK.circleData.Count == 0)
        {
            ExTK.circleData.Add(new CircleData() { name = "ButtonLayout", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Scale", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Rotate", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Move", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Manipulate", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Animations", buttons = new List<GameObject>() });
            ExTK.circleData.Add(new CircleData() { name = "Subsystems", buttons = new List<GameObject>() });
        }

        ExTK.scaleData.scalestring = "Scale Model";
        ExTK.rotateData.rotatestring = "Rotate Model";
        ExTK.moveData.movestring = "Move Model";
    }
}