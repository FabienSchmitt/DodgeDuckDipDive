using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] GameObject player;
    private UIManager uiManager;
    private bool isMole = false;


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
            if (!isMole)
            {
                var playerAnimator = player.GetComponent<Animator>();
                var newScale = player.transform.localScale;
                newScale.y = Math.Abs(newScale.y);
                playerAnimator.SetBool("becomeMole", true);
                player.transform.localScale = newScale;
            }
        }
    }
}
