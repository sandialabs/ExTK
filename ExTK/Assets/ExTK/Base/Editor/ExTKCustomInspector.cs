using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[CustomEditor(typeof(ExplorationToolKit))]
public class ExTKCustomInspector : Editor
{
    protected ExplorationToolKit ExTK;
    public GameObject ButtonPrefab;

    private Texture image;

    private ExTKSerialization exTKSerialization;
    private ExTKReorderableLists exTKReorderableLists;

    private void OnEnable()
    {
        image = (Texture)Resources.Load("Sandia_National_Laboratories_logoWhite_Final");
       
        ExTK = (ExplorationToolKit)target;
        ButtonPrefab = ExTK.buttonPrefab;

        exTKSerialization = (ExTKSerialization)ScriptableObject.CreateInstance(typeof(ExTKSerialization));
        EditorUtility.SetDirty(exTKSerialization);
        exTKSerialization.SetEnabledValues(ExTK);
        exTKSerialization.AssignSerializedFields(serializedObject);

        exTKReorderableLists = (ExTKReorderableLists)ScriptableObject.CreateInstance(typeof(ExTKReorderableLists));
        EditorUtility.SetDirty(exTKReorderableLists);
        exTKReorderableLists.BuildReorderableList(ExTK, serializedObject);
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        int imageWidth = 80;
        GUILayout.Space(Screen.width / 15 - imageWidth / 2);
        GUILayout.Label(image);
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        if (ExTK.model != null)
        {
            EditorGUILayout.Space();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
            GUILayout.ExpandWidth(false);
            GUILayout.Width(5f);
            ExTK.currentTab = GUILayout.Toolbar(ExTK.currentTab, new string[] { "ExTK Functionality", "Model Operations" });

            switch (ExTK.currentTab)
            {
                case 0:
                    EditorGUI.indentLevel++;
                    GUI.enabled = ExTK.buttonPrefab;
                    //ButtonLayout
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();

                    exTKSerialization.soMenuData.FindPropertyRelative("buttonlayout").boolValue = EditorGUILayout.Foldout(exTKSerialization.soMenuData.FindPropertyRelative("buttonlayout").boolValue, new GUIContent("Button Layout"), true);
                    GUI.enabled = exTKSerialization.soMenuData.FindPropertyRelative("buttonLayoutEnable").boolValue;
                    if (exTKSerialization.soMenuData.FindPropertyRelative("buttonlayout").boolValue)
                    {
                        exTKSerialization.soMenuData.FindPropertyRelative("horizontal").boolValue = GUILayout.Toggle(!exTKSerialization.soMenuData.FindPropertyRelative("vertical").boolValue, "Horizontal");
                        exTKSerialization.soMenuData.FindPropertyRelative("vertical").boolValue = GUILayout.Toggle(!exTKSerialization.soMenuData.FindPropertyRelative("horizontal").boolValue, "Vertical");
                        if (GameObject.Find("ButtonLayout"))
                            Utils.EnableMenuLayout(ExTK, exTKSerialization.soMenuData.FindPropertyRelative("horizontal").boolValue, exTKSerialization.soMenuData.FindPropertyRelative("vertical").boolValue, exTKSerialization.soMenuData.FindPropertyRelative("circular").boolValue);
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soMenuData.FindPropertyRelative("buttonLayoutEnable"), GUIContent.none, GUILayout.MaxWidth(35));
                    EditorGUILayout.EndHorizontal();

                    //Additionals
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    exTKSerialization.soAdditionalsData.FindPropertyRelative("additionals").boolValue = EditorGUILayout.Foldout(exTKSerialization.soAdditionalsData.FindPropertyRelative("additionals").boolValue, new GUIContent("Main Controls"), true);
                    if (exTKSerialization.soAdditionalsData.FindPropertyRelative("additionals").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue;
                        EditorGUILayout.PropertyField(exTKSerialization.soAdditionalsData.FindPropertyRelative("palmEnable"), new GUIContent("Palm (Hololens 2 Only)"));
                        EditorGUILayout.PropertyField(exTKSerialization.soAdditionalsData.FindPropertyRelative("minimizeEnable"), new GUIContent("Minimize (Hololens 2 Only)"));
                        EditorGUILayout.PropertyField(exTKSerialization.soAdditionalsData.FindPropertyRelative("followEnable"), new GUIContent("Follow (Hololens Only)"));
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable"), GUIContent.none, GUILayout.MaxWidth(35f));

                    if (!ExTK.menuData.topPanelCanvas)
                    {
                        if (exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue)
                        {
                            Utils.GenerateCanvasObj(ExTK, exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue, "TopPanel", ExTK.additionalsData.topPanelCanvas);
                            Utils.GenerateCanvasObj(ExTK, exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue, "Exit", ExTK.additionalsData.exitCanvas);
                            for (int i = 0; i < ExTK.additionalsData.additionalsStrings.Count; i++)
                            {
                                if (ExTK.additionalsData.additionalsStrings[i] == "Yes" || ExTK.additionalsData.additionalsStrings[i] == "No")
                                {
                                    if (!ExTK.additionalsData.exitCanvas)
                                        Utils.GenerateGameObjects(ExTK, null, ExTK.additionalsData.additionalsStrings[i], "ExitCanvas", true, exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue);
                                }
                                else
                                {
                                    if (ExTK.additionalsData.additionalsStrings[i] == "Palm")
                                        Utils.GenerateGameObjects(ExTK, null, ExTK.additionalsData.additionalsStrings[i], "TopPanelCanvas", true, exTKSerialization.soAdditionalsData.FindPropertyRelative("palmEnable").boolValue);
                                    else if (ExTK.additionalsData.additionalsStrings[i] == "Minimize")
                                        Utils.GenerateGameObjects(ExTK, null, ExTK.additionalsData.additionalsStrings[i], "TopPanelCanvas", true, exTKSerialization.soAdditionalsData.FindPropertyRelative("minimizeEnable").boolValue);
                                    else if (ExTK.additionalsData.additionalsStrings[i] == "Follow")
                                        Utils.GenerateGameObjects(ExTK, null, ExTK.additionalsData.additionalsStrings[i], "TopPanelCanvas", true, exTKSerialization.soAdditionalsData.FindPropertyRelative("followEnable").boolValue);
                                    else
                                        Utils.GenerateGameObjects(ExTK, null, ExTK.additionalsData.additionalsStrings[i], "TopPanelCanvas", true, exTKSerialization.soAdditionalsData.FindPropertyRelative("additionalsEnable").boolValue);
                                }
                            }
                        }
                        else
                        {
                            DestroyImmediate(GameObject.Find("TopPanelObj"));
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //Background Functionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    exTKSerialization.soBackgroundData.FindPropertyRelative("background").boolValue = EditorGUILayout.Foldout(exTKSerialization.soBackgroundData.FindPropertyRelative("background").boolValue, new GUIContent("Background"), true);
                    if (exTKSerialization.soBackgroundData.FindPropertyRelative("background").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soBackgroundData.FindPropertyRelative("backgroundEnable").boolValue;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginVertical();
                        exTKSerialization.soBackgroundData.FindPropertyRelative("cleanRoom").boolValue = GUILayout.Toggle(!exTKSerialization.soBackgroundData.FindPropertyRelative("custom").boolValue, "Clean Room");
                        exTKSerialization.soBackgroundData.FindPropertyRelative("custom").boolValue = GUILayout.Toggle(!exTKSerialization.soBackgroundData.FindPropertyRelative("cleanRoom").boolValue, "Custom");
                        GUI.enabled = exTKSerialization.soBackgroundData.FindPropertyRelative("custom").boolValue;
                        if (exTKSerialization.soBackgroundData.FindPropertyRelative("custom").boolValue == true && exTKSerialization.soBackgroundData.FindPropertyRelative("backgroundEnable").boolValue == true)
                            GUI.enabled = true;
                        else
                            GUI.enabled = false;
                        EditorGUILayout.ObjectField(exTKSerialization.soBackgroundData.FindPropertyRelative("customObj"), new GUIContent("Custom Background: "));
                        GUI.enabled = true;
                        EditorGUILayout.EndVertical();
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soBackgroundData.FindPropertyRelative("backgroundEnable"), GUIContent.none, GUILayout.MaxWidth(35f));

                    Utils.GenerateGameObjects(ExTK, null, "Background", "ButtonLayout", true, exTKSerialization.soBackgroundData.FindPropertyRelative("backgroundEnable").boolValue);

                    EditorGUILayout.EndHorizontal();

                    //Scale FUnctionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    exTKSerialization.soScaleData.FindPropertyRelative("scale").boolValue = EditorGUILayout.Foldout(exTKSerialization.soScaleData.FindPropertyRelative("scale").boolValue, new GUIContent("Scale"), true);
                    if (exTKSerialization.soScaleData.FindPropertyRelative("scale").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soScaleData.FindPropertyRelative("scaleEnable").boolValue;
                        EditorGUI.indentLevel++;

                        EditorGUILayout.Slider(exTKSerialization.soScaleData.FindPropertyRelative("scalespeed"), 0.01f, 1f, new GUIContent("Scale Speed: "));
                        EditorGUILayout.PropertyField(exTKSerialization.soScaleData.FindPropertyRelative("scalestring"), new GUIContent("Audio Feedback: "));
                        EditorGUILayout.PropertyField(exTKSerialization.soScaleData.FindPropertyRelative("scaleupmax"), new GUIContent("Scale Up Max Size: "));
                        EditorGUILayout.PropertyField(exTKSerialization.soScaleData.FindPropertyRelative("scaledownmax"), new GUIContent("Scale Down Max Size: "));
                        for (int i = 0; i < ExTK.scaleData.scaleList.Count; i++)
                        {
                            EditorGUILayout.PropertyField(exTKSerialization.soScaleData.FindPropertyRelative("scaleListBool").GetArrayElementAtIndex(i), new GUIContent("Scale " + ExTK.scaleData.scaleList[i]));
                        }
                        EditorGUI.indentLevel--;
                    }

                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soScaleData.FindPropertyRelative("scaleEnable"), GUIContent.none, GUILayout.MaxWidth(35f));

                    Utils.GenerateCanvasObj(ExTK, exTKSerialization.soScaleData.FindPropertyRelative("scaleEnable").boolValue, "Scale", ExTK.scaleData.MenuS);
                    if (exTKSerialization.soScaleData.FindPropertyRelative("scaleEnable").boolValue && !ExTK.scaleData.MenuS)
                    {
                        foreach (var scale in ExTK.scaleData.scaleList)
                        {
                            Utils.GenerateGameObjects(ExTK, null, "Scale " + scale, "ScaleCanvas", true, ExTK.scaleData.scaleListBool[ExTK.scaleData.scaleList.IndexOf(scale)]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //Rotate Functionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    exTKSerialization.soRotateData.FindPropertyRelative("rotate").boolValue = EditorGUILayout.Foldout(exTKSerialization.soRotateData.FindPropertyRelative("rotate").boolValue, new GUIContent("Rotate"), true);
                    if (exTKSerialization.soRotateData.FindPropertyRelative("rotate").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soRotateData.FindPropertyRelative("rotateEnable").boolValue;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.Slider(exTKSerialization.soRotateData.FindPropertyRelative("rotatespeed"), 0.01f, 1f, new GUIContent("Rotate Speed: "));
                        EditorGUILayout.PropertyField(exTKSerialization.soRotateData.FindPropertyRelative("rotatestring"), new GUIContent("Audio Feedback: "));

                        for (int i = 0; i < ExTK.rotateData.rotateList.Count; i++)
                        {
                            EditorGUILayout.PropertyField(exTKSerialization.soRotateData.FindPropertyRelative("rotateListBool").GetArrayElementAtIndex(i), new GUIContent(ExTK.rotateData.rotateList[i] + " Axis"));
                        }
                        EditorGUI.indentLevel--;
                    }

                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soRotateData.FindPropertyRelative("rotateEnable"), GUIContent.none, GUILayout.MaxWidth(35f));

                    Utils.GenerateCanvasObj(ExTK, exTKSerialization.soRotateData.FindPropertyRelative("rotateEnable").boolValue, "Rotate", ExTK.rotateData.MenuR);
                    if (exTKSerialization.soRotateData.FindPropertyRelative("rotateEnable").boolValue && !ExTK.rotateData.MenuR)
                    {
                        foreach (var rotate in ExTK.rotateData.rotateList)
                        {
                            Utils.GenerateGameObjects(ExTK, null, rotate + " Axis", "RotateCanvas", true, ExTK.rotateData.rotateListBool[ExTK.rotateData.rotateList.IndexOf(rotate)]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //Move Functionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    exTKSerialization.soMoveData.FindPropertyRelative("move").boolValue = EditorGUILayout.Foldout(exTKSerialization.soMoveData.FindPropertyRelative("move").boolValue, new GUIContent("Move"), true);
                    if (exTKSerialization.soMoveData.FindPropertyRelative("move").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soMoveData.FindPropertyRelative("moveEnable").boolValue;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.Slider(exTKSerialization.soMoveData.FindPropertyRelative("movespeed"), 0.01f, 2f, new GUIContent("Move Speed: "));
                        EditorGUILayout.PropertyField(exTKSerialization.soMoveData.FindPropertyRelative("movestring"), new GUIContent("Audio Feedback: "));
                        for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
                        {
                            //EditorGUILayout.PropertyField(exTKSerialization.soMoveData.FindPropertyRelative("moveListBool").GetArrayElementAtIndex(i), new GUIContent(ExTK.moveData.moveList[i]));
                        }
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soMoveData.FindPropertyRelative("moveEnable"), GUIContent.none, GUILayout.MaxWidth(35f));

                    Utils.GenerateCanvasObj(ExTK, exTKSerialization.soMoveData.FindPropertyRelative("moveEnable").boolValue, "Move", ExTK.moveData.moveCanvas);
                    if (exTKSerialization.soMoveData.FindPropertyRelative("moveEnable").boolValue && !ExTK.moveData.moveCanvas)
                    {
                        foreach (var move in ExTK.moveData.moveList)
                        {
                            Utils.GenerateGameObjects(ExTK, null, "Move " + move, "MoveCanvas", true, ExTK.moveData.moveListBool[ExTK.moveData.moveList.IndexOf(move)]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    break;

                case 1:

                    //Model Manipulation Functionality
                    EditorGUI.indentLevel++;

                    //Details
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    exTKSerialization.soMenuData.FindPropertyRelative("manipulate").boolValue = EditorGUILayout.Foldout(exTKSerialization.soMenuData.FindPropertyRelative("manipulate").boolValue, new GUIContent("Manipulation"), true);
                    if (exTKSerialization.soMenuData.FindPropertyRelative("manipulate").boolValue)
                    {
                        GUI.enabled = exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue;
                        Utils.GenerateCanvasObj(ExTK, exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue, "Manipulate", ExTK.menuData.manipulateCanvas);
                        //Colliders
                        EditorGUILayout.PropertyField(exTKSerialization.soMenuData.FindPropertyRelative("collidersEnable"), new GUIContent("Enable Colliders"));

                        //Hide Parts
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();
                        exTKSerialization.soHidePartsData.FindPropertyRelative("hideparts").boolValue = EditorGUILayout.Foldout(exTKSerialization.soHidePartsData.FindPropertyRelative("hideparts").boolValue, new GUIContent("Hide Parts"), true);
                        if (exTKSerialization.soHidePartsData.FindPropertyRelative("hideparts").boolValue)
                        {
                            if (exTKSerialization.soHidePartsData.FindPropertyRelative("hidepartsEnable").boolValue && exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue)
                                GUI.enabled = true;
                            else
                                GUI.enabled = false;
                            EditorGUI.indentLevel++;
                            ExTK.hidePartsData.hidePartColor = EditorGUILayout.ColorField("Model Hide Part Color", new Color32(0, 51, 89, 1));
                            EditorGUI.indentLevel--;
                        }
                        EditorGUILayout.EndVertical();
                        GUI.enabled = exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue;
                        EditorGUILayout.PropertyField(exTKSerialization.soHidePartsData.FindPropertyRelative("hidepartsEnable"), GUIContent.none, GUILayout.MaxWidth(45f));

                        EditorGUILayout.EndHorizontal();
                        EditorGUI.indentLevel--;

                        //ExplodeContract
                        EditorGUI.indentLevel++;

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();
                        exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontract").boolValue = EditorGUILayout.Foldout(exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontract").boolValue, new GUIContent("Exploded/Contract"), true);
                        if (exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontract").boolValue)
                        {
                            if (exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontractEnable").boolValue && exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue)
                                GUI.enabled = true;
                            else
                                GUI.enabled = false;
                            EditorGUI.indentLevel++;
                            EditorGUILayout.Slider(exTKSerialization.soExplodeContractData.FindPropertyRelative("explosionSpeed"), 0.01f, 1f, new GUIContent("Explosion Speed: "));
                            EditorGUILayout.Slider(exTKSerialization.soExplodeContractData.FindPropertyRelative("modelChildDistance"), 1f, 15f, new GUIContent("Distance: "));
                            EditorGUI.indentLevel--;
                        }
                        EditorGUILayout.EndVertical();
                        GUI.enabled = exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue;
                        EditorGUILayout.PropertyField(exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontractEnable"), GUIContent.none, GUILayout.MaxWidth(45f));

                        EditorGUILayout.EndHorizontal();
                        EditorGUI.indentLevel--;

                        //Tool Tips
                        EditorGUILayout.PropertyField(exTKSerialization.soMenuData.FindPropertyRelative("toolTipEnable"), new GUIContent("Tool Tips (Requires MRTK)"));


                    }
                    
                    if (exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue)
                    {
                        if (!(ExTK.menuData.manipulateCanvas))
                        {
                            Utils.GenerateGameObjects(ExTK, null, "Tool Tips", "ManipulateCanvas", true, exTKSerialization.soMenuData.FindPropertyRelative("toolTipEnable").boolValue);
                            Utils.GenerateGameObjects(ExTK, null, "Hide Parts", "ManipulateCanvas", true, exTKSerialization.soHidePartsData.FindPropertyRelative("hidepartsEnable").boolValue);
                            Utils.GenerateGameObjects(ExTK, null, "Explode", "ManipulateCanvas", true, exTKSerialization.soExplodeContractData.FindPropertyRelative("explodecontractEnable").boolValue);
                        }
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable"), GUIContent.none, GUILayout.MaxWidth(35f));
                    if (!exTKSerialization.soMenuData.FindPropertyRelative("manipulateEnable").boolValue) { DestroyImmediate(GameObject.Find("ManipulateObj")); }
                    EditorGUILayout.EndHorizontal();

                    //Animation Functionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    if (ExTK.animatorController && ExTK.buttonPrefab) { GUI.enabled = true; }
                    else { GUI.enabled = false; }
                    exTKSerialization.soAnimations.boolValue = EditorGUILayout.Foldout(exTKSerialization.soAnimations.boolValue, new GUIContent("Animations"), true);
                    if (exTKSerialization.soAnimations.boolValue)
                    {
                        if (exTKSerialization.soAnimationsEnabled.boolValue && ExTK.animatorController) { GUI.enabled = true; }
                        else { GUI.enabled = false; }
                        EditorGUI.indentLevel++;
                        EditorGUILayout.Slider(exTKSerialization.soAnimationSpeed, 0.01f, 1f, new GUIContent("Animation Speed: "));
                        exTKReorderableLists.animSortment.DoLayoutList();
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();
                    if (ExTK.animatorController && ExTK.buttonPrefab)
                    {
                        GUI.enabled = true;
                        EditorGUILayout.PropertyField(exTKSerialization.soAnimationsEnabled, GUIContent.none, GUILayout.MaxWidth(35f));

                        Utils.GenerateCanvasObj(ExTK, exTKSerialization.soAnimationsEnabled.boolValue, "Animations", ExTK.animCanvas);
                        if (exTKSerialization.soAnimationsEnabled.boolValue && !ExTK.animCanvas)
                            for (int i = 0; i < exTKReorderableLists.animSortment.count; i++)
                            {
                                exTKReorderableLists.animSortment.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("animButton").objectReferenceValue = Utils.GenerateGameObjects(ExTK, null, "AnimButton" + i, "AnimationsCanvas", true, exTKSerialization.soAnimationsEnabled.boolValue);
                            }
                    }
                    else
                    {
                        GUI.enabled = false;
                        DestroyImmediate(GameObject.Find("AnimationsObj"));
                    }
                    EditorGUILayout.EndHorizontal();


                    //Subsystems Functionality
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    exTKSerialization.soSubsystems.boolValue = EditorGUILayout.Foldout(exTKSerialization.soSubsystems.boolValue, new GUIContent("Subsystems"), true);
                    if (exTKSerialization.soSubsystems.boolValue)
                    {
                        GUI.enabled = exTKSerialization.soSubsystemsEnable.boolValue;
                        EditorGUI.indentLevel++;
                        exTKReorderableLists.subsysSortment.DoLayoutList();
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();
                    GUI.enabled = ExTK.buttonPrefab;
                    EditorGUILayout.PropertyField(exTKSerialization.soSubsystemsEnable, GUIContent.none, GUILayout.MaxWidth(35f));


                    Utils.GenerateCanvasObj(ExTK, exTKSerialization.soSubsystemsEnable.boolValue, "Subsystems", ExTK.subSysCanvas);

                    if (exTKSerialization.soSubsystemsEnable.boolValue && !(ExTK.subSysCanvas))
                        for (int i = 0; i < exTKReorderableLists.subsysSortment.count; i++)
                        {
                            exTKReorderableLists.subsysSortment.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("subsysButton").objectReferenceValue = Utils.GenerateGameObjects(ExTK, null, "SubButton" + i, "SubsystemsCanvas", true, exTKSerialization.soSubsystemsEnable.boolValue);
                        }

                    EditorGUILayout.EndHorizontal();
                    break;
                default:
                    break;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}