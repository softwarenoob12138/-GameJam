using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Options : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainMenuScene";
    [SerializeField] private UI_FadeScreen fadeScreen;

    public bool isFade;

    void Start()
    {

    }

    public void SwitchToMainMenu()
    {
        if (isFade == false)
        {
            isFade = true;
            StartCoroutine(LoadMainMenuWithFadeEffect(1.5f));
        }
    }

    IEnumerator LoadMainMenuWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        // 使用 LoadScene 方法前确定导入了 UnityEngine.SceneManagement 命名空间
        // 该方法 位于 UnityEngine.SceneManagement 命名空间下的 SceneManager 类中
        SceneManager.LoadScene(sceneName);
    }




}
