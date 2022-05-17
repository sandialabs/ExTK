using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData : ExplorationToolKit
{
    public Material originalMat;
    void Start()
    {
        originalMat = GetComponent<Renderer>().material;
    }
}
