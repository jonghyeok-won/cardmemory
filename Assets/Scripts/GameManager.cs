using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance;
    // 게임 보드 참조
    public Board board;

    // 게임의 상태와 타이머 관련 변수들
    [Header("Game State")]
    private bool canClick = false; 
    private bool isGameOver = false;
    private float currentTime;
    [SerializeField] private float timeLimit = 60f;

    // 카드 관련 변수들
    [Header("Card Interaction")]
    private Card reversedCard; // 현재 뒤집힌 카드를 추적

    [Header("UI Components")]
    [SerializeField] private Slider timeSlider; 
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    // 패널 관련 변수들
    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject startPanel;

    // 카운트다운 및 결과 점수 표시 관련 컴포넌트
    [Header("Countdown and Score Effects")]
    [SerializeField] private CountingEffect overResultScore;
    [SerializeField] private CountingEffect clearResultScore; 
    [SerializeField] private TextMeshProUGUI countDownText;


    // 점수와 매치 카운트 관련 변수들
    [Header("Score and Matches")]
    [SerializeField] private int matchScore = 5; 
    private int score = 0;
    private int matchCount = 0;
    private int pairs;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        pairs = board.Pairs;
        currentTime = timeLimit;
        SetCurrentTimeText();
        StartCoroutine(StartCountDown());
        UpdateScoreText();
    }

    private IEnumerator StartCountDown()
    {
        canClick = true;
        startPanel.SetActive(true); 
        int count = 3; 

        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--; 
        }

        countDownText.text = "Go!";


        yield return new WaitForSeconds(1f);

        canClick = false;
        StartCoroutine("CountDownTimer");
        startPanel.SetActive(false);
    }


    public void CardClicked(Card card)
    {
        if(canClick || isGameOver)
        {
            return;
        }

        card.ReverseCard();

        if(reversedCard == null )
        {
            reversedCard = card;
        }
        else
        {
            //매치 체크
            StartCoroutine(CheckMatch(reversedCard, card));
        }
    }

    private void SetCurrentTimeText()
    {
        string timeFormatted = currentTime.ToString("F2");
        timeText.SetText($"Time: {timeFormatted}");
    }


    IEnumerator CheckMatch(Card card1, Card card2)
    {
        canClick = true;

        if(card1.cardIdx == card2.cardIdx)
        {
            card1.SetMatched();
            card2.SetMatched();

            ScoreManager.Instance.AddScore(matchScore);
            UpdateScoreText();
            matchCount++;

            if(matchCount == pairs)
            {
                GameOver(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            card1.ReverseCard();
            card2.ReverseCard();

            //yield return new WaitForSeconds(0.2f);
        }

        canClick = false;
        reversedCard = null;
    }

    private void UpdateScoreText()
    {
        scoreText.SetText($"{ScoreManager.Instance.Score}");
    }


    IEnumerator CountDownTimer()
    {
        while ( currentTime > 0 )
        {
            currentTime -= Time.deltaTime;
            timeSlider.value = currentTime / timeLimit;
            SetCurrentTimeText();
            yield return null;
        }


        GameOver(false);
    }

    private void GameOver(bool success)
    {
        if(!isGameOver)
        {
            isGameOver = true;

            StopAllCoroutines();
            if (success)
            {
                gameClearPanel.SetActive(true);
                clearResultScore.Play(0, ScoreManager.Instance.Score);
                Debug.Log("success");
            }
            else
            {
                gameOverPanel.SetActive(true);
                overResultScore.Play(0, ScoreManager.Instance.Score);
                Debug.Log("fail");
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        canClick = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; 
        canClick = false;
        pausePanel.SetActive(false);
    }
}
