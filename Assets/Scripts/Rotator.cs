using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
    public float speed = 2f;
    RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();

    }
    // Update is called once per frame
    void Update () 
	{
        rt.transform.Rotate(0, 0, speed * Time.deltaTime);
	}
}
