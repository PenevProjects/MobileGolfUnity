using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasierResults : MonoBehaviour
{
    public GameObject playerBall;
    Rigidbody playerRB;
    public float m_magnetSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = playerBall.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == playerBall.transform)
        {
            if (playerRB.velocity.magnitude < 10)
            {
                //in the direction 
                Vector3 heading = (transform.position - other.transform.position);
                Vector3 normalizedDir = heading / heading.magnitude;
                playerRB.AddForce(normalizedDir * m_magnetSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }
}
