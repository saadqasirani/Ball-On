using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 20.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    private float powerupStrength = 10.0f;
    public GameObject powerUpIndicator;
    public bool gameover = false;
    public bool ultraPowerup = false;
    public bool onground = false;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
    }

    // Update is called once per frame
    void Update()
    {
        float PlayerForward = Input.GetAxis("Vertical");//getting vertical controls of the player to move the player forward and backward
        playerRb.AddForce(focalPoint.transform.forward * speed * PlayerForward);//appling vertical controls from input manager to move player
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);//using a powerup indicator and giving player position to powerup


        if (transform.position.y < -10)
        {
            gameover = true;
            Debug.Log("Game Over");
        }
        if (ultraPowerup)
        {
            StartCoroutine(delay());
        }
    }
    private void OnTriggerEnter(Collider other)//whenever the player collide with powerup obstacle it will increase player power
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(powerCountdown());
            
        }

        if (other.CompareTag("Ultrapower"))
        {
            ultraPowerup = true;
            Destroy(other.gameObject);
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(ultrapowerCountdown());
        }
    }
    IEnumerator powerCountdown()//its a special function that is used to run a script for a specific period of time
    {
        yield return new WaitForSeconds(5);//it will return the time for which the script will run
        hasPowerup = false;
        powerUpIndicator.gameObject.SetActive(false);
        
    }
    IEnumerator ultrapowerCountdown()//for the ultrapower ablity i again used ienumertor fucntion to return the time for which we will use that specific ablity
    {
        yield return new WaitForSeconds(5);
        ultraPowerup = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
    IEnumerator delay()//i used this ienumerator to get a delay in my jump
    {
        if (ultraPowerup)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                playerRb.AddForce(Vector3.up * 30, ForceMode.Impulse);
                yield return new WaitForSeconds(0.25f);
                playerRb.AddForce(Vector3.down * 60, ForceMode.Impulse);
                onground = true;
                yield return new WaitForSeconds(0.25f);
                onground = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigid = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided With" + collision.gameObject.name + "with Powerup Set to" + hasPowerup);
            enemyRigid.AddForce(awayfromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
