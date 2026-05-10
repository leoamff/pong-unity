using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public GameObject startButton;
    public GameObject restartButton;
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;
    public GameObject backgroundImage;

    void Start()
    {
        Time.timeScale = 0f;

        UpdateScore();

        startButton.SetActive(true);
        restartButton.SetActive(false);
        winnerPanel.SetActive(false);
        backgroundImage.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        startButton.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void Player1Point()
    {
        player1Score++;
        UpdateScore();
        CheckWinner();
    }

    public void Player2Point()
    {
        player2Score++;
        UpdateScore();
        CheckWinner();
    }

    void UpdateScore()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    void CheckWinner()
    {
        if (player1Score >= 5)
        {
            EndGame("Player 1 venceu!");
        }
        else if (player2Score >= 5)
        {
            EndGame("Player 2 venceu!");
        }
    }

    void EndGame(string message)
    {
        Time.timeScale = 0f;

        winnerText.text = message;
        winnerPanel.SetActive(true);
        restartButton.SetActive(true);
        startButton.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}