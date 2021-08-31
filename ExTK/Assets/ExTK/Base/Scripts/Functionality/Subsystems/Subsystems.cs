using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SubsystemSort
{
    public GameObject subsysModel;
    public GameObject subsysButton;
}

[Serializable]
public struct SubsystemsData
{
    public bool subsystems, subsystemsEnable, subSysbuttonState;
    public GameObject subsystembutton, subsystemmenu, subsystemObject, subSysCanvas;
    public List<SubsystemSort> subsysSortment;
    public Button SubsystemButton;
    public Subsystems subSystemsScript;
}

public class Subsystems : ExplorationToolKit
{
    public List<GameObject> modelObjects = new List<GameObject>();
    public bool subSystemSet = false;

    private void Start()
    {
        foreach (Transform obj in GetComponentInChildren<Transform>())
        {
            modelObjects.Add(obj.gameObject);
        }
    }

    public void BeenClicked(string buttonName)
    {
        subSystemSet = !subSystemSet;
        if (ExTK.model != null)
        {
            SetModelActive();
            foreach (var items in modelObjects)
                if (items)
                    if (buttonName != items.name)
                        GameObject.Find(items.name).SetActive(false);
        }
    }

    public void SetModelActive()
    {
        if (ExTK.model != null)
        {
            foreach (Transform obj in GetComponentInChildren<Transform>())
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}