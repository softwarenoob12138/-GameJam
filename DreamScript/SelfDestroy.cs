using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelfDestroy : MonoBehaviour
{
    public Transform ropeCord;
    private SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ropeCord = GameObject.FindGameObjectWithTag("¹³×Ó").transform;
    }
    void Update()
    {
        OnDestroyNull();
        DestroyDropItem();
    }

    private void OnDestroyNull()
    {
        if(sr.sprite == null)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyDropItem()
    {
        if (GameManagerInDream.instance.GetComponent<GameManagerInDream>().GetState == State.Rock && transform.parent == ropeCord )
        {
            
            Destroy(gameObject);
            CreatePrefabs.instance.CreateDropItemPrefab();
            InventoryManagerInDream.instance.equipmentSlot.sprite = sr.sprite;
            StartCoroutine(WaitToDrop());
            
        }
    }

    private IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(0.5f);

        GameManagerInDream.instance.canDrop = true;
        
    }

}
