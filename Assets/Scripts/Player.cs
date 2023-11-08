using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private int level;
    [SerializeField] private float exp;
    public PlayerAttack attack;
    private Vector2 movementInput;
    public bool canMove = true;

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
        if (canMove && movementInput != Vector2.zero)
        {
            if (movementInput.x < 0) spriteRenderer.flipX = true;
            else if (movementInput.x > 0) spriteRenderer.flipX = false;
            rb.MovePosition(rb.position + movementInput.normalized * MoveSpeed * Time.fixedDeltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    #region Player Input
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        attack.Attack();
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
