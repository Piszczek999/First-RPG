using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : Entity
{
    public int level = 1;
    public float exp = 0;
    public SwordAttack swordAttack;
    Vector2 movementInput;
    bool canMove = true;

    public override void Start()
    {
        LoadPlayer();
        base.Start();
    }
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

    void OnApplicationQuit()
    {
        SavePlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Player saved");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null) return;

        level = data.level;
        exp = data.exp;
        health = data.health;
        maxHealth = data.maxHealth;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }
}
