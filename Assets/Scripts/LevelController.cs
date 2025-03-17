using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    [SerializeField] float RequiredXp = 10;

    [SerializeField] public float xpMult = 1;

    [SerializeField] float RequiredXpMultiplier = 1.4f;
    [SerializeField] float CurrentLevel;
    [SerializeField] float CurrentXp;

    [SerializeField] public GameObject LevelUpGui;

    [SerializeField] Slider xpSlider;

    [SerializeField] TextMeshProUGUI FirstItemName;

    [SerializeField] TextMeshProUGUI FirstItemDesc;

    [SerializeField] UnityEngine.UI.Image FirstItemIcon;

    [SerializeField] TextMeshProUGUI SecondItemName;

    [SerializeField] TextMeshProUGUI SecondItemDesc;

    [SerializeField] UnityEngine.UI.Image SecondItemIcon;

    [SerializeField] TextMeshProUGUI ThirdItemName;

    [SerializeField] TextMeshProUGUI ThirdItemDesc;

    [SerializeField] UnityEngine.UI.Image ThirdItemIcon;

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
        CurrentXp = CurrentXp + (IncomingXP * xpMult);
        xpSlider.value = CurrentXp;

        if (CurrentXp >= RequiredXp)
        {
            TriggerLevelUp();
        }
    }


    public void TriggerLevelUp()
    {
        CurrentLevel++;

        LevelUpGui.SetActive(true);

        LevelUpGui.GetComponentInParent<UpgradeController>().GenerateUpgrades();

        CurrentXp = CurrentXp - RequiredXp;

        RequiredXp = RequiredXp + Random.Range(0.7f, 1.3f) * CurrentLevel * RequiredXpMultiplier;

        xpSlider.maxValue = RequiredXp;


        xpSlider.value = CurrentXp;

        Time.timeScale = 0;

    }

    public void ShowUpgrades(string Name1, string Description1, UnityEngine.UI.Image Icon1, string Name2, string Description2, UnityEngine.UI.Image Icon2, string Name3, string Description3, UnityEngine.UI.Image Icon3)
    {
        FirstItemName.text = Name1;
        FirstItemDesc.text = Description1;
        FirstItemIcon.sprite = Icon1.sprite;

        SecondItemName.text = Name2;
        SecondItemDesc.text = Description2;
        SecondItemIcon.sprite = Icon2.sprite;

        ThirdItemName.text = Name3;
        ThirdItemDesc.text = Description3;
         ThirdItemIcon.sprite = Icon3.sprite;  
    }

    public void UpgradeSelected()
    {
                LevelUpGui.SetActive(false);
                Time.timeScale = 1;


    }

}
