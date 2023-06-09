using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mainCharacter : LivingObject
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _runSpeedMultiplication;
    [SerializeField] private float _rotationSpeed;
    private bool _isRunning;

    [Header("PickUpOptions")]
    [SerializeField] private Transform _itemPos;
    [SerializeField] private int _maxSpaceInInventory = 5;
    private PickUp _itemInteractable;
    private List<PickUp> _pickUp;
    private bool _canInteract;
    private bool _havePistol;
    private int _choixIndex;


    [Header("HungerOptions")]
    [SerializeField] private float _currentHunger;
    [SerializeField] private float _hungerDecrease;
    [SerializeField] private float _hungerDecreaseRun;
    private float _maxHunger;

    [Header("Inputs")]
    [SerializeField] private InputAction _shootAction;
    [SerializeField] private InputAction _interactAction;
    [SerializeField] private InputAction _buttonAction;
    [SerializeField] private InputAction _eatAction;
    [SerializeField] private InputAction _dropItemAction;
    [SerializeField] private InputAction _pauseAction;


    [Header("Audio Player")]
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _shootClip;

    [Header("Animation")]
    [SerializeField] private Animator _weaponAnimator;

    [Header("Particules")]
    [SerializeField] private Fx _walkFx;
    [SerializeField] private Fx _hitFx;
    [SerializeField] private Fx _bloodFx;






    private TrailRenderer _trailRenderer;
    private bool _canAttack = true;

    private Rigidbody _rb;

    [SerializeField] private UiManager uiManager;
    [SerializeField] private LayerMask _IgnoreLayer;


    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    public PickUp ItemInteractable { get => _itemInteractable; set => _itemInteractable = value; }
    public bool HavePistol { get => _havePistol; set => _havePistol = value; }
    public List<PickUp> PickUps { get => _pickUp; set => _pickUp = value; }
    public int ChoixIndex { get => _choixIndex; set => _choixIndex = value; }
    public Transform ItemPos { get => _itemPos; set => _itemPos = value; }
    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public UiManager UiManager { get => uiManager; set => uiManager = value; }
    public InputAction ButtonAction { get => _buttonAction; set => _buttonAction = value; }
    public AudioSource PlayerAudioSource { get => _playerAudioSource; set => _playerAudioSource = value; }
    public int MaxSpaceInInventory { get => _maxSpaceInInventory; set => _maxSpaceInInventory = value; }
    public Animator WeaponAnimator { get => _weaponAnimator; set => _weaponAnimator = value; }
    public Fx BloodFx { get => _bloodFx; set => _bloodFx = value; }

    private void OnEnable()
    {
        ZombieEvents.onFoodEaten += EatFood;

        ZombieEvents.onPlayerWin += CursorMode;
    }

    private void OnDisable()
    {
        ZombieEvents.onFoodEaten -= EatFood;

        ZombieEvents.onPlayerWin -= CursorMode;

    }
    private void Start()
    {
        CursorMode(false);
        PickUps = new List<PickUp>();
        MaxLife = 100;
        CurrentLife = MaxLife;

        ChoixIndex = 0;

        _dropItemAction.Enable();
        _dropItemAction.performed += DropItem;
        _shootAction.Enable();
        _shootAction.performed += Attack;

        _eatAction.Enable();
        _eatAction.performed += Eat;

        //StartCoroutine("DecreaseHunger");

        _interactAction.Enable();
        _interactAction.performed += Interact;

        _buttonAction.Enable();

        _pauseAction.Enable();
        _pauseAction.performed += Pause;

        _playerAudioSource = gameObject.GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();    


    }
    private void Update()
    {
        Move();
        ShowItemSelected();

    }

    //Crash apr�s l'utilisation de la touche maj si  _runSpeedMultiplication == 0
    //TODO : Rajouter un test pour eviter que �a plante
    private void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0 , moveVertical);

        movement.Normalize();

       _rb.velocity = transform.TransformDirection(movement * _movementSpeed);






    }


    public void Eat(InputAction.CallbackContext ctx)
    {
        if (GetItemSelected() == null) return;
        if (GetItemSelected() is Food)
        {
            ZombieEvents.onFoodEaten(PlayerAudioSource);
            uiManager.UpdateSpriteOfInventory(this);
        }
    }

    public void Run()  //la m�thode est appel� gr�ce aux event de l'input player
    {
        if (Input.GetButton("Run"))
        {
            if (_isRunning == false)
            {
                _movementSpeed = _movementSpeed * _runSpeedMultiplication;
                _hungerDecrease = _hungerDecrease / _hungerDecreaseRun;          //augmente la perte de faim quand on court
                _isRunning = true;
                //gameManager.AddFX(_walkFx, this.transform.position, transform.rotation);

            }
        }
        else
        {
            if (_isRunning == true)
            {
                _isRunning = false;

                _movementSpeed = _movementSpeed / _runSpeedMultiplication;
                _hungerDecrease = _hungerDecrease * _hungerDecreaseRun;
            }

        }
    }


    IEnumerator DecreaseHunger()
    {
        while (_currentHunger > 0)
        {
            yield return new WaitForSeconds(_hungerDecrease);
            _currentHunger -= 1;
            ZombieEvents.onHungerChanged(_currentHunger);
        }
        while (_currentHunger == 0)
        {
            yield return new WaitForSeconds(1f);
            _currentLife -= 1;
            ZombieEvents.onLifeChanged(_currentLife);
            CheckIfDead();
            if (IsDead)
            {
                CursorMode(true);
                ZombieEvents.onPlayerDeath(true);
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (_canInteract && ItemInteractable != null)
        {
            ItemInteractable.PickUpItem();
            CanInteract = false;
            ItemInteractable = null;
            ChooseWeaponAnimator(true);


        }
        else return;
    }

    public void ShowItemSelected()
    {
        if (PickUps == null) return;


        if (Input.GetButton("Item1"))
        {
            ChooseItem(0);
        }
        if (Input.GetButton("Item2"))
        {
            ChooseItem(1);
        }
        if (Input.GetButton("Item3"))
        {
            ChooseItem(2);
        }
        if (Input.GetButton("Item4"))
        {
            ChooseItem(3);
        }
        if (Input.GetButton("Item5"))
        {
            ChooseItem(4);
        }
        if (Input.GetButton("Item6"))
        {
            ChooseItem(5);
        }
    }

    public PickUp GetItemSelected()
    {
        if (PickUps != null && PickUps.Count > 0 && ChoixIndex < PickUps.Count)
            return PickUps[ChoixIndex];
        else return null;

    }

    public void AfficherItemEquipe(GameObject go)
    {
        if (go == null) return;
        go.SetActive(true);
        go.transform.parent = this.ItemPos;
        go.transform.localPosition = Vector3.zero;
        go.transform.rotation = ItemPos.rotation * Quaternion.Euler(0, -90, 0);

        ChooseWeaponAnimator(true);
    }
    public void EnleverItemEquipe(GameObject go)
    {
        if (go == null) return;
        go.SetActive(false);
    }

    public void ChooseItem(int choixindex)
    {
        if (GetItemSelected() != null)
        {
            EnleverItemEquipe(GetItemSelected().GetGameObject());
        }
        ChoixIndex = choixindex;

        if (GetItemSelected() != null)
        {
            AfficherItemEquipe(GetItemSelected().GetGameObject());
        }
    }


    public void DropItem(InputAction.CallbackContext ctx)
    {
        if (GetItemSelected() != null)
        {
            GetItemSelected().transform.parent = null;
            GetItemSelected().GetComponent<Rigidbody>().isKinematic = false;
            GetItemSelected().GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 15, ForceMode.Impulse);
            GetItemSelected().GetComponent<SphereCollider>().enabled = true;
            GetItemSelected().GetComponentInChildren<BoxCollider>().enabled = true;
            ChooseWeaponAnimator(false);
            PickUps.Remove(GetItemSelected());
            ChooseItem(0);
            ZombieEvents.onItemChanged(this);

        }

    }


    private void ChooseWeaponAnimator(bool activate)
    {
        if (GetItemSelected() is Knife || GetItemSelected() is Pistol)
        {
            if (activate == true)
            {
                if (GetItemSelected() is Pistol)
                {
                    _weaponAnimator = GetItemSelected().GetComponentInChildren<Animator>();

                }
                else
                {
                    _weaponAnimator = GetItemSelected().GetComponent<Animator>();
                    _trailRenderer = GetItemSelected().GetComponentInChildren<TrailRenderer>();


                }
                _weaponAnimator.enabled = activate;

            }
            else
            {
                _weaponAnimator.enabled = activate;
                if (GetItemSelected() is Pistol)
                {
                    Component[] Transform;
                    Transform = GetItemSelected().GetComponentsInChildren<Transform>();

                    Transform[1].gameObject.transform.localPosition = new Vector3(0, 0, 0);



                }

            }
        }
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (_canAttack == true)
        {

            if (GetItemSelected() is Pistol)
            {
                Pistol pistol = GetItemSelected().GetComponent<Pistol>();
                if (pistol.CurrentAmmo <= 0) return;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                Debug.DrawRay(ray.origin, Camera.main.transform.forward * 50, Color.red);

                RaycastHit hit;
                DecreasedAmmo(1, pistol);
                ZombieEvents.onAmmoChanged(pistol.CurrentAmmo, pistol.MaxAmmo);
                ZombieEvents.onShoot(PlayerAudioSource);
                PlayShootFx(pistol);

                StartCoroutine(AttackAnimation(GetItemSelected()));
                if (Physics.Raycast(ray, out hit, 150, ~_IgnoreLayer))
                {

                    gameManager.AddFX(pistol.MuzzleFx, pistol.MuzzlePoint.position, pistol.MuzzlePoint.localRotation);
                    if (Physics.Raycast(ray, out hit, 150, ~_IgnoreLayer))
                    {
                        
                        if (hit.collider.CompareTag("Zombie"))
                        {

                            Zombie zombie = hit.collider.GetComponent<Zombie>();
                            pistol.Attack(this, hit.collider.GetComponent<IDamageable>());
                            zombie.StartCoroutine(zombie.ShowZombieLife());
                            FxImpact(_bloodFx, hit.point);

                        }
                        else
                        {
                            FxImpact(_hitFx, hit.point);

                        }
                    }
                }
                
            }

            if (GetItemSelected() is Knife)
            {
                StartCoroutine(AttackAnimation(GetItemSelected()));

            }
        }

        
    }




    IEnumerator AttackAnimation(PickUp TypeOfWeapon)
    {
        _canAttack = false;

        if (TypeOfWeapon is Knife)
        {
            int randomAttack = UnityEngine.Random.Range(1, 3);

            _trailRenderer.enabled = true;
            _weaponAnimator.SetInteger("CanAttack", randomAttack);

            yield return new WaitForSeconds(0.50f);
            _trailRenderer.enabled = false;

            yield return new WaitForSeconds(0.60f);

            _weaponAnimator.SetInteger("CanAttack", 3);
            _canAttack = true;


        }
        if (TypeOfWeapon is Pistol)
        {
            _weaponAnimator.SetBool("CanShoot", true);
            yield return new WaitForSeconds(0.45f);
            _weaponAnimator.SetBool("CanShoot", false);
            //yield return new WaitForSeconds(0.35f);

            _canAttack = true;



        }


    }



    public override void TakeDamage(int damage, IDamageable Attaquant)
    {
        base.TakeDamage(damage, Attaquant);
        ZombieEvents.onLifeChanged(_currentLife);
        if (IsDead)
        {
            CursorMode(true);
            ZombieEvents.onPlayerDeath(true);
        }
    }

    public void EatFood(AudioSource audioSource)
    {
        Food _food = GetItemSelected().gameObject.GetComponent<Food>();
        _currentHunger += _food.Satiety;
        Destroy(GetItemSelected().gameObject, 0.1f);
        EnleverItemEquipe(GetItemSelected().gameObject);
        PickUps.Remove(GetItemSelected());
        if (_currentHunger >= 100)
        {
            _currentHunger = 100;
        }
    }

    public void PlayShootFx(Pistol pistol)
    {
        gameManager.AddFX(pistol.MuzzleFx, pistol.MuzzlePoint.position, pistol.MuzzlePoint.localRotation);
    }

    public void DecreasedAmmo(int value, Pistol pistol)
    {
        pistol.CurrentAmmo -= value;
    }

    public void CursorMode(bool value)
    {
        Cursor.visible = value;
        if (value)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void FxImpact(Fx fx, Vector3 impactPoint)
    {
        fx = gameManager.AddFX(fx, impactPoint, Quaternion.identity);
        fx.transform.LookAt(this.transform);
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        CursorMode(true);
        UiManager.PauseGame();
    }
}
