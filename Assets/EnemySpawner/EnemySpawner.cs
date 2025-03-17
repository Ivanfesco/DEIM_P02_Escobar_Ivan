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


    [SerializeField] LayerMask layerMask;

    int IndexToSpawn;

    float spawnindexadd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnchance = Time.timeSinceLevelLoad;
        if (Random.Range(1, 10000) <= spawnchance)
        {
            radiusspawnloc = Random.insideUnitCircle.normalized * maxspawndist;

            spawnloc = new Vector3(playertrf.position.x, 0, playertrf.position.z) + new Vector3(radiusspawnloc.x, playertrf.position.y, radiusspawnloc.y);

            RaycastHit hit;
            Debug.DrawRay(spawnloc + new Vector3(0, 2, 0), transform.TransformDirection(Vector3.down), Color.green, Mathf.Infinity);
            if (Physics.Raycast(spawnloc + new Vector3(0, 2, 0), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))

            {
                spawnindexadd = spawnchance / 500;

                IndexToSpawn = (int)Mathf.Floor((Random.Range(0f, (float)enemies.Length)) + (spawnindexadd));

                if(IndexToSpawn>enemies.Length)
                {
                    IndexToSpawn=enemies.Length;
                }

                Instantiate(enemies[IndexToSpawn], spawnloc, Quaternion.identity);

            }

            else
            {
                print("out of bounds");
            }

        }
    }

}
