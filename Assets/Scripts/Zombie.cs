using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : LivingObject
{
    //private float _range = 5;
    private float _speedWalk = 5;
    //private float _speedRun = 8;

    private int _damage;
    private int _currentWaypointIndex = 0;

    private bool _isPatrol;

    private Transform _target;
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent _agent;

    public Transform Target { get => _target; set => _target = value; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speedWalk;
        _agent.autoBraking = false;
        _isPatrol = true;

        GotoNextPoint();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Target = other.transform;
    //        transform.LookAt(Target);
    //    }
    //}

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isPatrol)
        {
            // Choisi un autre point de destination lorque le zombie arrive proche de sa destination
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _agent.isStopped = true;
                StopAllCoroutines();
                StartCoroutine(WaitOnDestination());
            }
        }
    }

    private void GotoNextPoint()
    {
        if (_waypoints.Length == 0) return;

        _agent.destination = _waypoints[_currentWaypointIndex].position;

        _currentWaypointIndex = (_currentWaypointIndex + 1 ) % _waypoints.Length;
    }

    IEnumerator WaitOnDestination()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        _agent.isStopped = false;
        GotoNextPoint();
    }

}
