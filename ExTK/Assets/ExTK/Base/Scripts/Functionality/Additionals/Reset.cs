using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Reset : ExplorationToolKit
{

    ////Save location and orientation of model
    private Quaternion OriginalRotationValue;
    private Vector3 OriginalScaleValue;
    private Vector3 OriginalPositionValue;


    ////Lerp and Slerp need a float value, but it doesn't change anything
    //float SpeedVal = 5.0f;
    /**
    * Standard Unity Methods  
    **/
    // Use this for initialization
    private void Start()
    {
        ////Store original rotational value for main model
        OriginalRotationValue = ExTK.model.transform.localRotation;
        OriginalScaleValue = ExTK.model.transform.localScale;
        OriginalPositionValue = ExTK.model.transform.localPosition;

        GetComponent<Reset>().enabled = false;
    }

    /// <summary>
    /// Reset will trigger the reset functionality
    /// </summary>
    public void BeenClicked()
    {
        if (ExTK.animationsEnable)
        {
            ExTK.model.GetComponent<Animations>().ResetAnimation();
            ExTK.model.GetComponent<Animator>().enabled = false;
            ExTK.model.GetComponent<Animations>().AnimButtonState = false;
        }

        ExTK.model.transform.localScale = new Vector3(1f,1f,1f);

        //Subsystem Components
        if (ExTK.subsystemsEnable)
        {
            ExTK.model.GetComponent<Subsystems>().SetModelActive();
            ExTK.subSysbuttonState = false;
            ExTK.subSysCanvas.SetActive(false);
        }

        foreach (var modelItem in ExTK.modelOriginal)
        {
            if (modelItem.originalGameObject)
            {
                modelItem.originalGameObject.SetActive(true);
                if (modelItem.originalGameObject.GetComponent<HidePart>())
                    modelItem.originalGameObject.GetComponent<Renderer>().material.color = modelItem.originalGameObject.GetComponent<HidePart>().hidePartsData.origColor;
            }
        }



        if (ExTK.menuData.manipulateEnable)
        {
            // Explode Components
            if (ExTK.explodeContractData.explodecontractEnable)
            {
                //Contract the model if exploded
                ExTK.model.GetComponent<ExplodeContract>().isInExplodedView = false;
                ExTK.model.GetComponent<ExplodeContract>().enabled = false;
                if (!ExTK.explodeContractData.explodebutton)
                {
                    ExTK.menuData.manipulateCanvas.SetActive(true);
                    ExTK.explodeContractData.explodebutton = GameObject.Find("Explode");
                }
                if (ExTK.explodeContractData.explodebutton.GetComponent<Button>())
                {
                    if (ExTK.explodeContractData.explodebutton.GetComponent<Button>().GetComponent<TextMeshPro>())
                        ExTK.explodeContractData.explodebutton.GetComponent<Button>().GetComponent<TextMeshPro>().text = "Explode";
                }
                else
                {
                    ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[0].text = "Explode";
                    ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[1].text = "Explode";
                }
            }

            //Hide Part Components
            if (ExTK.hidePartsData.hidepartsEnable)
            {
                ExTK.hidePartsData.hidePartsToggle = false;
                //foreach (var parts in ExTK.hidePartsData.hidepartSortment)
                //{

                    //parts.hidepartModel.SetActive(true);
                    //if (parts.hidepartModel.GetComponent<Renderer>())
                    //    parts.hidepartModel.GetComponent<Renderer>().material.color = ExTK.hidePartsData.origColor;
                    //else
                    //{
                    //    parts.hidepartModel.AddComponent<Renderer>();
                    //    if (parts.hidepartModel.GetComponent<Renderer>())
                    //        parts.hidepartModel.GetComponent<Renderer>().material.color = ExTK.hidePartsData.origColor;
                    //}
                //}
            }
            ExTK.menuData.manipulateCanvas.SetActive(false);
        }


        int counter = 0;
        foreach (var item in ExTK.model.GetComponentsInChildren<MeshRenderer>())
        {
            if (item != null && ExTK.modelOriginal[counter] != null)
            {
                item.transform.position = ExTK.modelOriginal[counter].originalPosition;
                item.transform.rotation = ExTK.modelOriginal[counter].originalRotation;
                //item.transform.localScale = ExTK.modelOriginal[counter].originalScale;
            }

            if (ExTK.model.GetComponent<ExplodeContract>())
            {
                if (counter < ExTK.model.GetComponent<ExplodeContract>().childMeshRenderers.Count && counter < ExTK.modelOriginal.Count)
                {
                    ExTK.model.GetComponent<ExplodeContract>().childMeshRenderers[counter].originalPosition = ExTK.modelOriginal[counter].originalPosition;
                    ExTK.model.GetComponent<ExplodeContract>().childMeshRenderers[counter].originalRotation = ExTK.modelOriginal[counter].originalRotation;
                    //ExTK.model.GetComponent<ExplodeContract>().childMeshRenderers[counter].originalScale = ExTK.modelOriginal[counter].originalScale;
                }
            }
            counter++;
        }

        //background
        if (ExTK.backgroundData.backgroundEnable) { ExTK.model.GetComponent<Background>().ResetBackground(); }

        //Scale Components
        if (ExTK.scaleData.scaleEnable) { ExTK.model.GetComponent<Scale>().ResetClicked(); }

        //Rotate Components
        if (ExTK.rotateData.rotateEnable) { if (ExTK.model.GetComponent<Rotate>().RbuttonState == true) { ExTK.model.GetComponent<Rotate>().ResetClicked(); } }

        //Moving Components
        if (ExTK.moveData.moveEnable) { if (ExTK.model.GetComponent<Move>().MbuttonState == true) { ExTK.model.GetComponent<Move>().ResetClicked(); } }

        // //Main Model reset
        //ExTK.model.transform.localRotation = OriginalRotationValue;
        //ExTK.model.transform.localPosition = OriginalPositionValue;
        //ExTK.model.transform.localScale = OriginalScaleValue;
    }
}