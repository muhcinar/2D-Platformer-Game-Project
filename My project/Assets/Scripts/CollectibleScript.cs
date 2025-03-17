using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    [SerializeField] LogicScript logic;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] AudioManager audioManager;

    void Start()
   {
        logic = logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerInfo = playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && playerInfo.isAlive)
        {
            Destroy(gameObject.transform.parent.gameObject);
            audioManager.PlaySFX(audioManager.heal);
            logic.AddScore(1);
            logic.HealPlayer(1);
        }
    }
}
