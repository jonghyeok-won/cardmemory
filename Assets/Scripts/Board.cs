using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // 카드 프리팹 참조
    [SerializeField]
    private GameObject cardPrefab;

    // 보드의 행과 열 설정
    [SerializeField]
    private int rowCount = 1;
    [SerializeField]
    private int columnCount = 1;

    // 사용할 카드 스프라이트 배열
    [SerializeField]
    private Sprite[] cardSprites;

    // 생성된 모든 카드의 인덱스를 저장하는 리스트
    private List<int> cardList = new List<int>();

    // 매치해야 할 쌍의 개수
    public int Pairs { get; private set; }

    private void Awake()
    {
        Pairs = (rowCount * columnCount) / 2;
    }

    void Start()
    {
        // 필요한 카드 쌍 생성
        GenerateCard(Pairs);

        // 카드 리스트를 무작위로 섞음
        ShuffleCardList();

        // 보드에 카드 배치
        InitBoard(rowCount, columnCount);
    }

    // 필요한 카드 쌍 생성
    private void GenerateCard(int pairsNeeded)
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        while (selectedIndices.Count < pairsNeeded)
        {
            int randomIndex = Random.Range(0, cardSprites.Length);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                cardList.Add(randomIndex); // 해당 카드 쌍 추가
                cardList.Add(randomIndex); // 동일한 카드 쌍 추가
            }
        }
    }

    // 카드 리스트를 무작위로 섞는 메소드
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

    // 보드에 카드 배치
    private void InitBoard(int rows, int cols)
    {
        float spaceY = 2.2f; // 카드 사이의 세로 간격
        float spaceX = 1.9f; // 카드 사이의 가로 간격

        int cardIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // 카드의 위치 계산
                float posX = (col - (int)(cols / 2)) * spaceX + (spaceX / 2);
                float posY = (row - (int)(rows / 2)) * spaceY - 0.6f;
                Vector3 pos = new Vector3(posX, posY, 0f);

                // 카드 프리팹 인스턴스화 및 설정
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardIdx = cardList[cardIndex++];
                card.SetCardIdx(cardIdx);
                card.SetCardSprite(cardSprites[cardIdx]);
            }
        }
    }
}
