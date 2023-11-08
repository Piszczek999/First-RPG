using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    public Collider2D swordCollider;
    public float damage = 3;
    Vector2 AttackOffset;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        AttackOffset = transform.localPosition;
    }

    public void Attack()
    {
        // if(player.)
        transform.localPosition = AttackOffset;
        swordCollider.enabled = true;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(AttackOffset.x * -1, AttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // Deal damage to the enemy
            Entity enemy = other.GetComponent<Entity>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                // if (enemy.IsDefeated) player.exp += 10;
            }
        }
    }
    public void Test()
    {
        Debug.Log("jd");
    }
}
