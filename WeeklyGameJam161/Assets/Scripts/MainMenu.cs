using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioMixer musicGroup;
    [SerializeField] private AudioMixer sfxGroup;
    
    
    private Resolution[] resolutions;


    private void Start() {
        toggle.isOn = Screen.fullScreen;

        dropdown.ClearOptions();
        resolutions = Screen.resolutions;
        dropdown.AddOptions(resolutions.Select(resolution => resolution.ToString()).ToList());

        for (int i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].Equals(Screen.currentResolution)) {
                dropdown.value = i;
                break;
            }
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse)) {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    public void PlayGame() {
        //TODO add some form of animation when switching scenes
        StartCoroutine(FadeOutMusic(1));
        StartCoroutine(WaitAndStartGame());
    }

    private IEnumerator WaitAndStartGame() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene");
    }
    
    private IEnumerator FadeOutMusic (float fadeTime) {
        float startVolume = musicSource.volume;
 
        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
 
        musicSource.Stop ();
        musicSource.volume = startVolume;
    }

    public void OpenOptionsMenu() {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OptionsBackButton() {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SetFullScreen() {
        Screen.fullScreen = toggle.isOn;
    }

    public void UpdateMusicVolume(float volume) {
        musicGroup.SetFloat("musicVolume", volume);
    }

    public void UpdateSFXVolume(float volume) {
        musicGroup.SetFloat("sfxVolume", volume);
    }

    public void UpdateResolution(int resolutionIndex) {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void QuitGame() {
        Application.Quit();
    }
    
}
