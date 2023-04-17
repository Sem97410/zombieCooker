using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private Sprite _defaultSprite;
    public List<Image> Images { get => images; set => images = value; }
    public Sprite DefaultSprite { get => _defaultSprite; set => _defaultSprite = value; }

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
        for (int i = 0; i < Images.Count-1; i++)
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

}
