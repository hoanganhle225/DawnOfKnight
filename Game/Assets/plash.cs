using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInAndOut : MonoBehaviour
{
    public Image imageToFade;
    public float fadeDuration = 1.0f; // Thời gian hiệu ứng fade (giây)
    public int fadeSteps = 10; // Số bước fade (số lần thay đổi alpha)

    private void OnEnable()
    {
        // Bắt đầu coroutine khi bắt đầu Scene
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float alphaStep = 1.0f / (float)fadeSteps; // Giá trị thay đổi alpha cho mỗi bước
        float targetAlpha = 0f; // Giá trị alpha cuối cùng của hình ảnh

        while (true)
        {
            // Hiệu ứng fade out từ rõ đến mờ
            for (int i = 0; i <= fadeSteps; i++)
            {
                targetAlpha = 1.0f - (i * alphaStep);
                SetImageAlpha(targetAlpha);

                // Chờ một khoảng thời gian giữa các bước
                yield return new WaitForSeconds(fadeDuration / (float)fadeSteps);
            }

            // Hiệu ứng fade in từ mờ đến rõ
            for (int i = fadeSteps; i >= 0; i--)
            {
                targetAlpha = 1.0f - (i * alphaStep);
                SetImageAlpha(targetAlpha);

                // Chờ một khoảng thời gian giữa các bước
                yield return new WaitForSeconds(fadeDuration / (float)fadeSteps);
            }
        }
    }

    private void SetImageAlpha(float alpha)
    {
        Color currentColor = imageToFade.color;
        currentColor.a = alpha;
        imageToFade.color = currentColor;
    }
}