using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [Header("Animation")]
    public float frameRate = 24;
    YieldInstruction WaitOneFrame;
    bool animating = true;
    int spriteIndex = 0;
    public AnimationElement[] animationProfile;
    AnimationElement currentAnimElement;

    [Space]
    [Header("References")]
    SpriteRenderer spriteRenderer;

    public enum AnimationState
    {
        idle_centre,
        idle_left,
        idle_right,
        turning_left,
        turning_right,
        returning_left,
        returning_right,
        looping
    }

    [System.Serializable]
    public struct AnimationElement
    {
        [SerializeField]
        public AnimationState animationState;
        [SerializeField]
        public AnimationState followupState;
        [SerializeField]
        public Sprite[] sprites;
    }

    void Start()
    {
        WaitOneFrame = new WaitForSeconds(1f / frameRate);
        currentAnimElement = FindAnimationElement(AnimationState.idle_centre);
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        while (animating)
        {
            yield return WaitOneFrame;
            spriteIndex++;
            if (spriteIndex == currentAnimElement.sprites.Length)
            {
                spriteIndex = 0;
                if (currentAnimElement.followupState != AnimationState.looping)
                {
                    currentAnimElement = FindAnimationElement(currentAnimElement.followupState);
                }
            }
            spriteRenderer.sprite = currentAnimElement.sprites[spriteIndex];
        }
    }

    AnimationElement FindAnimationElement(AnimationState inAnimState)
    {
        for (int i = 0, e = animationProfile.Length; i < e; i++)
        {
            if (animationProfile[i].animationState == inAnimState)
            {
                return animationProfile[i];
            }
        }
        Debug.LogError("No matching animation element was found");
        return animationProfile[0];
    }

    //////////////////////
    /// PUBLIC METHODS ///
    //////////////////////

    public void TurnLeft()
    {
        currentAnimElement = FindAnimationElement(AnimationState.turning_left);
        spriteIndex = 0;
    }

    public void ReturnLeft()
    {
        currentAnimElement = FindAnimationElement(AnimationState.returning_left);
        spriteIndex = 0;
    }

    public void TurnRight()
    {
        currentAnimElement = FindAnimationElement(AnimationState.turning_right);
        spriteIndex = 0;
    }

    public void ReturnRight()
    {
        currentAnimElement = FindAnimationElement(AnimationState.returning_right);
        spriteIndex = 0;
    }

}
