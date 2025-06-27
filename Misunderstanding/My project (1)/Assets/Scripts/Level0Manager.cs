using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class Level0Manager : MonoBehaviour
{
    [SerializeField] GameObject cloudPrefab;
    [SerializeField] bool birdGoingRight;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject player;

    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > spawnTime)
        {
            SpawnCloud();
            elapsedTime = 0f;
        }
    }
    void SpawnCloud()
    {
        var birdGoingRight = player.transform.localScale.x > 0;
        float offset = birdGoingRight ? 1.1f : -1.1f;
        Vector3 pos = new Vector3(Random.value + offset, Random.value, 10);
        pos = Camera.main.ViewportToWorldPoint(pos);

        var cloudObject = Instantiate(cloudPrefab, pos, Quaternion.identity);
        var cloudBehavior = cloudObject.GetComponent<CloudMovement>();
        cloudBehavior.Initialize(player);

    }
}
