using UnityEngine;
using System.Collections;

public class InfoPanel : MonoBehaviour
{
    CanvasGroup CanvasGroup = null;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActive()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
