using System;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    public void SetIsMovingParam(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }
}
