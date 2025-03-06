
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PointerController : MonoBehaviour
    
{
    [SerializeField] private Camera Cam;
    [SerializeField] Transform playerTrf;
    [SerializeField] private float maxRayDistance = 30000;
    [SerializeField] private LayerMask RaycastLayer;

private Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        

        if (Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit, maxRayDistance, RaycastLayer))
        {
            
            target = hit.point;

            while(Vector3.Distance(playerTrf.position, target) > 5 )
            {
              target = Vector3.MoveTowards(target, playerTrf.position, 0.01f);
            }
            
            
                transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);;
            
     





        }
    }

}
