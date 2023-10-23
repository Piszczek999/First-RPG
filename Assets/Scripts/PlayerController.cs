using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : Entity
{
    public SwordAttack swordAttack;
    Vector2 movementInput;
    bool canMove = true;

    private void FixedUpdate()
    {
        if (canMove)
        {
            bool success = Move(movementInput);
            animator.SetBool("isMoving", success);
        }
    }

    // ANIMATOR TRIGGERS
    public void SwordAttack()
    {
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }
    private void LockMovement()
    {
        canMove = false;
    }

    private void UnlockMovement()
    {
        canMove = true;
    }
    //------------------------------


    // PLAYER INPUT
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    // ------------------------------
}
