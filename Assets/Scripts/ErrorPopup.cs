using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CubesGenerator
{
    public class ErrorPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text messagePlace;
        [SerializeField] private Button okButton;

        private void Awake()
        {
            okButton.onClick.AddListener(Hide);
        }

        public void Show(string message)
        {
            messagePlace.text = message;
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}