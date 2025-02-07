using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelfDestroy2 : MonoBehaviour
{
    public Transform ropeCord;
    private SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ropeCord = GameObject.FindGameObjectWithTag("¹³×Ó2").transform;
    }
    void Update()
    {
        OnDestroyNull();
        DestroyDropItem();
    }

    private void OnDestroyNull()
    {
        if (sr.sprite == null)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyDropItem()
    {
        if (GameManagerInDream2.instance.GetComponent<GameManagerInDream2>().GetState == State.Rock && transform.parent == ropeCord)
        {

            Destroy(gameObject);
            CreatePrefabs2.instance.CreateDropItemPrefab();
            InventoryManagerInDream2.instance.equipmentSlot.sprite = sr.sprite;
            StartCoroutine(WaitToDrop());

        }
    }

    private IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(0.5f);

        GameManagerInDream2.instance.canDrop = true;

    }

}
