using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    [System.Serializable]
    struct EditorItem
    {
        public KeyCode selectKey;
        public Sprite sprite;
        public GameObject prefab;
    }

    [SerializeField] private List<EditorItem> items = new List<EditorItem>();

    private KeyCode lastSelected = KeyCode.Alpha1;

    private SpriteRenderer sRender;

    private List<GameObject> placedObjects = new List<GameObject>();
    private List<Shooter> shooters = new List<Shooter>();

    private bool runGame = false;

    private void Awake()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) lastSelected = KeyCode.Alpha1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) lastSelected = KeyCode.Alpha2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) lastSelected = KeyCode.Alpha3;

        if (Input.GetKeyDown(KeyCode.R)) transform.Rotate(Vector3.forward, -90f);

        UpdateSprite();

        if (Input.GetMouseButtonUp(0))
            PlaceItem();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            runGame = !runGame;

            if (runGame)
                foreach (Shooter s in shooters)
                    s.ShootNote();
            else
                foreach (Shooter s in shooters)
                    s.DestroyNote();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            foreach (Shooter s in shooters)
                s.DestroyNote();

            foreach (GameObject go in placedObjects)
                Destroy(go);

            placedObjects.Clear();
            shooters.Clear();

            runGame = false;
        }
    }

    private EditorItem GetSelectedItem()
    {
        foreach (EditorItem item in items)
            if (item.selectKey == lastSelected)
                return item;

        return items[0];
    }

    private void UpdateSprite()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        sRender.sprite = GetSelectedItem().sprite;
    }

    private void PlaceItem()
    {
        Vector3 gridPos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition)) + Vector3.one * 0.5f;
        gridPos.z = 0;

        GameObject go = Instantiate(GetSelectedItem().prefab, gridPos, transform.rotation);

        placedObjects.Add(go);
        if (GetSelectedItem().selectKey == KeyCode.Alpha1)
            shooters.Add(go.GetComponent<Shooter>());
    }
}
