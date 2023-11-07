using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
  [SerializeField] private float damage;
  [SerializeField] private bool isRaged;

  private Vector2 direction;
  private GameObject[] players;
  private bool canMove = false;
  Collider2D slimeCollider;

  public float Damage { get { return damage; } }
  public bool IsRaged { get { return isRaged; } }

  protected override void Awake()
  {
    base.Awake();
    slimeCollider = GetComponentInChildren<Collider2D>();
  }

  protected override void Start()
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
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
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

  protected override void Defeated()
  {
    base.Defeated();
    canMove = false;
    slimeCollider.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
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