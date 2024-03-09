using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    public static UICanvas Instance;
    [SerializeField] private TextMeshProUGUI _healthPoints;
    [SerializeField] private TextMeshProUGUI _gamePoints;
    [SerializeField] private GameObject _loseObject;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateHealthPoints(int healthPoints)
    {
        _healthPoints.text = healthPoints.ToString();
    }
    public void UpdateGamePoints(int gamePoints)
    {
        _gamePoints.text = gamePoints.ToString();
    }

    public void BTNRestart()
    {
        LevelManager.Instance.StartGame();
    }

    public void LoseUIActive(bool active) => _loseObject.SetActive(active);
}
