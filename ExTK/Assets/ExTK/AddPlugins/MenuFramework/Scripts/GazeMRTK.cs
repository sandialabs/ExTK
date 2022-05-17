using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMRTK : ExplorationToolKit, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    public Vector3 originalPosition, originalScale, distancePositionPositive, distancePositionNegative;
    public Quaternion originalRotation;
    public float distance = .09f;
 
    void Update()
    {
        if (!ExTK.ToolTip)
        {
            ExTK.ToolTip = Instantiate(Resources.Load("Simple Line ToolTip")) as GameObject;
            ExTK.ToolTip.SetActive(false);
        }
        if (ExTK.ToolTip.activeSelf)
        {
            ExTK.ToolTip.transform.position = Vector3.Lerp(ExTK.ToolTip.transform.position, ExTK.currentObject.transform.position, .12f*Time.deltaTime);
        }
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        if (ExTK.menuData.toggleToolTip)
        {
            var item = GetComponent<Renderer>();
            ExTK.ToolTip.SetActive(true);
            ExTK.ToolTip.GetComponent<ToolTipConnector>().Target = gameObject;
            var toolTip = ExTK.ToolTip.GetComponent<ToolTip>();
            toolTip.ToolTipText = gameObject.name;
            ExTK.ToolTip.transform.position = gameObject.transform.position;
            ExTK.currentObject = gameObject;
        }
    }

    public void OnFocusExit(FocusEventData eventData) {ExTK.ToolTip.SetActive(false);}

    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}
