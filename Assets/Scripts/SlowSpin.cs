using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpin : MonoBehaviour
{
    public GameObject UICircle;
    private void FixedUpdate()
    {
        if (this.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            UICircle.SetActive(true);
        }
        else
            UICircle.SetActive(false);
    }
}
