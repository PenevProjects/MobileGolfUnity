using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    Vector3 originalScale;
    public void Start()
    {
        originalScale = gameObject.transform.localScale;
    }
    public void scaleDownClick()
    {
        gameObject.transform.localScale *= 0.95f;
    }
    public void returnToNormalScale()
    {
        gameObject.transform.localScale = originalScale;
    }
}
