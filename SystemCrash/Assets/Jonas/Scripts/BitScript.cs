using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitScript : MonoBehaviour
{
    private int age = 0;
    private GameObject player;
    private Rigidbody rb;
    private Vector3 moveDirection;
    public GameSettings gameSettings;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (gameSettings.playerAlive == false) Destroy(gameObject);

        age += 1;
        //determine if the player is close enough
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 distanceVector = gameObject.transform.position - player.transform.position;
        float distanceFromPoint = distanceVector.sqrMagnitude;
        if (distanceFromPoint <= 1)
        {
            player.GetComponent<PlayerStats>().points += 1;
            player.GetComponent<PlayerStats>().Damage(-1);
            Destroy(gameObject);
        }
        else if (distanceFromPoint <= 15)
        {
            transform.LookAt(player.transform.position);
            moveDirection = transform.forward;
            rb.AddForce(moveDirection.normalized * 50f, ForceMode.Force);
            //Debug.Log("close!")
        }

        else if (age >= 200) Destroy(gameObject);
    }
}