using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    [SerializeField] public string ItemName;


    [SerializeField] public string ItemDesc;


    [SerializeField] public UnityEngine.UI.Image ItemIcon;


    [SerializeField] public PlayerController pc;

    [SerializeField] public LevelController lc;

    void Start()
    {

        
        
    }
    public void AddItem()
    {
        print("aaa");
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
