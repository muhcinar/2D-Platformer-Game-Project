using UnityEngine;

public class Sword : MonoBehaviour
{
    PlayerAttack playerAttack;

    private float range;

    public void SetSword(float newRange)
    {
        range = newRange;
    }

    void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    void Update()
    {
        SetSwordPosition();
    }

    public void SetSwordPosition()
    {
        float offset = playerAttack.CalculateOffset(range);
        float xPosition = playerAttack.gameObject.transform.position.x + offset;

        gameObject.transform.position = new Vector3(xPosition, playerAttack.gameObject.transform.position.y, playerAttack.gameObject.transform.position.z);

    }
}
