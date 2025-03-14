using UnityEngine;

public class SK_minionAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AttackEnded()
    {
        transform.GetComponent<EnemyController>().DealDamage();
    }
}
