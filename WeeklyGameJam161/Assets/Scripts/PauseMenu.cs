using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject pausePanel;

    private bool paused;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pausePanel.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
        }
    }

    public void Resume() {
        paused = false;
        pausePanel.SetActive(paused);
        Time.timeScale = 1;
    }
    
}
