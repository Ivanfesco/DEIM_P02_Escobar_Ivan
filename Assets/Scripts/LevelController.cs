using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    [SerializeField] float RequiredXp = 10;

    [SerializeField] float RequiredXpMultiplier = 1.4f;
    float CurrentLevel;
    float CurrentXp;

    [SerializeField] public GameObject LevelUpGui;

    [SerializeField] Slider xpSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xpSlider.maxValue = RequiredXp;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddXp(float IncomingXP)
    {
        CurrentXp = CurrentXp + IncomingXP;
        xpSlider.value = CurrentXp;

        if(CurrentXp >= RequiredXp)
        {
            TriggerLevelUp();
        }
    }


    public void TriggerLevelUp()
    {
        CurrentLevel++;

        // LevelUpGui.SetActive(true);

        RequiredXp = RequiredXp + Random.Range(0.7f, 1.3f) * CurrentLevel * RequiredXpMultiplier;

        xpSlider.maxValue = RequiredXp;

        xpSlider.value = 0;


    }

}
