using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[Serializable]
public class HidePartMRTK : ExplorationToolKit, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    private MeshRenderer meshRenderer; //How you visualize an object
    public Material materialOriginal;
    private void Start()
    {
        if (ExTK == null)
        {
            ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        }
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        hidePartsData.origColor = GetComponent<Renderer>().material.color;
        materialOriginal = GetComponent<Renderer>().material;
        ExTK.hidePartsData.origColor = hidePartsData.origColor;
    }

    public void BeenClicked()
    {
        if (ExTK.hidePartsData.hidePartsToggle)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Renderer>().material.color = materialOriginal.color;
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
        item.material.color = ExTK.hidePartsData.hidePartColor; //Highlight color
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (ExTK.hidePartsData.hidePartsToggle) { gameObject.GetComponent<Renderer>().material.color = materialOriginal.color; gameObject.SetActive(false); }
        else { gameObject.SetActive(true); }
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        if (ExTK.hidePartsData.hidePartsToggle)
        {
            var item = GetComponent<Renderer>();
            item.material.color = ExTK.hidePartsData.hidePartColor; //Highlight color
        }
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        var item = gameObject.GetComponent<Renderer>();
        item.material.color = hidePartsData.origColor;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData) { gameObject.GetComponent<Renderer>().material.color = materialOriginal.color; }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { gameObject.GetComponent<Renderer>().material.color = materialOriginal.color; }

}
