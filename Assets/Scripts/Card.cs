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
        if (isReversing || isMatched) return; // �̹� �������� �ְų� ��ġ�� ��� ����

        isReversing = true;
        isReversed = !isReversed;

        // ī�带 Y�� �������� 90�� ȸ����Ű�� �����ϴ� �ִϸ��̼�
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            // ȸ���� 90�� �Ϸ�Ǹ� ��������Ʈ ���
            cardRenderer.sprite = isReversed ? cardSprite : basicSprite;

            // ��������Ʈ ���� �� ������ 90�� ȸ��
            transform.DORotate(new Vector3(0, 0, 0), 0.25f).OnComplete(() =>
            {
                isReversing = false;
            });
        });
    }


    // ���콺 Ŭ�� �̺�Ʈ ó��
    private void OnMouseDown()
    {
        // �������� ���� �ʰ�, ��ġ���� �ʾ�����, �̹� �������� ���� ���¿����� ó��
        if (!isReversing && !isMatched && !isReversed)
        {
            GameManager.Instance.CardClicked(this);
        }
    }
}
