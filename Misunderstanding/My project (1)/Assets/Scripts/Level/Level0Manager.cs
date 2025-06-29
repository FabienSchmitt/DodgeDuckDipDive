using UnityEngine;

public class Level0Manager : MonoBehaviour
{

    DialogueManager dialogueManager;
    private string[] GetLevelStartDialogue() => new string[]
    {
        "Poor me, migrating bird with no sense of where to go, with those crazy humans burning everything with their oversized egos and twisted minds.",
        "Well anyways, let's find a refreshing spot where I can finally rest away from this hassle!",
        "Keyboard: WASD | Xbox controller: Joystick. Faster than the wind : E - but sometimes I get exited about speed."
    };


    private void Awake()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        dialogueManager.ShowDialogue(GetLevelStartDialogue());
    }


}