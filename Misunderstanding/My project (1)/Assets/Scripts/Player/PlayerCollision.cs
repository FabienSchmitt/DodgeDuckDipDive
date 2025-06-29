using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] AudioClip obstacleCollisionSound;
    [SerializeField] AudioClip encounterSound;
    [SerializeField] AudioClip planetReachedSound;
    
    UIManager uiManager;
    DialogueManager dialogueManager;
    LevelTimer levelTimer;
    
    public bool Invincible { get; set; } = false;
    
    private void Awake()
    {
        uiManager = GameObject.FindAnyObjectByType<UIManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
        levelTimer = FindAnyObjectByType<LevelTimer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !Invincible)
        {
            SoundManager.Instance.PlaySound(obstacleCollisionSound);
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
            SoundManager.Instance.PlaySound(encounterSound);
            dialogueManager.ShowDialogue(encounterBirdManager.GetDialogueMessage());
        }
        else if (collision.gameObject.tag == "SpaceDog")
        {
            var spaceDogManager = collision.gameObject.GetComponent<SpaceDogManager>();
            if (spaceDogManager.EncounterDone) // fixes a bug when dialogue not pausing quick enough and collision being entered twice
                return;

            spaceDogManager.EncounterDone = true;
            spaceDogManager.enabled = false;
            SoundManager.Instance.PlaySound(encounterSound);
            dialogueManager.ShowDialogue(spaceDogManager.GetDialogueMessage());
            levelTimer.RemoveSeconds(10);
        }
        else if (collision.gameObject.tag == "Planet")
        {
            SoundManager.Instance.PlaySound(planetReachedSound);
            var planetManager = collision.gameObject.GetComponent<PlanetManager>();
            dialogueManager.ShowDialogue(planetManager.GetPlanetIsReachedMessage());
            planetManager.SetPlanetReached();
        }
    }
}
