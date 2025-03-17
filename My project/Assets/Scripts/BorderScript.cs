using UnityEngine;

public class BorderScript : MonoBehaviour
{

    public GameObject player;
    private Vector3 checkpointPosition;

    private void Start()
    {
        checkpointPosition = new Vector3(0, 0, 0);
    }

    public void UpdateCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.transform.position = checkpointPosition;
        }

    }
}
