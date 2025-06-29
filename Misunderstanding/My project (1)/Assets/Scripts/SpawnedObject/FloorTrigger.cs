using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawner;
    private UIManager uiManager;
    private DialogueManager dialogueManager;

    public bool IsMole { get; private set; }


    private void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        dialogueManager= FindAnyObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            // Bird player start to fall (for real)
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.NeedToMove();

            // The floor stops its upward movement
            var floorMovemement = collision.gameObject.GetComponentInParent<SpawnObjectMovement>();
            floorMovemement.Stop();

            //Disable spawners
            spawner.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (!IsMole)
            {
                IsMole = true;
                var playerAnimator = player.GetComponent<Animator>();
                var playerCollision = player.GetComponent<PlayerCollision>();
                playerCollision.Invincible = true;
                var newScale = player.transform.localScale;
                newScale.y = Math.Abs(newScale.y);
                playerAnimator.SetBool("becomeMole", true);
                player.transform.localScale = newScale;
                dialogueManager.ShowDialogue(new string[] { "Turns out I have always been a mole all that time, who would have believed it? I am so glad I could dig this out, what an adventure it has been." });
            }
        }
    }
}
