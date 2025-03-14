using System.Runtime.InteropServices;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    [SerializeField] public string ItemName;


    [SerializeField] public string ItemDesc;


    [SerializeField] public Texture ItemIcon;


    [SerializeField] PlayerController pc;

    [SerializeField] LevelController lc;

    void Start()
    {

        pc = FindAnyObjectByType<PlayerController>();
        lc = pc.gameObject.GetComponent<LevelController>();
        AddItem();
        
    }
    public void AddItem()
    {
        switch(ItemName){
        case "Crown":
        Crown(); 
        break;

        case"Grinder":
        Grinder();
        break;

        case"Shield":
        Shield();
        break;

        case"Medicine":
        Medicine();
        break;

        case"Healthcare":
        Healthcare();
        break;

        case"Express Healthcare Card":
        HealthCard();
        break;

    }
    }

    void Crown()
    {
        lc.xpMult = lc.xpMult + 0.1f;
    }

    void Grinder()
    {
        pc.damage = pc.damage + 0.5f;
    }

    void Shield()
    {
        pc.incomingDamageMult = pc.incomingDamageMult - 0.05f;
    }

    void Medicine()
    {
        pc.Health = pc.Health + pc.Health*0.1f;
    }

    void Healthcare()
    {
        pc.HealthRegen = pc.HealthRegen+1;
    }    

    void HealthCard()
    {
        pc.regencooldown = pc.regencooldown - 3;
    }



}
