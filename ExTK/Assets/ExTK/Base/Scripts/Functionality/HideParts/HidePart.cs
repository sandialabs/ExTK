using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct HidePartSort
{
    public GameObject hidepartModel;
}

[Serializable]
public struct HidePartsData
{
    [SerializeReference]
    public bool hideparts, hidePartsToggle, hidepartsEnable;
    [SerializeReference]
    public GameObject hidepartbutton;
    public Color32 hidePartColor;
    public Color origColor;
    public List<HidePartSort> hidepartSortment;
}

[Serializable]
public class HidePart : ExplorationToolKit
{
    //How you visualize an object
    private MeshRenderer meshRenderer;

    private void Start()
    {
        if (ExTK == null)
        {
            ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        }
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        hidePartsData.origColor = GetComponent<Renderer>().material.color;
        ExTK.hidePartsData.origColor = hidePartsData.origColor;
    }

    public void BeenClicked()
    {
        if (ExTK.hidePartsData.hidePartsToggle)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        RaycastHit hitinfo;
        if (ExTK.hidePartsData.hidePartsToggle)
        {
            if (Physics.Raycast(headPosition, gazeDirection, out hitinfo) || Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitinfo))
            {
                meshRenderer.enabled = true;

                //Get the current focus object
                var newFocusedObject = hitinfo.collider.gameObject;
                if (newFocusedObject != gameObject) { OnReset(); }
                else { OnSelect(); }
            }
            else { OnReset(); }
        }
    }
    void OnReset()
    {
        var item = gameObject.GetComponent<Renderer>();
        item.material.color = hidePartsData.origColor;
    }

    void OnSelect()
    {
        var item = GetComponent<Renderer>();

        //Highlight color
        item.material.color = ExTK.hidePartsData.hidePartColor;
    }

    private void OnMouseOver()
    {
        if (ExTK.hidePartsData.hidePartsToggle)
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
            }
    }
}
