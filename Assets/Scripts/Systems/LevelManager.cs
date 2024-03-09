using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private ShipView _prefabPlayer;
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private WavesConfig _configWaves;
    public Transform spawnParent => _spawnParent;
    private int _gamePoints;
    private int _currentWave;
    private int _indexLevel;
    private Coroutine _waveCoroutine;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        UICanvas.Instance.LoseUIActive(false);
        ClearSpawnParent();
        ResetGamePoints();
        _currentWave = 0;
        _indexLevel = 1;
        Instantiate(_prefabPlayer);
        
        if (_waveCoroutine != null) 
            StopCoroutine(_waveCoroutine);
        _waveCoroutine = StartCoroutine(WaveTime());
    }

    public void PlayerLose()
    {
        UICanvas.Instance.LoseUIActive(true);
    }

    public void AddGamePoints(int points)
    {
        _gamePoints += points;
        UICanvas.Instance.UpdateGamePoints(_gamePoints);
    }

    public void ResetGamePoints()
    {
        _gamePoints = 0;
        UICanvas.Instance.UpdateGamePoints(_gamePoints);
    }

    IEnumerator WaveTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_configWaves.waves[_currentWave].delay);

            for (int i = 0; i < _configWaves.waves[_currentWave].count * _indexLevel; i++)
            {
                var waveObject = Instantiate(_configWaves.waves[_currentWave].prefab, _spawnParent);
                waveObject.transform.position = GameArea.Instance.GetRandomSpawnPoint();
                yield return new WaitForSeconds(_configWaves.waves[_currentWave].spawnRate / _indexLevel);
            }

            _currentWave++;
            if (_currentWave >= _configWaves.waves.Count)
            {
                _currentWave = 0;
                _indexLevel++;
            }
        }
    }

    private void ClearSpawnParent()
    {
        foreach (Transform child in _spawnParent)
        {
            Destroy(child.gameObject);
        }
    }
}