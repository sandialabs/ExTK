using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AdditionalsData
{
    [SerializeReference]
    public bool additionals, additionalsEnable, exitEnable, resetEnable, minimizeEnable, palmEnable, followEnable;
    [SerializeReference]
    public List<GameObject> additionalsButtons;
    public List<bool> addtionalsBools;
    public List<string> additionalsStrings;
    [SerializeReference]
    public GameObject additionalsButton, exitButton, yesButton, noButton, exitObject, exitCanvas, resetButton, minimizeButton, palmButton, followButton, topPanelCanvas, topPanelButton;
}


[Serializable]
public class Exit : ExplorationToolKit
{
    private bool exitClicked = false;

    /**
    * Standard Unity Methods
    **/
    // Use this for initialization
    void Start()
    {
        ExTK.additionalsData.exitCanvas.SetActive(false);
    }

    /**
    * Exit Control Methods
    **/
    public void BeenClicked()
    {
        exitClicked = !exitClicked;
        if (ExTK.additionalsData.exitCanvas) { ExTK.additionalsData.exitCanvas.SetActive(exitClicked); }
    }

    /// <summary>
    /// Yes Been Clicked is the click event 
    /// for the yes button to close the application
    /// </summary>
    public void YesBeenClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// No Been Clicked is the click event 
    /// for the no button to close the application
    /// </summary>
    public void NoBeenClicked()
    {
        if (ExTK.additionalsData.exitCanvas != null)
        {
            ExTK.additionalsData.exitCanvas.SetActive(false);
        }
    }

    /// <summary>
    /// Default exit button functionality
    /// </summary>
    public void BeenClickedDefault()
    {
        exitClicked = !exitClicked;
        ExTK.additionalsData.yesButton.SetActive(exitClicked);
        ExTK.additionalsData.noButton.SetActive(exitClicked);
    }
}
