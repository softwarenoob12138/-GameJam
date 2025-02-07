using System.Collections;
using UnityEngine;

public enum State
{
    Rock, // ҡ��
    Stretch, //����
    Shorten //����
}

public enum TPPoint
{
    Up,
    Down,
    Left,
    Right
}
public class GameManagerInDream : MonoBehaviour
{
    public static GameManagerInDream instance;
    private State state;
    private Vector3 dir;

    public Transform rope1;
    public Transform ropeCord1;

    public Transform rope2;
    public Transform ropeCord2;

    public Transform rope;
    public Transform ropeCord;

    private float StartropeCordScaleY;
    public float ropeSpeed;
    public float ropeLength;

    private float length;

    public float RotationScaleForward;
    public float RotationScaleBack;

    public bool canDrop = true;

    public bool fadeOut;

    public bool isGoodEnding;

    public State GetState
    {
        set { state = value; } // ������˽�б��� ��ÿ�д�� һ�ֲ���
        get { return state; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {


        state = State.Rock;

        // Vector3.back ����˳ʱ����ת   forward ������ʱ����ת   (���� Z ��)
        dir = Vector3.back;

        length = 1;

        rope = rope1;
        ropeCord = ropeCord1;

        StartropeCordScaleY = ropeCord.localScale.y;

        // SetActive �� gameObject �ķ���
        rope2.transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (state == State.Rock)
        {
            Rock();
            if (Input.GetKeyDown(KeyCode.Space) && canDrop)
            {
                state = State.Stretch;
                canDrop = false;
            }
        }
        else if (state == State.Stretch)
        {

            Stretch();
        }
        else if (state == State.Shorten)
        {
            Shorten();
        }
    }


    // Up �� -0.6 �� 0.6
    // Down �� ��һ�� ץ�� -0.6 �� 0.6
    // Left �� 0.12 �� 0.993
    // Right �� -0.993 �� -0.12
    private void Rock()
    {

        if (rope.localRotation.z <= RotationScaleForward) // -0.5f �� -60��
        {
            dir = Vector3.forward;
        }
        else if (rope.localRotation.z >= RotationScaleBack)
        {
            dir = Vector3.back;
        }
        rope.Rotate(dir * 60 * Time.deltaTime);
    }

    private void Stretch()
    {
        if (length >= ropeLength)
        {
            state = State.Shorten;
            return;
        }

        length += Time.deltaTime * ropeSpeed;
        rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);

        // ������ Y ���ϵ� Scale �Ŵ���٣������� Y ���ϵ� Scale ����Ӧ�س��Զ���
        ropeCord.localScale = new Vector3(ropeCord.localScale.x, StartropeCordScaleY / length, ropeCord.localScale.z);



    }
    private void Shorten()
    {
        if (length <= 1)
        {
            length = 1;
            state = State.Rock;

            ropeCord.GetComponent<Collider2D>().enabled = true;

            StartCoroutine(WaitToDrop());

            return;
        }
        length -= Time.deltaTime * ropeSpeed;
        rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);

        // ������ Y ���ϵ� Scale �Ŵ���٣������� Y ���ϵ� Scale ����Ӧ�س��Զ���, ����֤�����岻�ᱻ����
        ropeCord.localScale = new Vector3(ropeCord.localScale.x, StartropeCordScaleY / length, ropeCord.localScale.z);
    }

    public void ChangeRopeCord2()
    {
        rope2.transform.gameObject.SetActive(true);
        rope = rope2;
        ropeCord = ropeCord2;
        rope1.transform.gameObject.SetActive(false);
        StartropeCordScaleY = ropeCord.localScale.y;
    }

    public void ChangeRopeCord1()
    {
        rope1.transform.gameObject.SetActive(true);
        rope = rope1;
        ropeCord = ropeCord1;
        rope2.transform.gameObject.SetActive(false);
        StartropeCordScaleY = ropeCord.localScale.y;
    }

    private IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(0.5f);

        canDrop = true;
    }

    public void ExitDreamScene()
    {
        if (fadeOut == false)
        {
            fadeOut = true;
            StartCoroutine(ExitDreamSceneWithFadeEffect(1.5f));
        }
    }

    public IEnumerator ExitDreamSceneWithFadeEffect(float _delay)
    {
        UI.instance.fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        // ʹ�� LoadScene ����ǰȷ�������� UnityEngine.SceneManagement �����ռ�
        // �÷��� λ�� UnityEngine.SceneManagement �����ռ��µ� SceneManager ����
        UI.instance.fadeScreen.FadeIn();

        GameManager.instance.mainGameScene.SetActive(true);

        yield return new WaitForSeconds(.1f);

        GameManager.instance.dreamScene.SetActive(false);

        fadeOut = false;
    }

    private void OnDisable()
    {
        GameManager.instance.isGoodEnding = isGoodEnding;
    }

}
