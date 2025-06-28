using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] GameObject player;
    private UIManager uiManager;


    private void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.NeedToMove();

            var floorMovemement = collision.gameObject.GetComponentInParent<SpawnObjectMovement>();
            floorMovemement.Stop();
        }

        if (collision.gameObject.tag == "Player")
        {
            uiManager.ShowGameEnd(true);
        }
    }
}
