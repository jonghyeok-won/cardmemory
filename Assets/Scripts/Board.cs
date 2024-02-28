using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // ī�� ������ ����
    [SerializeField]
    private GameObject cardPrefab;

    // ������ ��� �� ����
    [SerializeField]
    private int rowCount = 1;
    [SerializeField]
    private int columnCount = 1;

    // ����� ī�� ��������Ʈ �迭
    [SerializeField]
    private Sprite[] cardSprites;

    // ������ ��� ī���� �ε����� �����ϴ� ����Ʈ
    private List<int> cardList = new List<int>();

    // ��ġ�ؾ� �� ���� ����
    public int Pairs { get; private set; }

    private void Awake()
    {
        Pairs = (rowCount * columnCount) / 2;
    }

    void Start()
    {
        // �ʿ��� ī�� �� ����
        GenerateCard(Pairs);

        // ī�� ����Ʈ�� �������� ����
        ShuffleCardList();

        // ���忡 ī�� ��ġ
        InitBoard(rowCount, columnCount);
    }

    // �ʿ��� ī�� �� ����
    private void GenerateCard(int pairsNeeded)
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        while (selectedIndices.Count < pairsNeeded)
        {
            int randomIndex = Random.Range(0, cardSprites.Length);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                cardList.Add(randomIndex); // �ش� ī�� �� �߰�
                cardList.Add(randomIndex); // ������ ī�� �� �߰�
            }
        }
    }

    // ī�� ����Ʈ�� �������� ���� �޼ҵ�
    private void ShuffleCardList()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            int randomIndex = Random.Range(0, cardList.Count);
            int temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
    }

    // ���忡 ī�� ��ġ
    private void InitBoard(int rows, int cols)
    {
        float spaceY = 2.2f; // ī�� ������ ���� ����
        float spaceX = 1.9f; // ī�� ������ ���� ����

        int cardIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // ī���� ��ġ ���
                float posX = (col - (int)(cols / 2)) * spaceX + (spaceX / 2);
                float posY = (row - (int)(rows / 2)) * spaceY - 0.6f;
                Vector3 pos = new Vector3(posX, posY, 0f);

                // ī�� ������ �ν��Ͻ�ȭ �� ����
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardIdx = cardList[cardIndex++];
                card.SetCardIdx(cardIdx);
                card.SetCardSprite(cardSprites[cardIdx]);
            }
        }
    }
}
