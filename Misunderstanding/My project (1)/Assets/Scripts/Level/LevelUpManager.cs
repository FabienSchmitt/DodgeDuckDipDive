using TMPro;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private UIManager uiManager;
    private LevelTimer levelTimer;

    [SerializeField] TextMeshProUGUI victoryMessage;
    DialogueManager dialogueManager;
    private string[] GetLevelStartDialogue() => new string[]
    {
        "YES, I feel this is the right way, up into space following the stars, till I reach a decent planet with no dumb humans to disturb me!",
        "Though I feel a slight urge to find oxygen since I got here... But no worries, there are enough abandoned space gears lying around here that I can reuse to get where I want.",
        "I hope the ride is not too long to this never dreamed-of planet!"
    };

    private string GetLevelEndingMessage() => $"Congratulations, you have reached a human-free planet and can now safely rest for the remaining of your bird life.";

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        levelTimer = FindFirstObjectByType<LevelTimer>();
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        dialogueManager.ShowDialogue(GetLevelStartDialogue());
    }

    public bool IsPlanetReached { get; set; }

    // Update is called once per frame
    private void Update()
    {
        if (IsPlanetReached)
        {
            ManageGameEnding();
        }
        else if (levelTimer.IsTimeUp)
        {
            if (!uiManager.IsGameOverScreenShowing())
                uiManager.ShowGameOver();
        }
    }

    private void ManageGameEnding()
    {
        if (uiManager.IsGameEndingScreenShowing())
            return;
        if (uiManager.IsDialogeBoxShowing())
            return; // wait until dialogbox is closed to show other screens
        levelTimer.enabled = false;
        victoryMessage.text = GetLevelEndingMessage();
        StartCoroutine(ShowGameEnding());
    }

    private System.Collections.IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.ShowGameEnd(true);
    }
}
