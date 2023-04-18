using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class mainCharacter : LivingObject
{
    [SerializeField]
    private float _movementSpeed;

    private List<PickUp> _pickUp;

    [SerializeField]
    private float _runSpeedMultiplication;      //cette variable est multiplié avec la vitesse pour calculer la vitesse de course

    private PickUp _itemInteractable;

    private int _choixIndex;
    [SerializeField] private Transform _itemPos;


    private float _maxHunger;

    [SerializeField]
    private float _currentHunger;


    [SerializeField] private bool _canInteract;
    private bool _havePistol;
    private bool _haveKnife;
    [SerializeField]
    private float _hungerDecrease;   //la vitesse a laquelle on perd de la faim

    [SerializeField]
    private float _hungerDecreaseRun; // la perte de faim quand on court

    [SerializeField] private UiManager uiManager;

    [SerializeField] private InputAction _dropItemAction;

    private bool _isRunning;

    [SerializeField]
    private Fx _walkFx;

    [Header("Inputs")]
    [SerializeField] private InputAction _shootAction;

    [SerializeField] private InputAction _interactAction;

    [SerializeField] private InputAction _buttonAction;

    [SerializeField] private InputAction _eatAction;


    [SerializeField] private LayerMask _IgnoreLayer;


    [Header("Audio Player")]
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _shootClip;

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    public PickUp ItemInteractable { get => _itemInteractable; set => _itemInteractable = value; }
    public bool HavePistol { get => _havePistol; set => _havePistol = value; }
    public List<PickUp> PickUps { get => _pickUp; set => _pickUp = value; }
    public int ChoixIndex { get => _choixIndex; set => _choixIndex = value; }
    public Transform ItemPos { get => _itemPos; set => _itemPos = value; }
    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public bool HaveKnife { get => _haveKnife; set => _haveKnife = value; }
    public UiManager UiManager { get => uiManager; set => uiManager = value; }
    public InputAction ButtonAction { get => _buttonAction; set => _buttonAction = value; }
    public AudioSource PlayerAudioSource { get => _playerAudioSource; set => _playerAudioSource = value; }

    private void Start()
    {
        //lock le cursor pour la caméra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        StartCoroutine("DecreaseHunger");

        _interactAction.Enable();
        _interactAction.performed += Interact;

        _buttonAction.Enable();

        _playerAudioSource = this.GetComponent<AudioSource>();

    }
    private void Update()
    {
        Move();
        ShowItemSelected();
       
    }

    //Crash après l'utilisation de la touche maj si  _runSpeedMultiplication == 0
    //TODO : Rajouter un test pour eviter que ça plante
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime; ;
        float verticalInput = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime; ;
        transform.Translate(horizontalInput, 0, verticalInput);
    }


    public void Eat(InputAction.CallbackContext ctx)
    {
        if (GetItemSelected() == null) return;
        if (GetItemSelected() is Food)
        {
            Debug.Log("food dans mains");
            Food _food = GetItemSelected().gameObject.GetComponent<Food>();
            Debug.Log(_food.Satiety);
            _currentHunger += _food.Satiety;
            Destroy(GetItemSelected().gameObject, 0.1f);
            EnleverItemEquipe(GetItemSelected().gameObject);
            PickUps.Remove(GetItemSelected());
            if (_currentHunger >= 100)
            {
                _currentHunger = 100;
            }
           

        }
    }

    public void Run()  //la méthode est appelé grâce aux event de l'input player
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
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (_canInteract && ItemInteractable != null)
        {
            ItemInteractable.PickUpItem();
            CanInteract = false;
            ItemInteractable = null;
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
            PickUps.Remove(GetItemSelected());
            ChooseItem(0);
            ZombieEvents.onItemChanged(this);

        }

    }




    public void Attack(InputAction.CallbackContext ctx)
    {
        if (GetItemSelected() is Pistol)
        {
            Pistol pistol = GetItemSelected().GetComponent<Pistol>();
            if (pistol.CurrentAmmo <= 0) return;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Debug.DrawRay(ray.origin, Camera.main.transform.forward * 50, Color.red);

            RaycastHit hit;
            pistol.CurrentAmmo--;
            ZombieEvents.onAmmoChanged(pistol.CurrentAmmo, pistol.MaxAmmo);

            _playerAudioSource.clip = _shootClip;
            _playerAudioSource.Play();

            if (Physics.Raycast(ray, out hit, 150, ~_IgnoreLayer))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Zombie"))
                {
                    hit.collider.GetComponent<Zombie>().SetTarget(this.transform);
                    pistol.Attack(this, hit.collider.GetComponent<IDamageable>());
                    if (hit.collider.GetComponent<LivingObject>().CurrentLife <= 0)
                    {
                        hit.collider.GetComponent<IDamageable>().Die(hit.collider.GetComponent<IDamageable>());
                    }
                }
            }
        }

        else if (GetItemSelected() is Knife)
        {
            //Mettre l'animation d'attaque et le takeDamage au moment ou le couteau touche un enemy

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Zombie"))
                {
                    GetItemSelected().gameObject.GetComponent<Knife>().Attack(this, collider.GetComponent<IDamageable>());
                }
            }
        }
    }

    public override void TakeDamage(int damage, IDamageable Attaquant)
    {
        base.TakeDamage(damage, Attaquant);

        ZombieEvents.onLifeChanged(_currentLife);
    }
}
