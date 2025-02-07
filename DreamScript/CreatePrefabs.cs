using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePrefabs : MonoBehaviour
{
    public static CreatePrefabs instance;
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
        dropPrefab.transform.parent = GameManager.instance.dreamScene.transform;
      
    }
}
