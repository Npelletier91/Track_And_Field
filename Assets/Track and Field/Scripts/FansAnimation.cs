using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FansAnimation : MonoBehaviour
{
    Animator[] myAnimator;
    void Start()
    {
        myAnimator = GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        
    }

    public void Cheer()
    { 
        foreach(Animator animator in myAnimator)
        {
            animator.SetBool("winCheer", true);
        }
    }
}
