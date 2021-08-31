using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MoveData
{
    [SerializeReference]
    public List<string> moveList;
    [SerializeReference]
    public List<bool> moveListBool;
    [SerializeReference]
    public List<bool> onOffSpeeds;
    [SerializeReference]
    public List<GameObject> moveButtons;
    [SerializeReference]
    public GameObject movebutton, moveObject, movemenu;
    [SerializeReference]
    public float movespeed;
    [SerializeReference]
    public string movestring;
    [SerializeReference]
    public bool MbuttonState, moveEnable, moveup, movedown, moveleft, moveright, moveforward, moveback, move, movestate;
    [SerializeReference]
    public bool forwardSpeedOn, backSpeedOn, leftSpeedOn, rightSpeedOn, upSpeedOn, downSpeedOn, BmenuState;
    [SerializeReference]
    public GameObject moveupbutton, movedownbutton, moveleftbutton, moverightbutton, moveforwardbutton, movebackbutton, moveCanvas;
}

[Serializable]
public class Move : ExplorationToolKit
{

    [HideInInspector] public bool MbuttonState;
    [HideInInspector] private List<Vector3> vectorMoveList;

    /// <summary>
    /// Start() Unity default method to initialize variables
    /// </summary>
    void Start()
    {
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.upSpeedOn);
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.downSpeedOn);
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.leftSpeedOn);
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.rightSpeedOn);
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.forwardSpeedOn);
        ExTK.moveData.onOffSpeeds.Add(ExTK.moveData.backSpeedOn);

        vectorMoveList = new List<Vector3>(new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back });

        ExTK.moveData.movebutton = GameObject.Find("Move");
        ExTK.moveData.moveCanvas = GameObject.Find("MoveCanvas");
        for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
        {
            if (ExTK.moveData.moveListBool[i])
            {
                ExTK.moveData.moveButtons.Add(GameObject.Find("Move " + ExTK.moveData.moveList[i]));
            }
        }
        ExTK.moveData.moveCanvas.SetActive(false);
        MbuttonState = false;
    }

    /// <summary>
    /// BeenClicked() used to oven the Move Canvas
    /// </summary>
    public void BeenClicked()
    {
        MbuttonState = !MbuttonState;
    }

    /**
    * Move Control Methods
    **/
    public void BeenClicked(string movement)
    {
        for (int i = 0; i < ExTK.moveData.moveList.Count; i++)
        {
            if (movement == ExTK.moveData.moveList[i])
            {
                ExTK.moveData.onOffSpeeds[i] = !ExTK.moveData.onOffSpeeds[i];
            }
        }
    }

    public void Movement(Vector3 vectorMove)
    {
        ExTK.model.transform.position += vectorMove * (ExTK.moveData.movespeed * Time.deltaTime);
    }

    public void StopMove()
    {
        for (int i = 0; i < ExTK.moveData.onOffSpeeds.Count; i++)
        {
            ExTK.moveData.onOffSpeeds[i] = false;
        }
    }

    public void ResetClicked()
    {
        MbuttonState = false;
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < ExTK.moveData.onOffSpeeds.Count; i++)
        {
            if (ExTK.moveData.onOffSpeeds[i])
            {
                Movement(vectorMoveList[i]);
            }
        }
        if (MbuttonState)
        {
            ExTK.moveData.moveCanvas.SetActive(true);
        }
        else if (MbuttonState == false)
        {
            ExTK.moveData.moveCanvas.SetActive(false);
            StopMove();
        }
    }
}