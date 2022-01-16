using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitchAffectedGameObject : MonoBehaviour
{
    public Animator animator;

    public CurrentAnimationState currentAnimationState;
    public enum CurrentAnimationState
    {
        One,
        Two
    }

    public void ChangeState()
    {
        animator.ResetTrigger("One");
        animator.ResetTrigger("Two");

        switch (currentAnimationState)
        {
            case CurrentAnimationState.One:
                animator.SetTrigger("One");
                currentAnimationState = CurrentAnimationState.Two;
                break;
            case CurrentAnimationState.Two:
                animator.SetTrigger("Two");
                currentAnimationState = CurrentAnimationState.One;
                break;
        }
    }
}
