using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 6.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private PlayerController playerControllerScript;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 backDirection = (transform.position - player.transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if(transform.position.y<-10)
        {
            Destroy(gameObject);
        }
        if(playerControllerScript.ultraPowerup)
        {
         if(playerControllerScript.onground)
            {
                enemyRb.AddForce(backDirection * 20);
            }
        }
    }
    
   
}
