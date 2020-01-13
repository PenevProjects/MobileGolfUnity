using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHitter : MonoBehaviour
{
    public float m_hitStrength = 0.4f;
    Rigidbody rb;
    public GameObject cameraGuide;
    public GameObject dragIndicatorObject;
    LineRenderer lr;
    private void Start()
    {
        lr = dragIndicatorObject.GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody>();
    }
    public void GolfHit()
    {
        Vector2 start = dragIndicatorObject.GetComponent<DragIndicator>().m_StartPosOnScreen;
        Vector2 end = dragIndicatorObject.GetComponent<DragIndicator>().m_CurrentPosOnScreen;
        var direction = -(end - start);
        var normalizedDirection = direction / direction.magnitude;
        var hitStrength = Vector2.Distance(start, end) * m_hitStrength;
        //rotate the vector in relation to the rotation of the camera
        Vector3 forceVec = Quaternion.AngleAxis(cameraGuide.transform.localEulerAngles.y, Vector3.up) * new Vector3(normalizedDirection.x * hitStrength, 0, normalizedDirection.y * hitStrength);
        if (lr.enabled)
        {
            //use ForceMode.Impulse
            Debug.Log(start + end + direction);
            rb.AddForce(forceVec, ForceMode.Impulse);
        }
    }
}
