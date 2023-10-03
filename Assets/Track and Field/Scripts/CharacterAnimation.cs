using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public CapsuleCollider2D myCapsuleCollider2D;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpColliderOn()
    {
        myCapsuleCollider2D.enabled = true;
    }
    public void JumpColliderOff()
    {
        myCapsuleCollider2D.enabled = false;
    }
}
