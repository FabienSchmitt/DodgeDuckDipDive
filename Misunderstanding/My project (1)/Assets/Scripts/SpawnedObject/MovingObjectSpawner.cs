using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] bool playerGoingRight;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject player;

    private float elapsedTime = 0f;

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
        var playerGoingRight = player.transform.localScale.x > 0;
        float offset = playerGoingRight ? 1.1f : -1.1f;
        Vector3 pos = new Vector3(Random.value + offset, Random.value, 10);
        pos = Camera.main.ViewportToWorldPoint(pos);

        var spawnedObject = Instantiate(objectPrefab, pos, Quaternion.identity);
        var spawnObjectBehavior = spawnedObject.GetComponent<SpawnObjectMovement>();
        spawnObjectBehavior.Initialize(player);

    }
}
