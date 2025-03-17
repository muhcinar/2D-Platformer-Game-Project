using UnityEngine;

public class CollectibleSpawnerScript : MonoBehaviour
{

    public GameObject collectable;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Instantiate(collectable, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
}
