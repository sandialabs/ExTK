using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ScaleData
{
    [SerializeReference]
    public List<string> scaleList;
    [SerializeReference]
    public List<bool> scaleListBool;
    [SerializeReference]
    public List<GameObject> scaleButtons;
    [SerializeReference]
    public bool scaleEnable, scale, scalingUp, scalingDown, scaleUpBool, scaleDownBool;
    [SerializeReference]
    public GameObject scalebutton, scaleupbutton, scaledownbutton, scalemenu, scaleObject;
    [SerializeReference]
    public float scalespeed;
    [SerializeReference]
    public string scalestring;
    [SerializeReference]
    public int scaleupmax, scaledownmax;
    [SerializeReference]
    public GameObject MenuS;
}


[Serializable]
public class Scale : ExplorationToolKit
{
    [HideInInspector] public bool SbuttonState;

    [HideInInspector] public bool ScaleUpSpeedOn = false;
    [HideInInspector] public bool ScaleDownSpeedOn = false;

    // Start is called before the first frame update
    void Start()
    {
        SbuttonState = false;
        ExTK.scaleData.scalebutton = GameObject.Find("Scale");
        ExTK.scaleData.MenuS = GameObject.Find("ScaleCanvas");
        for (int i = 0; i < ExTK.scaleData.scaleList.Count; i++)
        {
            if (ExTK.scaleData.scaleListBool[i])
            {
                ExTK.scaleData.scaleButtons.Add(GameObject.Find("Scale " + ExTK.scaleData.scaleList[i]));
            }
        }
        ExTK.scaleData.MenuS.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScaleUpSpeedOn) { ScaleUp(); }
        if (ScaleDownSpeedOn) { ScaleDown(); }

        if (SbuttonState)
        {
            ExTK.scaleData.MenuS.SetActive(true);
        }
        else if (SbuttonState == false)
        {
            ExTK.scaleData.MenuS.SetActive(false);
            ExTK.model.GetComponent<Scale>().StopScale();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void BeenClicked()
    {
        SbuttonState = !SbuttonState;
    }

    public void ResetClicked()
    {
        SbuttonState = false;
        ExTK.model.GetComponent<Scale>().StopScale();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scale"></param>
    public void BeenClicked(string scale)
    {
        switch (scale)
        {
            case "Up":
                ScaleUpSpeedOn = !ScaleUpSpeedOn;
                break;
            case "Down":
                ScaleDownSpeedOn = !ScaleDownSpeedOn;
                break;
            default:
                break;
        }
    }

    private void ScaleUp()
    {
        if (ExTK.model.transform.localScale.x < ExTK.scaleData.scaleupmax && ExTK.model.transform.localScale.y < ExTK.scaleData.scaleupmax && ExTK.model.transform.localScale.z < ExTK.scaleData.scaleupmax)
        {
            ExTK.model.transform.localScale += new Vector3(ExTK.scaleData.scalespeed, ExTK.scaleData.scalespeed, ExTK.scaleData.scalespeed);
        }
        else
            //Not allow object to go into negatives 
            ExTK.model.transform.localScale = new Vector3(ExTK.scaleData.scaleupmax, ExTK.scaleData.scaleupmax, ExTK.scaleData.scaleupmax);
            
    }

    private void ScaleDown()
    {
        if (ExTK.model.transform.localScale.x > ExTK.scaleData.scaledownmax && ExTK.model.transform.localScale.y > ExTK.scaleData.scaledownmax && ExTK.model.transform.localScale.z > ExTK.scaleData.scaledownmax)
        {
            ExTK.model.transform.localScale -= new Vector3(ExTK.scaleData.scalespeed, ExTK.scaleData.scalespeed, ExTK.scaleData.scalespeed);
        }
    }

    public void StopScale()
    {
        ScaleUpSpeedOn = false;
        ScaleDownSpeedOn = false;
    }
}
