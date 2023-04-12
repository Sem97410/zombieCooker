using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : LivingObject
{
    [Header("Enemy")]
    private float _speedWalk = 3;
    private float _speedRun = 5;

    private int _damage;
    private int _currentWaypointIndex = 0;

    private bool _isPatrol;

    private Transform _target;
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent _agent;

    public Transform Target { get => _target; set => _target = value; }

    [Header("Field Of View")]
    public float _radius;
    [Range(0,360)] public float _angle;
    public GameObject _playerRef;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _ObstructionMask;

    public bool _canSeePlayer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speedWalk;
        _agent.autoBraking = false;
        _isPatrol = true;

        _playerRef = GameObject.FindGameObjectWithTag("Player");

        GotoNextPoint();
        StartCoroutine(FOVRoutine());
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
                StopCoroutine(WaitOnDestination());
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

    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;

            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _ObstructionMask))
                {
                    _canSeePlayer = true;
                    Target = target;
                    _agent.speed = _speedRun;
                    _agent.destination = Target.position;
                }
                else
                {
                    _agent.speed = _speedWalk;
                    _agent.destination = _waypoints[_currentWaypointIndex].position;
                    _canSeePlayer = false;
                }
                   
            }
            else
                _canSeePlayer = false;
        }

        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }
}
