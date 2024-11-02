using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RandomCharacterSelector : MonoBehaviour
{
    public ScrollRect scrollView; // Referensi komponen ScrollRect
    public List<RectTransform> characterButtons; // List RectTransform dari tombol karakter

    public void OnRandomButtonClick()
    {
        // Pilih salah satu tombol karakter secara acak
        int randomIndex = Random.Range(0, characterButtons.Count);
        RectTransform selectedButton = characterButtons[randomIndex];

        // Hitung posisi normalisasi dari tombol yang dipilih
        float normalizedPosition = (float)randomIndex / (characterButtons.Count - 1);

        // Gulir ke tombol yang dipilih dengan animasi halus
        StartCoroutine(SmoothScrollTo(normalizedPosition));
    }

    private IEnumerator SmoothScrollTo(float targetPosition)
    {
        float duration = 0.5f; // Durasi animasi scroll
        float elapsedTime = 0f;
        float startPosition = scrollView.horizontalNormalizedPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            scrollView.horizontalNormalizedPosition = newPosition;
            yield return null;
        }

        scrollView.horizontalNormalizedPosition = targetPosition;
    }
}
