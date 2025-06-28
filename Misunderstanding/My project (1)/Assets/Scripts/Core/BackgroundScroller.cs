using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] float scrollSpeed = 0.1f;
    [SerializeField] float resetPositionX = -20f;
    [SerializeField] float startPositionX = 20f;

    [SerializeField] float resetPositionY = -20f;
    [SerializeField] float startPositionY = 20f;

    void Update()
    {
        PlayerMotion playerMotion = player.GetComponent<PlayerMovement>().playerMotion;
        if(playerMotion == PlayerMotion.Vertical)
        {
            transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
            if (transform.position.x <= resetPositionX)
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
