using System.Collections;
using DG.Tweening;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject ui_Camera;

    public GameObject stageWindow; 

    public CanvasGroup canvasGroup; // ���� �� ����

    [SerializeField] private float fadeSpeed;


    private void OnTriggerEnter(Collider other)
    {
        stageWindow.SetActive(true);
        ui_Camera.SetActive(true);

        StartCoroutine(Fade(true));
    }

    private void OnTriggerExit(Collider other)
    {
        ui_Camera.SetActive(true);

        StartCoroutine(Fade(false));
    }

    #region Main Methods
    private IEnumerator Fade(bool isFadeIn)
    {
        float startAlpha = isFadeIn ? 0f : 1f; // ���̵� �� 
        float endAlpha = isFadeIn ? 1f : 0f; // ���̵� �ƿ�
        float timer = 0f;
     
        while (timer < 1f)
        {
            timer += Time.unscaledDeltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer); // ���̵� ó��
            yield return null;
        }

        // ���̵尡 ������ â�� ���� ���� ��Ȱ��ȭ
        if (!isFadeIn)
        {
            stageWindow.SetActive(false);
        }
    }
    #endregion
}
