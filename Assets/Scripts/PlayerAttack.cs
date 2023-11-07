using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    public Collider2D swordCollider;
    public float damage = 3;
    Vector2 rightAttackOffset;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        rightAttackOffset = transform.localPosition;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
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