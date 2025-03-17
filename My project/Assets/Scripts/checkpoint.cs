using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Transform checkPointLocation;
    public BorderScript borderScript;

    private void Start()
    {
        borderScript = GameObject.FindGameObjectWithTag("Border").GetComponent<BorderScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            borderScript.UpdateCheckpoint(checkPointLocation.position);
        }
    }
}
