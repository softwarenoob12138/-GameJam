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

        // ʹ�� LoadScene ����ǰȷ�������� UnityEngine.SceneManagement �����ռ�
        // �÷��� λ�� UnityEngine.SceneManagement �����ռ��µ� SceneManager ����
        SceneManager.LoadScene(sceneName);
    }




}
