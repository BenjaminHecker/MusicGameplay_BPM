using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : NoteInteractor
{
    private SpriteRenderer sRender;

    private void Awake()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    public override void Interact(Note note)
    {
        if (sRender.flipX)
        {
            if (note.direction == Vector3Int.right) note.direction = Vector3Int.down;
            else if (note.direction == Vector3Int.left) note.direction = Vector3Int.up;
            else if (note.direction == Vector3Int.up) note.direction = Vector3Int.left;
            else if (note.direction == Vector3Int.down) note.direction = Vector3Int.right;
        }
        else
        {
            if (note.direction == Vector3Int.right) note.direction = Vector3Int.up;
            else if (note.direction == Vector3Int.left) note.direction = Vector3Int.down;
            else if (note.direction == Vector3Int.up) note.direction = Vector3Int.right;
            else if (note.direction == Vector3Int.down) note.direction = Vector3Int.left;
        }
    }
}
