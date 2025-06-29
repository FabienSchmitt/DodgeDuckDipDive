using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EncounterBirdManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBubble;
    [SerializeField] GameObject bubble;
    [SerializeField] BoxCollider2D boxCollider2D;

    public bool EncounterDone { get; set; }

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

    private string GetConversationCandidate(int choice)
    {
        switch (choice)
        {
            case 0:
                return "Hello there, listen pal, I am positive you are going the wrong way. I heard from a guy who heard it from a guy that there is a sweet refreshing spot going in the other direction.";
            case 1:
                return "Why me, you are a little cute one, aren't you? A shame you should be going that way, instead of joining me to the safe paradise island where I'm heading to.";
            case 2:
                return "Hey birdy, trust me you don't want to go there. Although I heard a strange rumour saying that if you keep the same course long enough you might find an original way through this mess. Ah bollocks!";
            case 3:
                return "You hear me out, friend of the air, I am the wisest bird of 'em all. Keep going straight and you will end up in hell, you should definitely NOT do that!";
            case 4:
                return "Too many becquerels for my beak that way, no radiation roasting for old Bob I tell you that.";
            default:
                throw new IndexOutOfRangeException($"Index {choice} is out of bound!");
        }
    }

    private void Awake()
    {
        var random = new System.Random();
        textBubble.text = bubbleTextCandidates[random.Next(bubbleTextCandidates.Count())];
    }

    public string[] GetDialogueMessage()
    {
        var random = new System.Random();
        int choice = random.Next(5);
        return new string[]
        {
            GetConversationCandidate(choice),
             "Why not go with me and change direction? (press Space | X)"
        };
    }

    private void OnDisable()
    {
        // do not show bubble or trigger dialogue once it has been seen
        bubble.SetActive(false);
        boxCollider2D.enabled = false;
    }
}