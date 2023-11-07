using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    Player player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator.GetComponent<Animator>();
        player.GetComponentInParent<Player>();
        spriteRenderer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwordAttack()
    {
        animator.SetTrigger("swordAttack");
        if (spriteRenderer.flipX == true)
        {
            player.attack.AttackLeft();
        }
        else
        {
            player.attack.AttackRight();
        }
    }

    private void EndSwordAttack()
    {
        player.attack.StopAttack();
    }
    private void LockMovement()
    {
        player.canMove = false;
    }

    private void UnlockMovement()
    {
        player.canMove = true;
    }
}
