using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void ShakeCam(float shakeAmount, float duration)
    {
        if (cam == null) return;
        StartCoroutine(ShakeCoroutine(shakeAmount, duration));
    }

    private IEnumerator ShakeCoroutine(float shakeAmount, float duration)
    {
        Vector3 originalPosition = cam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0);

            cam.transform.localPosition += randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Smoothly transition back to the original position
        float smoothTime = 0.5f;
        Vector3 velocity = Vector3.zero;
        Vector3 currentPos = cam.transform.localPosition;

        while (currentPos != originalPosition)
        {
            currentPos = Vector3.SmoothDamp(currentPos, originalPosition, ref velocity, smoothTime);
            cam.transform.localPosition = currentPos;
            yield return null;
        }
    }

    void BlurCam()
    {
        // Implementation for blur effect
    }
}
