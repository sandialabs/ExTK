using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ExplodeContractData
{
    public float modelChildDistance, explosionSpeed;
    public bool explodecontract, explodecontractEnable;
    public GameObject explodebutton, contractbutton, explodemodel;
    public List<ModelChildMeshes> subMeshList;
}


[Serializable]
public class ModelChildMeshes
{
    public MeshRenderer meshRenderer;
    public Vector3 originalPosition;
    public Vector3 explodedPosition;

    public Quaternion originalRotation;
    public Quaternion explodedRotation;

}


public class ExplodeContract : ExplorationToolKit
{
    public List<ModelChildMeshes> childMeshRenderers;
    public bool isInExplodedView = false;
    bool isMoving = false;
    int loopCount = 0;

    /// <summary>
    /// Start() default unity method used similar to constructor
    /// </summary>
    public void Start()
    {
        ExTK.explodeContractData.explosionSpeed = 0.1f;
        ExTK.explodeContractData.explodebutton = GameObject.Find("Explode");
        enabled = false;
        ModelPositions();
    }

    /// <summary>
    /// Update default unity method for frame updates
    /// </summary>
    void Update()
    {
        if (isMoving)
        {
            loopCount = 0;
            foreach (var item in childMeshRenderers)
            {
                loopCount++;
                if (item != null)
                {
                    if (isInExplodedView)
                    {
                        ChangePosition(item, item.explodedPosition);
                    }
                    else
                    {
                        ChangePosition(item, item.originalPosition);
                    }
                }
            }
        }
    }

    public void ChangePosition(ModelChildMeshes item, Vector3 position)
    {
        item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, position, ExTK.explodeContractData.explosionSpeed);
        if (Vector3.Distance(item.meshRenderer.transform.position, position) < 0f) { isMoving = false; }
        if (item.meshRenderer.transform.position == position && loopCount == childMeshRenderers.Count) { isMoving = false; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelPosition"></param>
    /// <param name="movePosition"></param>
    /// <param name="loopCount"></param>
    public void ExplodeAndContract(Vector3 modelPosition, Vector3 movePosition, int loopCount)
    {
        modelPosition = Vector3.Lerp(modelPosition, movePosition, ExTK.explodeContractData.explosionSpeed);
        if (Vector3.Distance(modelPosition, movePosition) < 0f) { isMoving = false; }
        if (modelPosition == movePosition && loopCount == childMeshRenderers.Count) { isMoving = false; }
    }

    /// <summary>
    /// Toggle Exploded View method used to toggle the movement
    /// </summary>
    public void ToggleExplodedView()
    {
        if (isInExplodedView)
        {
            isInExplodedView = false;
            isMoving = true;
        }
        else
        {
            enabled = true;
            ModelPositions();
            isInExplodedView = true;
            isMoving = true;
        }
    }

    /// <summary>
    /// ModelPositions(), used to save the current model positions by meshrenderer
    /// </summary>
    public void ModelPositions()
    {
        childMeshRenderers = new List<ModelChildMeshes>();
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            ModelChildMeshes mesh = new ModelChildMeshes();
            mesh.meshRenderer = item;
            mesh.originalPosition = item.transform.position;
            mesh.originalRotation = item.transform.rotation;
            mesh.explodedRotation = item.transform.rotation;
            mesh.explodedPosition = (item.bounds.center - transform.position) * ExTK.explodeContractData.modelChildDistance + transform.position;
            childMeshRenderers.Add(mesh);
            ExTK.explodeContractData.subMeshList.Add(mesh);
        }
    }
}