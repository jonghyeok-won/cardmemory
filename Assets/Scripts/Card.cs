using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cardRenderer;
    [SerializeField]
    private Sprite basicSprite;
    [SerializeField]
    private Sprite cardSprite;

    private bool isReversed = false;
    private bool isReversing;

    private bool isMatched = false;


    public int cardIdx;
    public void SetCardIdx(int idx)
    {
        cardIdx = idx;
    }

    public void SetMatched()
    { 
        isMatched = true; 
    }

    public void SetCardSprite(Sprite newSprite)
    {
        cardSprite = newSprite;
    }

    public void ReverseCard()
    {
        if (isReversing || isMatched) return; // 이미 뒤집히고 있거나 매치된 경우 무시

        isReversing = true;
        isReversed = !isReversed;

        // 카드를 Y축 기준으로 90도 회전시키며 시작하는 애니메이션
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            // 회전이 90도 완료되면 스프라이트 편경
            cardRenderer.sprite = isReversed ? cardSprite : basicSprite;

            // 스프라이트 변경 후 나머지 90도 회전
            transform.DORotate(new Vector3(0, 0, 0), 0.25f).OnComplete(() =>
            {
                isReversing = false;
            });
        });
    }


    // 마우스 클릭 이벤트 처리
    private void OnMouseDown()
    {
        // 뒤집히고 있지 않고, 매치되지 않았으며, 이미 뒤집히지 않은 상태에서만 처리
        if (!isReversing && !isMatched && !isReversed)
        {
            GameManager.Instance.CardClicked(this);
        }
    }
}
