using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class SlideWeapon : MonoBehaviour
{
    [Header("Slots & Images")]
    public List<RectTransform> slots;   // 6개의 슬롯
    public List<Image> images;          // 6개의 이미지 (A,B,C × 2)

    [Header("Animation Settings")]

    public float duration = 0.4f;
    public Vector2 centerSize = new Vector2(160, 160);
    public Vector2 sideSize = new Vector2(120, 120);
    public float sideAlpha = 0.5f;
    public float edgeAlpha = 0f;

    [Header("OtherAnim")]
    public Animator anim; 
    
    [Header("Bools")]
    public bool isSeting = false;
    public bool isDot = false;

    [Header("Other int")]
    public int bullindex;                   // 0 = F, 1 = V, 2 = B

    private CanvasGroup[] imageGroups;

    public AmmoManage ammoManage;

    public Debugging debugging;

    public WHelp wHelp;

    void Start()
    {
        anim.enabled = true;
        isDot = false;
        // CanvasGroup 자동 세팅
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

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.I))
        {
            if (ammoManage.isQTEActive) return;
            if (isSeting) return;

            isSeting = true;

            anim.Play("WeaponB");
            wHelp.HelpStart(1);
            StartCoroutine(StartDot());
        }
    }

    // 오른쪽으로 이동
    void ShiftRight()
    {
        isDot = true;
        for (int i = 0; i < images.Count; i++)
        {
            int nextSlotIndex = (i + slots.Count - 1) % slots.Count;

            Image img = images[i];
            CanvasGroup cg = imageGroups[i];

            // 이동 + 크기 변화 + 투명도 변화 동시에
            img.rectTransform.DOAnchorPos(slots[nextSlotIndex].anchoredPosition, duration).SetEase(Ease.OutQuad);
            img.rectTransform.DOSizeDelta(GetTargetSize(nextSlotIndex), duration).SetEase(Ease.OutQuad);
            cg.DOFade(GetTargetAlpha(nextSlotIndex), duration).SetEase(Ease.OutQuad);
        }

        // 리스트 순서 재배열만 나중에 수행
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
            isDot = false;
            anim.enabled = true;
        });
    }

    // 왼쪽으로 이동
    void ShiftLeft()
    {
        isDot = true;
        for (int i = 0; i < images.Count; i++)
        {
            int nextSlotIndex = (i + 1) % slots.Count;

            Image img = images[i];
            CanvasGroup cg = imageGroups[i];

            // 이동 + 크기 변화 + 투명도 변화 동시에
            img.rectTransform.DOAnchorPos(slots[nextSlotIndex].anchoredPosition, duration).SetEase(Ease.OutQuad);
            img.rectTransform.DOSizeDelta(GetTargetSize(nextSlotIndex), duration).SetEase(Ease.OutQuad);
            cg.DOFade(GetTargetAlpha(nextSlotIndex), duration).SetEase(Ease.OutQuad);
        }

        // 리스트 순서 재배열만 나중에 수행
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
            isDot = false;
            anim.enabled = true;
        });
    }

    // 현재 슬롯 상태 반영
    void RefreshCarousel(bool instant)
    {
        for (int slotIndex = 0; slotIndex < slots.Count; slotIndex++)
        {
            Image img = images[slotIndex];
            CanvasGroup cg = imageGroups[slotIndex];

            Vector2 targetSize = GetTargetSize(slotIndex);
            float targetAlpha = GetTargetAlpha(slotIndex);

            if (instant)
            {
                img.rectTransform.anchoredPosition = slots[slotIndex].anchoredPosition;
                img.rectTransform.sizeDelta = targetSize;
                cg.alpha = targetAlpha;
            }
            else
            {
                img.rectTransform.DOAnchorPos(slots[slotIndex].anchoredPosition, duration).SetEase(Ease.OutQuad);
                img.rectTransform.DOSizeDelta(targetSize, duration).SetEase(Ease.OutQuad);
                cg.DOFade(targetAlpha, duration).SetEase(Ease.OutQuad);
            }
        }
    }

    // 슬롯 위치별 크기 반환
    Vector2 GetTargetSize(int slotIndex)
    {
        return slotIndex == 2 ? centerSize : sideSize;
    }

    // 슬롯 위치별 투명도 반환
    float GetTargetAlpha(int slotIndex)
    {
        if (slotIndex == 2) return 1f;
        if (slotIndex == 0 || slotIndex == 4 || slotIndex == 5) return edgeAlpha;
        return sideAlpha;
    }

    IEnumerator StartDot()
    {
        while (isSeting)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isDot)
                {
                    yield return null;
                    continue;
                }
                anim.enabled = false;
                ShiftLeft();
                IndexChange(1);               
            }

            else if (Input.GetKeyDown(KeyCode.I))
            {
                if (isDot)
                {
                    yield return null;
                    continue;
                }                
                anim.enabled = false;
                ShiftRight();
                IndexChange(2);
            }

            else if (Input.GetKeyDown(KeyCode.O))
            {
                ammoManage.CheckBull();
                debugging.BullCheck();
                anim.Play("WeaponS");
                wHelp.HelpStart(2);
                isSeting = false;
                yield break;
            }



            yield return null;
        }


    }

    public void IndexChange(int i)            // 1 = Left,  2 = Right
    {
        switch (i)
        {
            case 1:
                if (bullindex == 0)
                {
                    bullindex = 2;
                    break;
                }
                else
                {
                    bullindex = bullindex - 1;
                    break;
                }
            case 2:
                if (bullindex == 2)
                {
                    bullindex = 0;
                    break;
                }
                else
                {
                    bullindex = bullindex + 1;
                    break;
                }
        }
    }
}