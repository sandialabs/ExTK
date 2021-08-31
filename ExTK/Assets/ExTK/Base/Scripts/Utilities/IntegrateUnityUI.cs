using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IntegrateUnityUI : MonoBehaviour
{
    ExplorationToolKit ExTK;
    void Start()
    {
        if (!GameObject.Find("EventSystem"))
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        if (GameObject.Find("Canvas"))
        {
            if (!GameObject.Find("Canvas").GetComponent<GraphicRaycaster>())
                GameObject.Find("Canvas").AddComponent<GraphicRaycaster>();
        }

        if (ExTK == null)
            ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();


        if (ExTK.backgroundData.backgroundEnable)
        {
            ExTK.backgroundData.backgroundbutton = GameObject.Find("Background");
            if (ExTK.backgroundData.backgroundbutton.GetComponent<Button>())
                ExTK.backgroundData.backgroundbutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Background>().BeenClicked());
        }

        //Scale setup buttons
        if (ExTK.scaleData.scaleEnable)
        {
            ExTK.scaleData.scalebutton = GameObject.Find("Scale");
            if (ExTK.scaleData.scalebutton.GetComponent<Button>())
                ExTK.scaleData.scalebutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Scale>().BeenClicked());
            for (int i = 0; i < ExTK.scaleData.scaleList.Count; i++)
            {
                string tempString = ExTK.scaleData.scaleList[i];
                if (ExTK.scaleData.scaleListBool[i])
                {
                    if (ExTK.scaleData.scaleButtons[i].GetComponent<Button>())
                        ExTK.scaleData.scaleButtons[i].GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Scale>().BeenClicked(tempString));
                }
            }
        }

        //Rotate setup buttons
        if (ExTK.rotateData.rotateEnable)
        {
            ExTK.rotateData.rotatebutton = GameObject.Find("Rotate");
            if (ExTK.rotateData.rotatebutton.GetComponent<Button>())
                ExTK.rotateData.rotatebutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Rotate>().BeenClicked());

            for (int i = 0; i < ExTK.rotateData.rotateList.Count; i++)
            {
                string tempString = ExTK.rotateData.rotateList[i];
                if (ExTK.rotateData.rotateListBool[i])
                {
                    if (ExTK.rotateData.rotateButtons[i].GetComponent<Button>())
                        ExTK.rotateData.rotateButtons[i].GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Rotate>().BeenClicked(tempString));
                }
            }
        }

        //Move setup buttons
        if (ExTK.moveData.moveEnable)
        {
            ExTK.moveData.movebutton = GameObject.Find("Move");
            if (ExTK.moveData.movebutton.GetComponent<Button>())
                ExTK.moveData.movebutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Move>().BeenClicked());

            for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
            {
                string tempString = ExTK.moveData.moveList[i];
                if (ExTK.moveData.moveListBool[i])
                {
                    if (ExTK.moveData.moveButtons[i].GetComponent<Button>())
                        ExTK.moveData.moveButtons[i].GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Move>().BeenClicked(tempString));
                }
            }
        }

        //Additionals setup buttons
        if (ExTK.additionalsData.additionalsEnable)
        {

            if (ExTK.additionalsData.resetButton.GetComponent<Button>())
                ExTK.additionalsData.resetButton.GetComponent<Button>().onClick.AddListener(() => ExTK.additionalsData.resetButton.GetComponent<Reset>().BeenClicked());
            if (ExTK.additionalsData.resetButton.GetComponent<RectTransform>())
                ExTK.additionalsData.resetButton.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.mainButtonSizeWidth, ExTK.menuLayout.mainButtonSizeHeight);

            if (ExTK.additionalsData.exitButton.GetComponent<Button>())
                ExTK.additionalsData.exitButton.GetComponent<Button>().onClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().BeenClicked());
            if (ExTK.additionalsData.exitButton.GetComponent<RectTransform>())
                ExTK.additionalsData.exitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.mainButtonSizeWidth, ExTK.menuLayout.mainButtonSizeHeight);

            if (ExTK.additionalsData.yesButton.GetComponent<Button>())
                ExTK.additionalsData.yesButton.GetComponent<Button>().onClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().YesBeenClicked());
            if (ExTK.additionalsData.yesButton.GetComponent<RectTransform>())
                ExTK.additionalsData.yesButton.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.subButtonSizeWidth, ExTK.menuLayout.subButtonSizeHeight);

            if (ExTK.additionalsData.noButton.GetComponent<Button>())
                ExTK.additionalsData.noButton.GetComponent<Button>().onClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().NoBeenClicked());
            if (ExTK.additionalsData.noButton.GetComponent<RectTransform>())
                ExTK.additionalsData.noButton.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.subButtonSizeWidth, ExTK.menuLayout.subButtonSizeHeight);
        }

        if (ExTK.menuData.manipulateEnable)
        {
            //Explode/Contract Model Operations
            if (ExTK.explodeContractData.explodecontractEnable)
            {
                ExTK.explodeContractData.explodebutton = GameObject.Find("Explode");
                if (ExTK.explodeContractData.explodebutton.GetComponent<Button>())
                {
                    ExTK.explodeContractData.explodebutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<ExplodeContract>().ToggleExplodedView());
                    ExTK.explodeContractData.explodebutton.GetComponent<Button>().onClick.AddListener(() => SetExplodeContractText());
                }
            }

            //Hideparts setup buttons
            if (ExTK.hidePartsData.hidepartsEnable)
            {
                ExTK.hidePartsData.hidePartsToggle = false;
                if (GameObject.Find("Hide Parts").GetComponent<Button>())
                    GameObject.Find("Hide Parts").GetComponent<Button>().onClick.AddListener(() => ToggleHideParts());

                foreach (var parts in ExTK.model.GetComponentsInChildren<MeshRenderer>())
                {
                    if (parts.gameObject.GetComponent<MeshCollider>())
                    {
                        parts.gameObject.GetComponent<MeshCollider>().convex = true;
                        parts.gameObject.AddComponent<HidePart>();
                        var partButton = parts.gameObject.AddComponent<Button>();
                        if (parts.gameObject.GetComponent<Button>())
                            parts.gameObject.GetComponent<Button>().onClick.AddListener(() => parts.gameObject.GetComponent<HidePart>().BeenClicked());
                    }
                    else
                    {
                        Debug.LogWarning("Error: Unable to find Hide Part");
                    }
                }
            }

            if (ExTK.menuData.manipulateEnable)
            {
                ExTK.menuData.manipulateButton.GetComponent<Button>().onClick.AddListener(() => ModelManipulationToggle());
                ExTK.menuData.manipulateCanvas.SetActive(false);
            }
        }
        //Animations setup buttons
        if (ExTK.animationsEnable)
        {
            var animScript = ExTK.model.GetComponent<Animations>();
            ExTK.animationbutton = GameObject.Find("Animations");
            if (ExTK.animationbutton.GetComponent<Button>())
                ExTK.animationbutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Animations>().AnimButtonClicked());


            ExTK.animCanvas.SetActive(true);
            int index = 0;
            foreach (var anim in ExTK.animSortment)
            {
                if (anim.animButton == null)
                    BuildAnimButtons(GameObject.Find("AnimButton" + index), animScript, anim.animClip, anim.animTitle);
                else
                    BuildAnimButtons(anim.animButton, animScript, anim.animClip, anim.animTitle);

                index++;
            }
            ExTK.animCanvas.SetActive(false);
        }

        //Subsystems setup buttons
        if (ExTK.subsystemsEnable)
        {
            var subsystemScript = ExTK.model.GetComponent<Subsystems>();

            if (ExTK.subsystembutton.GetComponent<Button>())
                ExTK.subsystembutton.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<MenuLayout>().SubsystemButtonBeenClicked());

            ExTK.subSysCanvas.SetActive(true);
            int index = 0;
            foreach (var subsystem in ExTK.subsysSortment)
            {
                if (!subsystem.subsysButton)
                    BuildSubSystemButtons(GameObject.Find("SubButton" + index), subsystem.subsysModel);
                else
                    BuildSubSystemButtons(subsystem.subsysButton, subsystem.subsysModel);
                index++;
            }
            ExTK.subSysCanvas.SetActive(false);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <param name="model"></param>
    public void BuildSubSystemButtons(GameObject button, GameObject model)
    {
        //MRTK
        if (button.GetComponent<Button>())
        {
            button.AddComponent<Subsystems>();
            button.GetComponent<Button>().onClick.AddListener(() => ExTK.model.GetComponent<Subsystems>().BeenClicked(model.name));
        }

        if (button.GetComponentInChildren<TextMeshPro>())
            if (model != null)
                button.GetComponentInChildren<TextMeshPro>().text = model.name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <param name="animScript"></param>
    /// <param name="animClip"></param>
    /// <param name="animTitle"></param>
    public void BuildAnimButtons(GameObject button, Animations animScript, AnimationClip animClip, string animTitle)
    {
        if (button.GetComponent<Button>())
            button.GetComponent<Button>().onClick.AddListener(() => animScript.BeenClicked(animClip));

        if (button.GetComponentInChildren<TextMeshPro>())
            button.GetComponentInChildren<TextMeshPro>().text = animTitle;
    }

    public void SetExplodeContractText()
    {
        string text = "null";
        if (ExTK.model.GetComponent<ExplodeContract>().isInExplodedView)
            text = "Contract";
        else
            text = "Explode";
        if (ExTK.explodeContractData.explodebutton)
            if (ExTK.explodeContractData.explodebutton.GetComponentInChildren<TextMeshPro>())
                ExTK.explodeContractData.explodebutton.GetComponentInChildren<TextMeshPro>().text = text;
    }


    /// <summary>
    /// 
    /// </summary>
    public void ModelManipulationToggle()
    {
        ExTK.menuData.toggleManipulate = !ExTK.menuData.toggleManipulate;
        if (ExTK.menuData.toggleManipulate)
        {
            ExTK.menuData.manipulateCanvas.SetActive(ExTK.menuData.toggleManipulate);
        }
        else
        {
            ExTK.menuData.manipulateCanvas.SetActive(ExTK.menuData.toggleManipulate);
            if (ExTK.menuData.toolTipEnable)
                ExTK.menuData.toggleToolTip = false;
            if (ExTK.hidePartsData.hidepartsEnable)
                ExTK.hidePartsData.hidePartsToggle = false;
        }
    }

    /// <summary>
    /// ToggleHideParts is used to enable hide parts
    /// </summary>
    public void ToggleHideParts()
    {
        ExTK.hidePartsData.hidePartsToggle = !ExTK.hidePartsData.hidePartsToggle;
    }
}
