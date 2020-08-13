using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private BoolStorage isPaused;
    
    [Header("References")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSound;
    
    private void Update() {
        /*if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse)) {
            sfxSource.PlayOneShot(clickSound);
        } else**/ if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused.value = !isPaused.value;
            pausePanel.SetActive(isPaused.value);
            Time.timeScale = isPaused.value ? 0 : 1;
        }
    }

    public void Resume() {
        Debug.Log("Resuming game!");
        isPaused.value = false;
        pausePanel.SetActive(isPaused.value);
        Time.timeScale = 1;
    }
    
    public void QuitGame() {
        SceneManager.LoadScene("Menu");
    }
    
}
