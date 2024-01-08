using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 2.0f;
    public bool hasPowerUp;
    public bool hasRockets;
    private float powerUpForce = 15.0f;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public GameObject rocketsPowerUpIndicator;
    public GameObject rocketPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("powerup"))
        {
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCoroutine());
        } else if (other.CompareTag("RocketsPowerUp"))
        {
            hasRockets = true;
            rocketsPowerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(RocketsCoroutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " and power up is set to " + hasPowerUp);
        }
    }

    IEnumerator PowerUpCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    IEnumerator RocketsCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasRockets = false;
        rocketsPowerUpIndicator.SetActive(false);
    }
}
