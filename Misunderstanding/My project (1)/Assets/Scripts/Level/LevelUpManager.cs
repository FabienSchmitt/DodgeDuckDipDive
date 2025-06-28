using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private UIManager uiManager;
    private LevelTimer levelTimer;

    void Awake()
    {
        levelTimer = FindFirstObjectByType<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTimer.IsTimeUp)
        {
            uiManager.ShowGameOver();
        }
    }
}
