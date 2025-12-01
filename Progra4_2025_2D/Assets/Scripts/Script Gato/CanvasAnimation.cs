using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] GameObject[] objectsOnPanel;
    [SerializeField] TMPro.TextMeshProUGUI textPoint;
    [SerializeField] float tiempoTotal;
    [SerializeField] float enableObjectsTime = 0.05f;
    [SerializeField] AnimationCurve animationCurve;
    bool isAnim = false;


    private void Start()
    {
        //StartCoroutine(ShowPointsCoroutine(111));
        StartCoroutine(AnimPanelCoroutineConCurva());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isAnim) return;
            StartCoroutine(AnimPanelCoroutineConCurva());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isAnim) return;
            StartCoroutine(AnimPanelCoroutine(false));
        }
    }

    public IEnumerator ShowPointsCoroutine(int points)
    {
        yield return AnimPanelCoroutine(true);
        float suma = points / 10;
        int currentPoints = 0;
        for (int i = 0; i < 10; i++)
        {
            currentPoints += (int)suma;

            textPoint.text = currentPoints.ToString();

            textPoint.transform.localScale = Vector3.one * 1.3f;
            yield return new WaitForSeconds(enableObjectsTime);
            textPoint.transform.localScale = Vector3.one;
            //Agregar sonido
            yield return new WaitForSeconds(enableObjectsTime);
        }

        textPoint.text = points.ToString();
    }

    public IEnumerator AnimPanelCoroutineOpen()
    {

        bool isOnAnim = true;
        Vector2 starPos = rectTransform.anchoredPosition;
        Vector2 endPos = Vector2.zero;
        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, lerp);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
    }

    public IEnumerator AnimPanelCoroutineConCurva()
    {

        bool isOnAnim = true;
        Vector2 starPos = rectTransform.anchoredPosition;
        Vector2 endPos = Vector2.zero;
        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            float animValue = animationCurve.Evaluate(lerp);
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, animValue);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
    }

    public IEnumerator AnimPanelCoroutineClose()
    {
        bool isOnAnim = true;
        Vector2 starPos = Vector2.zero;
        Vector2 endPos = new Vector2(rectTransform.rect.width, rectTransform.anchoredPosition.y);
        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, lerp);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
    }

    public IEnumerator AnimPanelCoroutine(bool isEnable)
    {
        isAnim = true;
        bool isOnAnim = true;
        Vector2 starPos = rectTransform.anchoredPosition;
        Vector2 endPos = Vector2.zero;

        if (!isEnable)
        {
            endPos = new Vector2(rectTransform.rect.width, rectTransform.anchoredPosition.y);
            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(false);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }

        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, lerp);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
        isAnim = false;

        if (isEnable)
        {
            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }
    }

    public IEnumerator OpenPanelGeneric(AnimSide side, bool isEnable)
    {
        isAnim = true;
        bool isOnAnim = true;
        Vector2 starPos = rectTransform.anchoredPosition;
        Vector2 endPos = Vector2.zero;

        if (!isEnable)
        {
            endPos = new Vector2(rectTransform.rect.width, rectTransform.anchoredPosition.y);


            switch (side)
            {
                case AnimSide.Left:
                    endPos = new Vector2(-rectTransform.rect.width, rectTransform.anchoredPosition.y);
                    break;
                case AnimSide.Right:

                    break;
                case AnimSide.Top:
                    endPos = new Vector2(rectTransform.anchoredPosition.x, rectTransform.rect.height);
                    break;
                case AnimSide.Bottom:
                    endPos = new Vector2(rectTransform.anchoredPosition.x, -rectTransform.rect.height);
                    break;
                default:
                    break;
            }

            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(false);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }

        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, lerp);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
        isAnim = false;

        if (isEnable)
        {
            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }
    }
}

public enum AnimSide
{
    Left,
    Right,
    Top,
    Bottom,
}
