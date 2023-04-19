using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Image _foodBar;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private Text _ammoText;
    [SerializeField] private GameObject _iconGrabItem;
    [SerializeField] private Text _recipeDoneText;


    public List<Image> Images { get => images; set => images = value; }
    public Sprite DefaultSprite { get => _defaultSprite; set => _defaultSprite = value; }
    public Image FoodBar { get => _foodBar; set => _foodBar = value; }
    public Image LifeBar { get => _lifeBar; set => _lifeBar = value; }
    public Text AmmoText { get => _ammoText; set => _ammoText = value; }
    public GameObject IconGrabItem { get => _iconGrabItem; set => _iconGrabItem = value; }
    public Text RecipeDoneText { get => _recipeDoneText; set => _recipeDoneText = value; }

    public void OnEnable()
    {
        ZombieEvents.onHungerChanged += UpdateHungerBar;

        ZombieEvents.onLifeChanged += UpdateLifeBar;

        ZombieEvents.onAmmoChanged += UpdateAmmoText;

        ZombieEvents.onItemChanged += UpdateSpriteOfInventory;

        ZombieEvents.onTriggerItemEnter += ShowIconGrabItem;

        ZombieEvents.onTriggerItemExit += HideIconGrabItem;

        ZombieEvents.onRecipeDone += UpdateNumberOfPlateUi;

    }

    public void OnDisable()
    {
        ZombieEvents.onHungerChanged -= UpdateHungerBar;

        ZombieEvents.onLifeChanged -= UpdateLifeBar;

        ZombieEvents.onAmmoChanged -= UpdateAmmoText;

        ZombieEvents.onItemChanged -= UpdateSpriteOfInventory;

        ZombieEvents.onTriggerItemEnter -= ShowIconGrabItem;

        ZombieEvents.onTriggerItemExit -= HideIconGrabItem;

        ZombieEvents.onRecipeDone -= UpdateNumberOfPlateUi;




    }
    public void Start()
    {
        foreach (Image image in images)
        {
            image.sprite = DefaultSprite;
        }
    }
    public void UpdateSpriteOfInventory(mainCharacter player)
    {
        if (player.PickUps.Count == 0)
        { 
            Images[0].sprite = DefaultSprite;
        }
        for (int i = 0; i < Images.Count -1; i++)
        {
            if (i > player.PickUps.Count-1)
            {
                Images[i].sprite = DefaultSprite;
            }
            else
            {
                Images[i].sprite = player.PickUps[i].Sprite;
            }
        }
    }

    public void UpdateBar(float value, Image image)
    {
        image.fillAmount = ConvertInPercent(value);
    }

    public void UpdateHungerBar(float value)
    {
        UpdateBar(value, FoodBar);
    }

    public void UpdateLifeBar(float value)
    {
        UpdateBar(value, LifeBar);
    }


    public float ConvertInPercent(float value)
    {
        return value / 100;
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        AmmoText.text = String.Format("{0}/{1}", currentAmmo, maxAmmo);
    }

    public void ShowIconGrabItem()
    {
        IconGrabItem.SetActive(true);
    }

    public void HideIconGrabItem()
    {
        IconGrabItem.SetActive(false);
    }

    private void UpdateNumberOfPlateUi(int recipeDone, int recipeNeed)
    {
        AmmoText.text = String.Format("Plats cuisin� : {0}/{1}", recipeDone, recipeNeed);
    }



}
