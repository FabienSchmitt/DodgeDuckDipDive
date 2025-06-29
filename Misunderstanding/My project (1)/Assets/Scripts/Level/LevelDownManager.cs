using TMPro;
using UnityEngine;

public class LevelDownManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] spawners;
    [SerializeField] float maxDistance;
    [SerializeField] GameObject floor;
    [SerializeField] TextMeshProUGUI victoryMessage;
    [SerializeField] AudioClip levelMusicTheme;

    private FloorTrigger floorTrigger;
    private float currentHeight;
    private float minHeight;
    private UIManager uiManager;
    private DialogueManager dialogueManager;

    private string[] GetLevelStartDialogue() => new string[]
    {
        "To think of it, I was never really happy up here, what if instead I could live under the earth? No human could ever bother me there!",
        "Let's find out if I can actually find a peaceful cave of mine down there, and never again see this loathed and polluted sky."
    };

    private string GetLevelEndingMessage() => "Congratulations on becoming a mole and escaping the hard reality up there!";

    private void Awake()
    {
        if (!SoundManager.Instance.IsMusicThemePlaying(levelMusicTheme))
            SoundManager.Instance.ChangeMusicTheme(levelMusicTheme);
        currentHeight = player.transform.position.y;
        minHeight = currentHeight - maxDistance;

        uiManager = FindAnyObjectByType<UIManager>();
        dialogueManager = FindAnyObjectByType<DialogueManager>(FindObjectsInactive.Include);
        var movement = floor.GetComponent<SpawnObjectMovement>();
        movement.Initialize(player);
        floorTrigger = FindAnyObjectByType<FloorTrigger>();
        floor.transform.position = new Vector2(floor.transform.position.x, minHeight);
        floor.gameObject.SetActive(true);
    }

    private void Start()
    {
        dialogueManager.ShowDialogue(GetLevelStartDialogue());
    }

    private void Update()
    {
        if (floorTrigger.IsMole)
        {
            ManageGameEnding();
        }
    }

    private void ManageGameEnding()
    {
        if (uiManager.IsGameEndingScreenShowing())
            return;
        if (uiManager.IsDialogeBoxShowing())
            return; // wait until dialogbox is closed to show other screens
        victoryMessage.text = GetLevelEndingMessage();
        StartCoroutine(ShowGameEnding());
    }

    private System.Collections.IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(3f);
        uiManager.ShowGameEnd(true);
    }
}
