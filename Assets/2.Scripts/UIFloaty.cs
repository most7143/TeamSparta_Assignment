using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIFloaty : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public RectTransform Rect;
    public float AliveTime;

    private Vector3 offset;

    public void Spawn(string value, Vector3 position)
    {
        Text.SetText(value);
        StartCoroutine(ProcessAlive());

        offset = new Vector3(0, 1f, 0);

        Rect.anchoredPosition = Camera.main.WorldToScreenPoint(position + offset);

        Rect.DOMoveY(Rect.anchoredPosition.y + 30f, 0.5f).SetEase(Ease.Linear);
    }

    private IEnumerator ProcessAlive()
    {
        yield return new WaitForSeconds(AliveTime);
        gameObject.SetActive(false);
    }
}