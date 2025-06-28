using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] AudioClip collectionSound;

    private LevelTimer levelTimer;

    private void Awake()
    {
        levelTimer = FindAnyObjectByType<LevelTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpaceGear")
        {
            SoundManager.Instance.PlaySound(collectionSound);
            levelTimer.AddSeconds(10);
            Destroy(collision.gameObject);
        }
    }
}
