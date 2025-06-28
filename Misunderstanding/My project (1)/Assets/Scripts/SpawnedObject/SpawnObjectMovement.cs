using UnityEngine;

public class SpawnObjectMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D boxCollider;
    
    GameObject player;

    public void Initialize(GameObject _player)
    {
        player = _player;
    }

    void Update()
    {
        var playerGoingRight = player.transform.localScale.x > 0;

        var direction = playerGoingRight ? Vector3.left : Vector3.right; // spawn object go the opposite way.
        transform.position += direction * speed * Time.deltaTime;

        var relativePosition = Camera.main.WorldToViewportPoint(transform.position);
        Debug.Log($"relative position : {relativePosition.x} transform : {transform.position.x}");
        if (relativePosition.x > 3f || relativePosition.x < -2f)
        {
            Destroy(this.gameObject);
        }
    }
}
