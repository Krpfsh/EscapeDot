using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private bool hasGameFinished;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreTextFinal;
    [SerializeField] private TMP_Text _bestScoreTextFinal;

    private float score;
    private float scoreSpeed;
    private int currentLevel;

    [SerializeField] private List<int> _levelSpeed, _levelMax;
    [SerializeField] private GameObject _gameOverObj;
    private void Awake()
    {
        GameManager.Instance.IsInitialized = true;

        score = 0;
        currentLevel = 0;
        _scoreText.text = ((int)score).ToString();

        scoreSpeed = _levelSpeed[currentLevel];
    }

    private void Update()
    {
        if (hasGameFinished) return;

        score += scoreSpeed * Time.deltaTime;

        _scoreText.text = ((int)score).ToString();

        if (score > _levelMax[Mathf.Clamp(currentLevel, 0, _levelMax.Count - 1)])
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, _levelMax.Count - 1);
            scoreSpeed = _levelSpeed[currentLevel];
        }
    }

    public void GameEnded()
    {
        hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)score;

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        if (highScore < currentScore)
        {
            GameManager.Instance.HighScore = currentScore;
        }
        _scoreTextFinal.text = GameManager.Instance.CurrentScore.ToString();
        _bestScoreTextFinal.text = GameManager.Instance.HighScore.ToString();
        StartCoroutine(GameOver());
    }


    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        _gameOverObj.SetActive(true);
    }
    public void RestartButton()
    {
        GameManager.Instance.GotoGameplay();
    }















}
