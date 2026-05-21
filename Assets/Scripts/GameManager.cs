using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int Winner { get; private set; } // 0 = Player 1, 1 = Player 2

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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }

    void CheckSetup()
    {
        // Se não houver botão de start, o jogo apenas inicia automaticamente de forma silenciosa
        if (startButton == null) Debug.Log("GameManager: Início automático ativado (sem botão de Start).");
        
        // Tenta encontrar os textos de score se estiverem nulos
        if (player1ScoreText == null || player2ScoreText == null)
        {
            TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();
            foreach (var t in allTexts)
            {
                if (t.name.Contains("Score1") || t.name.Contains("Player1")) player1ScoreText = t;
                if (t.name.Contains("Score2") || t.name.Contains("Player2")) player2ScoreText = t;
            }
        }

        if (player1ScoreText == null || player2ScoreText == null) Debug.LogError("GameManager: Textos de placar não atribuídos!");
        
        if (audioManager == null) audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogWarning("GameManager: 'Audio Manager' não atribuído! O jogo funcionará, mas sem som.");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (startButton != null) startButton.SetActive(false);
        if (backgroundImage != null) backgroundImage.SetActive(true); 
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }

    public void Player1Point()
    {
        player1Score++;
        UpdateScore();
        if (AudioManager.instance != null) AudioManager.instance.PlayScore();
        CheckWinner();
    }

    public void Player2Point()
    {
        player2Score++;
        UpdateScore();
        if (AudioManager.instance != null) AudioManager.instance.PlayScore();
        CheckWinner();
    }

    void UpdateScore()
    {
        if (player1ScoreText != null) player1ScoreText.text = player1Score.ToString();
        if (player2ScoreText != null) player2ScoreText.text = player2Score.ToString();
    }

    void CheckWinner()
    {
        if (player1Score >= scoreToWin)
        {
            Winner = 0;
            EndGame("Player 1 venceu!");
        }
        else if (player2Score >= scoreToWin)
        {
            Winner = 1;
            EndGame("Player 2 venceu!");
        }
    }

    void EndGame(string message)
    {
        if (winnerText != null) winnerText.text = message;
        if (winnerPanel != null) winnerPanel.SetActive(true);
        if (restartButton != null) restartButton.SetActive(false);
        if (startButton != null) startButton.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopBackgroundMusic();
            AudioManager.instance.PlayVictory();
        }

        StartCoroutine(AutoRestartCoroutine());
    }

    System.Collections.IEnumerator AutoRestartCoroutine()
    {
        yield return new WaitForSecondsRealtime(victoryDelay);
        
        // Em vez de reiniciar a cena, vamos para a cena de vitória se ela existir
        SceneManager.LoadScene("Victory");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}