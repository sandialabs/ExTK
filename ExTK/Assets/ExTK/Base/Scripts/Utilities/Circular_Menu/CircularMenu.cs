using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CircleData
{
    public string name;
    public List<GameObject> buttons;
}
public class CircularMenu : ExplorationToolKit
{
    public float gapWidth = 6f;

    public static void CheckSetCircle(ExplorationToolKit ExTK)
    {
        foreach (var circleLayer in ExTK.circleData)
        {
            if (circleLayer.buttons.Count > 0)
            {
                var stepLength = 360f / circleLayer.buttons.Count;
                int index = 0;
                foreach (var button in circleLayer.buttons)
                {
                    button.transform.localPosition = button.transform.localPosition + Quaternion.AngleAxis(index * stepLength, Vector3.forward) * Vector3.up * 3f;
                    index++;
                }
            }
        }
    }
}