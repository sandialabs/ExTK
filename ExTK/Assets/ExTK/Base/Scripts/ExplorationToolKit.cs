using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExplorationToolKit : MonoBehaviour
{
    [HideInInspector] public List<object> functionalityList;

    [HideInInspector] public int currentTab;
    [HideInInspector] public MenuLayout menuLayout;
    [HideInInspector] public BoxCollider menuBoxCollider;
    [HideInInspector] public GameObject mainMenuLabel;

    [Header("Drag and drop your model here to get started")]
    //Model control
    public GameObject model;
    
    //[Header("Drag and drop animator controller here to enable animations")]
    public RuntimeAnimatorController animatorController;
    public GameObject buttonPrefab;

    [HideInInspector] public List<CircleData> circleData;
    [HideInInspector] public MenuData menuData;
    
    //Tab 1: Defaults
    [HideInInspector] public BackgroundData backgroundData;
    [HideInInspector] public ScaleData scaleData;
    [HideInInspector] public RotateData rotateData;
    [HideInInspector] public MoveData moveData;
    [HideInInspector] public AdditionalsData additionalsData;

    //Tab 2: Models
    [HideInInspector] public ExplodeContractData explodeContractData;
    [HideInInspector] public bool animations, animationsEnable;
    [HideInInspector] public float animationspeed;
    [HideInInspector] public GameObject animationbutton, animmenu, animObject, animCanvas;
    [HideInInspector] public List<AnimSort> animSortment;

    [HideInInspector] public HidePartsData hidePartsData;
    [HideInInspector] public bool subsystems, subsystemsEnable, subSysbuttonState;
    [HideInInspector] public GameObject subsystembutton, subsystemmenu, subsystemObject, subSysCanvas;
    [HideInInspector] public List<SubsystemSort> subsysSortment;
    [HideInInspector] public Subsystems subSystemsScript;

    [HideInInspector] public List<ModelChildMeshes> modelOriginal;
    [HideInInspector] public ExplorationToolKit ExTK;
    [HideInInspector] public GameObject ToolTip, currentObject;


    /// <summary>
    /// Unity standard method, Awake is called before runtime to initialize data
    /// </summary>
    private void Awake()
    {
        ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        modelOriginal = new List<ModelChildMeshes>();

        if (model != null)
        {
            foreach (var item in model.GetComponentsInChildren<MeshRenderer>())
            {
                item.gameObject.AddComponent<MeshCollider>();
                item.gameObject.GetComponent<MeshCollider>().convex = true;

                ModelChildMeshes mesh = new ModelChildMeshes();
                mesh.meshRenderer = item;
                mesh.originalPosition = item.transform.position;
                mesh.originalRotation = item.transform.rotation;
                modelOriginal.Add(mesh);
            }

            ExTK.model.AddComponent<MenuLayout>();
            // ExTK Functionality
            if (backgroundData.backgroundEnable)
            {
                model.AddComponent<Background>();
                ExTK.backgroundData.backgroundbutton = GameObject.Find("Background");
            }
            //Scale
            if (scaleData.scaleEnable)
            {
                model.AddComponent<Scale>();
                for (int i = 0; i < ExTK.scaleData.scaleList.Count; i++)
                    if (ExTK.scaleData.scaleListBool[i])
                        ExTK.scaleData.scaleButtons.Add(GameObject.Find("Scale " + ExTK.scaleData.scaleList[i]));
            }
            //Rotate
            if (rotateData.rotateEnable)
            {
                model.AddComponent<Rotate>();
                rotateData.MenuR = GameObject.Find("RotateCanvas");
                for (int i = 0; i < ExTK.rotateData.rotateList.Count; i++)
                    if (ExTK.rotateData.rotateListBool[i])
                        ExTK.rotateData.rotateButtons.Add(GameObject.Find(ExTK.rotateData.rotateList[i] + " Axis"));
            }
            //Additionals
            if (additionalsData.additionalsEnable)
            {
                ExTK.additionalsData.exitCanvas = GameObject.Find("ExitCanvas");
                ExTK.additionalsData.exitButton = GameObject.Find("Exit");
                ExTK.additionalsData.yesButton = GameObject.Find("Yes");
                ExTK.additionalsData.noButton = GameObject.Find("No");
                ExTK.additionalsData.resetButton = GameObject.Find("Reset");
                ExTK.additionalsData.exitButton.AddComponent<Exit>();
                ExTK.additionalsData.resetButton.AddComponent<Reset>();
                if (GameObject.Find("Palm"))
                    ExTK.additionalsData.palmButton = GameObject.Find("Palm");
                if (GameObject.Find("Minimize"))
                    ExTK.additionalsData.minimizeButton = GameObject.Find("Minimize");
                if (GameObject.Find("Follow"))
                    ExTK.additionalsData.followButton = GameObject.Find("Follow");
            }
            //Move
            if (moveData.moveEnable)
            {
                model.AddComponent<Move>();
                moveData.moveCanvas = GameObject.Find("MoveCanvas");
                for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
                    if (ExTK.moveData.moveListBool[i])
                        ExTK.moveData.moveButtons.Add(GameObject.Find("Move " + ExTK.moveData.moveList[i]));
            }
            //Manipulation
            if (menuData.manipulateEnable)
            {
                if (explodeContractData.explodecontractEnable)
                    model.AddComponent<ExplodeContract>();

                if (menuData.toolTipEnable)
                    menuData.toolTipButton = GameObject.Find("Tool Tips");

                if (hidePartsData.hidepartsEnable)
                    hidePartsData.hidePartsToggle = false;

                menuData.manipulateButton = GameObject.Find("Manipulate");
                menuData.manipulateCanvas = GameObject.Find("ManipulateCanvas");
            }
            //Animations
            if (animationsEnable)
            {
                model.AddComponent<Animations>();
                animationbutton = GameObject.Find("Animations");
                animCanvas = GameObject.Find("AnimationsCanvas");
            }
            //Subsystems
            if (subsystemsEnable)
            {
                ExTK.subSystemsScript = model.AddComponent<Subsystems>();
                ExTK.subsystembutton = GameObject.Find("Subsystems");
                ExTK.subSysCanvas = GameObject.Find("SubsystemsCanvas");
                ExTK.subSysCanvas.SetActive(false);
                ExTK.subSysbuttonState = false;
            }
        }
    }
    /// <summary>
    /// On Enable to set the exploration toolkit 
    /// </summary>
    private void OnEnable()
    {
        ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
    }
}