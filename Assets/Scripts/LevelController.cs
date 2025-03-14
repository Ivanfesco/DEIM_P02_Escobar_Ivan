using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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

    [SerializeField] Texture FirstItemIcon;

    [SerializeField] TextMeshProUGUI SecondItemName;

    [SerializeField] TextMeshProUGUI SecondItemDesc;

    [SerializeField] Texture SecondItemIcon;

    [SerializeField] TextMeshProUGUI ThirdItemName;

    [SerializeField] TextMeshProUGUI ThirdItemDesc;

    [SerializeField] Texture ThirdItemIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xpSlider.maxValue = RequiredXp;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.L))
        {
            TriggerLevelUp();
        }

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

        LevelUpGui.GetComponent<UpgradeController>().GenerateUpgrades();

        CurrentXp = CurrentXp - RequiredXp;

        RequiredXp = RequiredXp + Random.Range(0.7f, 1.3f) * CurrentLevel * RequiredXpMultiplier;

        xpSlider.maxValue = RequiredXp;


        xpSlider.value = CurrentXp;

    }

    public void ShowUpgrades(string Name1, string Description1, Texture Icon1, string Name2, string Description2, Texture Icon2, string Name3, string Description3, Texture Icon3)
    {
        FirstItemName.text = Name1;
        FirstItemDesc.text = Description1;
        FirstItemIcon = Icon1;

        SecondItemName.text = Name2;
        SecondItemDesc.text = Description2;
        SecondItemIcon = Icon2;

        ThirdItemName.text = Name3;
        ThirdItemDesc.text = Description3;
         ThirdItemIcon = Icon3;  
    }

}
