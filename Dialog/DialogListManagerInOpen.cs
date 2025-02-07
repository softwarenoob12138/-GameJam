using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogListManagerInOpen : MonoBehaviour
{
    [SerializeField] private GameObject dialogItemPrefab; // 对话项预制体
    [SerializeField] private Transform contentContainer; // Content对象的Transform
    [SerializeField] private Text currentDialogText; // 当前显示的对话文本
    [SerializeField] private float itemSpacing = 10f; // 对话项之间的间距
    public UI_FadeScreen fadeScreen;

    private List<GameObject> dialogItems = new(); // 存储所有对话项
    private Queue<string> dialogQueue = new();

    public GameObject mainGameScene;
    public GameObject dialogSceneInOpen;

    public bool fadeOut;

    private bool isTyping = false; // 新增标志位，用于判断当前是否正在进行打字动画

    private void Awake()
    {
        mainGameScene = GameObject.FindGameObjectWithTag("MainGameScene");
        dialogSceneInOpen = GameObject.FindGameObjectWithTag("DialogSceneInOpen");
        InitializeDialogQueue();
    }

    private void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogQueue.Count > 0 && !isTyping)
            {
                TriggerDialog();
            }
            else if (dialogQueue.Count == 0 && !fadeOut)
            {
                fadeOut = true;
                StartCoroutine(LoadSceneWithFadeEffect(1.5f));
            }
            else if (isTyping)
            {
                // 如果正在打字，直接显示完整对话文本
                SkipTyping();
            }
        }
    }

    // 新增函数，用于跳过打字动画，直接显示完整对话文本
    private void SkipTyping()
    {
        if (dialogItems.Count > 0)
        {
            Text dialogText = dialogItems[dialogItems.Count - 1].GetComponentInChildren<Text>();
            if (dialogText != null)
            {
                dialogText.text = dialogQueue.Peek(); // 获取当前对话队列中的完整文本并显示
            }
            isTyping = false;
        }
    }

    //private void LoadDialogs()
    //{
    //    string[] dialogs = new string[] { "你好", "世界", "欢迎来到Unity" }; // 模拟的对话数据
    //
    //    foreach (string dialog in dialogs)
    //    {
    //        CreateDialogItem(dialog);
    //    }
    //}

    private void CreateDialogItem(string text)
    {
        GameObject item = Instantiate(dialogItemPrefab, contentContainer);
        Text dialogText = item.GetComponentInChildren<Text>();
        if (dialogText != null)
        {
            dialogText.text = ""; // 初始化为空文本，用于后续打字效果
            StartCoroutine(TypeText(dialogText, text)); // 启动协程来实现打字效果
        }
        dialogItems.Add(item);

        // 调整所有对话项的位置
        AdjustDialogItemsPosition();
    }

    private IEnumerator TypeText(Text targetText, string fullText)
    {
        isTyping = true;
        int index = 0;
        while (index < fullText.Length)
        {
            targetText.text += fullText[index];
            index++;
            yield return new WaitForSeconds(0.05f); // 控制打字速度，可根据实际效果调整时间间隔

            // 每次更新文本后检测鼠标点击，如果点击则直接显示完整文本
            if (Input.GetMouseButtonDown(0))
            {
                targetText.text = fullText;
                yield break; // 直接结束协程，完成文本显示
            }
        }
        isTyping = false;
    }

    private void AdjustDialogItemsPosition()
    {
        if (dialogItems.Count == 0)
        {
            return; // 如果没有对话项，直接返回，无需调整位置
        }

        // 记录当前对话框（最新添加的对话项）的固定垂直位置，可按需调整
        float currentDialogYPosition = -200f;
        for (int i = 0; i < dialogItems.Count; i++)
        {
            RectTransform rectTransform = dialogItems[i].GetComponent<RectTransform>();

            // 除了最新添加的对话项（当前对话框），其他对话项都要上移
            if (i < dialogItems.Count - 1)
            {
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    rectTransform.anchoredPosition.y + rectTransform.rect.height + itemSpacing
                );
            }
            else
            {
                // 当前对话框（最新添加的对话项）保持固定的垂直位置
                rectTransform.anchoredPosition = new Vector2(10f, currentDialogYPosition);
            }
        }

        // 调整Scroll View的垂直滚动范围，确保始终滚动到最底部（最新对话所在位置）
        ScrollRect scrollRect = contentContainer.GetComponentInParent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0;
        }
    }

    private void InitializeDialogQueue()
    {
        string[] dialogs = new string[] { "猫头鹰：你好啊，年轻的小羊！", "老板：你...你好，请问你是？", "猫头鹰：我是捕梦网旅店的老板，或者说前任老板", "老板：什么叫前任老板？", "猫头鹰：因为我要有别的事去了，这家旅店就交给你经营了。现在你才是老板", "(猫头鹰一振翅便飞走了，留下了茫然的你以及这间捕梦网旅店)" };
        foreach (string dialog in dialogs)
        {
            dialogQueue.Enqueue(dialog);
        }
    }

    private void TriggerDialog()
    {
        string newDialog = dialogQueue.Dequeue();
        CreateDialogItem(newDialog);
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        mainGameScene.SetActive(true);

        ShopManager.instance.shop.SetActive(false);

        GoldManager.instance.GetPrimaryGold();

        GameManager.instance.StartDayOne();

        GameManager.instance.dialog.SetActive(false);
        CustomerCheck.instance.transform.position = new Vector3(CustomerCheck.instance.transform.position.x - 5, CustomerCheck.instance.transform.position.y, 0);
        GameManager.instance.ServeNext = true;

        dialogSceneInOpen.SetActive(false);

        fadeOut = false;

        GetNeed.instance.gameStart = true;

        fadeScreen.FadeIn();


    }
}
