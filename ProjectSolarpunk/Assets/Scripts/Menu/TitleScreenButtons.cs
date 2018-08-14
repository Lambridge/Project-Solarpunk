using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("slashTutorial1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenFeedbackWebpage()
    {
        Application.OpenURL("https://docs.google.com/forms/d/1nXGIgBTNgMqxyRdzhwdXq5RTvB1G0HmKjBHYZEQ_R5o");
    }

}
