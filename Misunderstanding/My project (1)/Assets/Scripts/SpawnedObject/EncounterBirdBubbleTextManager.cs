using System.Linq;
using TMPro;
using UnityEngine;

public class EncounterBirdBubbleTextManager : MonoBehaviour
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


    private void Awake()
    {
        var random = new System.Random();
        bubble.text = bubbleTextCandidates[random.Next(bubbleTextCandidates.Count())];
    }
}