using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject _barWindow;
        [SerializeField] private TMP_Text _interactableText;
        [SerializeField] private Vector2 _offset;

        public void OnAwake()
        {
            WorldManager.StaticInstance.Player.OnInteractableChanged += UpdateUI;
        }

        public void OnSceneChanged()
        {
            WorldManager.StaticInstance.Player.OnInteractableChanged -= UpdateUI;
        }

        private void UpdateUI(Interactable interactable, Vector2 cursorPosition)
        {
            if (interactable != null)
            {
                transform.position = cursorPosition + _offset;
                _barWindow.SetActive(true);
                _interactableText.text = interactable.GetText();
            }
            else
            {
                _barWindow.SetActive(false);
                _interactableText.text = string.Empty;
            }
        }
    }
}