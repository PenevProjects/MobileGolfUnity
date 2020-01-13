using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldController : MonoBehaviour
{
    public bool draw = false;
    public int currentFinger;
    public bool fingerSet = false;
    RectTransform rt;
    public GameObject playerBall;
    public int hitCount = 0;
    public bool m_tutorial = true;
    public GameObject tutorialArrow;
    public GameObject tutorialText;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    public void onHold()
    {
        if (m_tutorial)
        {
            StartCoroutine(Delay());
        }
        if (!fingerSet)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchInRect = rt.InverseTransformPoint(touch.position);
                if (rt.rect.Contains(touchInRect))
                {
                    currentFinger = touch.fingerId;
                    fingerSet = true;
                    draw = true;
                }
            }
        }
    }
    public void onRelease()
    {
        playerBall.GetComponent<AddHit>().GolfHit();
        if (tutorialArrow.GetComponent<RawImage>().enabled)
            tutorialArrow.GetComponent<RawImage>().enabled = false;
        if (tutorialText.GetComponent<Text>().enabled)
            tutorialText.GetComponent<Text>().enabled = false;
            foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == currentFinger)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    draw = false;
                    fingerSet = false;
                }
            }
        }
    }
    IEnumerator Delay()
    {
        tutorialArrow.GetComponent<RawImage>().enabled = true;
        tutorialText.GetComponent<Text>().enabled = true;
        m_tutorial = false;
        yield return new WaitForSeconds(2);
        tutorialArrow.GetComponent<RawImage>().enabled = false;
        tutorialText.GetComponent<Text>().enabled = false;
    }
}
