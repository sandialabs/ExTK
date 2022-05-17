using Microsoft.MixedReality.Toolkit.Examples.Demos;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntegrateMRTK : MonoBehaviour
{
    private List<GameObject> activeButtons = new List<GameObject>();

    ExplorationToolKit ExTK;
    AudioFeedBackCustom audioFeedback;

    /// <summary>
    /// Unity standard method, used to Start and initialize data
    /// </summary>
    public void Start()
    {

        ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        audioFeedback = GameObject.Find("AudioManager").GetComponent<AudioFeedBackCustom>();
        ExTK.menuData.toggleManipulate = false;
        if (ExTK.model != null)
        {

            foreach (var item in ExTK.model.GetComponentsInChildren<MeshRenderer>())
            {
                if (ExTK.menuData.collidersEnable)
                {
                    //MRTK
                    item.gameObject.AddComponent<ObjectManipulator>();
                    item.gameObject.AddComponent<NearInteractionGrabbable>();
                    if (ExTK.menuData.toolTipEnable && ExTK.menuData.manipulateEnable)
                        item.gameObject.AddComponent<GazeMRTK>();
                }
            }
            ExTK.model.AddComponent<MinMaxScaleConstraint>();
            var bounds = ExTK.model.AddComponent<BoundsControl>();
            bounds.Target = ExTK.model;
            bounds.BoundsOverride = ExTK.model.GetComponent<BoxCollider>();
            bounds.BoxDisplayConfig.BoxMaterial = Resources.Load("BoundingBox1") as Material;
            bounds.BoxDisplayConfig.BoxGrabbedMaterial = Resources.Load("BoundingBoxGrabbed1") as Material;

            bounds.ScaleHandlesConfig.HandleMaterial = Resources.Load("BoundingBox1") as Material;
            bounds.ScaleHandlesConfig.HandleGrabbedMaterial = Resources.Load("BoundingBoxGrabbed1") as Material;
            bounds.ScaleHandlesConfig.HandlePrefab = Resources.Load("MRTK_BoundingBox_ScaleHandle1") as GameObject;
            bounds.ScaleHandlesConfig.HandleSize = 0.016f;
            bounds.ScaleHandlesConfig.ColliderPadding = new Vector3(0.016f, 0.016f, 0.016f);
            bounds.ScaleHandlesConfig.HandleSlatePrefab = Resources.Load("MRTK_BoundingBox_ScaleHandle_Slate1") as GameObject;

            bounds.RotationHandlesConfig.HandleMaterial = Resources.Load("BoundingBox1") as Material;
            bounds.RotationHandlesConfig.HandleGrabbedMaterial = Resources.Load("BoundingBoxGrabbed1") as Material;
            bounds.RotationHandlesConfig.HandleSize = 0.016f;
            bounds.RotationHandlesConfig.ColliderPadding = new Vector3(0.016f, 0.016f, 0.016f);
            bounds.RotationHandlesConfig.HandlePrefab = Resources.Load("MRTK_BoundingBox_ScaleHandle1") as GameObject;

            ExTK.model.AddComponent<ObjectManipulator>();
            ExTK.model.AddComponent<NearInteractionGrabbable>();
            ExTK.model.GetComponent<BoundsControl>().enabled = false;
            ExTK.model.GetComponent<BoxCollider>().enabled = true;

            if (ExTK.additionalsData.additionalsEnable)
            {
                activeButtons.Add(ExTK.additionalsData.exitButton);
                activeButtons.Add(ExTK.additionalsData.yesButton);
                activeButtons.Add(ExTK.additionalsData.noButton);
                activeButtons.Add(ExTK.additionalsData.resetButton);

                GameObject menuSystem = GameObject.Find("MenuFramework");
                MenuController menuController = GameObject.Find("MenuFramework").GetComponent<MenuController>();

                ExTK.additionalsData.exitButton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().BeenClicked());
                ExTK.additionalsData.exitButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Are you sure you want to exit"));
                ExTK.additionalsData.yesButton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().YesBeenClicked());
                ExTK.additionalsData.noButton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.additionalsData.exitButton.GetComponent<Exit>().NoBeenClicked());
                ExTK.additionalsData.noButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Canceling"));

                ExTK.additionalsData.resetButton.GetComponent<Interactable>().OnClick.AddListener(() => SetAllToggle());
                ExTK.additionalsData.resetButton.GetComponent<Interactable>().OnClick.AddListener(() => ResetManipulation());
                ExTK.additionalsData.resetButton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.additionalsData.resetButton.GetComponent<Reset>().BeenClicked());
                ExTK.additionalsData.resetButton.GetComponent<Interactable>().OnClick.AddListener(() => SetMaterialsBackToOrig());
                ExTK.additionalsData.resetButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Resetting Model"));

                if (ExTK.additionalsData.followEnable)
                {
                    audioFeedback.followText = ExTK.additionalsData.followButton.GetComponentsInChildren<TextMeshPro>()[0];
                    ExTK.additionalsData.followButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.FollowResponse());
                    ExTK.additionalsData.followButton.GetComponent<Interactable>().OnClick.AddListener(() => menuController.ToggleRadialView());
                }
                if (ExTK.additionalsData.palmEnable)
                {
                    ExTK.additionalsData.palmButton.GetComponent<Interactable>().OnClick.AddListener(() => menuController.SetPalm());
                    ExTK.additionalsData.palmButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Palm", menuController.checkPalmUp, true));
                }
                if (ExTK.additionalsData.minimizeEnable)
                {
                    ExTK.additionalsData.minimizeButton.GetComponent<Interactable>().OnClick.AddListener(() => menuSystem.gameObject.SetActive(false));
                    ExTK.additionalsData.minimizeButton.GetComponent<Interactable>().OnClick.AddListener(() => menuController.minimizeMenu.gameObject.SetActive(true));
                    ExTK.additionalsData.minimizeButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Minimizing"));
                }
            }


            if (ExTK.backgroundData.backgroundEnable)
            {
                activeButtons.Add(ExTK.backgroundData.backgroundbutton);
                //MRTK
                if (ExTK.backgroundData.backgroundbutton.GetComponent<Interactable>())
                {
                    ExTK.backgroundData.backgroundbutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Background>().BeenClicked());
                    ExTK.backgroundData.backgroundbutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Background", ExTK.model.GetComponent<Background>().turnOnBackground, true));
                }

            }

            if (ExTK.scaleData.scaleEnable)
            {
                ExTK.scaleData.scalebutton = GameObject.Find("Scale");
                activeButtons.Add(ExTK.scaleData.scalebutton);
                //MRTK
                if (ExTK.scaleData.scalebutton.GetComponent<Interactable>())
                {
                    if (ExTK.scaleData.scalebutton.GetComponent<Interactable>().Profiles.Count > 1)
                    {
                        ExTK.scaleData.scalebutton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.scaleData.MenuS));
                    }
                    ExTK.scaleData.scalebutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Scale>().BeenClicked());
                    ExTK.scaleData.scalebutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Scale", ExTK.model.GetComponent<Scale>().SbuttonState, true));
                }
                AddSlider(ExTK.scaleData.scalebutton, 2);
                if (ExTK.scaleData.scaleButtons.Count > 0)
                    for (int i = 0; i < ExTK.scaleData.scaleList.Count; i++)
                    {
                        string tempString = ExTK.scaleData.scaleList[i];
                        if (ExTK.scaleData.scaleListBool[i])
                        {
                            activeButtons.Add(ExTK.scaleData.scaleButtons[i]);
                            if (ExTK.scaleData.scaleButtons[i].GetComponent<Interactable>())
                            {
                                ExTK.scaleData.scaleButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Scale>().BeenClicked(tempString));
                                ExTK.scaleData.scaleButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Scale " + tempString, true, false));
                            }
                        }
                    }
            }

            if (ExTK.rotateData.rotateEnable)
            {
                ExTK.rotateData.rotatebutton = GameObject.Find("Rotate");
                activeButtons.Add(ExTK.rotateData.rotatebutton);
                //MRTK
                if (ExTK.rotateData.rotatebutton.GetComponent<Interactable>())
                {
                    if (ExTK.rotateData.rotatebutton.GetComponent<Interactable>().Profiles.Count > 1)
                    {
                        ExTK.rotateData.rotatebutton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.rotateData.MenuR));
                    }
                    ExTK.rotateData.rotatebutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Rotate>().BeenClicked());
                    ExTK.rotateData.rotatebutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Rotate Model", ExTK.model.GetComponent<Rotate>().RbuttonState, true));
                }

                AddSlider(ExTK.rotateData.rotatebutton, 1);
                if (ExTK.rotateData.rotateButtons.Count > 0)
                    for (int i = 0; i < ExTK.rotateData.rotateList.Count; i++)
                    {
                        string tempString = ExTK.rotateData.rotateList[i];
                        if (ExTK.moveData.moveListBool[i])
                        {
                            activeButtons.Add(ExTK.rotateData.rotateButtons[i]);
                            if (ExTK.rotateData.rotateButtons[i].GetComponent<Interactable>())
                            {
                                ExTK.rotateData.rotateButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Rotate>().BeenClicked(tempString));
                                ExTK.rotateData.rotateButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak(tempString + " Axis", true, false));
                            }
                        }
                    }
            }

            if (ExTK.moveData.moveEnable)
            {
                ExTK.moveData.movebutton = GameObject.Find("Move");
                activeButtons.Add(ExTK.moveData.movebutton);
                //MRTK
                if (ExTK.moveData.movebutton.GetComponent<Interactable>())
                {
                    if (ExTK.moveData.movebutton.GetComponent<Interactable>().Profiles.Count > 1)
                        ExTK.moveData.movebutton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.moveData.moveCanvas));

                    ExTK.moveData.movebutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Move>().BeenClicked());
                    ExTK.moveData.movebutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Move Model", ExTK.model.GetComponent<Move>().MbuttonState, true));
                }

                AddSlider(ExTK.moveData.movebutton, 0);
                if (ExTK.moveData.moveButtons.Count > 0)
                    for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
                    {
                        string tempString = ExTK.moveData.moveList[i];
                        if (ExTK.moveData.moveListBool[i])
                        {
                            activeButtons.Add(ExTK.moveData.moveButtons[i]);
                            if (ExTK.moveData.moveButtons[i].GetComponent<Interactable>())
                            {
                                ExTK.moveData.moveButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Move>().BeenClicked(tempString));
                                ExTK.moveData.moveButtons[i].GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Moving " + tempString, true, false));
                            }
                        }
                    }
            }

            //Model Operations
            if (ExTK.menuData.manipulateEnable)
            {
                ExTK.menuData.manipulateButton = GameObject.Find("Manipulate");
                if (ExTK.menuData.toolTipEnable)
                {
                    ExTK.ToolTip = Instantiate(Resources.Load("Simple Line ToolTip")) as GameObject;
                    ExTK.ToolTip.SetActive(false);
                    activeButtons.Add(ExTK.menuData.toolTipButton);
                    ExTK.menuData.toolTipButton.GetComponent<Interactable>().OnClick.AddListener(() => ToggleToolTips());
                    ExTK.menuData.toolTipButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Tool Tips", ExTK.menuData.toggleToolTip, true));
                }

                if (ExTK.hidePartsData.hidepartsEnable)
                {
                    foreach (var parts in ExTK.model.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (parts.gameObject != null)
                        {
                            if (parts.gameObject.GetComponent<MeshCollider>())
                            {
                                parts.gameObject.GetComponent<MeshCollider>().convex = true;
                                parts.gameObject.AddComponent<HidePartMRTK>();
                                parts.gameObject.AddComponent<HidePart>();
                                parts.gameObject.GetComponent<HidePart>().hidePartsData.origColor = parts.gameObject.GetComponent<Renderer>().material.color; 
                                parts.gameObject.GetComponent<HidePart>().enabled=false;
                                parts.gameObject.GetComponent<HidePartMRTK>().materialOriginal = parts.gameObject.GetComponent<Renderer>().material;
                                var partButton = parts.gameObject.AddComponent<Button>();

                            }
                        }
                    }
                    ExTK.hidePartsData.hidepartbutton = GameObject.Find("Hide Parts");

                    activeButtons.Add(ExTK.hidePartsData.hidepartbutton);
                    //MRTK
                    if (GameObject.Find("Hide Parts").GetComponent<Interactable>())
                        GameObject.Find("Hide Parts").GetComponent<Interactable>().OnClick.AddListener(() => ToggleHideParts());

                    GameObject.Find("Hide Parts").GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Hide Parts", ExTK.hidePartsData.hidePartsToggle, true));
                    
                    foreach (var parts in ExTK.model.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (parts.GetComponent<MeshCollider>())
                        {
                            //MRTK
                            if (parts.GetComponent<Interactable>())
                            {
                                parts.GetComponent<Interactable>().OnClick.AddListener(() => parts.gameObject.GetComponent<HidePartMRTK>().BeenClicked());
                                parts.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Hiding " + parts.gameObject.name, true, false));
                            }
                        }
                    }
                }

                if (ExTK.explodeContractData.explodecontractEnable)
                {
                    ExTK.explodeContractData.explodebutton = GameObject.Find("Explode");
                    activeButtons.Add(ExTK.explodeContractData.explodebutton);
                    AddSlider(ExTK.explodeContractData.explodebutton, 3);
                    //MRTK
                    if (ExTK.explodeContractData.explodebutton.GetComponent<Interactable>())
                    {
                        ExTK.explodeContractData.explodebutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<ExplodeContract>().ToggleExplodedView());
                        ExTK.explodeContractData.explodebutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.ExplodeContract());
                        ExTK.explodeContractData.explodebutton.GetComponent<Interactable>().OnClick.AddListener(() => SetExplodeContractText());
                        ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[0].text = "Say 'Explode'";
                        ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[1].text = "Explode";
                    }
                }


                if (ExTK.menuData.manipulateEnable)
                {
                    ExTK.menuData.manipulateCanvas = GameObject.Find("ManipulateCanvas");
                    ExTK.menuData.manipulateCanvas.SetActive(false);

                    activeButtons.Add(ExTK.menuData.manipulateButton);
                    if (ExTK.menuData.manipulateButton.GetComponent<Interactable>().Profiles.Count > 1)
                    {
                        ExTK.menuData.manipulateButton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.menuData.manipulateCanvas));
                    }
                    ExTK.menuData.manipulateButton.GetComponent<Interactable>().OnClick.AddListener(() => ModelManipulationToggle());
                    ExTK.menuData.manipulateButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Manipulate", ExTK.menuData.toggleManipulate, true));
                }
            }

            if (ExTK.animationsEnable)
            {
                activeButtons.Add(ExTK.animationbutton);

                var animScript = ExTK.model.GetComponent<Animations>();
                ExTK.animationbutton = GameObject.Find("Animations");
                //MRTK
                if (ExTK.animationbutton.GetComponent<Interactable>())
                {
                    if (ExTK.animationbutton.GetComponent<Interactable>().Profiles.Count > 1)
                    {
                        ExTK.animationbutton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.animCanvas));
                    }
                    ExTK.animationbutton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Animations>().AnimButtonClicked());
                    ExTK.animationbutton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Animations", ExTK.model.GetComponent<Animations>().AnimButtonState, true));
                }

                AddSlider(ExTK.animationbutton, 4);

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
            else
            {
                if (ExTK.model != null)
                    if (ExTK.model.GetComponent<Animator>())
                    {
                        ExTK.model.GetComponent<Animator>().enabled = false;
                    }
            }

            if (ExTK.subsystemsEnable)
            {
                GameObject subsystemButton;
                if (ExTK.subsystembutton)
                {
                    subsystemButton = ExTK.subsystembutton;
                }
                else
                {
                    subsystemButton = GameObject.Find("Subsystems");
                }
                activeButtons.Add(subsystemButton);
                //MRTK
                if (subsystemButton.GetComponentsInChildren<TextMeshPro>()[0] && subsystemButton.GetComponentsInChildren<TextMeshPro>()[1])
                {
                    subsystemButton.GetComponentsInChildren<TextMeshPro>()[0].text = "Subsystems";
                    subsystemButton.GetComponentsInChildren<TextMeshPro>()[1].text = "Subsystems";
                }
                //MRTK
                if (subsystemButton.GetComponent<Interactable>())
                {
                    if (subsystemButton.GetComponent<Interactable>().Profiles.Count > 1)
                    {
                        subsystemButton.GetComponent<Interactable>().OnClick.AddListener(() => UnToggleAll(ExTK.subSysCanvas));
                    }
                    subsystemButton.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<MenuLayout>().SubsystemButtonBeenClicked());
                    subsystemButton.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak("Subsystems", ExTK.subSysbuttonState, true));
                }
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
            AddVoiceCommands();
        }
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
        //MRTK
        if (button.GetComponent<Interactable>())
        {
            button.GetComponent<Interactable>().OnClick.AddListener(() => animScript.BeenClicked(animClip));
            button.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak(animTitle, true, false));
        }

        if (button.GetComponentsInChildren<TextMeshPro>()[0] && button.GetComponentsInChildren<TextMeshPro>()[1])
        {
            button.GetComponentsInChildren<TextMeshPro>()[0].text = animTitle;
            button.GetComponentsInChildren<TextMeshPro>()[1].text = animTitle;
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
        if (button.GetComponent<Interactable>())
        {
            button.AddComponent<Subsystems>();
            button.GetComponent<Interactable>().OnClick.AddListener(() => ExTK.model.GetComponent<Subsystems>().BeenClicked(model.name));
            button.GetComponent<Interactable>().OnClick.AddListener(() => audioFeedback.CustomSpeak(model.name, model.activeSelf, false));
            button.GetComponent<Interactable>().OnClick.AddListener(() => SetAllSubsystemButtonsToggle());
            activeButtons.Add(button);
        }

        if (button.GetComponentsInChildren<TextMeshPro>()[0] && button.GetComponentsInChildren<TextMeshPro>()[1])
        {
            if (model != null)
            {
                button.GetComponentsInChildren<TextMeshPro>()[0].text = model.name;
                button.GetComponentsInChildren<TextMeshPro>()[1].text = model.name;
            }
        }
    }

    public void UnToggleAll(GameObject canvas)
    {
        foreach (var item in canvas.GetComponentsInChildren<Interactable>())
            item.IsToggled = false;
    }

    public void AddSlider(GameObject button, int index)
    {
        if (button.GetComponentInChildren<ApplySliderMRTK>())
        {
            GameObject slider = Instantiate(Resources.Load("Prefabs/PinchSlider")) as GameObject;
            slider.transform.SetParent(button.GetComponentInChildren<ApplySliderMRTK>().gameObject.transform);
            slider.transform.position = button.GetComponentInChildren<ApplySliderMRTK>().gameObject.transform.position;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetAllToggle()
    {
        foreach (var button in activeButtons)
        {
            button.GetComponent<Interactable>().IsToggled = false;
        }
    }

    public void ResetManipulation()
    {
        ExTK.menuData.toggleManipulate = false;
        ExTK.menuData.manipulateCanvas.SetActive(false);

        ExTK.model.GetComponent<BoxCollider>().enabled = true;
        ExTK.menuData.manipulateCanvas.SetActive(ExTK.menuData.toggleManipulate);
        if (ExTK.menuData.toolTipEnable)
            ExTK.menuData.toggleToolTip = false;
        if (ExTK.hidePartsData.hidepartsEnable)
            ExTK.hidePartsData.hidePartsToggle = false;
    }


    public void SetExplodeContractText()
    {
        string text = "null";
        if (ExTK.model.GetComponent<ExplodeContract>().isInExplodedView)
            text = "Contract";
        else
            text = "Explode";
        if (!ExTK.explodeContractData.explodebutton)
            ExTK.explodeContractData.explodebutton = GameObject.Find("Explode");

        if (ExTK.explodeContractData.explodebutton)
            if (ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[0] && ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[1])
            {
                ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[0].text = text;
                ExTK.explodeContractData.explodebutton.GetComponentsInChildren<TextMeshPro>()[1].text = text;
            }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ModelManipulationToggle()
    {
        ExTK.menuData.toggleManipulate = !ExTK.menuData.toggleManipulate;
        if (ExTK.menuData.toggleManipulate)
        {
            ExTK.model.GetComponent<BoxCollider>().enabled = false;
            ExTK.menuData.manipulateCanvas.SetActive(ExTK.menuData.toggleManipulate);
        }
        else
        {
            ExTK.model.GetComponent<BoxCollider>().enabled = true;
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

    /// <summary>
    /// 
    /// </summary>
    public void ToggleToolTips()
    {
        ExTK.menuData.toggleToolTip = !ExTK.menuData.toggleToolTip;
    }
    /// <summary>
    /// 
    /// </summary>
    public void SetAllSubsystemButtonsToggle()
    {
        foreach (var item in ExTK.subsysSortment)
        {
            if (item.subsysButton != null)
                item.subsysButton.GetComponent<Interactable>().IsToggled = false;
        }
    }

    private void AddVoiceCommands()
    {
        if (GameObject.Find("SpeechManager").GetComponent<SpeechInputHandler>())
        {
            SpeechInputHandler speechHandler = GameObject.Find("SpeechManager").GetComponent<SpeechInputHandler>();
            foreach (var button in activeButtons)
            {
                foreach (var speechCommand in speechHandler.Keywords)
                {
                    if (button.name == speechCommand.Keyword)
                    {
                        speechCommand.Response.AddListener(() => button.GetComponent<Interactable>().TriggerOnClick());
                    }
                }
            }
        }
    }

    public void SetMaterialsBackToOrig()
    {
        foreach (var modelItem in ExTK.model.GetComponents<Renderer>())
        {
            if (ExTK.hidePartsData.hideparts)
                if (modelItem.gameObject.GetComponent<HidePartMRTK>())
                    modelItem.material = modelItem.gameObject.GetComponent<HidePartMRTK>().materialOriginal;
        }
    }
}