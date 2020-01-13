using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public static float timeElapsed = 0;
    Text theText;
    // Start is called before the first frame update
    void Start()
    {
        theText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        theText.text = "Time: " + Mathf.FloorToInt((timeElapsed%3600)/60).ToString("00") + ":" + Mathf.FloorToInt(timeElapsed%60).ToString("00");
    }
}
