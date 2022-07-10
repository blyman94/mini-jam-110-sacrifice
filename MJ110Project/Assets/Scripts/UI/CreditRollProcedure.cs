using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRollProcedure : MonoBehaviour
{
    [SerializeField] private CanvasGroupRevealer gameOverScreen;
    [SerializeField] private List<CanvasGroupRevealer> creditSlides;
    [SerializeField] private float creditShowTime;
    [SerializeField] private float timeBetweenCredits;

    private WaitForSeconds waitTime;
    private WaitForSeconds showTime;

    private void Start()
    {
        waitTime = new WaitForSeconds(timeBetweenCredits);
        showTime = new WaitForSeconds(creditShowTime);
    }

    public void StartCreditRoll()
    {
        StartCoroutine(CreditRollRoutine());
    }

    private IEnumerator CreditRollRoutine()
    {
        yield return gameOverScreen.FadeInGroupRoutine();

        yield return creditSlides[0].FadeInGroupRoutine();
        yield return new WaitForSeconds(10);
        yield return creditSlides[0].FadeOutGroupRoutine();


        for (int i = 1; i < creditSlides.Count - 1; i++)
        {
            yield return waitTime;
            yield return creditSlides[i].FadeInGroupRoutine();
            yield return showTime;
            yield return creditSlides[i].FadeOutGroupRoutine();
        }

        yield return waitTime;
        yield return creditSlides[creditSlides.Count - 1].FadeInGroupRoutine();
    }
}
