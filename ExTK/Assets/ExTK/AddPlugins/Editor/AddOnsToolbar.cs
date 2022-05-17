using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AddOnsToolbar : MonoBehaviour
{
    [MenuItem("ExTK/Add Component/Add MenuFramework")]
    static void CreateMenuSystem()
    {
        //Add ExTK GameObject with script
        GameObject PalmUpController = Instantiate(Resources.Load("Prefabs/PalmUpController") as GameObject);
        GameObject IOManager = Instantiate(Resources.Load("Prefabs/IOManager") as GameObject);
    }
}