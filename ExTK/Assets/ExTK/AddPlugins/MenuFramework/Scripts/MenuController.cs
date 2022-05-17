using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : ExplorationToolKit
{
    public Camera cam = new Camera();


    public float changePalmX = 0;
    public float changePalmY = 0;
    public float changePalmZ = 0;
    bool checkIfAttached;
    
    //Variables for making background transparent
    public GameObject contentQuad, contentBackPlate, backPlate, warningWindowBack, minPanelBack;
    public bool toggleTransparentBackGround = false;
    public TextMeshPro mainMenuLabelText, MainDescription;
    public TextMeshPro helpText = null, followText = null;
    
    //Used for manipulation of the menu layout
    public GameObject descriptionBox, menuHolder;
    private bool proximityToggle = true;
    private bool transparentToggle = false;
    private AudioFeedBackCustom audioFeedBack;

    [HideInInspector] public GameObject sideMenus;
    
    //Variables for menu control
    [Tooltip("Add more menus to the menu system")]
    public List<GameObject> helpMenus = new List<GameObject>();

    //Variables for menu control
    private bool multiScene = false, allowMenuToggleOnSceneStart = true, isHelpCommandsOn = false;
    private RadialView radialViewMainMenu;
    public GameObject mainMenu, minimizeMenu;

    private string userName;
    private int helpMenuCounter = 0;

    [SerializeField] private string startSpeech, loginSpeech;

    private void Start()
    {
        //IMPORTANT
        //Objects and types need to be instantiated if the item is not found
        if (audioFeedBack == null) { audioFeedBack = GameObject.Find("AudioManager").GetComponent(typeof(AudioFeedBackCustom)) as AudioFeedBackCustom; }
        if (radialViewMainMenu == null) { radialViewMainMenu = GameObject.Find("MenuFramework").GetComponent(typeof(RadialView)) as RadialView; }
        if (mainMenuLabelText == null) { mainMenuLabelText = FindObjectOfType<TextMeshPro>(); }
        if (sideMenus == null) { sideMenus = GameObject.FindGameObjectWithTag("SideMenu"); }
        if (loginSpeech == null || loginSpeech == "") { loginSpeech = ""; }
        if (startSpeech == null) { startSpeech = ""; }

        ApplicationsBeginSpeech();
    }

    /// <summary>
    /// ApplicationBeginSpeech will set the beginning speech when the application starts and then play out through voice feedback
    /// </summary>
    public void ApplicationsBeginSpeech()
    {
        if (startSpeech != null && startSpeech != "")
        {
            audioFeedBack.CustomSpeak(startSpeech, true, false);
            mainMenuLabelText.text = startSpeech;
        }
    }
    public bool checkPalmUp = false;
    public void SetPalm()
    {
        checkPalmUp = !checkPalmUp;
        GameObject.Find("PalmUpController").GetComponent<HandConstraintPalmUp>().enabled = checkPalmUp;
        radialViewMainMenu.enabled = checkPalmUp;
    }
    private bool checkMinimize = false;
    public void SetMinimize()
    {
        checkMinimize = !checkMinimize;
        sideMenus.SetActive(checkMinimize);
    }

    public void OnButtonClick() { Debug.Log("Button Clicked!"); }

    /// <summary>
    /// Update is a default Unity method that will update continously for check changes
    ///  - Example: Checks camera position to menu, changes if needed
    /// </summary>
    void Update()
    {

        if (mainMenu != null)
        {
            if (mainMenu.activeSelf)
            {
                if (proximityToggle && ExTK.additionalsData.followButton && !checkPalmUp)
                {
                    Vector3 screenPos = cam.WorldToScreenPoint(mainMenu.transform.position);

                    if (mainMenu.activeSelf && screenPos.z > 3.0)
                    {
                        radialViewMainMenu.enabled = true;
                        ExTK.additionalsData.followButton.GetComponentsInChildren<TextMeshPro>()[0].text = "Say \"Follow Off\"";
                    }
                }
            }
            if (minimizeMenu != null)
            {
                if (minimizeMenu.activeSelf)
                {
                    //radialViewMinMenu.enabled = true;
                }
            }
        }
    }


    /// <summary>
    /// Login Greeting is used to set the mainmenu label to the given name at the beginning
    /// </summary>
    /// <param name="userText"></param>
    public void loginGreeting(string userText)
    {
        userName = userText;
        if (userText != null)
        {
            if (mainMenuLabel != null)
            {
                mainMenuLabelText.text = "Welcome " + userName + ", " + loginSpeech;
            }
        }
        else
        {
            if (mainMenuLabel != null)
            {
                mainMenuLabelText.text = loginSpeech;
            }
        }
    }

    /// <summary>
    /// TransparentBackGroundOn will turn on the transparent background making the back panels active false
    /// </summary>
    public void TransparentBackGroundOn()
    {
        audioFeedBack.CustomSpeak("Transparent On", true, false);
        contentQuad.SetActive(false);
        contentBackPlate.SetActive(false);
        backPlate.SetActive(false);
    }

    /// <summary>
    /// TransparentBackGroundOff will get the background panels active to false making the invisible
    /// </summary>
    public void TransparentBackGroundOff()
    {
        audioFeedBack.CustomSpeak("Transparent Off", true, false);
        contentQuad.SetActive(true);
        contentBackPlate.SetActive(true);
        backPlate.SetActive(true);
    }


    public void SetDescriptionText(string description)
    {
        MainDescription.text = description;
    }

    public void ResetMenuEdit()
    {
        //menuHolder.transform.localRotation = Quaternion.Slerp(transform.localRotation, OriginalMenuRotationValue, Time.deltaTime);
        //menuHolder.transform.localScale = Vector3.Lerp(transform.localScale, OriginalMenuScaleValue, Time.deltaTime);
        //menuHolder.transform.localPosition = Vector3.Lerp(transform.localPosition, OriginalMenuPositionValue, Time.deltaTime);

        //descriptionBox.transform.localRotation = Quaternion.Slerp(transform.localRotation, OriginalDescriptionRotationValue, Time.deltaTime);
        //descriptionBox.transform.localScale = Vector3.Lerp(transform.localScale, OriginalDescriptionScaleValue, Time.deltaTime);
        //descriptionBox.transform.localPosition = Vector3.Lerp(transform.localPosition, OriginalDescriptionPositionValue, Time.deltaTime);
    }


    /// <summary>
    /// TransparentBackGround will toggle between making the menu system transparent or not for visual aid.
    /// </summary>
    public void TransparentBackGround()
    {
        toggleTransparentBackGround = !toggleTransparentBackGround;
        if (toggleTransparentBackGround) { TransparentBackGroundOn(); }
        else { TransparentBackGroundOff(); }
    }

    bool boundingBox = true;
    public void ModelManipulation()
    {
        boundingBox = !boundingBox;
        ExTK.model.GetComponent<BoxCollider>().enabled = boundingBox;
        ExTK.model.GetComponent<BoundsControl>().enabled = boundingBox;
    }

    /// <summary>
    /// TransparentBackGround will toggle between making the menu system transparent or not for visual aid.
    /// </summary>
    //bool toggleMenuEdit = false;
    //public void MenuEdit()
    //{
    //    toggleMenuEdit = !toggleMenuEdit;
    //    if (toggleMenuEdit)
    //    {
    //        menuholderBounding.enabled = true;
    //        menuholderMH.enabled = true;
    //        descriptionBounding.enabled = true;
    //        descriptionMH.enabled = true;
    //        audioFeedBack.CustomSpeak("Menu Edit On");
    //    }
    //    else
    //    {
    //        menuholderBounding.enabled = false;
    //        menuholderMH.enabled = false;
    //        descriptionBounding.enabled = false;
    //        descriptionMH.enabled = false;
    //        audioFeedBack.CustomSpeak("Menu Edit Off");
    //    }
    //}

    /// <summary>
    /// setDescription this will set the description text in the menu window
    /// </summary>
    public void SetDescription()
    { MainDescription.text = " "; }

    /// <summary>
    /// Sets the children items in the object to inactive when clicking the back button
    /// </summary>
    //public void BackButtonSetChildrenInactive()
    //{ trainingButtons.SetChildrenActive(false); }

    /// <summary>
    /// setMainMenuLabel is used to set the main menu label when ever tracing back or main menu is hit
    /// </summary>
    public void ResetMainMenuLabel()
    {
        if (userName != null)
        {
            if (mainMenuLabel != null)
            {
                mainMenuLabelText.text = userName + " " + loginSpeech;
            }
        }
        else
        {
            mainMenuLabelText.text = "Welcome To the Exploration Toolkit";
        }
    }

    /// <summary>
    /// This method will set the main menu label
    /// </summary>
    /// <param name="sceneName"></param>
    public void SetMainMenuLabel(string sceneName) { mainMenuLabelText.text = sceneName; }

    /// <summary>
    /// ResetMenuSystem this method will set the popUpMenu active or inactive
    /// </summary>

    public void ResetMenuSystem()
    {
        //if (mainMenu != null)
        //{
        //    mainMenu.SetActive(true);
        //}
    }

    private bool menuToggleFollow = false;
    public void ToggleRadialView()
    {
        menuToggleFollow = !menuToggleFollow;
        if (menuToggleFollow) { radialViewMainMenu.enabled = true; }
        else { radialViewMainMenu.enabled = false; }
    }

    /// <summary>
    /// ToggleProximity change the feedback depending on if it is following or not
    /// </summary>
    public void ToggleProximity()
    {
        proximityToggle = !proximityToggle;
        if (audioFeedBack != null)
        {
            if (proximityToggle) { audioFeedBack.CustomSpeak("Proximity On", true, false); }
            else { audioFeedBack.CustomSpeak("Proximity Off", true, false); }
        }
    }


    /// <summary>
    /// ToggleMenuEnableDisable change the feedback depending on if it is following or not
    /// </summary>
    public void ToggleMenuEnableDisable()
    {
        allowMenuToggleOnSceneStart = !allowMenuToggleOnSceneStart;
        if (audioFeedBack != null)
        {
            if (allowMenuToggleOnSceneStart == true) { audioFeedBack.CustomSpeak("Menu Toggle On", true, false); }
            else { audioFeedBack.CustomSpeak("Menu Toggle Off", true, false); }
        }
    }

    /// <summary>
    /// ToggleMenuSetting this method will stop the menu from being toggling going into a scene
    /// </summary>
    public void ToggleMenuSetting()
    {
        //if (allowMenuToggleOnSceneStart == true)
        //{
        //    mainMenu.SetActive(false);
        //    minimizeMenu.SetActive(true);
        //}
    }

    /// <summary>
    /// ToggleOpenMenu will onpen the menu and close the minimize menu
    /// </summary>
    public void ToggleOpenMenu()
    {
        //mainMenu.SetActive(true);
        //minimizeMenu.SetActive(false);
    }

    /// <summary>
    /// ToggleHelpCommands method this will check to see if the user wants the help menu on or off
    /// </summary>
    public void ToggleHelpCommands()
    {
        isHelpCommandsOn = !isHelpCommandsOn;
        if (audioFeedBack != null)
        {
            if (isHelpCommandsOn == true)
            {
                audioFeedBack.CustomSpeak("Help Commands On", true, false);
                helpText.text = "Say \"Help Off\"";
            }
            else
            {
                audioFeedBack.CustomSpeak("Help Commands Off", true, false);
                helpText.text = "Say \"Help On\"";
            }
        }
    }


    /// <summary>
    /// EnableMultiScene will allow the user to run multiple scenes at once
    /// </summary>
    public void EnableMultiScene()
    {
        if (audioFeedBack != null)
        {
            multiScene = !multiScene;
            if (multiScene == false) { audioFeedBack.CustomSpeak("Multi Scene On", true, false); }
            else { audioFeedBack.CustomSpeak("Multi Scene Off", true, false); }
        }
    }

    /// <summary>
    /// EnableMultiScene will allow the user to run multiple scenes at once
    /// </summary>
    public void EnableBacking()
    {
        if (audioFeedBack != null)
        {
            transparentToggle = !transparentToggle;
            if (transparentToggle == false) { audioFeedBack.CustomSpeak("Transparent On", true, false); }
            else { audioFeedBack.CustomSpeak("Transparent Off", true, false); }
        }
    }

    /// <summary>
    /// NextHelpMenu this function will change the help menu displayed
    /// </summary>
    public void NextHelpMenu()
    {
        if (helpMenus.Count > 0)
        {
            if (helpMenus.Count - 1 == helpMenuCounter)
            {
                helpMenus[helpMenuCounter].SetActive(false);
                helpMenuCounter = 0;
                helpMenus[helpMenuCounter].SetActive(true);
            }
            else if (helpMenus.Count > helpMenuCounter)
            {
                helpMenus[helpMenuCounter].SetActive(false);
                helpMenuCounter += 1;
                helpMenus[helpMenuCounter].SetActive(true);
            }
            else
            {
                Debug.LogError("Help Menu is out of scope!");
            }
        }
    }
}
