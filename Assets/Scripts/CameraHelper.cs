using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    public GameObject playerBall;
    public float HorizontalSensitivity = 70f;
    public float VerticalSensitivity = 20f;
    public GameObject drawControlObject;
    HoldController drawControl;

    float pitch = 0;
    float yaw = 0;
    // Start is called before the first frame update
    void Start()
    {
        drawControl = drawControlObject.GetComponent<HoldController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerBall.transform.position;
        drawControlObject.transform.position = Camera.main.WorldToScreenPoint(playerBall.transform.position);
        if (!drawControl.draw)
        {
            foreach (Touch touch in Input.touches)
            {

                //on touch 
                if (touch.phase == TouchPhase.Moved)
                {
                    yaw += touch.deltaPosition.x * HorizontalSensitivity * Time.deltaTime;
                    pitch -= touch.deltaPosition.y * VerticalSensitivity * Time.deltaTime;
                    if (pitch < -40)
                        pitch = -40;
                    if (pitch > 30)
                        pitch = 30;
                    this.transform.eulerAngles = new Vector3(pitch, yaw, 0);
                    //transform.Rotate(0, mD.x * speed * Time.deltaTime, 0);
                }
            }
        }
    }
}
