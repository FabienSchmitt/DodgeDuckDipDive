using System;
using UnityEngine;

public class EndZoneSpawner : MonoBehaviour
{
    [SerializeField] GameObject endZone;
    [SerializeField] GameObject player;
    [SerializeField] float endZoneSpawnTime = 10f;
    [SerializeField] GameObject[] spawners;

    private float elapsedTime = 0;
    private bool canSpawn = true;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.ChangeDirectionHandler += ResetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = elapsedTime + Time.deltaTime;
        if (elapsedTime > endZoneSpawnTime && canSpawn)
        {
            SpawnEndZone();
            // We make sure we only spawn the endtzone once.
            canSpawn = false;
        }
    }

    private void ResetTimer(object sender, EventArgs e)
    {
        elapsedTime = 0;
    }

    void SpawnEndZone()
    {
        // We don't want to spawn clouds or birds anymore
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }

        // player cannot change direction anymore
        playerMovement.CanChangeDirection = false;

        var playerGoingRight = player.transform.localScale.x > 0;
        float offset = playerGoingRight ? 1.1f : -1.1f;
        Vector3 pos = new Vector3(UnityEngine.Random.value + offset, 0.5f, 10); // should spawn in the middle of the screen
        pos = Camera.main.ViewportToWorldPoint(pos);

        endZone.transform.position = pos;
        var movement = endZone.GetComponent<SpawnObjectMovement>();
        movement.Initialize(player);
        endZone.gameObject.SetActive(true);
    }
}
