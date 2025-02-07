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
        // 用 Awake 使之在 非游戏模式中关闭某 UI 后能调用该 UI 的 Awake 方法 来 在 skill 脚本中分配事件之前 在 skill tree 上分配事件
        // 避免 在非 游戏模式 关闭 UI 后 分配事件的顺序出问题
        fadeScreen.gameObject.SetActive(true);
        options = GameObject.FindGameObjectWithTag("Option");
        options.transform.gameObject.SetActive(false);
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null; // 用它来让 fade screen 保持活跃状态

            // 如果 不存在 UI_FadeScreen 组件就 禁用 该子对象 (加这个判断的原因不必深究 就是单纯 让 fade screen 保持活跃状态)
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
