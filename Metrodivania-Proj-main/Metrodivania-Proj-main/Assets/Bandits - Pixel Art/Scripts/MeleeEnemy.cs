using System;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [Header("Attack properties")]
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;

    
    private float cooldownTimer;

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown || canAttack == false) return;
        if (PlayerInSight())
        {
            animator.SetTrigger("Attacking");
            AttackPlayer();
        }
        else
        {
            Debug.Log("Player not in sight");
        }
    }

    private void AttackPlayer()
    {
        cooldownTimer = 0;
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            print("Making player take damage");
            playerHealth.TakeDamage();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer ==  playerLayer)
        {
            GetComponent<Health>().TakeDamage();
        }
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }
}