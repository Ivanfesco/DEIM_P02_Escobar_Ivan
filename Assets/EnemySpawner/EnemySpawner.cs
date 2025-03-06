using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Transform playertrf;

    [SerializeField] GameObject[] enemies;

    [SerializeField] float spawnchance = 1;

     float maxspawndist = 25;

    [SerializeField] Vector2 radiusspawnloc;
    [SerializeField] Vector3 spawnloc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Random.Range(1,1000) <= spawnchance)
        {
            spawnchance = Time.timeSinceLevelLoad;
            radiusspawnloc = Random.insideUnitCircle.normalized * maxspawndist;

            spawnloc = new Vector3(playertrf.position.x, 0, playertrf.position.z) + new Vector3 (radiusspawnloc.x, playertrf.position.y, radiusspawnloc.y);
            
            Instantiate(enemies[0], spawnloc, Quaternion.identity);
            
        }
    }

}
