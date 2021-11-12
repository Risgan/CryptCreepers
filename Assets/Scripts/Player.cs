using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float horizontalPlayer, verticalPlayer, Speed = 3f, angle, fireRate = 1;
    
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector2 facingDirection;
    [SerializeField] Quaternion targetRotation;
    public bool gunLoaded = true;
    public int health = 10;


    [SerializeField] Transform aim;
    [SerializeField] Camera camara;
    [SerializeField] Transform bulledPrefab;



    private void Awake()
    {
        print("awake");
    }
    void Start()
    {
        print("star");
    }

    void Update()
    {
        horizontalPlayer = Input.GetAxis("Horizontal");
        verticalPlayer = Input.GetAxis("Vertical");

        moveDirection.x = horizontalPlayer;
        moveDirection.y = verticalPlayer;

        transform.position += moveDirection * Time.deltaTime * Speed;

        //Movimiento de la mira
        facingDirection = camara.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        Bulled();


    }

    void Bulled()
    {
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            //sacar angulo entre jugador y mira;
            angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;//pasa de radianes a grados

            targetRotation = Quaternion.AngleAxis(angle,Vector3.forward);

            Instantiate(bulledPrefab, transform.position, targetRotation);
            StartCoroutine(ReloadGun());//llamar a la cortina
        }
    }


    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            print("Murio");
        }
    }

    //Cortina
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

}
