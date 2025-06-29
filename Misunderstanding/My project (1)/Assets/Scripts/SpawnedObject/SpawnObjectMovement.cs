using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObjectMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject optionalSecondaryGameObject;
    [SerializeField] bool canBeDestroyed = true;
    [SerializeField] float secondarySpeed;
    [SerializeField] int toldInReverseOccurence;

    private static System.Random toldInReverseRandomizer = new();
        
    GameObject player;
    PlayerMovement playerMovement;
    PlayerMotion playerMotion;

    public void Initialize(GameObject _player)
    {
        player = _player;
        // flip sprite to go the opposite way of the player
        FlipSprite(spriteRenderer.gameObject);

        // flip bird bubble randomly once every XX birds
        if(toldInReverseRandomizer.Next(toldInReverseOccurence) == 0)
            FlipSprite(optionalSecondaryGameObject);

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMotion = playerMovement.GetPlayerMotion();
    }

    private void FlipSprite(GameObject _gameObject)
    {
        if (_gameObject != null)
        {
            if (Mathf.Sign(_gameObject.transform.localScale.x) == Mathf.Sign(player.transform.localScale.x))
                _gameObject.transform.localScale = new Vector2(-_gameObject.transform.localScale.x, _gameObject.transform.localScale.y);
        }
    }

    public void Stop()
    {
        speed = 0;
    }

    void Update()
    {
        if (playerMotion == PlayerMotion.Vertical)
        {
            var playerGoingRight = player.transform.localScale.x > 0;

            float speedBoost = playerMovement.IsBoostActive ? 2f : 1f;
            var direction = playerGoingRight ? Vector3.left : Vector3.right; // spawn object go the opposite way.
            transform.position += direction * speed * speedBoost * Time.deltaTime;

            var relativePosition = Camera.main.WorldToViewportPoint(transform.position);
            if (relativePosition.x > 3f || relativePosition.x < -2f)
            {
                Destroy(this.gameObject);
            }
        }

        else if (playerMotion == PlayerMotion.Horizontal)
        {
            var playerGoingUp = player.transform.localScale.y > 0;

            var direction = playerGoingUp ? Vector3.down : Vector3.up; // spawn object go the opposite way.
            if (secondarySpeed > 0)
            {
                direction *= speed;
                bool isGoingLeft = transform.localScale.x > 0;
                direction += (isGoingLeft ? Vector3.left : Vector3.right) * secondarySpeed;
                transform.position += direction * Time.deltaTime;
            }
            else
            {
                transform.position += direction * speed * Time.deltaTime;
            }

            var relativePosition = Camera.main.WorldToViewportPoint(transform.position);
            if (relativePosition.y > 3f || relativePosition.y < -2f && canBeDestroyed)
            {
                Destroy(this.gameObject);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && secondarySpeed > 0)
        {
            // when they hit a wall, they go the oppsosite direction
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
