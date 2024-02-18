using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] private List<Transform> SpawnPositionTransformList;
    [SerializeField] private Transform NextWaveSpawnPositionTransform;

    private State WaveManagerState;
    private int WaveCount;
    private float WaveSpawnTimer;
    private float EnemySpawnTimer;
    private int RemainingEnemySpawnAmount;
    private Vector3 SpawnPosition;
    

    private void Awake()
    {
        WaveCount = 0;
        WaveSpawnTimer = 3f;
        EnemySpawnTimer = 0f;
        RemainingEnemySpawnAmount = 0;

        SpawnPosition = SpawnPositionTransformList[Random.Range(0, SpawnPositionTransformList.Count)].position;
        NextWaveSpawnPositionTransform.position = SpawnPosition; 
    }

    private void Start()
    {
        WaveManagerState = State.WaitingToSpawnNextWave;   
    }

    private void Update()
    {
        switch (WaveManagerState)
        {
            case State.WaitingToSpawnNextWave:
                WaveSpawnTimer -= Time.deltaTime;
                if (WaveSpawnTimer <= 0)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (RemainingEnemySpawnAmount > 0)
                {
                    EnemySpawnTimer -= Time.deltaTime;
                    if (EnemySpawnTimer <= 0)
                    {
                        EnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.CreateEnemy(SpawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                        RemainingEnemySpawnAmount--;
                    }
                }
                else
                {
                    WaveManagerState = State.WaitingToSpawnNextWave;

                    SpawnPosition = SpawnPositionTransformList[Random.Range(0, SpawnPositionTransformList.Count)].position;
                    NextWaveSpawnPositionTransform.position = SpawnPosition;
                    WaveSpawnTimer = WaveCount % 5 == 0 ? 20f : 15f;
                }
                break;
        }

        

       
        

    }
    private void SpawnWave()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.EnemyWaveStarting);
        RemainingEnemySpawnAmount = 5 + 3 * WaveCount;
        WaveManagerState = State.SpawningWave;
        WaveCount++;
        if(OnWaveNumberChanged != null)
        {
            OnWaveNumberChanged(this, EventArgs.Empty);
        }
    }

    public int GetWaveCount()
    {
        return WaveCount;
    }

    public float GetWaveSpawnTimer()
    {
        return WaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return SpawnPosition;
    }
}
