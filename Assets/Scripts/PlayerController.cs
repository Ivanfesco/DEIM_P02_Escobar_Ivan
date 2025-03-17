using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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


    public float Health = 100;


    float XpMult = 1;
    bool hasObjectInHand;

    bool moving;
    bool canAttack = true;

    public float damage = 1;

    public float incomingDamageMult = 1;

    public float HealthRegen;

    public float regentimer;

    public float regencooldown = 90;

    [SerializeField] TextMeshProUGUI hptext;

    [SerializeField] GameObject gameovercanvas;

        [SerializeField] GameObject gamewoncanvas;

        [SerializeField] TextMeshProUGUI timetext;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        timetext.text = ((int)Time.timeSinceLevelLoad).ToString();

        if (Time.timeSinceLevelLoad > 300)
        {
            gamewoncanvas.SetActive(true);
            Time.timeScale = 0;
        }


        hptext.text = ((int)Health).ToString();

        regentimer = regentimer + Time.deltaTime;

        if (regentimer > regencooldown)
        {
            Health = Health + HealthRegen;
            regentimer = 0;
        }

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
                attacksource.pitch = Random.Range(0.95f,1.05f);
                attacksource.Play();
                ExecuteAttack();
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
                    enemycontrollertoattack.ReceiveDamage(transform.forward, damage);
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



    public void ReceiveDamage(float incomingDamage)
    {
        Health = Health - incomingDamage * incomingDamageMult;
        if (Health <= 0)
        {

            gameovercanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
