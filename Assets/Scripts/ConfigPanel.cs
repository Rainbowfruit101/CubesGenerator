using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CubesGenerator
{
    public class ConfigPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField speedInputField;
        [SerializeField] private TMP_InputField distanceInputField;
        [SerializeField] private TMP_InputField spawnDelayInputField;
        [SerializeField] private Button startPauseButton;
        [SerializeField] private ErrorPopup errorPopup;
        [SerializeField] private CubeGenerator cubeGenerator;

        private bool _isStarted;

        private void Awake()
        {
            startPauseButton.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            if (cubeGenerator.IsEnabled)
            {
                cubeGenerator.StopGenerate();
                return;
            }

            var configData = new ConfigData();

            if (!Validate(speedInputField.text, "Speed is not valid", out var speed))
            {
                return;
            }

            configData.Speed = speed;

            if (!Validate(distanceInputField.text, "Distance is not valid", out var distance))
            {
                return;
            }

            configData.Distance = distance;

            if (!Validate(spawnDelayInputField.text, "Spawn delay is not valid", out var spawnDelay))
            {
                return;
            }

            configData.SpawnDelay = spawnDelay;

            cubeGenerator.StartGenerate(configData);
        }

        private bool Validate(string value, string errorMessage, out float result)
        {
            if (float.TryParse(value.Replace('.', ','), out result))
            {
                return true;
            }

            errorPopup.Show(errorMessage);
            return false;
        }
    }
}