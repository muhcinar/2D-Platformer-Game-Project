using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public float direction;
    public bool canDamagePlayer;
    public float moveSpeed;

    [SerializeField] public PlayerInfo playerInfo;
    [SerializeField] public LogicScript logic;

    public ProjectileScript(float newDirection, float newMoveSpeed, bool newCanDamagePlayer)
    {
        direction = newDirection;
        moveSpeed = newMoveSpeed;
        canDamagePlayer = newCanDamagePlayer;

    }

    void Start()
    {
        logic = logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += (Vector3.right * moveSpeed * direction) * Time.deltaTime;
    }

    public void SetProjectile(float newDirection, float newMoveSpeed, bool newCanDamagePlayer)
    {
        direction = newDirection;
        moveSpeed = newMoveSpeed;
        canDamagePlayer = newCanDamagePlayer;

        gameObject.transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);

    }

    bool DestroyOnImpact(Collider2D collision)
    {
        int collidedLayer = collision.gameObject.layer;


        if (collidedLayer == 6 || collidedLayer == 7 || collidedLayer == 8 || collidedLayer == 12) // Projectile hits wall, ground, hazard or enemy
        {
            return true;
        }

        if (collidedLayer == 3 && canDamagePlayer) // Player's hit by an enemy projectile
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (DestroyOnImpact(collision))
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }

    }
}
