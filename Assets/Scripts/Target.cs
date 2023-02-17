using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : NoteInteractor
{
    public override void Interact(Note note)
    {
        note.origin.ShootNote();
        Destroy(note.gameObject);
    }
}
