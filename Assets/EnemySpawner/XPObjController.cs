using UnityEngine;

public class XPObjController : MonoBehaviour
{

    [SerializeField] float XpToGive = 3;

    [SerializeField] public PlayerController pc;

    [SerializeField] SphereCollider sphcol;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            pc.TriggerXPPass(XpToGive);
            Destroy(gameObject);
        }
    }


}
