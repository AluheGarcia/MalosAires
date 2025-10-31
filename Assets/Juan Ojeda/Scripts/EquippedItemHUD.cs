using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class EquippedItemHUD : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text amountText;
        

    private Sprite currentIcon;
    private int currentAmount;

    void Start()
    {
        ClearDisplay();
    }

    public void UpdateDisplay(Sprite icon, int amount)
    {
       
        if (icon == null || amount <=0)
        {
            ClearDisplay();
            return;
        }

        currentIcon = icon;
        currentAmount = amount;

        iconImage.enabled = true;
        amountText.enabled = true;
        iconImage.sprite = currentIcon;
        amountText.text = "X" + currentAmount.ToString();
    }

    public void ClearDisplay()
    {
        iconImage.enabled = false;
        amountText.enabled = false;
        currentIcon = null;
        currentAmount = 0;
    }
}
