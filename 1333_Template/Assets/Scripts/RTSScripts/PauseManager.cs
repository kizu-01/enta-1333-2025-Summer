using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private SlidePanelScript _pausePanel;
    private bool _isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        _pausePanel.Close();
        Time.timeScale = 1f; // Unpause game time
        _isPaused = false;
    }

    public void Pause()
    {
        _pausePanel.Open();
        Time.timeScale = 0f; // Freeze game time
        _isPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before switching scenes
        SceneManager.LoadScene("RTS_Demo");
    }
}