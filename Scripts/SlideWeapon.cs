using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
public class SlideWeapon : MonoBehaviour
{
    [Header("Slots & Images")]
    public List<RectTransform> slots;   // 6개의 슬롯
    public List<Image> images;           // 6개의 이미지 (A,B,C × 2)

    [Header("Animation Settings")]
    public float duration = 0.4f;
    public Vector2 centerSize = new Vector2(160, 160);
    public Vector2 sideSize = new Vector2(120, 120);
    public float sideAlpha = 0.5f;
    public float edgeAlpha = 0f;

    private CanvasGroup[] imageGroups;

    void Start()
    {
        // CanvasGroup 준비
        imageGroups = new CanvasGroup[images.Count];
        for (int i = 0; i < images.Count; i++)
        {
            CanvasGroup cg = images[i].GetComponent<CanvasGroup>();
            if (cg == null)
                cg = images[i].gameObject.AddComponent<CanvasGroup>();
            imageGroups[i] = cg;
        }

        RefreshCarousel(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ShiftRight();
        else if (Input.GetKeyDown(KeyCode.P))
            ShiftLeft();
    }

    void ShiftRight()
    {
        for (int i = 0; i < images.Count; i++)
        {
            int nextSlotIndex = (i + slots.Count - 1) % slots.Count;
            images[i].rectTransform
                .DOAnchorPos(slots[nextSlotIndex].anchoredPosition, duration)
                .SetEase(Ease.OutQuad);
        }

        DOVirtual.DelayedCall(duration, () =>
        {
            Image firstImg = images[0];
            CanvasGroup firstCg = imageGroups[0];

            images.RemoveAt(0);
            images.Add(firstImg);

            CanvasGroup[] tempGroups = new CanvasGroup[imageGroups.Length];
            imageGroups.CopyTo(tempGroups, 0);
            for (int j = 0; j < imageGroups.Length - 1; j++)
                imageGroups[j] = tempGroups[j + 1];
            imageGroups[imageGroups.Length - 1] = tempGroups[0];

            RefreshCarousel(false);
        });
    }

    void ShiftLeft()
    {
        for (int i = 0; i < images.Count; i++)
        {
            int nextSlotIndex = (i + 1) % slots.Count;
            images[i].rectTransform
                .DOAnchorPos(slots[nextSlotIndex].anchoredPosition, duration)
                .SetEase(Ease.OutQuad);
        }

        DOVirtual.DelayedCall(duration, () =>
        {
            Image lastImg = images[images.Count - 1];
            CanvasGroup lastCg = imageGroups[images.Count - 1];

            images.RemoveAt(images.Count - 1);
            images.Insert(0, lastImg);

            CanvasGroup[] tempGroups = new CanvasGroup[imageGroups.Length];
            imageGroups.CopyTo(tempGroups, 0);
            for (int j = 1; j < imageGroups.Length; j++)
                imageGroups[j] = tempGroups[j - 1];
            imageGroups[0] = tempGroups[imageGroups.Length - 1];

            RefreshCarousel(false);
        });
    }

    void RefreshCarousel(bool instant)
    {
        for (int slotIndex = 0; slotIndex < slots.Count; slotIndex++)
        {
            Image img = images[slotIndex];
            CanvasGroup cg = imageGroups[slotIndex];

            Vector2 targetSize = sideSize;
            float targetAlpha = sideAlpha;

            // 슬롯 위치 기준으로 투명도/크기 적용
            if (slotIndex == 2)
            {
                targetSize = centerSize; // 중앙 강조
                targetAlpha = 1f;
            }
            else if (slotIndex == 0 || slotIndex == 4 || slotIndex == 5)
            {
                targetAlpha = edgeAlpha; // 끝단 완전 투명
            }
            else
            {
                targetAlpha = sideAlpha; // 측면
            }

            if (instant)
            {
                img.rectTransform.anchoredPosition = slots[slotIndex].anchoredPosition;
                img.rectTransform.sizeDelta = targetSize;
                cg.alpha = targetAlpha;
            }
            else
            {
                img.rectTransform.DOSizeDelta(targetSize, duration).SetEase(Ease.OutQuad);
                cg.DOFade(targetAlpha, duration).SetEase(Ease.OutQuad);
            }
        }
    }
}