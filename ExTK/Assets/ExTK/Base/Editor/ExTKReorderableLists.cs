using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ExTKReorderableLists : Editor
{

    public ReorderableList animSortment;
    public ReorderableList hidepartSortment;
    public ReorderableList subsysSortment;
    public float lineHeight, lineheightspace;

    /// <summary>
    /// BuildReorderableList(), used to build up the reorderable lists for the custom inspector
    /// </summary>
    /// <param name="ExTK"></param>
    /// <param name="soExTK"></param>
    public void BuildReorderableList(ExplorationToolKit ExTK, SerializedObject soExTK)
    {

        lineHeight = EditorGUIUtility.singleLineHeight;
        lineheightspace = lineHeight + 5;

        //Animation Sortment
        animSortment = new ReorderableList(soExTK, soExTK.FindProperty("animSortment"), true, true, true, true);
        animSortment.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Drag and drop animations below");
        };
        animSortment.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 0;
        };
        animSortment.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            int i = 0;
            var element = animSortment.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 3;
            EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y + (lineheightspace * i), rect.width, lineHeight),
                 element.FindPropertyRelative("animClip"), new GUIContent("Animation"));
            i++;
            EditorGUI.PropertyField(
                 new Rect(rect.x + 25, rect.y + (lineheightspace * i), rect.width - 25, lineHeight),
                 element.FindPropertyRelative("animTitle"), new GUIContent("Animation Name"));
            i++;
            EditorGUI.PropertyField(
                new Rect(rect.x + 25, rect.y + (lineheightspace * i), rect.width - 25, lineHeight),
                element.FindPropertyRelative("animAudio"), new GUIContent("Audio Feedback"));
        };
        animSortment.onAddCallback = (List) =>
        {
            SerializedProperty addElement;
            List.serializedProperty.arraySize++;
            addElement = List.serializedProperty.GetArrayElementAtIndex(List.serializedProperty.arraySize - 1);
            var animClip = addElement.FindPropertyRelative("animClip");
            var animTitle = addElement.FindPropertyRelative("animTitle");
            var animAudio = addElement.FindPropertyRelative("animAudio");
            animClip.objectReferenceValue = null;
            animTitle.stringValue = "";
            animAudio.stringValue = "";

            var animButton = addElement.FindPropertyRelative("animButton");
            GameObject ButtonAnim = Instantiate(ExTK.buttonPrefab);
            if (ButtonAnim.GetComponent<Button>())
            {
                ButtonAnim.GetComponent<Button>().name = "AnimButton" + (List.serializedProperty.arraySize - 1);
            }
            else
            {
                if (ButtonAnim.GetComponentsInChildren<TextMeshPro>()[0])
                    ButtonAnim.GetComponentsInChildren<TextMeshPro>()[0].text = "Say " + " 'AnimButton" + (List.serializedProperty.arraySize - 1) + "'";
                if (ButtonAnim.GetComponentsInChildren<TextMeshPro>()[1])
                    ButtonAnim.GetComponentsInChildren<TextMeshPro>()[1].text = "AnimButton" + (List.serializedProperty.arraySize - 1);
            }

            if (ButtonAnim.GetComponent<RectTransform>())
                ButtonAnim.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.subButtonSizeWidth, ExTK.menuLayout.subButtonSizeHeight);
            
            GameObject newCanvas = GameObject.Find("AnimationsCanvas");
            ButtonAnim.transform.SetParent(newCanvas.transform);
            if (ButtonAnim.GetComponent<RectTransform>())
                ButtonAnim.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f, 0f, 0f);

            animButton.objectReferenceValue = ButtonAnim;
            animButton.objectReferenceValue.name = "AnimButton" + (List.serializedProperty.arraySize - 1);
        };
        animSortment.onRemoveCallback = (List) =>
        {
            var element = List.serializedProperty.GetArrayElementAtIndex(List.index);
            var button = element.FindPropertyRelative("animButton");
            DestroyImmediate(GameObject.Find("AnimationsCanvas"));
            Utils.GenerateGameObjects(ExTK, null, "AnimationsCanvas", "AnimationsObj", false, true);
            ReorderableList.defaultBehaviours.DoRemoveButton(List);
        };

        animSortment.elementHeightCallback = (int index) => { return lineheightspace * 3; };

        //Hide Part Sortment
        hidepartSortment = new ReorderableList(soExTK, soExTK.FindProperty("hidePartsData").FindPropertyRelative("hidepartSortment"), true, true, true, true);
        hidepartSortment.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Drag and drop parts to hide below");
        };
        hidepartSortment.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 0;
        };
        hidepartSortment.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = hidepartSortment.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
             element.FindPropertyRelative("hidepartModel"), new GUIContent("Hidden Part"));
        };
        hidepartSortment.onAddCallback = (List) =>
        {
            SerializedProperty addElement;
            List.serializedProperty.arraySize++;
            addElement = List.serializedProperty.GetArrayElementAtIndex(List.serializedProperty.arraySize - 1);
            var hidepart = addElement.FindPropertyRelative("hidepartModel");
            hidepart.objectReferenceValue = null;
        };

        //Subsystem Sortment
        subsysSortment = new ReorderableList(soExTK, soExTK.FindProperty("subsysSortment"), true, true, true, true);
        subsysSortment.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Drag and drop subsystem model below");
        };
        subsysSortment.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 0;
        };
        subsysSortment.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = subsysSortment.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
             element.FindPropertyRelative("subsysModel"), new GUIContent("Subsystem"));
        };
        subsysSortment.onAddCallback = (List) =>
        {
            SerializedProperty addElement;
            List.serializedProperty.arraySize++;
            addElement = List.serializedProperty.GetArrayElementAtIndex(List.serializedProperty.arraySize - 1);
            var SubsysModel = addElement.FindPropertyRelative("subsysModel");
            SubsysModel.objectReferenceValue = null;

            var subsysButton = addElement.FindPropertyRelative("subsysButton");
            GameObject ButtonSubsystem = Instantiate(ExTK.buttonPrefab);
            if (ButtonSubsystem.GetComponent<Button>())
            {
                ButtonSubsystem.GetComponent<Button>().name = "SubButton" + (List.serializedProperty.arraySize - 1);
            }
            else
            {
                if (ButtonSubsystem.GetComponentsInChildren<TextMeshPro>()[0])
                    ButtonSubsystem.GetComponentsInChildren<TextMeshPro>()[0].text = "Say " + " 'SubButton" + (List.serializedProperty.arraySize - 1) + "'";
                if (ButtonSubsystem.GetComponentsInChildren<TextMeshPro>()[1])
                    ButtonSubsystem.GetComponentsInChildren<TextMeshPro>()[1].text = "SubButton" + (List.serializedProperty.arraySize - 1);
            }
            if (ButtonSubsystem.GetComponent<Button>())
                GameObject.Find("SubButton" + (List.serializedProperty.arraySize - 1)).GetComponentInChildren<TextMeshPro>().text = "SubButton" + (List.serializedProperty.arraySize - 1);

            if (ButtonSubsystem.GetComponent<RectTransform>())
                ButtonSubsystem.GetComponent<RectTransform>().sizeDelta = new Vector2(ExTK.menuLayout.subButtonSizeWidth, ExTK.menuLayout.subButtonSizeHeight);
            GameObject newCanvas;
            if (ExTK.animCanvas != null)
                newCanvas = ExTK.subSysCanvas;
            else
            {
                newCanvas = GameObject.Find("SubsystemsCanvas");
            }
            ButtonSubsystem.transform.SetParent(newCanvas.transform);
            if (ButtonSubsystem.GetComponent<RectTransform>())
                ButtonSubsystem.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f, 0f, 0f);

            subsysButton.objectReferenceValue = ButtonSubsystem;
            subsysButton.objectReferenceValue.name = "SubButton" + (List.serializedProperty.arraySize - 1);
        };
        subsysSortment.onRemoveCallback = (List) =>
        {
            var element = List.serializedProperty.GetArrayElementAtIndex(List.index);
            var button = element.FindPropertyRelative("subsysButton");
            DestroyImmediate(GameObject.Find("SubsystemsCanvas"));
            Utils.GenerateGameObjects(ExTK, null, "SubsystemsCanvas", "SubsystemsObj", false, true);
            ReorderableList.defaultBehaviours.DoRemoveButton(List);
        };

        subsysSortment.elementHeightCallback = (int index) => { return lineheightspace; };
    }
}
