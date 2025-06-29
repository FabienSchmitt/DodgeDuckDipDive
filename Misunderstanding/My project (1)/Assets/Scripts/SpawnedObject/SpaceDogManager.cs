using UnityEngine;

public class SpaceDogManager : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider2D;

    public bool EncounterDone { get; set; }

    public string[] GetDialogueMessage()
    {
        return new string[]
        {
             "Hi there, could you please take some time to sit here with me? I am most grateful!"
        };
    }

    private void OnDisable()
    {
        // do not show bubble or trigger dialogue once it has been seen
        boxCollider2D.enabled = false;
    }
}