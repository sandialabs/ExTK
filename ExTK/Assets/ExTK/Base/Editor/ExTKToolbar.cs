using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExTKToolbar : MonoBehaviour
{
    public ExplorationToolKit ExTK;
    public GameObject buttonPrefab;

    private void OnEnable()
    {
        if (ExTK == null)
        {
            ExTK = GameObject.Find("ExplorationToolKit").GetComponent<ExplorationToolKit>();
        }

        buttonPrefab = ExTK.buttonPrefab;
    }

    [MenuItem("ExTK/Add Component/Add Exploration ToolKit")]
    static void CreateExTK()
    {
        //Add ExTK GameObject with script
        GameObject ExplorationToolKit = new GameObject("ExplorationToolKit");
        ExplorationToolKit.AddComponent(typeof(ExplorationToolKit));

        if (!GameObject.Find("Canvas"))
        {
            GameObject canvas = new GameObject("Canvas");

            if (canvas.GetComponent<RectTransform>())
                canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0.6f, 0.6f);
            if (GameObject.Find("Main Camera"))
            {
                var canvasItem = canvas.AddComponent<Canvas>();
                canvasItem.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>() as Camera;
            }
            else
            {
                GameObject camera = new GameObject("Main Camera");
                camera.AddComponent<Camera>();
                var canvasItem = canvas.AddComponent<Canvas>();
                canvasItem.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>() as Camera;
            }
            //Build Canvas ButtonLayout
            if (!GameObject.Find("ButtonLayout"))
            {
                GameObject buttonLayout = new GameObject("ButtonLayout");
                var alignment = buttonLayout.AddComponent<HorizontalLayoutGroup>();
                alignment.childAlignment = TextAnchor.MiddleCenter;
                alignment.padding = new RectOffset(-1, -1, 0, 0);
                alignment.spacing = -2.1f;
                buttonLayout.transform.SetParent(canvas.transform);
                if (buttonLayout.GetComponent<RectTransform>())
                    buttonLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            }
            else
            {
                Debug.LogWarning("ButtonLayout already exists in scene");
            }

            //Build Canvas TopPanel
            if (!GameObject.Find("TopPanel"))
            {
                GameObject topPanel = new GameObject("TopPanel");
                topPanel.transform.SetParent(canvas.transform);
            }
            else
            {
                Debug.LogWarning("TopPanel already exists in scene");
            }
        }
    }
}
