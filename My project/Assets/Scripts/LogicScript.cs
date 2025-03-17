using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{

    public Text scoreText;
    public Text healthText;

    public PlayerInfo playerInfo;
    public GameObject gameOverScreen;

    [SerializeField] GameObject healthBar;
    [SerializeField] Image currentHealthBar;

    private int score = 0;

    private void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        currentHealthBar = healthBar.GetComponent<Image>();
    }


    [ContextMenu("Increase Score")]
    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = score.ToString();

        return;
    }

    public void HealPlayer(int healAmount)
    {
        if (playerInfo.playerHealth < playerInfo.playerMaxHealth)
        {
            playerInfo.playerHealth += healAmount;
            healthText.text = playerInfo.playerHealth.ToString();
            currentHealthBar.fillAmount = playerInfo.playerHealth / 10f;
        }
    }

    public int DamageCreature(GameObject creature, int currentHealth, int damageTaken)
    {
        int finalHealth;

        finalHealth = currentHealth - damageTaken;
        
        if (finalHealth < 0)
        {
            finalHealth = 0;
        }

        if (creature.layer == 3)
        {
            HealthToText(finalHealth);
            currentHealthBar.fillAmount = finalHealth / 10f;
        }

        return finalHealth;
    }

    public void HealthToText(int health)
    {
        healthText.text = health.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
