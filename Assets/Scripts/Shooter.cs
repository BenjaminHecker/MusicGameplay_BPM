using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;

    private void Awake()
    {
        ShootNote();
    }
    
    public void ShootNote()
    {
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
        note.GetComponent<Note>().Setup(this, Vector3Int.RoundToInt(transform.right));
    }
}
