using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class Note : MonoBehaviour
{
    [SerializeField] private Beat.TickValue tickValue;

    [SerializeField] private float moveTime = 0.5f;

    private Clock clock;
    private bool trigger = false;

    private Vector3 direction = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;

    #region Delegate

    private void OnEnable()
    {
        clock = Clock.Instance;
        clock.Beat += OnBeat;
        clock.Eighth += OnBeat;
    }
    private void OnDisable()
    {
        if (clock == null) return;

        clock.Beat -= OnBeat;
        clock.Eighth -= OnBeat;
    }

    private void OnBeat(Beat.Args beatArgs)
    {
        if (tickValue == beatArgs.BeatVal)
        {
            ReactAction();
        }
    }

    #endregion

    public void Setup(Vector3 direction)
    {
        this.direction = direction;
        targetPos = transform.position;
    }


    void ReactAction()
    {
        trigger = true;
    }

    void Move()
    {
        trigger = false;
        targetPos += direction;

        StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        Vector3 prevPos = transform.position;
        float timer = 0f;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(prevPos, targetPos, timer / moveTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = targetPos;
    }

    private void Update()
    {
        if (trigger)
        {
            Move();
        }
    }
}
