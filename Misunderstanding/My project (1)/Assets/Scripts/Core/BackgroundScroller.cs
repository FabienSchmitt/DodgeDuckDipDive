using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] float scrollSpeed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    private float resetPositionXLeft;
    private float resetPositionXRight;
    private float startPositionX;

    private float resetPositionYUp;
    private float resetPositionYDown;
    private float startPositionY;

    private void Awake()
    {
        startPositionX = transform.position.x;
        resetPositionXLeft = transform.position.x + spriteRenderer.bounds.size.x;
        resetPositionXRight = transform.position.x - spriteRenderer.bounds.size.x;

        startPositionY = transform.position.y;
        resetPositionYUp = transform.position.y + spriteRenderer.bounds.size.y;
        resetPositionYDown = transform.position.y - spriteRenderer.bounds.size.y;
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
            var goingUp = player.gameObject.transform.localScale.y > 0;
            var direction = goingUp ? Vector3.down : Vector3.up;
            transform.position += direction * scrollSpeed * Time.deltaTime;

            if (transform.position.y <= resetPositionYDown || transform.position.y >= resetPositionYUp)
            {
                Vector3 newPos = transform.position;
                newPos.y = startPositionY;
                transform.position = newPos;
            }
        }
    }
}
