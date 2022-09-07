using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Image sceneTransitionImage;
    public float secondsToFade = 0.25f;
    public Transform gameOverText;

    public Ease textEase;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        sceneTransitionImage.DOFade(1, 0);
        sceneTransitionImage.DOFade(0, secondsToFade);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        gameOverText.DOScale(1, 1).SetEase(textEase);
        yield return new WaitForSeconds(2f);
        sceneTransitionImage.DOFade(1, secondsToFade);
        yield return new WaitForSeconds(secondsToFade * 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
