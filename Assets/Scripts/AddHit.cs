using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHit : MonoBehaviour
{
    public float m_hitStrength;
    Rigidbody rb;
    public GameObject cameraGuide;
    public GameObject dragIndicatorObject;
    LineRenderer lr;
    static public int hitCount = 0;
    private void Start()
    {
        lr = dragIndicatorObject.GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody>();
    }
    public void GolfHit()
    {
        Vector2 start = dragIndicatorObject.GetComponent<DragIndicator>().m_StartPosOnCameraCanvas;
        Vector2 end = dragIndicatorObject.GetComponent<DragIndicator>().m_CurrentPosOnScreen;
        var direction = -(end - start);
        var normalizedDirection = direction / direction.magnitude;
        var hitStrength = Vector2.Distance(start, end);
        int screenHeight = Screen.height;
        //calculation of hitStrength:
        //screenHeight/18 =~ 1 cm, min value for strength distance
        //screenHeight/18 * 5 =~ 5 cm, max value for strength distance
        // subtract screenHeight/18 so that you we get 0 to X and then add 10 so that we have a minimum strength
        //divide by 4.5f so that it is normalized in range of 0 to 1
        hitStrength = (Mathf.Clamp(hitStrength, screenHeight/18, screenHeight / 18 * 5) - screenHeight/18 + 10) / screenHeight/4.5f;
        hitStrength *= m_hitStrength*1000 * Time.deltaTime;
        //rotate the vector in relation to the rotation of the camera
        Vector3 forceVec = Quaternion.AngleAxis(cameraGuide.transform.localEulerAngles.y, Vector3.up) * new Vector3(normalizedDirection.x * hitStrength, 0, normalizedDirection.y * hitStrength);
        if (lr.enabled)
        {
            //use ForceMode.Impulse
            rb.AddForce(forceVec, ForceMode.Impulse);
            hitCount++;
        }
    }
}
