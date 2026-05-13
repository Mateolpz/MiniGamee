using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BotonJugar()
    {
        SceneManager.LoadScene(1); // índice de tu escena de juego
        // O por nombre: SceneManager.LoadScene("NombreDeTuEscena");
    }
}