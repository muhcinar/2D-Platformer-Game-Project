using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LogicScript logic;
    [SerializeField] AudioManager audioManager;

    public GameObject projectile;
    public GameObject playerDetectorRay;

    private float detectRange;
    private int enemyHealth;

    private float attackCooldown;
    private float cooldownTimer;
    private float direction;

    void Start()
    {
        enemyHealth = 3;
        detectRange = 2f;

        attackCooldown = 3f;
        cooldownTimer = 0f;

        SetDirection();

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        UpdateCooldowns();
        if (isPlayerPresent())
        {
            if (cooldownTimer > attackCooldown)
            {
                FireProjectile();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
            enemyHealth = logic.DamageCreature(gameObject, enemyHealth, 1);
            audioManager.PlaySFX(audioManager.enemyDamage);
        }

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }

    }

    bool isPlayerPresent()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(playerDetectorRay.transform.position, Vector2.right * direction, detectRange);

        if (hitPlayer.collider != null && hitPlayer.collider.gameObject.layer == 3)
        {
            Debug.Log("Player Found");
            Debug.DrawRay(playerDetectorRay.transform.position, Vector2.right * hitPlayer.distance * new Vector2(direction, 0f), Color.red);
            return true;
        }

        return false;
    }

    void SetDirection()
    {
        if (Mathf.Sign(gameObject.transform.localScale.x) > 0)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
    }

    void FireProjectile()
    {
        float attackOffset = direction * 0.2f;

        audioManager.PlaySFX(audioManager.fireball);
        GameObject tempProjectile = Instantiate(projectile, new Vector3(gameObject.transform.position.x + attackOffset, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        tempProjectile.GetComponent<ProjectileScript>().SetProjectile(direction, 1.5f, true);
        cooldownTimer = 0;
    }

    void UpdateCooldowns()
    {
        if (cooldownTimer < attackCooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
    }
}
