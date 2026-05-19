using UnityEngine;
using UnityEngine.SceneManagement; // Biblioteca necessária para mudar de cena

public class MenuManager : MonoBehaviour
{
    // Método que será chamado ao clicar no botão "Jogar"
    public void Jogar()
    {
        SceneManager.LoadScene("Jogo"); // Nome exato da sua cena de jogo
    }

    // Método que será chamado ao clicar no botão "Sair"
    public void Sair()
    {
        Application.Quit();
        Debug.Log("O jogo foi fechado!"); // Aparece apenas no console do Unity
    }
}
