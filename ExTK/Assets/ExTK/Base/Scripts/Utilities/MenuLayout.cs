using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MenuData
{
    public GameObject buttonlayoutmenu;
    public bool buttonlayout, horizontal, vertical, invert, circular, buttonLayoutEnable;
    public bool details, toggleManipulate, detailsEnable, toolTip, toolTipEnable, toggleToolTip, manipulate, manipulateEnable, showHideTopPanel;
    public GameObject manipulateButton, manipulateCanvas, toolTipButton, topPanelCanvas;
    public List<string> canvasObjObjects;
}

[Serializable]
public class MenuLayout : MonoBehaviour
{
    [HideInInspector] public static List<string> objectBuildItems = new List<string>(new string[] { "Obj", "Canvas" });
    [HideInInspector] public float mainButtonSizeWidth = .5f;
    [HideInInspector] public float mainButtonSizeHeight = .5f;
    [HideInInspector] public float subButtonSizeWidth = .5f;
    [HideInInspector] public float subButtonSizeHeight = .5f;
    ExplorationToolKit ExTK;

    private void Start()
    {
        ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        if (GameObject.Find("Burger"))
            ExTK.menuData.showHideTopPanel = false;
        else
            ExTK.menuData.showHideTopPanel = true;

        if (!ExTK.menuData.topPanelCanvas)
            ExTK.menuData.topPanelCanvas = GameObject.Find("TopPanelCanvas");

        if (ExTK.menuData.topPanelCanvas)
            ExTK.menuData.topPanelCanvas.SetActive(ExTK.menuData.showHideTopPanel);
    }

    /// <summary>
    /// SubsystemButtonBeenClicked(), used to set the subsystems visible
    /// </summary>
    public void SubsystemButtonBeenClicked()
    {
        ExTK.subSysbuttonState = !ExTK.subSysbuttonState;

        if (ExTK.subSysbuttonState)
        {
            ExTK.subSysCanvas.SetActive(true);
        }
        else
        {
            ExTK.model.GetComponent<Subsystems>().SetModelActive();
            ExTK.subSysCanvas.SetActive(false);
        }
    }


    public void ShowHideTopPanel()
    {
        ExTK.menuData.showHideTopPanel = !ExTK.menuData.showHideTopPanel;
        if (ExTK.menuData.topPanelCanvas)
            ExTK.menuData.topPanelCanvas.SetActive(ExTK.menuData.showHideTopPanel);
    }
}
