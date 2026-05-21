using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia a tela de vitória. Exibe o painel correto (Player 1 ou Player 2) e contém
/// botões de navegação para o menu principal ou tela de créditos.
/// </summary>
[AddComponentMenu("Pong/Victory Manager")]
public class VictoryManager : MonoBehaviour
{
    // ---- PAINÉIS DE VITÓRIA (arraste no Inspector) ----
    [Header("Painéis de vitória")]
    [SerializeField] private GameObject leftPanel;   // Player 1 (lado esquerdo)
    [SerializeField] private GameObject rightPanel;  // Player 2 (lado direito)

    private void Awake()
    {
        // Começamos com ambos invisíveis; o Start() decide qual mostrar
        if (leftPanel != null) leftPanel.SetActive(false);
        if (rightPanel != null) rightPanel.SetActive(false);
    }

    private void Start()
    {
        // GameManager já tem a variável estática Winner (0 = Player1, 1 = Player2)
        int winner = GameManager.Winner;
        if (leftPanel != null) leftPanel.SetActive(winner == 0);
        if (rightPanel != null) rightPanel.SetActive(winner == 1);
    }

    // ==== BOTÕES DE NAVEGAÇÃO ====
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
