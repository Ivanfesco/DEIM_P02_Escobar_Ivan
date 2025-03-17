using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    [SerializeField] private Animator animator;

    [SerializeField] Rigidbody rb;

    [SerializeField] NavMeshAgent nma;


    private PlayerController pc;

    private bool InReachOfPlayer;
    float attacktimer;

    [SerializeField] float damagetimer;

    [SerializeField] float Health = 3;

    bool canTakeDamage = true;

    [SerializeField] float damage = 1;

    [SerializeField] GameObject XpObj;

    GameObject SpawnedObj;

    [SerializeField] GameObject particlestospawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("moving", true);
        if (pc == null)
        {
            pc = FindAnyObjectByType<PlayerController>();
            target = pc.transform;


        }
    }

    // Update is called once per frame
    void Update()
    {

        if (InReachOfPlayer)
        {
            if (attacktimer <= 0)
            {
                ExecuteAttack();
                attacktimer = 3;
            }
        }

        if (attacktimer >= 0)
        {
            attacktimer = attacktimer - Time.deltaTime;
        }

        if (!canTakeDamage)
        {
            if (damagetimer <= 0)
            {
                damagetimer = 0.2f;
                canTakeDamage = true;
                rb.linearVelocity = Vector3.zero;
                nma.enabled = true;
            }
        }

        if (damagetimer >= 0)
        {
            damagetimer = damagetimer - Time.deltaTime;
        }
        agent.SetDestination(target.position);

    }

    private void OnTriggerEnter(Collider other)
    {


        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            InReachOfPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InReachOfPlayer = false;
        }
    }

    private void ExecuteAttack()
    {
        animator.SetTrigger("attacking");
    }


    public void DealDamage()
    {
        if(InReachOfPlayer)
        {
            pc.ReceiveDamage(damage);
        }
    }
    public void ReceiveDamage(Vector3 playerpos, float damage)
    {
        if (canTakeDamage)
        {
            Health = Health - damage;
            canTakeDamage = false;
            nma.enabled = true;
            animator.SetTrigger("hit");

            rb.AddForce(playerpos * 15 , ForceMode.VelocityChange );


            if (Health <= 0)
            {
                SpawnedObj = Instantiate(XpObj, gameObject.transform.position, Quaternion.identity);
                SpawnedObj.GetComponent<XPObjController>().pc = pc;
                Instantiate(particlestospawn, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
