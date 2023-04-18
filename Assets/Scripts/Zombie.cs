using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : LivingObject
{
    public enum ZombieState
    {
        Guard,
        Waypoint,
        Random
    }

    [Header("Enemy")]
    public ZombieState _zombieState;

    private float _speedWalk = 3;
    private float _speedRun = 5;

    private int _damage;
    private int _currentWaypointIndex = 0;

    private bool _isPatrol;
    private bool _isAttacked = false;

    [SerializeField] private float _walkRadius;

    private Vector3 _guardPosition;

    private Quaternion _guardRotation;

    //[SerializeField] private Transform _target;
    [SerializeField] private Transform _target;
    //[SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent _agent;

    [Header("Field Of View")]
    [SerializeField] private float _radius;
    [Range(0, 360)][SerializeField] private float _angle;
    [SerializeField] private GameObject _playerRef;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _ObstructionMask;

    [SerializeField] private bool _canSeePlayer;

    //private ZombieSpawner _spawner;

    public Transform Target { get => _target; set => _target = value; }
    public bool IsAttacked { get => _isAttacked; set => _isAttacked = value; }
    public float WalkRadius { get => _walkRadius; set => _walkRadius = value; }
    public float Radius { get => _radius; set => _radius = value; }
    public float Angle { get => _angle; set => _angle = value; }
    public GameObject PlayerRef { get => _playerRef; set => _playerRef = value; }
    public bool CanSeePlayer { get => _canSeePlayer; set => _canSeePlayer = value; }
    public Transform[] Waypoints { get => _waypoints; set => _waypoints = value; }
    public LayerMask TargetMask { get => _targetMask; set => _targetMask = value; }
    public LayerMask ObstructionMask { get => _ObstructionMask; set => _ObstructionMask = value; }
    //public ZombieSpawner Spawner { get => _spawner; set => _spawner = value; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speedWalk;
        _agent.autoBraking = false;
        _isPatrol = true;

        _playerRef = GameObject.FindGameObjectWithTag("Player");

        MaxLife = 100;
        CurrentLife = MaxLife;

        _damage = 10;

        _guardPosition = this.transform.position;
        _guardRotation = this.transform.rotation;

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

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isPatrol)
        { 
            // Choisi un autre point de destination lorque le zombie arrive proche de sa destination
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f && !_agent.isStopped)
            {
                _agent.isStopped = true;
                StopCoroutine("WaitOnDestination");
                StartCoroutine(WaitOnDestination());
            }
        }
    }

    private void GotoNextPoint()
    {
        if (_zombieState == ZombieState.Guard)
        {
            _agent.destination = _guardPosition;
            transform.rotation = _guardRotation;
        }

        else if (_zombieState == ZombieState.Waypoint)
        {
            if (_waypoints.Length == 0) return;

            _agent.isStopped = false;

            _agent.destination = _waypoints[_currentWaypointIndex].position;

            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            Debug.Log("Destination : " + _currentWaypointIndex);
        }

        else if (_zombieState == ZombieState.Random)
        {
            Vector3 randomDirection = Random.insideUnitSphere * _walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalposition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, _walkRadius, 1))
            {
                finalposition = hit.position;
            }
            _agent.destination = finalposition;
        }
    }

    IEnumerator WaitOnDestination()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));

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

        if (rangeChecks.Length != 0 && !IsAttacked)
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
                    _agent.isStopped = false;
                    _isPatrol = false;
                }
                else
                {
                    _canSeePlayer = false;
                }
                   
            }
            else
            {
                _canSeePlayer = false;
                Target = null;
                _isPatrol = true;
            }
        }

        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Target == PlayerRef.transform && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LivingObject>().TakeDamage(_damage, this);
        }
    }


    //public override void Die(IDamageable Cible)
    //{
    //    if (Spawner != null)
    //    {
    //        Spawner.ZombieCount--;
    //    }

    //}

}
