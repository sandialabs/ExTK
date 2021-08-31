using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Utils : ExplorationToolKit
{
    /// <summary>
    /// GenerateGameObjects(), used to build up the gameobjects for the menu
    /// </summary>
    /// <param name="ExTK"></param>
    /// <param name="buttonObject"></param>
    /// <param name="name"></param>
    /// <param name="directory"></param>
    /// <param name="isButton"></param>
    /// <param name="isEnabled"></param>
    public static GameObject GenerateGameObjects(ExplorationToolKit ExTK, GameObject buttonObject, string name, string directory, bool isButton, bool isEnabled)
    {
        if (!ExTK.menuLayout)
            ExTK.menuLayout = ExTK.gameObject.AddComponent<MenuLayout>();
        if (isEnabled)
        {
            if (!GameObject.Find(name))
            {
                if (isButton)
                {
                    buttonObject = Instantiate(ExTK.buttonPrefab);

                    if (buttonObject.GetComponent<Button>())
                    {
                        buttonObject.GetComponentInChildren<TextMeshPro>().text = name;
                    }
                    else
                    {
                        if (buttonObject.GetComponentsInChildren<TextMeshPro>()[0] && buttonObject.GetComponentsInChildren<TextMeshPro>()[1])
                        {
                            buttonObject.GetComponentsInChildren<TextMeshPro>()[0].text = "Say " + "'" + name + "'";
                            buttonObject.GetComponentsInChildren<TextMeshPro>()[1].text = name;
                        }
                    }
                }
                else
                {
                    buttonObject = new GameObject(name);
                    if (!ExTK.menuData.circular)
                        VerticalHorizontalButtons(ExTK, GameObject.Find(name), false);
                    else
                        CircularButtons(ExTK, buttonObject, isButton);
                }
                buttonObject.name = name;
                if (buttonObject.GetComponent<RectTransform>())
                    buttonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.mainButtonSizeWidth, ExTK.menuLayout.mainButtonSizeHeight);

                if (GameObject.Find(directory))
                {
                    GameObject newCanvas = GameObject.Find(directory);
                    buttonObject.transform.SetParent(newCanvas.transform);
                    if (buttonObject.GetComponent<RectTransform>())
                        buttonObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }
            else
            {
                if (!isButton)
                {
                    if (!ExTK.menuData.circular)
                        VerticalHorizontalButtons(ExTK, GameObject.Find(name), true);
                    else
                        CircularButtons(ExTK, buttonObject, isButton);
                }
            }
            return buttonObject;
        }
        else
        {
            if (GameObject.Find(name))
                DestroyImmediate(GameObject.Find(name));
            return null;
        }
    }

    public static void CircularButtons(ExplorationToolKit ExTK, GameObject buttonObject, bool isButton)
    {
        if (!isButton && buttonObject)
        {
            if (buttonObject.GetComponent<VerticalLayoutGroup>())
                DestroyImmediate(buttonObject.GetComponent<VerticalLayoutGroup>());
            if (buttonObject.GetComponent<HorizontalLayoutGroup>())
                DestroyImmediate(buttonObject.GetComponent<HorizontalLayoutGroup>());
        }
    }

    /// <summary>
    /// For the main buttons this will change the orientation
    /// </summary>
    /// <param name="menuItem"></param>
    /// <param name="obj"></param>
    public static void VerticalHorizontalButtons(ExplorationToolKit ExTK, GameObject menuItem, bool obj)
    {
        if (menuItem != null)
        {
            if (ExTK.menuData.horizontal && !menuItem.GetComponent<HorizontalLayoutGroup>() && !menuItem.GetComponent<VerticalLayoutGroup>())
            {
                menuItem.AddComponent<VerticalLayoutGroup>();
                menuItem.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
                if (menuItem.name != "ButtonLayout")
                    menuItem.GetComponent<VerticalLayoutGroup>().spacing = -.466f;
                else
                {
                    menuItem.GetComponent<VerticalLayoutGroup>().spacing = -2.2f;
                    menuItem.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(-1, -1, 0, 0);
                }
            }
            else
            {
                if (menuItem.GetComponent<VerticalLayoutGroup>())
                    DestroyImmediate(menuItem.GetComponent<VerticalLayoutGroup>());
            }

            if (ExTK.menuData.vertical && !menuItem.GetComponent<VerticalLayoutGroup>() && !menuItem.GetComponent<HorizontalLayoutGroup>())
            {
                menuItem.AddComponent<HorizontalLayoutGroup>();
                menuItem.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
                if (menuItem.name != "ButtonLayout")
                    menuItem.GetComponent<HorizontalLayoutGroup>().spacing = -.466f;
                else
                {
                    menuItem.GetComponent<HorizontalLayoutGroup>().spacing = -2.2f;
                    menuItem.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, -1, -1);
                }
            }
            else
            {
                if (menuItem.GetComponent<HorizontalLayoutGroup>())
                    DestroyImmediate(menuItem.GetComponent<HorizontalLayoutGroup>());
            }
        }
    }


    public static void GenerateCanvasObj(ExplorationToolKit ExTK, bool enabled, string type, GameObject check)
    {
        if (check == null)
        {
            List<string> canvasObj = new List<string>(new string[] { "Obj", "Canvas" });
            if (type != "Exit" && type != "TopPanel") { GenerateGameObjects(ExTK, null, type + canvasObj[0], "ButtonLayout", false, enabled); }
            else if (type == "Exit") { GenerateGameObjects(ExTK, null, type + canvasObj[0], "TopPanelCanvas", false, enabled); }
            else { GenerateGameObjects(ExTK, null, type + canvasObj[0], "TopPanel", false, enabled); }
            GenerateGameObjects(ExTK, null, type, type + canvasObj[0], true, enabled);
            GenerateGameObjects(ExTK, null, type + canvasObj[1], type + canvasObj[0], false, enabled);
            if (type == "TopPanel")
            {
                DestroyImmediate(GameObject.Find("TopPanelCanvas").GetComponent<VerticalLayoutGroup>());
                DestroyImmediate(GameObject.Find("TopPanelCanvas").GetComponent<HorizontalLayoutGroup>());
                HorizontalLayoutGroup hl = GameObject.Find("TopPanelCanvas").AddComponent<HorizontalLayoutGroup>();
                hl.padding = new RectOffset(-1, -1, 0, 0);
                hl.spacing = -2.7f;
                hl.childAlignment = TextAnchor.MiddleCenter;
            }
        }
    }

    public static void EnableMenuLayout(ExplorationToolKit ExTK, bool horizontal, bool vertical, bool circular)
    {
        List<string> canvasObjCheck = new List<string>(new string[] { "AnimationsObj", "AnimationsCanvas", "SubsystemsObj", "SubsystemsCanvas", "ManipulateObj", "ManipulateCanvas", "ScaleObj", "ScaleCanvas", "RotateObj", "RotateCanvas", "MoveObj", "MoveCanvas" });
        bool isObj = true;
        ExTK.menuData.buttonlayoutmenu = GameObject.Find("ButtonLayout");

        if (horizontal)
        {
            ExTK.menuData.vertical = false;
            if (ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>())
            {
                DestroyImmediate(ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>());
            }
            if (!ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>() && !ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>())
            {
                HorizontalLayoutGroup hlg = ExTK.menuData.buttonlayoutmenu.AddComponent<HorizontalLayoutGroup>();
                hlg.padding = new RectOffset(-1, -1, 0, 0);
                hlg.spacing = -2.2f;
                hlg.childAlignment = TextAnchor.MiddleCenter;
                ExTK.menuData.buttonlayoutmenu.transform.localPosition = new Vector3(0f, 0f, 0f);
                foreach (var item in canvasObjCheck)
                {
                    if (GameObject.Find(item))
                        Utils.VerticalHorizontalButtons(ExTK, GameObject.Find(item), isObj);
                    isObj = !isObj;
                    if (GameObject.Find(item))
                        if (GameObject.Find(item).GetComponent<RectTransform>())
                            GameObject.Find(item).GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.mainButtonSizeWidth, ExTK.menuLayout.mainButtonSizeHeight);
                }
            }

            if (ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>())
            {
                if (ExTK.menuBoxCollider != null && ExTK.mainMenuLabel != null)
                {
                    ExTK.menuBoxCollider.center = new Vector3(0.005456698f, 0.01108723f, 0.05904647f);
                    ExTK.menuBoxCollider.size = new Vector3(1.608736f, 0.06656913f, 0.009999999f);
                    ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMax = new Vector2(ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMax.x, 400f);
                    ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMin = new Vector2(ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMin.x, 339.6f);
                }
            }
        }
        else
        {
            foreach (var item in canvasObjCheck)
            {
                if (GameObject.Find(item))
                    if (GameObject.Find(item).GetComponent<VerticalLayoutGroup>())
                        DestroyImmediate(GameObject.Find(item).GetComponent<VerticalLayoutGroup>());
            }
            DestroyImmediate(ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>());
        }

        if (vertical)
        {
            ExTK.menuData.horizontal = false;

            if (ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>())
            {
                DestroyImmediate(ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>());
            }
            if (!ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>() && !ExTK.menuData.buttonlayoutmenu.GetComponent<HorizontalLayoutGroup>())
            {
                VerticalLayoutGroup vlg = ExTK.menuData.buttonlayoutmenu.AddComponent<VerticalLayoutGroup>();
                vlg.padding = new RectOffset(0, 0, -1, -1);
                vlg.spacing = -2.2f;
                vlg.childAlignment = TextAnchor.MiddleLeft;
                ExTK.menuData.buttonlayoutmenu.transform.localPosition = new Vector3(-0.3f, 0f, 0f);
                foreach (var item in canvasObjCheck)
                {
                    if (GameObject.Find(item))
                        Utils.VerticalHorizontalButtons(ExTK, GameObject.Find(item), isObj);
                    isObj = !isObj;
                }
            }

            if (ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>())
            {
                if (ExTK.menuBoxCollider != null && ExTK.mainMenuLabel != null)
                {
                    ExTK.menuBoxCollider.center = new Vector3(-0.7087212f, -0.09897432f, 0.05904647f);
                    ExTK.menuBoxCollider.size = new Vector3(0.1225562f, 0.7520525f, 0.01f);
                    ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMax = new Vector2(ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMax.x, 700f);
                    ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMin = new Vector2(ExTK.mainMenuLabel.GetComponent<RectTransform>().offsetMin.x, 661f);
                }
            }
        }
        else
        {
            foreach (var item in canvasObjCheck)
            {
                if (GameObject.Find(item))
                    if (GameObject.Find(item).GetComponent<HorizontalLayoutGroup>())
                        DestroyImmediate(GameObject.Find(item).GetComponent<HorizontalLayoutGroup>());
            }
            DestroyImmediate(ExTK.menuData.buttonlayoutmenu.GetComponent<VerticalLayoutGroup>());
        }
    }
}
