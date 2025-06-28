using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    UIManager uiManager;
    DialogueManager dialogueManager;
    private void Awake()
    {
        uiManager = GameObject.FindAnyObjectByType<UIManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            uiManager.ShowGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EncounterBird")
        {
            var encounterBirdManager = collision.gameObject.GetComponent<EncounterBirdManager>();
            if (encounterBirdManager.EncounterDone) // fixes a bug when dialogue not pausing quick enough and collision being entered twice
                return;

            encounterBirdManager.EncounterDone = true;
            encounterBirdManager.enabled = false;
            dialogueManager.ShowDialogue(encounterBirdManager.GetDialogueMessage());
        }
        else if (collision.gameObject.tag == "Planet")
        {
            var planetManager = collision.gameObject.GetComponent<PlanetManager>();
            dialogueManager.ShowDialogue(planetManager.GetPlanetIsReachedMessage());
            planetManager.SetPlanetReached();
        }
    }
}
