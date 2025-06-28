using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject player;
    [SerializeField] bool flipSpawn = false;
    PlayerMovement playerMovement;

    private float elapsedTime = 0f;

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > spawnTime)
        {
            SpawnObject();
            elapsedTime = 0f;
        }
    }

    public void SpawnObject()
    {
        Vector3 pos = Vector3.zero;
        if (playerMovement.playerMotion == PlayerMotion.Vertical)
        {
            var playerGoingRight = player.transform.localScale.x > 0;
            float offset = playerGoingRight ? 1.1f : -1.1f;
            pos = new Vector3(Random.value + offset, Random.value, 10);
            pos = Camera.main.ViewportToWorldPoint(pos);
        }
        else if (playerMovement.playerMotion == PlayerMotion.Horizontal)
        {
           
            var playerGoingUp = player.transform.localScale.y > 0;
            float offset = playerGoingUp ? 1.1f : -1.1f;
            pos = new Vector3(Random.value, Random.value + offset, 10);
            pos = Camera.main.ViewportToWorldPoint(pos);
        }
        bool flip = flipSpawn && Random.value > 0.5;

        Debug.Log(flip);
        var spawnedObject = Instantiate(objectPrefab, pos, Quaternion.identity);
        
        var spawnObjectBehavior = spawnedObject.GetComponent<SpawnObjectMovement>();
        spawnObjectBehavior.Initialize(player);
        if (flip)
        {
            var current = spawnedObject.transform.localScale;
            spawnedObject.transform.localScale = new Vector3(-current.x, current.y, current.z);
        }
    }
}
