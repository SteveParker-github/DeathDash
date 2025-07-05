using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private Texture2D fullHealthImg;
    [SerializeField]
    private Texture2D halfHealthImg;
    [SerializeField]
    private Texture2D noHealthImg;
    [SerializeField]
    private GameObject healthImage;

    private List<RawImage> hearts;

    public void CreateHearts(int noHearts)
    {
        hearts = new List<RawImage>();

        for (int i = 0; i < noHearts; i++)
        {
            RawImage newHeart = Instantiate(healthImage, transform).GetComponent<RawImage>();
            hearts.Add(newHeart);
        }
    }

    public void UpdateHealth(int newHealth)
    {
        int healthUpdated = newHealth;

        for (int i = 0; i < hearts.Count; i++)
        {
            Texture2D image = fullHealthImg;

            if (healthUpdated == 0)
            {
                image = noHealthImg;
            }

            if (healthUpdated == 1)
            {
                image = halfHealthImg;
                healthUpdated--;
            }

            if (healthUpdated >= 2)
            {
                healthUpdated -= 2;
            }
            
            hearts[i].texture = image;
        }
    }
}
