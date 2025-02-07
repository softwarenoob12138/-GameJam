using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thanks : MonoBehaviour
{
    [SerializeField] UI_FadeScreen fadeScreen;

    public bool canExitGame;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // �ص����������ǻص������� Scene ���һ���÷�
    {
        fadeScreen.FadeIn();
    }

     
    private void OnDestroy()
    {
        // ����¼�����������ֹ�ڴ�й©
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PointToExitGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR  // �ڱ༭��ģʽ�˳�
        UnityEditor.EditorApplication.isPlaying = false;
#else  // �������˳�
        Application.Quit();
#endif
    }
    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        // ʹ�� LoadScene ����ǰȷ�������� UnityEngine.SceneManagement �����ռ�
        // �÷��� λ�� UnityEngine.SceneManagement �����ռ��µ� SceneManager ����
        ExitGame();
    }
}
