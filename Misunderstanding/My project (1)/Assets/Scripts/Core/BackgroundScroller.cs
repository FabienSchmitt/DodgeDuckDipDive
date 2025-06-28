using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] float scrollSpeed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    private float resetPositionXLeft;
    private float resetPositionXRight;
    private float startPositionX;

    [SerializeField] float resetPositionY = -20f;
    [SerializeField] float startPositionY = 20f;

    private void Awake()
    {
        startPositionX = transform.position.x;
        resetPositionXLeft = transform.position.x + spriteRenderer.bounds.size.x;
        resetPositionXRight = transform.position.x - spriteRenderer.bounds.size.x;

        startPositionY = transform.position.y;
        resetPositionY = spriteRenderer.size.y;
    }

    void Update()
    {
        PlayerMotion playerMotion = player.GetComponent<PlayerMovement>().playerMotion;
        if(playerMotion == PlayerMotion.Vertical)
        {
            var goingRight = player.gameObject.transform.localScale.x > 0;

            var direction = goingRight ? Vector3.left : Vector3.right;
            transform.position += direction * scrollSpeed * Time.deltaTime;
            if (transform.position.x <= resetPositionXRight ||
                transform.position.x >= resetPositionXLeft)
            {
                Vector3 newPos = transform.position;
                newPos.x = startPositionX;
                transform.position = newPos;
            }
        }
        else if(playerMotion == PlayerMotion.Horizontal)
        {
            transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
            if (transform.position.y <= resetPositionY)
            {
                Vector3 newPos = transform.position;
                newPos.y = startPositionY;
                transform.position = newPos;
            }
        }
    }
}
