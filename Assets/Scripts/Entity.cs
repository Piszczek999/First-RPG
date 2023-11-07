using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] private bool isDefeated;

    protected Rigidbody2D rb { get; private set; }
    protected SpriteRenderer spriteRenderer { get; private set; }
    protected Animator animator { get; private set; }
    protected FloatingHealthbar healthbar { get; private set; }

    public float MoveSpeed { get { return moveSpeed; } }
    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public bool IsDefeated { get { return isDefeated; } }

    protected virtual void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(health);
    }

    protected virtual void Defeated()
    {
        isDefeated = true;
        animator.SetTrigger("Defeated");
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        StartCoroutine(Hit());

        if (health <= 0)
        {
            Defeated();
        }
    }

    IEnumerator Hit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
