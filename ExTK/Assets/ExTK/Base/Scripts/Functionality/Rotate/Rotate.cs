using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RotateData
{
    [SerializeReference]
    public List<string> rotateList;
    [SerializeReference]
    public List<bool> rotateListBool;
    [SerializeReference]
    public List<GameObject> rotateButtons;
    [SerializeReference]
    public GameObject rotatebutton, rotateObject, rotatemenu;
    [SerializeReference]
    public float rotatespeed;
    [SerializeReference]
    public string rotatestring;
    [SerializeReference]
    public bool rotateEnable, isRotating, rotate, xaxis, yaxis, zaxis;
    [SerializeReference]
    public GameObject MenuR, xbutton, ybutton, zbutton;
}

[Serializable]
public class Rotate : ExplorationToolKit
{
    [HideInInspector] public bool  RbuttonState, RotateXSpeedOn = false, RotateYSpeedOn = false,RotateZSpeedOn = false;

    /// <summary>
    /// Unity default Start() Method used for initilization
    /// </summary>
    void Start()
    {
        RbuttonState = false;
        ExTK.rotateData.rotatebutton = GameObject.Find("Rotate");
        ExTK.rotateData.MenuR = GameObject.Find("RotateCanvas");
        for (int i = 0; i < ExTK.rotateData.rotateList.Count; i++)
        {
            if (ExTK.rotateData.rotateListBool[i])
            {
                ExTK.rotateData.rotateButtons.Add(GameObject.Find(ExTK.rotateData.rotateList[i] + " Axis"));
            }
        }

        ExTK.rotateData.MenuR.SetActive(false);
    }

    /// <summary>
    /// BeenClicked(), used to set the rotate button to true
    /// </summary>
    public void BeenClicked()
    {
        RbuttonState = !RbuttonState;
        if (RbuttonState && ExTK.model.GetComponent<ExplodeContract>())
        {
            ExTK.model.GetComponent<ExplodeContract>().enabled = false;
        }
    }

    /// <summary>
    /// BeenClicked(), used to set the bool for X,Y, or Z rotation 
    /// </summary>
    /// <param name="rotate"></param>
    public void BeenClicked(string rotate)
    {
        switch (rotate)
        {
            case "X":
                RotateXSpeedOn = !RotateXSpeedOn;
                break;
            case "Y":
                RotateYSpeedOn = !RotateYSpeedOn;
                break;
            case "Z":
                RotateZSpeedOn = !RotateZSpeedOn;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Update is called once per frame **Unity Default method
    /// </summary>
    void Update()
    {
        if (RotateXSpeedOn) { RotateX(); }
        if (RotateYSpeedOn) { RotateY(); }
        if (RotateZSpeedOn) { RotateZ(); }

        if (RbuttonState == true)
        {
            ExTK.rotateData.MenuR.SetActive(true);
        }
        else if (RbuttonState == false)
        {
            ExTK.rotateData.MenuR.SetActive(false);
            ExTK.model.GetComponent<Rotate>().StopRotate();
        }

    }

    /// <summary>
    /// ResetClicked(), used to set everything back to false
    /// </summary>
    public void ResetClicked()
    {
        RbuttonState = false;
        StopRotate();
    }

    /// <summary>
    /// RotateX(), used to set the rotation speed of X
    /// </summary>
    private void RotateX() { ExTK.model.transform.Rotate(ExTK.rotateData.rotatespeed * ExTK.rotateData.rotatespeed, 0, 0); }
    
    /// <summary>
    /// RotateY(), used to set the rotation speed of Y
    /// </summary>
    private void RotateY() { ExTK.model.transform.Rotate(0, ExTK.rotateData.rotatespeed * ExTK.rotateData.rotatespeed, 0); }
    
    /// <summary>
    /// RotateZ(), used to set the rotation speed of Z
    /// </summary>
    private void RotateZ() { ExTK.model.transform.Rotate(0, 0, ExTK.rotateData.rotatespeed * ExTK.rotateData.rotatespeed); }

    /// <summary>
    /// StopRotation(), used to set all rotation bools to false
    /// </summary>
    public void StopRotate()
    {
        RotateXSpeedOn = false;
        RotateYSpeedOn = false;
        RotateZSpeedOn = false;
    }
}