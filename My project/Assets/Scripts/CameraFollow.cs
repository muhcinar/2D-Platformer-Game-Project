using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset;
    public float offsetX;
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] Transform player;
    [SerializeField] PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        offsetX = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isFacingRight)
        {
            offset = new Vector3(offsetX, 0f, -10f);
        }
        else
        {
            offset = new Vector3(-offsetX, 0f, -10f);
        }
            Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
