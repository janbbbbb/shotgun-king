using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- add this

public class EndScreenUI : MonoBehaviour
{
    public GameObject panel;
    public Text titleText; // <-- change from TMP to legacy Text

    public void Show(string message)
    {
        titleText.text = message;
        panel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
