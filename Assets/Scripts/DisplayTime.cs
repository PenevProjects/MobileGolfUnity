using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = "Time: " + Mathf.FloorToInt((TimeCounter.timeElapsed % 3600) / 60).ToString("00") + ":" + Mathf.FloorToInt(TimeCounter.timeElapsed % 60).ToString("00");
    }
}
