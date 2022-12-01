using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("hasJump",playerController.hasJump);
        animator.SetBool("isMoving",playerController.xInput != 0);
        animator.SetBool("isShooting", playerController.isShooting);
        animator.SetBool("isCrouching", playerController.isCrouching);
        animator.SetBool("isHurt", playerController.isHurt);
    }
}
