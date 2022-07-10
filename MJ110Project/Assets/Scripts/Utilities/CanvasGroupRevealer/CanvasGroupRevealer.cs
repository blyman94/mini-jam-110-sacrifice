using System.Collections;
using UnityEngine;

/// <summary>
/// Utility to show and hide CanvasGroups representing menus in the game's UI.
/// Works very cleanly with GameEventListeners.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupRevealer : MonoBehaviour
{
    /// <summary>
    /// Should this CanvasGroup start hidden?
    /// </summary>
    [Tooltip("Should this CanvasGroup start hidden?")]
    [SerializeField] private bool startHidden = false;

    /// <summary>
    /// Should this canvas group fade in?
    /// </summary>
    [Tooltip("Should this canvas group fade in?")]
    public bool CanvasFadeIn;

    /// <summary>
    /// Should this canvas group fade out?
    /// </summary>
    [Tooltip("Should this canvas group fade out?")]
    public bool CanvasFadeOut;

    /// <summary>
    /// How long should it take for this canvas to fade in or out?
    /// </summary>
    [Tooltip("How long should it take for this canvas to fade in or out?")]
    [SerializeField] private float fadeTime;

    /// <summary>
    /// Animation curve depicting the pattern of the fade.
    /// </summary>
    [Tooltip("Animation curve depicting the pattern of the fade.")]
    [SerializeField] private AnimationCurve fadeCurve;

    /// <summary>
    /// The alpha value this CanvasGroup will have when in the shown state.
    /// </summary>
    [Tooltip("The alpha value this CanvasGroup will have when in the shown " +
        "state")]
    [SerializeField, Range(0.0f, 1.0f)] private float shownAlpha = 1;

    /// <summary>
    /// The BlockRaycast value this CanvasGroup will have when in the 
    /// shown state.
    /// </summary>
    [Tooltip("The BlockRaycast value this CanvasGroup will have when in the" +
        "shown state.")]
    [SerializeField] private bool shownBlockRaycast = true;

    /// <summary>
    /// The Interactable value this Canvas group will have when in 
    /// the shown state.
    /// </summary>
    [Tooltip("The Interactable value this Canvas group will have when in the " +
        "shown state.")]
    [SerializeField] private bool shownInteractable = true;

    /// <summary>
    /// CanvasGroup component to be shown and hidden.
    /// </summary>
    private CanvasGroup canvasGroup;

    /// <summary>
    /// Current active coroutine for proper cancellation.
    /// </summary>
    private IEnumerator activeCoroutine;

    #region Properties
    /// <summary>
    /// Tracks whether the Canvas group is currently shown or hidden.
    /// </summary>
    public bool Shown { get; set; } = true;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (startHidden)
        {
            Shown = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
    #endregion

    /// <summary>
    /// Hides the CanvasGroup. Alpha is set to zero, BlockRaycasts is set to 
    /// false and Interactable is set to false.
    /// </summary>
    public void HideGroup()
    {
        if (CanvasFadeOut)
        {
            FadeOut();
        }
        else
        {
            Shown = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    /// <summary>
    /// Shows the CanvasGroup. Assigns shown state parameters Alpha,
    /// BlockRaycast and Interactable.
    /// </summary>
    public void ShowGroup()
    {
        if (CanvasFadeIn)
        {
            FadeIn();
        }
        else
        {
            Shown = true;
            canvasGroup.alpha = shownAlpha;
            canvasGroup.blocksRaycasts = shownBlockRaycast;
            canvasGroup.interactable = shownInteractable;
        }
    }

    /// <summary>
    /// Shows the CanvasGroup if its hidden, hides the canvas group if its 
    /// shown.
    /// </summary>
    public void ToggleGroup()
    {
        if (Shown)
        {
            HideGroup();
        }
        else
        {
            ShowGroup();
        }
    }

    public void FadeIn()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        activeCoroutine = FadeInGroupRoutine();
        StartCoroutine(activeCoroutine);
    }

    public void FadeOut()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        activeCoroutine = FadeOutGroupRoutine();
        StartCoroutine(activeCoroutine);
    }

    public IEnumerator FadeInGroupRoutine()
    {
        float elapsedTime = 0.0f;
        float currentAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, shownAlpha,
                fadeCurve.Evaluate(elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = shownAlpha;

        Shown = true;
        canvasGroup.blocksRaycasts = shownBlockRaycast;
        canvasGroup.interactable = shownInteractable;
    }

    public IEnumerator FadeOutGroupRoutine()
    {
        float elapsedTime = 0.0f;
        float currentAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, 0.0f,
                fadeCurve.Evaluate(elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Shown = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}