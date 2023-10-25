using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] private bool isDefeated;
    [SerializeField] private float collisionOffset = 0.02f;
    [SerializeField] private ContactFilter2D movementFilter;

    private List<RaycastHit2D> castCollisions { get; } = new List<RaycastHit2D>();
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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    protected bool Move(Vector2 direction)
    {
        if (direction.x < 0) spriteRenderer.flipX = true;
        else if (direction.x > 0) spriteRenderer.flipX = false;
        bool success = TryMove(direction);

        if (!success)
        {
            success = TryMove(new Vector2(direction.x, 0));
            if (!success) success = TryMove(new Vector2(0, direction.y));
        }
        return success;
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveVector);
                return true;
            }
            else
            {
                // foreach (RaycastHit2D hit in castCollisions) Debug.Log(hit.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
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
