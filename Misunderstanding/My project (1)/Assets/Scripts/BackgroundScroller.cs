using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] float scrollSpeed = 0.1f;
    [SerializeField] float resetPositionX = -20f;
    [SerializeField] float startPositionX = 20f;

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
