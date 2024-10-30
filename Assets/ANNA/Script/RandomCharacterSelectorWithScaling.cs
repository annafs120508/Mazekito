using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomCharacterSelectorWithScaling : MonoBehaviour
{
    public Button[] characterButtons;   // Array tombol karakter di Scroll View
    public Button randomButton;         // Tombol Random
    public ScrollRect scrollRect;       // Komponen Scroll Rect dari Scroll View
    public float spinDuration = 1f;     // Durasi perpindahan antar tombol
    public int spinCount = 10;          // Jumlah putaran sebelum berhenti
    public float spinSpeed = 0.1f;      // Kecepatan jeda di antara pemilihan tombol saat spin

    public float maxScale = 1.5f;       // Skala terbesar di tengah
    public float midScale = 1.2f;       // Skala sedang di sekitar tengah
    public float minScale = 0.8f;       // Skala terkecil di sisi
    public float scaleSpeed = 10f;      // Kecepatan perubahan skala
    public float centerThreshold = 100f; // Zona "hampir tengah" di mana button mulai membesar

    private void Start()
    {
        randomButton.onClick.AddListener(() => StartCoroutine(RandomSelect()));
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    private IEnumerator RandomSelect()
    {
        // Proses acak awal dengan spin
        for (int i = 0; i < spinCount; i++)
        {
            // Pilih tombol acak setiap kali berputar
            int randomIndex = Random.Range(0, characterButtons.Length);
            Button tempButton = characterButtons[randomIndex];

            // Scroll ke tombol acak sementara untuk efek spin
            yield return ScrollToButton(tempButton);

            // Delay untuk memberikan kesan "spin"
            yield return new WaitForSeconds(spinSpeed);
        }

        // Pilih tombol akhir secara acak
        int finalIndex = Random.Range(0, characterButtons.Length);
        Button finalButton = characterButtons[finalIndex];

        // Scroll ke tombol akhir yang terpilih
        yield return ScrollToButton(finalButton);

        // Klik tombol yang terpilih
        finalButton.onClick.Invoke();
    }

    private IEnumerator ScrollToButton(Button targetButton)
    {
        RectTransform targetButtonRect = targetButton.GetComponent<RectTransform>();
        RectTransform contentRect = scrollRect.content;

        // Hitung posisi target untuk Scroll View agar tombol berada di tengah
        float targetPosition = Mathf.Clamp01(
            (targetButtonRect.anchoredPosition.x + (targetButtonRect.rect.width / 2)) / contentRect.rect.width
        );

        // Lakukan smooth scrolling dengan memulai dari posisi scroll saat ini
        float elapsedTime = 0f;
        float startPosition = scrollRect.horizontalNormalizedPosition;

        // Offset untuk memastikan tombol berada di tengah
        float middleOffset = 0.5f;

        // Loop untuk menggerakkan scroll secara mulus ke posisi target
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * spinDuration;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition - middleOffset, elapsedTime);

            // Pastikan scroll view tidak melampaui target
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition);

            yield return null;
        }

        // Pastikan posisi akhir tepat di posisi target setelah selesai scrolling
        scrollRect.horizontalNormalizedPosition = targetPosition - middleOffset;
        UpdateButtonScales();
    }

    private void OnScroll(Vector2 position)
    {
        UpdateButtonScales();
    }

    private void UpdateButtonScales()
    {
        // Posisi tengah dari viewport ScrollRect
        float centerPosition = scrollRect.viewport.rect.width / 2;

        // Loop melalui semua button di dalam content
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform button = scrollRect.content.GetChild(i).GetComponent<RectTransform>();

            if (button != null)
            {
                // Posisi button relatif terhadap Scroll View
                float distanceFromCenter = Mathf.Abs(centerPosition - (button.localPosition.x + scrollRect.content.anchoredPosition.x));

                // Atur skala berdasarkan jarak dari tengah menggunakan beberapa threshold
                float scale;
                if (distanceFromCenter < centerThreshold) // Zona di dekat tengah
                {
                    // Jarak sangat dekat dengan tengah
                    scale = Mathf.Lerp(maxScale, midScale, distanceFromCenter / centerThreshold);
                }
                else
                {
                    // Di luar zona tengah
                    scale = Mathf.Lerp(midScale, minScale, (distanceFromCenter - centerThreshold) / centerPosition);
                }

                // Terapkan skala ke button
                button.localScale = Vector3.Lerp(button.localScale, new Vector3(scale, scale, 1), Time.deltaTime * scaleSpeed);
            }
        }
    }
}
