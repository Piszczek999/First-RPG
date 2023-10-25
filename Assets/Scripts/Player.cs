using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private int level;
    [SerializeField] private float exp;

    public SwordAttack swordAttack;
    private Vector2 movementInput;
    private bool canMove = true;

    public int Level { get { return level; } }
    public float Exp { get { return exp; } }

    protected override void Awake()
    {
        base.Awake();
        LoadPlayer();
    }

    protected override void Start()
    {
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

    #region Animator Triggers
    private void SwordAttack()
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

    private void EndSwordAttack()
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
    #endregion

    #region Player Input
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    #endregion


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
