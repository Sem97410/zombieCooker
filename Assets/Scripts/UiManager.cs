using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Image _foodBar;
    [SerializeField] private Text _ammoText;
    [SerializeField] private GameObject _iconGrabItem;
    [SerializeField] private Text _recipeDoneText;

    [SerializeField] private Image _lifeBar;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _hudCanvas;
    [SerializeField] private Canvas _victoryCanvas;
    [SerializeField] private Text _objectif;

    [SerializeField] private Canvas _pauseMenu;

    public List<Image> Images { get => images; set => images = value; }
    public Sprite DefaultSprite { get => _defaultSprite; set => _defaultSprite = value; }
    public Image FoodBar { get => _foodBar; set => _foodBar = value; }
    
    public Text AmmoText { get => _ammoText; set => _ammoText = value; }
    public GameObject IconGrabItem { get => _iconGrabItem; set => _iconGrabItem = value; }
    public Text RecipeDoneText { get => _recipeDoneText; set => _recipeDoneText = value; }
    public Image LifeBar { get => _lifeBar; set => _lifeBar = value; }
    public Canvas GameOverCanvas { get => _gameOverCanvas; set => _gameOverCanvas = value; }
    public Canvas HudCanvas { get => _hudCanvas; set => _hudCanvas = value; }
    public Text Objectif { get => _objectif; set => _objectif = value; }
    public Canvas PauseMenu { get => _pauseMenu; set => _pauseMenu = value; }
    public Canvas VictoryCanvas { get => _victoryCanvas; set => _victoryCanvas = value; }

    public void OnEnable()
    {
        //ZombieEvents.onHungerChanged += UpdateHungerBar;

        ZombieEvents.onLifeChanged += UpdateLifeBar;

        ZombieEvents.onAmmoChanged += UpdateAmmoText;

        ZombieEvents.onItemChanged += UpdateSpriteOfInventory;

        ZombieEvents.onTriggerItemEnter += ShowIconGrabItem;

        ZombieEvents.onTriggerItemExit += HideIconGrabItem;

        ZombieEvents.onRecipeDone += UpdateNumberOfPlateUi;

        ZombieEvents.onPlayerDeath += ShowGameOver;
        ZombieEvents.onPlayerDeath += HideHud;

        ZombieEvents.onTriggerShowGrabIconEnter += ShowIconGrabItem;
        ZombieEvents.onTriggerHideGrabIconExit += HideIconGrabItem;
       

        ZombieEvents.onPlayerWin += ShowVictoryPanel;
        ZombieEvents.onPlayerWin += HideHud;

        ZombieEvents.onResumeGame += ResumeGame;
        ZombieEvents.onQuitGame += QuitGame;
    }

    public void OnDisable()
    {
        //ZombieEvents.onHungerChanged -= UpdateHungerBar;

        ZombieEvents.onLifeChanged -= UpdateLifeBar;

        ZombieEvents.onAmmoChanged -= UpdateAmmoText;

        ZombieEvents.onItemChanged -= UpdateSpriteOfInventory;

        ZombieEvents.onTriggerItemEnter -= ShowIconGrabItem;

        ZombieEvents.onTriggerItemExit -= HideIconGrabItem;

        ZombieEvents.onRecipeDone -= UpdateNumberOfPlateUi;

        ZombieEvents.onPlayerDeath -= ShowGameOver;
        ZombieEvents.onPlayerDeath -= HideHud;

        ZombieEvents.onTriggerShowGrabIconEnter -= ShowIconGrabItem;
        ZombieEvents.onTriggerHideGrabIconExit -= HideIconGrabItem;


        ZombieEvents.onPlayerWin -= ShowVictoryPanel;
        ZombieEvents.onPlayerWin -= HideHud;

        ZombieEvents.onResumeGame -= ResumeGame;
        ZombieEvents.onQuitGame -= QuitGame;

    }
    public void Start()
    {
        foreach (Image image in images)
        {
            image.sprite = DefaultSprite;
        }

        ShowObjectif();
        UpdateNumberOfPlateUi(gameManager.Instance().NumberOfPlate, gameManager.Instance().NumberOfPlateNeed);
    }
    public void UpdateSpriteOfInventory(mainCharacter player)
    {
        if (player.PickUps.Count == 0)
        { 
            Images[0].sprite = DefaultSprite;
        }
        for (int i = 0; i < Images.Count; i++)
        {
            if (i > player.PickUps.Count-1)
            {
                Images[i].sprite = DefaultSprite;
            }
            else
            {
                Images[i].sprite = player.PickUps[i].Sprite;
                Images[i].preserveAspect = true;
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
        RecipeDoneText.text = String.Format("Plats cuisiné : {0}/{1}", recipeDone, recipeNeed);
    }

    public void ShowGameOver(bool value)
    {
        GameOverCanvas.gameObject.SetActive(true);
    }

    public void HideHud(bool value)
    {
        HudCanvas.gameObject.SetActive(false);
    }

    public void ShowHud()
    {
        HudCanvas.gameObject.SetActive(true);
    }

    public void ShowObjectif()
    {
        StartCoroutine("ObjectifCoroutine");
    }

    IEnumerator ObjectifCoroutine()
    {
        Objectif.CrossFadeAlpha(1, 2, true);
        yield return new WaitForSeconds(5f);
        Objectif.CrossFadeAlpha(0, 1, true);
    }

    public void PauseGame()
    {
        PauseMenu.gameObject.SetActive(true);
        HideHud(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PauseMenu.gameObject.SetActive(false);
        ShowHud();
        Time.timeScale = 1;
    }

    public void ShowVictoryPanel(bool value)
    {
        VictoryCanvas.gameObject.SetActive(value);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Damien");
    }
}
