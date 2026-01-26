using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameDev");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
