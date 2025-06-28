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
    }

    void Update()
    {
        currentHeight -= Time.deltaTime;
        if (currentHeight < minHeight)
        {
            uiManager.ShowGameEnd(true);
        }

        if (true /*floor is visible*/)
        {
            var playerMovement = player.gameObject.GetComponent<PlayerMovement>();
            playerMovement.NeedToMove();
        }
        
    }
}
