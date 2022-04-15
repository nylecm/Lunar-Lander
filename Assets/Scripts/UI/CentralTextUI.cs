using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CentralTextUI : MonoBehaviour
{
    private TextMeshProUGUI _centralText;

    private void OnEnable()
    {
        Player.OnLanded += UpdateText;
    }

    private void OnDisable()
    {
        Player.OnLanded -= UpdateText;
    }

    private void Awake()
    {
        _centralText = GetComponent<TextMeshProUGUI>();
    }

    private async void UpdateText(CentreMessage msg)
    {
        _centralText.text = msg.ToString();
        await Task.Delay(2000);
        _centralText.text = "";
        if (msg.SceneToLoad != null) SceneManager.LoadScene(msg.SceneToLoad);
    }
}
