using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostControl : MonoBehaviour
{
    public enum State { Idle, Trace, Die }
    public State state = State.Idle;

    private float traceDistance = 100000000f;
    public bool isDie = false;

    private Transform playerTransform;
    private NavMeshAgent agent;

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        isDie = false;
        state = State.Idle;

        StartCoroutine(checkGhostState());
        StartCoroutine(GhostAction());
    }
    private void Awake()
    {
        //추적 대상인 플레이어의 transform 할당
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    IEnumerator checkGhostState()
    {
        while (!isDie)
        {
            if (state == State.Die)
            {
                yield break;
            }

            float dist = (playerTransform.position - transform.position).sqrMagnitude;
            if (dist <= traceDistance * traceDistance)
            {
                state = State.Trace;
            }
            else
            {
                state = State.Idle;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator GhostAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.Idle:
                    agent.isStopped = true;
                    break;
                case State.Trace:
                    agent.SetDestination(playerTransform.position);
                    agent.isStopped = false;
                    break;
                case State.Die:
                    isDie = true;
                    agent.isStopped = true;
                    Destroy(gameObject);
                    Debug.Log("Die");
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
