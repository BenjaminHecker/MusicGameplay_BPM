using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class Note : MonoBehaviour
{
    [SerializeField] private Beat.TickValue tickValue;

    [SerializeField] [Range(0f, 1f)] private float moveTime = 0.5f;

    private Clock clock;
    private bool trigger = false;
    private IEnumerator moveRoutine;

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

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);
        moveRoutine = SmoothMove();
        StartCoroutine(moveRoutine);
    }

    private IEnumerator SmoothMove()
    {
        Vector3 prevPos = transform.position;
        float timer = Time.deltaTime;
        float timerTotal = moveTime / (float) clock.BPM * 60f;

        yield return new WaitForSeconds((1 - moveTime) / (float) clock.BPM * 60f);

        while (timer < moveTime)
        {
            transform.position = Vector3.Lerp(prevPos, targetPos, timer / timerTotal);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
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
