using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public string[] GetPlanetIsReachedMessage()
    {
        return new string[]
        {
            "What a treat, this human-free planet looks just fine to me, let us all have a joyful Bird Mitzvah to celebrate!"
        };
    }

    public void SetPlanetReached()
    {
        levelUpManager.IsPlanetReached = true;
    }

    private LevelUpManager levelUpManager;

    private void Awake()
    {
        levelUpManager = FindAnyObjectByType<LevelUpManager>();
    }
}