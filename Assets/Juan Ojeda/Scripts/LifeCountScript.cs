using UnityEngine;
using UnityEngine.UI;
public class LifeCountScript : MonoBehaviour
{
    [SerializeField] private HealthBarScrpt Health;
    [SerializeField] private InventoryBehaviour PlayerLifes;

    [SerializeField] private GameObject [] LifeIcons;
    [SerializeField] private int Lifes;
     public int life => Lifes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lifes = 3;         
        LivesUI();
    }

    // Update is called once per frame
    void Update()
    {
       if (PlayerLifes.playerLife <= 0 && Lifes > 0)
        {
            LoseLife();
        }
        LivesUI();
    }

    private void LivesUI()
    {
       
            for (int i = 0; i < LifeIcons.Length; i++)
            {
                LifeIcons[i].SetActive(i < Lifes); // Activamos/desactivamos iconos de vida
            }
    }

    private void LoseLife()
    {
        Lifes--;
        Debug.Log($"El jugador perdio una vida. Vidas restantes: {Lifes}");

        if ( Lifes > 0 )
        {
            PlayerLifes.ResetHealth();
        }
        else
        {
            Debug.Log("SinVidas");
        }
    }

}
