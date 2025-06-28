using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class Level0Manager : MonoBehaviour
{

    [SerializeField] DialogueManager dialogueManager;
    private string[] GetLevelStartDialogue() => new string[]
    {
        "Poor me, migrating bird with no sense of where to go, with those crazy humans burning everything with their oversized egos and twisted minds.",
        "Well anyways, let's find a refreshing spot where I can finally rest away from this hassle!",
        "Keyboard: WASD | Xbox controller: Joystick"
    };


    private void Awake()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        dialogueManager.ShowDialogue(GetLevelStartDialogue());
    }

    private void Update()
    {
        // define level0 logic here
    }
}