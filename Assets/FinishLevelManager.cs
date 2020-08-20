using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FinishLevelManager : MonoBehaviour
{
    [SerializeField] private float _slowMotionDuration;
    [SerializeField] private LevelManager _levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FinishLevel());
        }
    }

    private IEnumerator FinishLevel()
    {
        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(_slowMotionDuration);

        _levelManager.ShowWinModal();
    }
}