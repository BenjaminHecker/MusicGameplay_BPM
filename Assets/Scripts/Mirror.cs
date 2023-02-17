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

        }
    }
}