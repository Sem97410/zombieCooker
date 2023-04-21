using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

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

    private float _speedWalk = 1.5f;
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

    [SerializeField] private ZombieSpawner _spawner;

    private Slider _sliderLifeBar;

    [SerializeField] private Animator _zombieAnimator;

    [Header("Particules")]
    [SerializeField] private Fx _deathFx;

    [Header("Audio")]
    [SerializeField] private AudioSource _zombieAudioSource;

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
    public ZombieSpawner Spawner { get => _spawner; set => _spawner = value; }
    public Slider SliderLifeBar { get => _sliderLifeBar; set => _sliderLifeBar = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public AudioSource ZombieAudioSource { get => _zombieAudioSource; set => _zombieAudioSource = value; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speedWalk;
        _agent.autoBraking = false;
        _isPatrol = true;

        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        SliderLifeBar = gameObject.GetComponentInChildren<Slider>();
        _zombieAnimator = GetComponentInChildren<Animator>();

        SliderLifeBar.gameObject.SetActive(false);

        MaxLife = 100;
        CurrentLife = MaxLife;

        _damage = 20;

        _guardPosition = this.transform.position;
        _guardRotation = this.transform.rotation;

        ZombieAudioSource = GetComponent<AudioSource>();

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
                _zombieAnimator.SetBool("Walk", false);
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
        _zombieAnimator.SetBool("Walk", true);
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

        if (rangeChecks.Length != 0 && !IsAttacked && !IsDead)
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
                    _agent.isStopped = false;
                    _agent.speed = _speedRun;
                    _agent.destination = Target.position;
                    _isPatrol = false;
                    _zombieAnimator.SetBool("Walk", false);
                    _zombieAnimator.SetBool("Run", true);
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
                _zombieAnimator.SetBool("Walk", true);
                _zombieAnimator.SetBool("Run", false);
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
            _agent.isStopped = true ;
            _zombieAnimator.SetBool("Run", false);
            _zombieAnimator.SetBool("Walk", false);
            _zombieAnimator.SetTrigger("Attack");
        }
    }

    public void UpdateZombieLifeBar(float curentValue, float maxLife)
    {
        SliderLifeBar.value = curentValue / maxLife;
    }

    public IEnumerator ShowZombieLife()
    {
        if (!IsDead)
        {
            SliderLifeBar.gameObject.SetActive(true);
            SliderLifeBar.transform.LookAt(PlayerRef.transform);
            UpdateZombieLifeBar(CurrentLife, MaxLife);

            yield return new WaitForSeconds(1.0f);

            SliderLifeBar.gameObject.SetActive(false); 
        }

    }


    public override void TakeDamage(int damage, IDamageable Attaquant)
    {
        base.TakeDamage(damage, Attaquant);
        if (IsDead) return;
        _zombieAnimator.SetTrigger("Hit");
        ZombieEvents.onZombieHit(ZombieAudioSource);
    }

    public override void Die(IDamageable Cible)
    {
        if (Spawner != null)
        {
            ZombieEvents.onZombieSpawnedDied?.Invoke();
           
            
        }
        IsDead = true;
        _agent.isStopped = true;
        enabled = false;
        _zombieAnimator.SetTrigger("Dead");
        Fx deathFx = gameManager.AddFX(_deathFx, this.transform.position + this.transform.up   , Quaternion.identity);
        deathFx.transform.LookAt(PlayerRef.transform);
        Destroy(this.gameObject,3f);


    }
}
