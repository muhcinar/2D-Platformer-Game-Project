using System.Collections;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public LogicScript logic;
    public TrailRenderer trailRenderer;
    public Animator animator;

    public int playerMaxHealth;
    public int playerHealth;
    public bool isAlive;
    public bool isInvulnerable;

    void Start()
    {
        logic = logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();

        playerMaxHealth = 3;
        playerHealth = playerMaxHealth;
        logic.HealthToText(playerHealth);
        isAlive = true;
        isInvulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 12) && (!isInvulnerable) )
        {
            playerHealth = logic.DamageCreature(gameObject, playerHealth, 1);
            CheckAliveAfterDamage();
            StartCoroutine(SetInvulnerable(1f));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 9) // Projectile
        {
            if ( (collision.gameObject.GetComponent<ProjectileScript>().canDamagePlayer) && (!isInvulnerable) ) // Check if it is an enemy projectile
            {
                playerHealth = logic.DamageCreature(gameObject, playerHealth, 1);
                CheckAliveAfterDamage();
                StartCoroutine(SetInvulnerable(1f));
            }

        }

    }

    void CheckAliveAfterDamage()
    {
        if (playerHealth == 0)
        {
            trailRenderer.emitting = false;
            isAlive = false;
            logic.GameOver();
        }
    }

    private IEnumerator SetInvulnerable(float invulnerabilityTime)
    {
        animator.SetBool("isDamaged", true);
        isInvulnerable = true;
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isDamaged", false);
        yield return new WaitForSeconds(invulnerabilityTime - 0.2f);
        isInvulnerable = false;

    }
}
