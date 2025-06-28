using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public string[] GetPlanetIsReachedMessage()
    {
        return new string[]
        {
            "You have reached the planet, congratulations!"
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