using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] UI_FadeScreen fadeScreen;
    public GameObject options;

    private void Start()
    {
        options = GameObject.FindGameObjectWithTag("Option");
        options.transform.gameObject.SetActive(false);
        // ע��һ���ص��������������������ʱ����
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


    public void NewGame()
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
        SceneManager.LoadScene(sceneName);
    }

    public void InOption()
    {
        options.transform.gameObject.SetActive(true);
    }

    public void ExitOption()
    {
        options.transform.gameObject.SetActive(false);
    }
}
