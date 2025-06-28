using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EncounterBirdTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubble;

    private string[] bubbleTextCandidates = new string[]
    {
        "Go the other way!",
        "This is not the way!",
        "Don't go there",
        "WTF you doing?",
        "Where are you going?",
        "Flee you fool",
        "Aaaaaaaarrrrghhhhhh!",
        "Don't trust anyone"
    };

    private string[] conversationCandidate0 = new string[]
    {
        "Hello there, listen pal, I am positive you are going the wrong way. I heard from a guy who heard it from a guy that there is a sweet refreshing spot going in the other direction.",
        "Why not go with me and change direction? (press Space/A)"
    };

    private string[] conversationCandidate1 = new string[]
{
        "Why me, you are a little cute one, aren't you? A shame you should be going that way, instead of joining me to the safe paradise island where I'm heading to.",
        "Why not go with me and change direction? (press Space/A)"
};


    private List<string[]> conversationCandidates = new();

    public EncounterBirdTextManager()
    {
        conversationCandidates.Add(conversationCandidate0);
        conversationCandidates.Add(conversationCandidate1);
    }

    private void Awake()
    {
        var random = new System.Random();
        bubble.text = bubbleTextCandidates[random.Next(bubbleTextCandidates.Count())];
    }

    public string[] GetDialogueMessage()
    {
        var random = new System.Random();
        return conversationCandidates[random.Next(conversationCandidates.Count)];
    }
}