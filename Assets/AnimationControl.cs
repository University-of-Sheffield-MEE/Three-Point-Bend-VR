using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToAndStop(System.Single pos)
    {
        animator.speed = 0f;
        animator.Play("Animation", -1, pos);
    }

    public void GoToAndStop(string animation, System.Single pos)
    {
        animator.speed = 0f;
        animator.Play(animation, -1, pos);
    }
}
