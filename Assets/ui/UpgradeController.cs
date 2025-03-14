using Unity.VisualScripting;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{

    [SerializeField] LevelController lc;

    [SerializeField] GameObject[] itemlist;
    [SerializeField] ItemInfo FirstitemInfo;
    [SerializeField] ItemInfo SeconditemInfo;

    [SerializeField] ItemInfo ThirditemInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lc = FindAnyObjectByType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateUpgrades()
    {
        FirstitemInfo = itemlist[Random.Range(0, itemlist.Length)].GetComponent<ItemInfo>();
        SeconditemInfo = itemlist[Random.Range(0, itemlist.Length)].GetComponent<ItemInfo>();
        ThirditemInfo = itemlist[Random.Range(0, itemlist.Length)].GetComponent<ItemInfo>();

        lc.ShowUpgrades(
        FirstitemInfo.ItemName, FirstitemInfo.ItemDesc, FirstitemInfo.ItemIcon,
        SeconditemInfo.ItemName, SeconditemInfo.ItemDesc, SeconditemInfo.ItemIcon,
        ThirditemInfo.ItemName, ThirditemInfo.ItemDesc, ThirditemInfo.ItemIcon
        
        );
    }

}
