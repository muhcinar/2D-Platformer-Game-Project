using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject projectile;
    public GameObject sword;

    public PlayerInfo playerInfo;
    public PlayerMovement playerMovement;
    public ProjectileScript projectileScript;
    public AudioManager audioManager;
    public Animator animator;

    private float projectileCooldown;
    private float projectileCooldownTimer;

    private bool canAttack = true;
    private bool isAttacking = false;
    private float attackCooldown;

    private float attackRange;
    private float direction;

    void Start()
    {
        projectileCooldown = 0.5f;
        projectileCooldownTimer = projectileCooldown + 1;
        attackCooldown = 0.5f;
        attackRange = 0.2f;


        isAttacking = false;

        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        projectileScript = GameObject.FindGameObjectWithTag("Projectile").GetComponent<ProjectileScript>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInfo.isAlive)
        {
            AttackMove();
            UpdateCooldowns();
        }

    }

    void FireProjectile()
    {
        float attackOffset = CalculateOffset(attackRange);
        audioManager.PlaySFX(audioManager.fireball);
        GameObject tempProjectile = Instantiate(projectile, new Vector3(gameObject.transform.position.x + attackOffset, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        tempProjectile.GetComponent<ProjectileScript>().SetProjectile(direction, 5f, false);
        projectileCooldownTimer = 0;
    }

    public float CalculateOffset(float offset)
    {
        if (playerMovement.isFacingRight)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        float result = offset * direction;

        return result;
    }

    void AttackMove()
    {
        if (Input.GetMouseButton(1) && projectileCooldownTimer > projectileCooldown && !playerMovement.GetIsDashing() && !isAttacking)
        {
            FireProjectile();
        }

        if (Input.GetMouseButton(0) && canAttack)
        {
            StartCoroutine(AttackWithSword(0.2f));
        }
    }



    private IEnumerator AttackWithSword(float attackigDuration)
    {
        canAttack = false;
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);

        GameObject tempSword;
        float attackOffset = CalculateOffset(attackRange);

        audioManager.PlaySFX(audioManager.sword);
        tempSword = Instantiate(sword, new Vector3(gameObject.transform.position.x + attackOffset, gameObject.transform.position.y, gameObject.transform.position.z), sword.transform.rotation);
        tempSword.GetComponent<Sword>().SetSword(attackRange);

        yield return new WaitForSeconds(attackigDuration);

        Destroy(tempSword);

        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void UpdateCooldowns()
    {
        if (projectileCooldownTimer < projectileCooldown)
        {
            projectileCooldownTimer += Time.deltaTime;
        }
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}
