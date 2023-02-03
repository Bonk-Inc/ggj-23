using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeLoadingScreen : LoadingScreen
{
    [SerializeField]
    private float openTime = 0.5f;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Canvas canvas;

    public override void OpenLoadingScreen(Action onOpened = null){
        canvas.enabled = true;
        StartCoroutine(LerpScreen(0, 1, () => {
            onOpened?.Invoke();
        }));
    }

    public override void CloseLoadingScreen(Action onClosed = null){
        StartCoroutine(LerpScreen(1, 0, () => {
            canvas.enabled = false;
            onClosed?.Invoke();
        }));
    }

    private IEnumerator LerpScreen(float from, float to, Action done = null){
        float way = 0;
        while (way < 1)
        {
            way += Time.unscaledDeltaTime / openTime;
            float alpha = Mathf.Lerp(from, to, (float)Math.Sin(way * (Math.PI / 2)));
            SetAlpha(alpha);
            yield return null;
        }
        done?.Invoke();
    }

    private void SetAlpha(float a){
        Color c = image.color;
        c.a = a;
        image.color = c;
    }
}
