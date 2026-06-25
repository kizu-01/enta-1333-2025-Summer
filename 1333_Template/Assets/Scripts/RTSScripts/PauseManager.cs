using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private SlidePanelScript _pausePanel;
    [SerializeField] private CameraController _cameraController;
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
        Time.timeScale = 1f;
        _isPaused = false;

        // Unlock camera when resuming
        if (_cameraController != null) _cameraController.IsMovementLocked = false;
    }

    public void Pause()
    {
        _pausePanel.Open();
        Time.timeScale = 0f;
        _isPaused = true;

        // Lock camera when paused
        if (_cameraController != null) _cameraController.IsMovementLocked = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before switching scenes
        SceneManager.LoadScene("RTS_Demo");
    }
}