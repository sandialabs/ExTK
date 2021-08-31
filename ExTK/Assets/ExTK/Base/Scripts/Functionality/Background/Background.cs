using System;
using UnityEngine;

[Serializable]
public struct BackgroundData
{
    [SerializeReference]
    public bool background, backgroundEnable, cleanRoom, custom;
    [SerializeReference]
    public GameObject backgroundbutton, cleanRoomObj, customObj;
}

[Serializable]
public class Background : ExplorationToolKit
{
    public bool turnOnBackground = false;

   /// <summary>
   /// Will activate the background,Background Event Methods
   /// </summary>
    public void BeenClicked()
    {
        turnOnBackground = !turnOnBackground;
        if ((ExTK.backgroundData.cleanRoom && ExTK.backgroundData.cleanRoomObj != null))
        {
            ExTK.backgroundData.cleanRoomObj.SetActive(turnOnBackground);
        }
        if((ExTK.backgroundData.custom && ExTK.backgroundData.customObj != null))
        {
            ExTK.backgroundData.customObj.SetActive(turnOnBackground);
        }
    }

    /// <summary>
    /// ResetBackground, used to set the background invisible
    /// </summary>
    public void ResetBackground()
    {
        if (ExTK.backgroundData.cleanRoomObj != null)
        {
            ExTK.backgroundData.cleanRoomObj.SetActive(false);
            turnOnBackground = false;
        }

        if (ExTK.backgroundData.customObj != null)
        {
            ExTK.backgroundData.customObj.SetActive(false);
            turnOnBackground = false;
        }
    }

    /// <summary>
    /// Standard Unity Methods
    /// Use this for initialization
    /// </summary> 
    private void Start()
    {
        ExTK.backgroundData.backgroundbutton = GameObject.Find("Background");

        if (ExTK.backgroundData.cleanRoom)
        {
            GameObject cleanRoom = Instantiate(Resources.Load("CleanRoom") as GameObject);
            cleanRoom.name = "CleanRoom";
            ExTK.backgroundData.cleanRoomObj = cleanRoom;
            ExTK.backgroundData.cleanRoomObj = GameObject.Find(cleanRoom.name);
            ExTK.backgroundData.cleanRoomObj.SetActive(false);
        }

        if (ExTK.backgroundData.customObj != null)
        {
            ExTK.backgroundData.customObj.SetActive(false);
        }
    }
}
