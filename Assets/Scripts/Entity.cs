using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float health = 10f;
    private float maxHealth = 10f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.02f;
    protected List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected FloatingHealthbar healthbar;

    public void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(health);
    }

    public virtual void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
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
