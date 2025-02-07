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

        // 计算目标位置
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
            // 使用MoveTowards方法移动对象
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 检查是否到达目标位置
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // 精确设置位置，确保完全到达目标
                transform.position = targetPosition;
                transform.GetComponent<Collider2D>().enabled = true;
                transform.GetComponent<MoveToTarget>().enabled = false;
                // 可以在这里添加到达目标后的逻辑
            }
        }
    }
}
