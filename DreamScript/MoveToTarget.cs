using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToTarget : MonoBehaviour
{
    public static MoveToTarget instance;
    public Vector3 target;
    private bool hasReachedTarget;
    public float moveSpeed;
    private Vector3 targetPosition;
    public SpriteRenderer sr;
    public Sprite changeSr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        changeSr = InventoryManagerInDream.instance.equipmentSlot.sprite;
        sr.sprite = changeSr;

        instance = this;
    }


    private void Start()
    {
        CheckObject[] checkObjects = FindObjectsOfType<CheckObject>();

        foreach(CheckObject checkObject in checkObjects)
        {
            target = checkObject.oldPosition;
        }

        // ����Ŀ��λ��
        targetPosition = target;

        transform.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (target != null)
        {
            // ʹ��MoveTowards�����ƶ�����
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ����Ƿ񵽴�Ŀ��λ��
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // ��ȷ����λ�ã�ȷ����ȫ����Ŀ��
                transform.position = targetPosition;
                transform.GetComponent<Collider2D>().enabled = true;
                transform.GetComponent<MoveToTarget>().enabled = false;
                // ������������ӵ���Ŀ�����߼�
            }
        }
    }
}
