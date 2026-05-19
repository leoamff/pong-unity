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
    public AudioManager audioManager;
    public float victoryDelay = 3f;
    public int scoreToWin = 5;

    void Start()
    {
        CheckSetup();
        
        // Define o tempo do jogo como ativo imediatamente
        Time.timeScale = 1f;

        UpdateScore();

        if (startButton != null) startButton.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        if (winnerPanel != null) winnerPanel.SetActive(false);
        if (backgroundImage != null) backgroundImage.SetActive(true);

        // Toca a música de fundo se o audioManager estiver presente
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic();
        }
    }

    void CheckSetup()
    {
        // Se não houver botão de start, o jogo apenas inicia automaticamente de forma silenciosa
        if (startButton == null) Debug.Log("GameManager: Início automático ativado (sem botão de Start).");
        if (player1ScoreText == null || player2ScoreText == null) Debug.LogError("GameManager: Textos de placar não atribuídos!");
        if (audioManager == null) Debug.LogWarning("GameManager: 'Audio Manager' não atribuído! O jogo funcionará, mas sem som.");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        startButton.SetActive(false);
        if (backgroundImage != null) backgroundImage.SetActive(true); // Garante que continue visível
        
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic();
        }
    }

    public void Player1Point()
    {
        player1Score++;
        UpdateScore();
        if (audioManager != null) audioManager.PlayScore();
        CheckWinner();
    }

    public void Player2Point()
    {
        player2Score++;
        UpdateScore();
        if (audioManager != null) audioManager.PlayScore();
        CheckWinner();
    }

    void UpdateScore()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    void CheckWinner()
    {
        if (player1Score >= scoreToWin)
        {
            EndGame("Player 1 venceu!");
        }
        else if (player2Score >= scoreToWin)
        {
            EndGame("Player 2 venceu!");
        }
    }

    void EndGame(string message)
    {
        // Em vez de pausar o tempo imediatamente, deixamos o jogo rodar um pouco 
        // ou apenas mostramos o painel de vitória.
        
        winnerText.text = message;
        winnerPanel.SetActive(true);
        if (restartButton != null) restartButton.SetActive(false); // Escondemos o botão pois será automático
        startButton.SetActive(false);

        if (audioManager != null)
        {
            audioManager.StopBackgroundMusic();
            audioManager.PlayVictory();
        }

        StartCoroutine(AutoRestartCoroutine());
    }

    System.Collections.IEnumerator AutoRestartCoroutine()
    {
        // Espera alguns segundos antes de reiniciar
        yield return new WaitForSecondsRealtime(victoryDelay);
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}