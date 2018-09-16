using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public Text txtPercentage;
    public EventSystem eventSystem;

    public float passTime = 0;

    AsyncOperation loadingTask;
    float start = 0;

    public void onPressQuitButton()
    {
#if UNITY_EDITOR_64
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void onPressPlayButton()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void onPressPlayButtonLoading()
    {
        StartCoroutine(LoadingGameScene());
    }

    public void onPressExitMenuButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    IEnumerator LoadingGameScene() 
    {
        start = Time.realtimeSinceStartup;
        passTime = 0;
        loadingTask = SceneManager.LoadSceneAsync("Scenes/Game");
        loadingTask.allowSceneActivation = false;
        while (loadingTask.isDone || passTime < 2)
        {
            passTime = Time.realtimeSinceStartup - start;
            loadingBar.value = loadingBar.maxValue * (passTime / 2);
            txtPercentage.text = Mathf.Round(loadingBar.value * 100) + "%";
            yield return null;
        }
        loadingTask.allowSceneActivation = true;
    }
}
