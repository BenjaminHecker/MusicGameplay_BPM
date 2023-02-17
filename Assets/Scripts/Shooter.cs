using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;

    private Note note;

    public void DestroyNote()
    {
        if (note != null)
            Destroy(note.gameObject);
    }
    
    public void ShootNote()
    {
        note = Instantiate(notePrefab, transform.position, Quaternion.identity).GetComponent<Note>();
        note.Setup(this, Vector3Int.RoundToInt(transform.right));
    }
}
