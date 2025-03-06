using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    PlayerController pc;

    private void Start() {
        pc = transform.parent.GetComponent<PlayerController>();
    }
    
    public void AttackEnded()
    {
        pc.AttackEnded();
    }  

    public void ThrowObject()
    {
        pc.ThrowObject();
    }
}
