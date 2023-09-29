using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;
    [SerializeField] private float _timeToStart;

    // AI
    private NavMeshAgent _agent; 

    private Transform _exitPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); 
        StartCoroutine(WaitBeforeStart());
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(_timeToStart);
        EnablePlayerAI();
    }

    private void EnablePlayerAI()
    {
        _exitPosition = _mazeGenerator.lastCell;

        _agent.SetDestination(_exitPosition.position);
    }

    public void RestartPlayer()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        gameObject.SetActive(true);
        StartCoroutine(WaitBeforeStart());
    }

}
