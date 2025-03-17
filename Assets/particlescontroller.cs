using UnityEngine;

public class particlescontroller : MonoBehaviour
{

    float timer;

    void FixedUpdate()
    {
        timer = timer+Time.fixedDeltaTime;
        if(timer>5)
        {
            Destroy(gameObject);
        }
    }


}
