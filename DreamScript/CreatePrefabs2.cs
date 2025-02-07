using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePrefabs2 : MonoBehaviour
{
    public static CreatePrefabs2 instance;
    public GameObject dropItemPrefab;
    private GameObject dropPrefab;

    public Image equipment;

    private void Awake()
    {
        instance = this;
    }
    public void CreateDropItemPrefab()
    {

        dropPrefab = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        dropPrefab.transform.parent = GameManager.instance.dreamScene1.transform;

    }
}