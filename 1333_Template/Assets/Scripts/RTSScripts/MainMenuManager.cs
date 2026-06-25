using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private SlidePanelScript _settingsPanel;

    public void PlayGame()
    {
        // Loads next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        _settingsPanel.Open();
    }

    public void CloseSettings()
    {
        _settingsPanel.Close();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Pressed");
        Application.Quit();
    }
}