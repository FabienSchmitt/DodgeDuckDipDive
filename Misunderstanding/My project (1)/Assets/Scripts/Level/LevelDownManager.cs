using UnityEngine;

public class LevelDownManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] spawners;
    [SerializeField] float maxDistance;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject floor;

    private float currentHeight;
    private float minHeight;

    private void Awake()
    {
        currentHeight = player.transform.position.y;
        minHeight = currentHeight - maxDistance;

        uiManager = GameObject.FindAnyObjectByType<UIManager>();
        var movement = floor.GetComponent<SpawnObjectMovement>();
        movement.Initialize(player);
        floor.transform.position = new Vector2(floor.transform.position.x, minHeight);
        floor.gameObject.SetActive(true);
    }

    void Update()
    {
    }
}
