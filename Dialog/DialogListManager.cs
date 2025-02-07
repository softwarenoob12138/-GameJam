using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogListManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogItemPrefab; // �Ի���Ԥ����
    [SerializeField] private Transform contentContainer; // Content�����Transform
    [SerializeField] private Text currentDialogText; // ��ǰ��ʾ�ĶԻ��ı�
    [SerializeField] private float itemSpacing = 10f; // �Ի���֮��ļ��
    public UI_FadeScreen fadeScreen;

    private List<GameObject> dialogItems = new(); // �洢���жԻ���
    private Queue<string> dialogQueue = new();

    public GameObject mainGameScene;
    public GameObject dialogScene;

    public bool fadeOut;

    private bool isTyping = false; // ������־λ�������жϵ�ǰ�Ƿ����ڽ��д��ֶ���

    private void Awake()
    {
        mainGameScene = GameObject.FindGameObjectWithTag("MainGameScene");
        dialogScene = GameObject.FindGameObjectWithTag("DialogScene");
        InitializeDialogQueue();
    }

    private void Update()
    {
        // ������������
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
                // ������ڴ��֣�ֱ����ʾ�����Ի��ı�
                SkipTyping();
            }
        }
    }

    // ���������������������ֶ�����ֱ����ʾ�����Ի��ı�
    private void SkipTyping()
    {
        if (dialogItems.Count > 0)
        {
            Text dialogText = dialogItems[dialogItems.Count - 1].GetComponentInChildren<Text>();
            if (dialogText != null)
            {
                dialogText.text = dialogQueue.Peek(); // ��ȡ��ǰ�Ի������е������ı�����ʾ
            }
            isTyping = false;
        }
    }

    //private void LoadDialogs()
    //{
    //    string[] dialogs = new string[] { "���", "����", "��ӭ����Unity" }; // ģ��ĶԻ�����
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
            dialogText.text = ""; // ��ʼ��Ϊ���ı������ں�������Ч��
            StartCoroutine(TypeText(dialogText, text)); // ����Э����ʵ�ִ���Ч��
        }
        dialogItems.Add(item);

        // �������жԻ����λ��
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
            yield return new WaitForSeconds(0.05f); // ���ƴ����ٶȣ��ɸ���ʵ��Ч������ʱ����

            // ÿ�θ����ı��������������������ֱ����ʾ�����ı�
            if (Input.GetMouseButtonDown(0))
            {
                targetText.text = fullText;
                yield break; // ֱ�ӽ���Э�̣�����ı���ʾ
            }
        }
        isTyping = false;
    }

    private void AdjustDialogItemsPosition()
    {
        if (dialogItems.Count == 0)
        {
            return; // ���û�жԻ��ֱ�ӷ��أ��������λ��
        }

        // ��¼��ǰ�Ի���������ӵĶԻ���Ĺ̶���ֱλ�ã��ɰ������
        float currentDialogYPosition = -200f;
        for (int i = 0; i < dialogItems.Count; i++)
        {
            RectTransform rectTransform = dialogItems[i].GetComponent<RectTransform>();

            // ����������ӵĶԻ����ǰ�Ի��򣩣������Ի��Ҫ����
            if (i < dialogItems.Count - 1)
            {
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    rectTransform.anchoredPosition.y + rectTransform.rect.height + itemSpacing
                );
            }
            else
            {
                // ��ǰ�Ի���������ӵĶԻ�����̶ֹ��Ĵ�ֱλ��
                rectTransform.anchoredPosition = new Vector2(10f, currentDialogYPosition);
            }
        }

        // ����Scroll View�Ĵ�ֱ������Χ��ȷ��ʼ�չ�������ײ������¶Ի�����λ�ã�
        ScrollRect scrollRect = contentContainer.GetComponentInParent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0;
        }
    }

    private void InitializeDialogQueue()
    {
        string[] dialogs = new string[] { "(һֻ�������������Ϣ�������������������ϵ�����������ɭ����������С�����ֵĶ��ӣ���Ȼ��Ҳ��һ����ɫ��С�����֡���������������Щ��̫����)",
            "���ܣ��ϰ���ã�����Ҫס��һ������һ����ͨ�ķ���ͺ�",
            "�ϰ壺�ǿɲ��У�����������������ķ�˿�����൱ϲ�����С���٣��һ���㰲����õķ���",
            "���ܣ������ı����Թ�һ˿Ц�ݣ���ȴ��תΪ�ǳ�������Ǻðɣ�ϣ�������������Σ��Ͼ���������������õ�԰�" };
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

        GameManager.instance.dialog.SetActive(false);
        CustomerCheck.instance.transform.position = new Vector3(CustomerCheck.instance.transform.position.x - 5, CustomerCheck.instance.transform.position.y, 0);
        GameManager.instance.ServeNext = true;

        dialogScene.SetActive(false);

        fadeOut = false;

        fadeScreen.FadeIn();


    }
}
