using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] private Transform pointerTrf;
    [SerializeField] Animator playerAnimator;

    [SerializeField] AudioSource attacksource;

    [SerializeField] BoxCollider pickupcol;

    [SerializeField] List<GameObject> triggergameobjects;

    [SerializeField] Transform handtrf;

    [SerializeField] GameObject objectInHand;

    [SerializeField] BoxCollider handObjectCollider;
    [SerializeField] Rigidbody handObjectRB;

    [SerializeField] EnemyController enemycontrollertoattack;

    [SerializeField] LevelController LevelCont;

    int[] nullObjectsToRemove;

    float XpMult = 1;
    bool hasObjectInHand;

    bool moving;
    bool canAttack = true;

    float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        moving = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.World);
            moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime, Space.World);
            moving = true;


        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.World);
            moving = true;


        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);
            moving = true;

        }


        playerAnimator.SetBool("moving", moving);

        transform.LookAt(pointerTrf.position);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);

        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                playerAnimator.SetTrigger("attacking");
                canAttack = false;
                attacksource.Play();
                ExecuteAttack();
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hasObjectInHand)
            {
                PickObjectUp();
            }
            else
            {
                playerAnimator.SetTrigger("Throwing");
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        triggergameobjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        triggergameobjects.Remove(other.gameObject);
    }


    void PickObjectUp()
    {

        foreach (var GO in triggergameobjects)
        {
            if (GO.GetComponent<pickup>())
            {
                //Inicializando referencias para no usar .GetComponent todas las veces
                handObjectCollider = GO.GetComponent<BoxCollider>();
                handObjectRB = GO.GetComponent<Rigidbody>();
                objectInHand = GO;

                //desactivar collider del objeto recogido
                handObjectCollider.enabled = false;


                //poner objeto en la mano
                GO.transform.SetParent(handtrf);
                GO.transform.localPosition = GO.GetComponent<pickup>().pickeduppos;
                GO.transform.localRotation = Quaternion.Euler(GO.GetComponent<pickup>().pickeduprot);

                //hacer que el rigidbody no se caiga
                handObjectRB.constraints = RigidbodyConstraints.FreezeAll;

                //booleano de objeto en mano
                hasObjectInHand = true;
                break;
            }
        }

    }

    public void ThrowObject()
    {

        //quitar objeto de lista de objetos en el trigger
        triggergameobjects.Remove(objectInHand);

        //reinicializar objeto para que sea un objeto fisico
        handObjectRB.constraints = RigidbodyConstraints.None;
        handObjectCollider.enabled = true;

        handObjectRB.AddForce(transform.forward * 250);
        handObjectRB.AddForce(new Vector3(0, 400, 0));
        handObjectRB.AddTorque(new Vector3(0, 0, 1) * 25);

        //quitar objeto de mano
        objectInHand.transform.SetParent(null);
        hasObjectInHand = false;
        objectInHand = null;



    }


    private void ExecuteAttack()
    {
        for (int objects = triggergameobjects.Count - 1; objects >= 0; objects--)
        {
            if (triggergameobjects[objects] == null)
            {
                triggergameobjects.RemoveAt(objects);
            }
            else
            {
                enemycontrollertoattack = triggergameobjects[objects].GetComponent<EnemyController>();
                if (enemycontrollertoattack != null)
                {
                    enemycontrollertoattack.ReceiveDamage(transform.position, damage);
                }
            }


        }




    }

    public void AttackEnded()
    {
        canAttack = true;
    }

    public void TriggerXPPass(float IncomingXP)
    {
        LevelCont.AddXp(IncomingXP * XpMult);
    }

}
