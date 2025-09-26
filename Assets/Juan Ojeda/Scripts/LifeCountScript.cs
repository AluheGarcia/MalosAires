using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LifeCountScript : MonoBehaviour
{
    
    [SerializeField] private PlayerLife PlayerLifes;

    [SerializeField] private GameObject [] LifeIcons;
    [SerializeField] private int Lifes;

    [SerializeField] private GameObject DefeatScreen;

     public int life => Lifes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        DefeatScreen.SetActive(false);
        Lifes = 3;         
        LivesUI();
    }

    // Update is called once per frame
    void Update()
    {
       if (PlayerLifes.hitsSuported <= 0 && Lifes > 0)
        {
            LoseLife();
        }
        LivesUI();

        if (Lifes <= 0)
        {

            DefeatScreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    private void LivesUI()
    {
       
            for (int i = 0; i < LifeIcons.Length; i++)
            {
                LifeIcons[i].SetActive(i < Lifes); 
            }
    }

    private void LoseLife()
    {
        Lifes--;
        Debug.Log($"El jugador perdio una vida. Vidas restantes: {Lifes}");

        if ( Lifes > 0 && PlayerLifes.hitsSuported <= 0)
        {
            PlayerLifes.ResetHits();
        }
        else
        {
            Debug.Log("SinVidas");
        }
    }

  
    public void ContinueGame()
    {
        
        Debug.Log("Continuar juego");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ExitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Inicio");
    }

}
