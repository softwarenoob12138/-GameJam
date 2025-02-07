using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Transport : MonoBehaviour
{
    public TPPoint tp;

    public Transform player;

    private Transform tpPosition;

    public float moveSpeed;

    public bool isMoving = false;

    private void Start()
    {
        tpPosition = transform;
    }

    private void Update()
    {
        if (!isMoving) return;

        // Vector3.Lerp 更为平滑， 这是匀速抵达
        player.position = Vector2.MoveTowards(player.position, tpPosition.position, moveSpeed * Time.deltaTime);

        if ((Vector2.Distance(player.position, tpPosition.position) < 0.1f))
        { 
            player.position = tpPosition.position;
            isMoving = false;

            StartCoroutine(WaitToDrop());

            if (tp == TPPoint.Up)
            {
                if (GameManagerInDream.instance.rope2.gameObject.activeSelf == true)
                {
                    GameManagerInDream.instance.ChangeRopeCord1();
                }
                GameManagerInDream.instance.RotationScaleForward = -.6f;
                GameManagerInDream.instance.RotationScaleBack = .6f;
                SelfDestroy[] components = FindObjectsOfType<SelfDestroy>();

                foreach (SelfDestroy component in components)
                {
                    component.ropeCord = GameManagerInDream.instance.ropeCord;
                }
            }
            else if (tp == TPPoint.Left)
            {
                if (GameManagerInDream.instance.rope2.gameObject.activeSelf == true)
                {
                    GameManagerInDream.instance.ChangeRopeCord1();
                }
                GameManagerInDream.instance.RotationScaleForward = .12f;
                GameManagerInDream.instance.RotationScaleBack = .993f;
                SelfDestroy[] components = FindObjectsOfType<SelfDestroy>();

                foreach (SelfDestroy component in components)
                {
                    component.ropeCord = GameManagerInDream.instance.ropeCord;
                }

            }
            else if (tp == TPPoint.Right)
            {
                if (GameManagerInDream.instance.rope2.gameObject.activeSelf == true)
                {
                    GameManagerInDream.instance.ChangeRopeCord1();
                }
                GameManagerInDream.instance.RotationScaleForward = -.993f;
                GameManagerInDream.instance.RotationScaleBack = -.12f;
                SelfDestroy[] components = FindObjectsOfType<SelfDestroy>();

                foreach (SelfDestroy component in components)
                {
                    component.ropeCord = GameManagerInDream.instance.ropeCord;
                }

            }

            else if (tp == TPPoint.Down)
            {
                if (GameManagerInDream.instance.rope1.gameObject.activeSelf == true)
                {
                    GameManagerInDream.instance.ChangeRopeCord2();
                }
                GameManagerInDream.instance.RotationScaleForward = -.6f;
                GameManagerInDream.instance.RotationScaleBack = .6f;
                SelfDestroy[] components = FindObjectsOfType<SelfDestroy>();

                foreach (SelfDestroy component in components)
                {
                    component.ropeCord = GameManagerInDream.instance.ropeCord;
                }
            }
        }
    }

    private IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(0.5f);

        GameManagerInDream.instance.canDrop = true;
    }

}
