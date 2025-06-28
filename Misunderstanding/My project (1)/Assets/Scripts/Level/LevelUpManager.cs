using TMPro;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private UIManager uiManager;
    private LevelTimer levelTimer;

    [SerializeField] TextMeshProUGUI victoryMessage;

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        levelTimer = FindFirstObjectByType<LevelTimer>();
    }

    private string GetLevelEndingMessage() => $"Congratulations, you have reached a human-free planet and can now safely rest for the remaining of your bird life.";

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
        //SoundManager.Instance.PlaySound(victorySound);
        victoryMessage.text = GetLevelEndingMessage();
        StartCoroutine(ShowGameEnding());
    }

    private System.Collections.IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.ShowGameEnd(true);
    }
}
