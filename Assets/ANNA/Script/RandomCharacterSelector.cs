using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomCharacterSelector : MonoBehaviour
{
    public Button[] characterButtons;   // Array tombol karakter di Scroll View
    public Button randomButton;         // Tombol Random
    public ScrollRect scrollRect;       // Komponen Scroll Rect dari Scroll View
    public float spinDuration = 1f;     // Durasi spin
    public int spinCount = 10;           // Jumlah putaran sebelum berhenti
    public float spinSpeed = 0.1f;       // Kecepatan jeda di antara pemilihan tombol saat spin

    private void Start()
    {
        randomButton.onClick.AddListener(() => StartCoroutine(RandomSelect()));
    }

    private IEnumerator RandomSelect()
    {
        // Lakukan spin
        for (int i = 0; i < spinCount; i++)
        {
            // Pilih indeks acak
            int randomIndex = Random.Range(0, characterButtons.Length);
            Button tempButton = characterButtons[randomIndex];

            // Scroll ke tombol yang dipilih
            yield return ScrollToButton(tempButton);

            // Delay untuk memberikan kesan "spin"
            yield return new WaitForSeconds(spinSpeed);
        }

        // Pilih indeks akhir secara acak
        int finalIndex = Random.Range(0, characterButtons.Length);
        Button finalButton = characterButtons[finalIndex];

        // Scroll ke tombol yang terpilih secara final
        yield return ScrollToButton(finalButton);

        // Klik tombol yang terpilih
        finalButton.onClick.Invoke();
    }

    private IEnumerator ScrollToButton(Button targetButton)
    {
        RectTransform targetButtonRect = targetButton.GetComponent<RectTransform>();
        RectTransform contentRect = scrollRect.content;

        // Hitung posisi target untuk Scroll View
        float targetPosition = Mathf.Clamp01(
            (targetButtonRect.anchoredPosition.x + (targetButtonRect.rect.width / 2)) / contentRect.rect.width
        );

        // Lakukan smooth scrolling
        float elapsedTime = 0f;
        float startPosition = scrollRect.horizontalNormalizedPosition;

        // Mendeteksi posisi tengah dan scrolling
        float middleOffset = 0.5f; // Offset untuk menempatkan di tengah
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / (spinDuration / spinCount);
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition - middleOffset, elapsedTime);
            yield return null;
        }

        // Pastikan posisi scroll tepat di posisi akhir
        scrollRect.horizontalNormalizedPosition = targetPosition - middleOffset;
    }
}
