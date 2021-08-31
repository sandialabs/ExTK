using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//AnimSort used to set of the animations reoderable list
[Serializable]
public struct AnimSort
{
    public AnimationClip animClip;
    public string animTitle;
    public string animAudio;
    public GameObject animButton;

}

[Serializable]
public struct AnimationsData
{
    [SerializeReference]
    public bool animations, animationsEnable;
    [SerializeReference]
    public float animationspeed;
    [SerializeReference]
    public AnimationClip animationclip;
    [SerializeReference]
    public GameObject animationbutton, animmenu, animObject, animCanvas;
    public List<AnimSort> animSortment;
}

[Serializable]
public class Animations : ExplorationToolKit
{
    Animator anim;

    public bool AnimButtonState;

    /// <summary>
    /// Start default unity method
    /// </summary>
    void Start()
    {

        if (ExTK.animatorController != null)
        {
            if (!ExTK.model.GetComponent<Animator>())
            {
                ExTK.model.AddComponent<Animator>().runtimeAnimatorController = ExTK.animatorController;
            }
            else
            {
                ExTK.model.GetComponent<Animator>().runtimeAnimatorController = ExTK.animatorController;
            }
        }

        int index = 0;
        if (ExTK.animSortment.Count > 0)
            ExTK.animCanvas.SetActive(true);
        foreach (var anim in ExTK.animSortment)
        {
            if (anim.animTitle != "" && anim.animTitle != null)
            {
                if (!anim.animButton)
                    GameObject.Find("AnimButton" + index).GetComponentInChildren<TextMeshPro>().text = anim.animTitle;
                else
                    anim.animButton.GetComponentInChildren<TextMeshPro>().text = anim.animTitle;
            }
            index++;
        }
        ExTK.animCanvas.SetActive(false);

        if (ExTK.model.GetComponent<Animator>())
        {
            anim = ExTK.model.GetComponent<Animator>();
            ExTK.model.GetComponent<Animator>().enabled = false;
        }

        ExTK.animCanvas.SetActive(false);
        AnimButtonState = false;
    }


    /// <summary>
    /// Animbutton click will be the function for the animations button
    /// </summary>
    public void AnimButtonClicked()
    {
        anim.Rebind();
        anim.Update(0f);
        AnimButtonState = !AnimButtonState;
        if (AnimButtonState)
            anim.enabled = true;
        else
            anim.enabled = false;
    }

    /// <summary>
    /// Update default Unity method
    /// </summary>
    void Update()
    {
        if (AnimButtonState) { ExTK.animCanvas.SetActive(true); }
        else { ExTK.animCanvas.SetActive(false); }
    }

    /// <summary>
    /// BeenClicked used to set up animation button click
    /// </summary>
    /// <param name="clip"></param>
    public void BeenClicked(AnimationClip clip)
    {
        if (clip != null && ExTK.model.GetComponent<Animator>())
        {
            anim = ExTK.model.GetComponent<Animator>();

            if (anim.HasState(0, Animator.StringToHash(clip.name)))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
                {
                    if (anim.speed == 0f)
                        anim.speed = ExTK.animationspeed;
                    else
                        PauseAnim();
                }
                else
                    anim.Play(clip.name);
            }
            else
                Debug.LogWarning("Animation state does not match animation clip name");
        }
    }

    //Used to pause the animation in current location
    public void PauseAnim()
    {
        anim.speed = 0f;
    }

    /// <summary>
    /// PlayAnim(), used to play the animations after pausing
    /// </summary>
    public void PlayAnim()
    {
        anim.speed = ExTK.animationspeed;
    }

    /// <summary>
    /// Reset Animation is used to reset the animations that are running
    /// </summary>
    public void ResetAnimation()
    {
        anim = ExTK.model.GetComponent<Animator>();
        AnimButtonState = true;
        anim.enabled = true;
        if (anim && ExTK.animatorController != null)
        {
            anim.Play("Idle");
            anim.Rebind();
            anim.Update(0f);
        }
        else { Debug.LogWarning("Error: Animator or Animator Controller Not Found"); }
        AnimButtonState = false;
        anim.enabled = false;
        ExTK.animCanvas.SetActive(false);
    }
}