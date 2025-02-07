using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    public UI_FadeScreen fadeScreen;

    public GameObject options;

    private void Awake()
    {
        instance = this;
        // �� Awake ʹ֮�� ����Ϸģʽ�йر�ĳ UI ���ܵ��ø� UI �� Awake ���� �� �� skill �ű��з����¼�֮ǰ �� skill tree �Ϸ����¼�
        // ���� �ڷ� ��Ϸģʽ �ر� UI �� �����¼���˳�������
        fadeScreen.gameObject.SetActive(true);
        options = GameObject.FindGameObjectWithTag("Option");
        options.transform.gameObject.SetActive(false);
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null; // �������� fade screen ���ֻ�Ծ״̬

            // ��� ������ UI_FadeScreen ����� ���� ���Ӷ��� (������жϵ�ԭ�򲻱�� ���ǵ��� �� fade screen ���ֻ�Ծ״̬)
            if (!fadeScreen)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame

    public void InOption()
    {
        options.transform.gameObject.SetActive(true);
    }

    public void ExitOption()
    {
        options.transform.gameObject.SetActive(false);
    }
}
