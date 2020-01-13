using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragIndicator : MonoBehaviour
{
    LineRenderer lr;
    public Vector3 m_StartPosOnCameraCanvas;
    public Vector2 m_StartPosOnScreen;
    public Vector3 m_CurrentPosOnScreen;
    public Canvas screenCanvas;
    public GameObject lineHelperForDistance;
    public GameObject drawControlObject;
    HoldController drawControl;
    public GameObject playerBall;
    RectTransform drawControlRT;
    public Vector3 m_StartDistancePlayer;
    public float m_Distance;
    Vector3 m_originalDrawControlObjectLocalScale;
    float CircleSize;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        drawControl = drawControlObject.GetComponent<HoldController>();
        drawControlRT = drawControlObject.GetComponent<RectTransform>();
        m_originalDrawControlObjectLocalScale = drawControlObject.transform.localScale;
        CircleSize = m_originalDrawControlObjectLocalScale.x * drawControlRT.rect.width;
    }
    void Update()
    {
        if (!drawControl.draw)
        {
            EndDrawingLine();
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == drawControl.currentFinger)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        m_StartDistancePlayer = GetTouchPosition(touch, true);
                        m_StartPosOnCameraCanvas = Camera.main.WorldToScreenPoint(playerBall.transform.position);
                        m_StartPosOnScreen = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        m_CurrentPosOnScreen = touch.position;
                        //multiply by 3 because distance is often way less than circle size
                        m_Distance = Vector2.Distance(m_StartPosOnCameraCanvas, m_CurrentPosOnScreen)*6;
                        //distance clamped has to be max 1.7 so we accomodate for it
                        float distanceClamped = (Mathf.Clamp(m_Distance, 0, CircleSize * 1.7f) / CircleSize);
                        //1 unit deadzone for dragging
                        if (Vector2.Distance(m_StartPosOnScreen, m_CurrentPosOnScreen) > 5)
                        {
                            //use the original scale to apply a scale the bigger the distance is
                            drawControlObject.transform.localScale = new Vector3(
                                m_originalDrawControlObjectLocalScale.x - distanceClamped,
                                m_originalDrawControlObjectLocalScale.y - distanceClamped,
                                m_originalDrawControlObjectLocalScale.z - distanceClamped);
                            //stop rendering circle if it is smaller than ball
                            if (drawControlObject.transform.localScale.x < m_originalDrawControlObjectLocalScale.x - 1.5f)
                            {
                                drawControlObject.GetComponent<RawImage>().enabled = false;
                            }
                            //else render it
                            else
                            {
                                drawControlObject.GetComponent<RawImage>().enabled = true;
                            }
                            //if distance from ball to current pos is > than current circle size AND
                            if (!drawControlObject.GetComponent<RawImage>().enabled)
                            {
                                PreviewLine(touch);
                            }
                            else
                                lr.enabled = false;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        EndDrawingLine();
                    }
                }
            }
        }
    }
    /*Get the position of the touch in relation to the canvas, which is in Screen Space - Camera mode*/
    private Vector3 GetTouchPosition(Touch _touch, bool _ofPlayer)
    {
        RectTransform canvasRT;
        canvasRT = screenCanvas.GetComponent<RectTransform>();
        Vector2 outpos;
        Vector2 screenPoint;
        if (_ofPlayer)
        {
            screenPoint = Camera.main.WorldToScreenPoint(playerBall.transform.position);
        }
        else
            screenPoint = _touch.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRT, screenPoint, Camera.main, out outpos);
        lineHelperForDistance.transform.position = outpos;
        lineHelperForDistance.transform.position = canvasRT.transform.TransformPoint(lineHelperForDistance.transform.position);
        return lineHelperForDistance.transform.position;
    }
    private void PreviewLine(Touch _touch)
    {
        lr.SetPositions(new Vector3[] { m_StartDistancePlayer, GetTouchPosition(_touch, false) });
        lr.enabled = true;
    }
    private void EndDrawingLine()
    {
        drawControlObject.transform.localScale = m_originalDrawControlObjectLocalScale;
        drawControlObject.GetComponent<RawImage>().enabled = true;
        lr.enabled = false;
    }
}
