using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using TMPro;
using System;

public class IntroManagerDotween : MonoBehaviour
{
    private enum IntroState
    {
        LogoReveal,
        GameTitleReveal, // Etapa final: Título/Menú
        MenuReady,
        LoadingGame
    }

    [Header("Referencias UI")]
    [SerializeField] private FaderDotween fader;
    [SerializeField] private Image teamLogoImage;
    [SerializeField] private Image gameTitleImage;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip logoSound;
    [SerializeField] private AudioClip menuMusic;

    [Header("Tiempos")]
    [SerializeField] private float logoDisplayTime = 3f;

    private IntroState currentState = IntroState.LogoReveal;

    private void Start()
    {
        DOTween.Init();
        // Desactiva todos los elementos de menú e intro al inicio
        menuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        teamLogoImage.gameObject.SetActive(false); // CORREGIDO
        gameTitleImage.gameObject.SetActive(false); // CORREGIDO

        StartLogoReveal();
    }

    // --- Secuencia de Animación ---

    // 1. Muestra el logo del equipo
    private void StartLogoReveal()
    {
        currentState = IntroState.LogoReveal;
        teamLogoImage.gameObject.SetActive(true); // CORREGIDO
        audioSource.PlayOneShot(logoSound);

        // Al terminar el FadeIn (revela logo), espera y luego hace FadeOut (fundido a negro)
        fader.SetOnFadeComplete(() => {
            StartCoroutine(WaitAndFadeOut(logoDisplayTime, ShowMenu));
        });

        fader.FadeIn();
    }

    // 2. Función de ayuda: Espera un tiempo y luego realiza el FadeOut
    private IEnumerator WaitAndFadeOut(float delay, Action nextAction)
    {
        yield return new WaitForSeconds(delay);

        // Al terminar el fundido a negro (FadeOut), ejecuta la siguiente acción (ShowMenu)
        fader.SetOnFadeComplete(() => {
            nextAction?.Invoke();
        });
        fader.FadeOut();
    }

    // 3. Muestra el título y el menú (pantalla final)
    private void ShowMenu()
    {
        currentState = IntroState.GameTitleReveal;
        teamLogoImage.gameObject.SetActive(false); // CORREGIDO

        // 1. Activar los elementos visuales finales
        gameTitleImage.gameObject.SetActive(true); // CORREGIDO
        menuPanel.SetActive(true);

        // 2. Tocar música
        audioSource.loop = true;
        audioSource.clip = menuMusic;
        audioSource.Play();

        // 3. Animación de aparición (DOTween)
        gameTitleImage.transform.localScale = Vector3.zero;
        gameTitleImage.transform.DOScale(1f, 1f).SetEase(Ease.OutExpo);

        menuPanel.transform.localScale = Vector3.zero;
        menuPanel.transform.DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetDelay(0.5f);

        // 4. Fundido a transparente final para revelar todo
        fader.SetOnFadeComplete(() => {
            currentState = IntroState.MenuReady;
            CanvasGroup menuGroup = menuPanel.GetComponent<CanvasGroup>();
            if (menuGroup != null) menuGroup.interactable = true;
        });

        fader.FadeIn();
    }

    // --- Lógica de Transición de Paneles (Controles/Créditos) ---

    public void OnControlsClicked()
    {
        if (currentState != IntroState.MenuReady) return;
        AnimatePanelSwitch(menuPanel, controlsPanel);
    }

    public void OnCreditsClicked()
    {
        if (currentState != IntroState.MenuReady) return;
        AnimatePanelSwitch(menuPanel, creditsPanel);
    }

    public void OnBackFromControlsClicked()
    {
        AnimatePanelSwitch(controlsPanel, menuPanel);
    }

    public void OnBackFromCreditsClicked()
    {
        AnimatePanelSwitch(creditsPanel, menuPanel);
    }

    // Método encapsulado para animar la transición entre dos paneles
    private void AnimatePanelSwitch(GameObject panelToHide, GameObject panelToShow)
    {
        CanvasGroup groupToHide = panelToHide.GetComponent<CanvasGroup>();
        if (groupToHide != null) groupToHide.interactable = false;

        panelToHide.transform.DOScale(0f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                panelToHide.SetActive(false);

                panelToShow.SetActive(true);
                panelToShow.transform.localScale = Vector3.zero;

                CanvasGroup groupToShow = panelToShow.GetComponent<CanvasGroup>();
                if (groupToShow != null) groupToShow.interactable = false;

                panelToShow.transform.DOScale(1f, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        if (groupToShow != null) groupToShow.interactable = true;
                    });
            });
    }

    // --- Carga y Salida ---

    public void OnStartClicked()
    {
        if (currentState != IntroState.MenuReady) return;
        currentState = IntroState.LoadingGame;

        menuPanel.SetActive(false);
        gameTitleImage.gameObject.SetActive(false); // CORREGIDO

        fader.SetOnFadeComplete(() => {
            StartCoroutine(LoadGameSceneAsync("GameScene"));
        });

        fader.FadeOut(); // Fundido a negro para ocultar la carga
    }

    // Método de carga asíncrona (Corrutina)
    private IEnumerator LoadGameSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }

    public void OnExitClicked()
    {
        if (currentState != IntroState.MenuReady) return;

        DOTween.KillAll();

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}