using UnityEngine;

public class SpawnObjectMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    GameObject player;

    public void Initialize(GameObject _player)
    {
        player = _player;
        // flip sprite to go the opposite way of the player
        if (Mathf.Sign(spriteRenderer.transform.localScale.x) == Mathf.Sign(player.transform.localScale.x))
            spriteRenderer.transform.localScale = new Vector2(-spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y);
    }

    void Update()
    {
        var playerGoingRight = player.transform.localScale.x > 0;

        var direction = playerGoingRight ? Vector3.left : Vector3.right; // spawn object go the opposite way.
        transform.position += direction * speed * Time.deltaTime;

        var relativePosition = Camera.main.WorldToViewportPoint(transform.position);
        if (relativePosition.x > 3f || relativePosition.x < -2f)
        {
            Destroy(this.gameObject);
        }
    }
}
