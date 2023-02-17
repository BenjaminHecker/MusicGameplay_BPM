using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mirror : NoteInteractor
{
    [SerializeField] private Note.Keys key;
    [SerializeField] private TextMeshProUGUI keyTxt;

    private SpriteRenderer sRender;

    private void Awake()
    {
        sRender = GetComponent<SpriteRenderer>();
        SetKeyText();
    }

    public override void Interact(Note note)
    {
        SoundManager.Play(key);

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

    private void SetKeyText()
    {
        if (key == Note.Keys.C) keyTxt.text = "C";
        if (key == Note.Keys.D) keyTxt.text = "D";
        if (key == Note.Keys.E) keyTxt.text = "E";
        if (key == Note.Keys.F) keyTxt.text = "F";
        if (key == Note.Keys.G) keyTxt.text = "G";
        if (key == Note.Keys.A) keyTxt.text = "A";
        if (key == Note.Keys.B) keyTxt.text = "B";
    }
}
