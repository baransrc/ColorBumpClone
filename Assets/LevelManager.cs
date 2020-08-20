using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string _nameOfNextLevel;
    [SerializeField] CanvasGroup _loseModal;
    [SerializeField] CanvasGroup _winModal;
    [SerializeField] GameObject _loseGo;
    [SerializeField] GameObject _winGo;
    [SerializeField] float _showDuration;

    private string _nameOfCurrentLevel;

    private void Awake()
    {
        _nameOfCurrentLevel = SceneManager.GetActiveScene().name;
    }

    public void ShowLoseModal()
    {
        StartCoroutine(ShowCanvasGroup(_loseModal, _loseGo));
    }

    public void ShowWinModal()
    {
        StartCoroutine(ShowCanvasGroup(_winModal, _winGo));
    }

    public void OnClickTryAgain()
    {
        _loseGo.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(_nameOfCurrentLevel);
    }

    public void OnClickNextLevel()
    {
        _winGo.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(_nameOfNextLevel);
    }

    private IEnumerator ShowCanvasGroup(CanvasGroup canvasGroup, GameObject gameObject)
    {
        var step = 0f;
        var durationInverse = 1f / _showDuration;
        canvasGroup.alpha = 0f;
        gameObject.SetActive(true);

        Time.timeScale = 0f;

        while (step < 1f)
        {
            step += durationInverse * Time.unscaledDeltaTime;
            canvasGroup.alpha = step;

            yield return null;
        }

        canvasGroup.alpha = step;
    }
}
