using System.Collections;
using UnityEngine;

public enum State
{
    Rock, // 摇摆
    Stretch, //拉伸
    Shorten //缩短
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
        set { state = value; } // 这是让私有变量 变得可写的 一种操作
        get { return state; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {


        state = State.Rock;

        // Vector3.back 就是顺时针旋转   forward 就是逆时针旋转   (按照 Z 轴)
        dir = Vector3.back;

        length = 1;

        rope = rope1;
        ropeCord = ropeCord1;

        StartropeCordScaleY = ropeCord.localScale.y;

        // SetActive 是 gameObject 的方法
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


    // Up 是 -0.6 和 0.6
    // Down 是 另一条 抓钩 -0.6 和 0.6
    // Left 是 0.12 和 0.993
    // Right 是 -0.993 和 -0.12
    private void Rock()
    {

        if (rope.localRotation.z <= RotationScaleForward) // -0.5f 是 -60°
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

        // 父物体 Y 轴上的 Scale 放大多少，子物体 Y 轴上的 Scale 就相应地除以多少
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

        // 父物体 Y 轴上的 Scale 放大多少，子物体 Y 轴上的 Scale 就相应地除以多少, 来保证子物体不会被拉伸
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

        // 使用 LoadScene 方法前确定导入了 UnityEngine.SceneManagement 命名空间
        // 该方法 位于 UnityEngine.SceneManagement 命名空间下的 SceneManager 类中
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
