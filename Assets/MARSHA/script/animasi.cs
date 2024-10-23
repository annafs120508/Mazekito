using DG.Tweening;
using UnityEngine;

public class animasi : MonoBehaviour
{
    [SerializeField] RectTransform panelRect;
    [SerializeField] float tweenDuration = 0.5f;  // Durasi animasi dalam detik
    [SerializeField] Vector2 startPos = new Vector2(0, 400); // Posisi awal di luar layar (di atas)
    [SerializeField] Vector2 endPos = Vector2.zero; // Posisi akhir di tengah layar
    [SerializeField] Vector2 hidePos = new Vector2(0, -400); // Posisi keluar di luar layar (di bawah)

    private void Start()
    {
        // Set initial position
        panelRect.anchoredPosition = startPos;
    }

    public void ShowPanel()
    {
        // Reset position
        panelRect.anchoredPosition = startPos;

        // Animate panel to move down into the center
        panelRect.DOAnchorPos(endPos, tweenDuration).SetEase(Ease.OutBack);
    }

    public void HidePanel()
    {
        // Animate panel to move down out of the screen
        panelRect.DOAnchorPos(hidePos, tweenDuration).SetEase(Ease.InBack);
    }
}
