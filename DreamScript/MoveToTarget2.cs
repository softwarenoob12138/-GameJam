using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToTarget2 : MonoBehaviour
{
    public static MoveToTarget2 instance;
    public Vector3 target;
    private bool hasReachedTarget;
    public float moveSpeed;
    private Vector3 targetPosition;
    public SpriteRenderer sr;
    public Sprite changeSr;

    public GameObject GouZi;

    private void Awake()
    {
        transform.GetComponent<Collider2D>().enabled = false;
        sr = GetComponent<SpriteRenderer>();
        changeSr = InventoryManagerInDream2.instance.equipmentSlot.sprite;
        sr.sprite = changeSr;

        instance = this;

        GouZi = GameObject.FindGameObjectWithTag("����2");
    }


    private void Start()
    {
        
        target = GouZi.GetComponent<CheckObject>().oldPosition;
        
        // ����Ŀ��λ��
        targetPosition = target;
        
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
                transform.GetComponent<MoveToTarget2>().enabled = false;
                // ������������ӵ���Ŀ�����߼�
            }
        }
    }
}
