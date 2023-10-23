using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
  [SerializeField] float damage = 2;
  Vector2 direction;
  GameObject[] players;
  bool isRaged = false;
  bool canMove = false;

  public override void Start()
  {
    base.Start();
    players = GameObject.FindGameObjectsWithTag("Player");
  }

  private void FixedUpdate()
  {
    if (isRaged)
    {
      if (canMove)
      {
        Move(direction);
      }
    }
    else
    {
      foreach (GameObject player in players)
      {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance < 1)
        {
          isRaged = true;
          StartCoroutine(RageCoroutine(player));
        }
      }
    }
  }


  IEnumerator RageCoroutine(GameObject player)
  {
    while (isRaged)
    {
      animator.SetTrigger("Jump");

      Vector3 target = player.transform.position + new Vector3(0, 0.05f, 0);
      direction = target - transform.position;
      direction.Normalize();
      if (direction.x < 0) spriteRenderer.flipX = true;
      else spriteRenderer.flipX = false;

      yield return new WaitForSeconds(Random.Range(2f, 3f));
    }
  }

  public override void Defeated()
  {
    base.Defeated();
    canMove = false;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("SLIME HIT");
    if (other.tag == "Player")
    {
      // Deal damage to the enemy
      Entity player = other.GetComponent<Entity>();

      if (player != null)
      {
        player.TakeDamage(damage);
      }
    }
  }

  // ANIMATOR TRIGGERS
  public void StartJump()
  {
    canMove = true;
  }

  public void EndJump()
  {
    canMove = false;
  }

}