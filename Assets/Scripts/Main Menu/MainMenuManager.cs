using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Title Screen")]
        [SerializeField] private GameObject _titleScreenWindow;
        [SerializeField] private Button _titleScreenButtonStartGame;
        [Header("Main Menu")]
        [SerializeField] private GameObject _mainMenuWindow;
        [SerializeField] private Button _mainMenuButtonNewGame;
        [SerializeField] private Button _mainMenuButtonSettings;
        [SerializeField] private Button _mainMenuButtonQuitGame;
        [Header("Settings")]
        [SerializeField] private GameObject _settingsWindow;
        [SerializeField] private Button _settingsButtonBackToMainMenu;

        private void Awake()
        {
            _titleScreenWindow.SetActive(true);
            _titleScreenButtonStartGame.Select();
            _titleScreenButtonStartGame.onClick.AddListener(OnTitleScreenButtonStartGamePressed);
            _mainMenuButtonNewGame.onClick.AddListener(OnMainMenuButtonNewGamePressed);
            _mainMenuButtonSettings.onClick.AddListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.AddListener(OnMainMenuButtonQuitGamePressed);
            _settingsButtonBackToMainMenu.onClick.AddListener(OnSettingsButtonBackToMainMenuPressed);
        }

        private void OnDestroy()// required???
        {
            _titleScreenButtonStartGame.onClick.RemoveListener(OnTitleScreenButtonStartGamePressed);
            _mainMenuButtonNewGame.onClick.RemoveListener(OnMainMenuButtonNewGamePressed);
            _mainMenuButtonSettings.onClick.RemoveListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.RemoveListener(OnMainMenuButtonQuitGamePressed);
            _settingsButtonBackToMainMenu.onClick.RemoveListener(OnSettingsButtonBackToMainMenuPressed);
        }

        private void OnTitleScreenButtonStartGamePressed()
        {
            _titleScreenWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonNewGame.Select();
        }

        private void OnMainMenuButtonNewGamePressed()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        private void OnMainMenuButtonSettingsPressed()
        {
            _mainMenuWindow.SetActive(false);
            _settingsWindow.SetActive(true);
            _settingsButtonBackToMainMenu.Select();
        }

        private void OnMainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }

        private void OnSettingsButtonBackToMainMenuPressed()
        {
            _settingsWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonSettings.Select();
        }
    }
}