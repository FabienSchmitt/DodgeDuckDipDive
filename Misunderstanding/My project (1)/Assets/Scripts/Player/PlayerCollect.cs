using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private LevelTimer levelTimer;

    private void Awake()
    {
        levelTimer = FindAnyObjectByType<LevelTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpaceGear")
        {
            levelTimer.AddSeconds(10);
            Destroy(collision.gameObject);
        }
    }
}
