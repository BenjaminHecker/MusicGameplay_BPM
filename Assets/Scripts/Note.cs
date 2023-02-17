using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class Note : MonoBehaviour
{
    [SerializeField] private Beat.TickValue tickValue;

    [SerializeField] [Range(0f, 1f)] private float moveFactor = 0.5f;

    [HideInInspector] public NoteInteractor interactor;
    [HideInInspector] public Shooter origin;

    private Clock clock;
    private bool trigger = false;
    private IEnumerator moveRoutine;

    [HideInInspector] public Vector3Int direction = Vector3Int.zero;
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

    public void Setup(Shooter origin, Vector3Int direction)
    {
        this.origin = origin;
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
        float timerTotal = moveFactor / (float) clock.BPM * 60f;

        yield return new WaitForSeconds((1 - moveFactor) / (float)clock.BPM * 60f);

        while (timer + Time.deltaTime < timerTotal)
        {
            transform.position = Vector3.Lerp(prevPos, targetPos, timer / timerTotal);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        transform.position = targetPos;
        Interact();
    }

    private void Interact()
    {
        if (interactor == null) return;

        if ((transform.position - interactor.transform.position).magnitude <= 0.2f)
            interactor.Interact(this);
    }

    private void Update()
    {
        if (trigger)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note Interactor"))
        {
            interactor = collision.GetComponent<NoteInteractor>();
        }
    }
}
