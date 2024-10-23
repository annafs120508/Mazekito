using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButtonScaler : MonoBehaviour
{
    public ScrollRect scrollRect;   // Drag ScrollRect dari Inspector
    public RectTransform content;   // Drag Content dari Inspector
    public float maxScale = 1.5f;   // Skala terbesar di tengah
    public float midScale = 1.2f;   // Skala sedang di sekitar tengah
    public float minScale = 0.8f;   // Skala terkecil di sisi
    public float scaleSpeed = 10f;  // Kecepatan perubahan skala
    public float centerThreshold = 100f;  // Zona "hampir tengah" di mana button mulai membesar (ubah sesuai kebutuhan)

    void Start()
    {
        // Tambahkan listener untuk event scroll
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    void OnScroll(Vector2 position)
    {
        UpdateButtonScales();
    }

    void UpdateButtonScales()
    {
        // Posisi tengah dari viewport ScrollRect
        float centerPosition = scrollRect.viewport.rect.width / 2;

        // Loop melalui semua button di dalam content
        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform button = content.GetChild(i).GetComponent<RectTransform>();

            if (button != null)
            {
                // Posisi button relatif terhadap Scroll View
                float distanceFromCenter = Mathf.Abs(centerPosition - (button.localPosition.x + content.anchoredPosition.x));

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
