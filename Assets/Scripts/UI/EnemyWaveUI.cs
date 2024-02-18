using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager WaveManager;

    private Camera MainCamera;
    private TextMeshProUGUI WaveNumberText;
    private TextMeshProUGUI WaveMessageText;
    private RectTransform WaveSpawnPositionIndicatorRectTranform;
    private RectTransform ClosestEnemyIndicatorRectTransform;

    private void Awake()
    {
        WaveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        WaveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        WaveSpawnPositionIndicatorRectTranform = transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        ClosestEnemyIndicatorRectTransform = transform.Find("EnemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        MainCamera = Camera.main;

        WaveManager.OnWaveNumberChanged += WaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + WaveManager.GetWaveCount());
    }

    private void WaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + WaveManager.GetWaveCount());
    }

    private void Update()
    {
       HandleNextWaveMessage();

        HandleWaveSpawnPositionIndicator();
        HandleClosestEnemyIndicator();
    }

    private void HandleNextWaveMessage()
    {
        float NextWaveSpawnTimer = WaveManager.GetWaveSpawnTimer();
        if (NextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + NextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleWaveSpawnPositionIndicator()
    {

        Vector3 DirectionToNextSpawnPosition = (WaveManager.GetSpawnPosition() - Camera.main.transform.position).normalized;

        WaveSpawnPositionIndicatorRectTranform.anchoredPosition = DirectionToNextSpawnPosition * 300f;
        WaveSpawnPositionIndicatorRectTranform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(DirectionToNextSpawnPosition));

        float DistanceToNextSpawnPosition = Vector3.Distance(WaveManager.GetSpawnPosition(), MainCamera.transform.position);
        WaveSpawnPositionIndicatorRectTranform.gameObject.SetActive(DistanceToNextSpawnPosition > MainCamera.orthographicSize * 1.5f);
    }

    private void HandleClosestEnemyIndicator()
    {
        Enemy TargetEnemy = null;

        float TargetMaxRadius = 9999f;
        Collider2D[] Collider2DArray = Physics2D.OverlapCircleAll(MainCamera.transform.position, TargetMaxRadius);
        foreach (Collider2D Target in Collider2DArray)
        {
            Enemy CollidedEnemy = Target.GetComponent<Enemy>();

            if (CollidedEnemy != null)
            {
                if (TargetEnemy == null)
                {
                    TargetEnemy = CollidedEnemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, CollidedEnemy.transform.position) <
                        Vector3.Distance(transform.position, TargetEnemy.transform.position))
                    {
                        TargetEnemy = CollidedEnemy;
                    }
                }
            }
        }

        if (TargetEnemy != null)
        {
            Vector3 DirectionToClosestEnemy = (TargetEnemy.transform.position - Camera.main.transform.position).normalized;

            ClosestEnemyIndicatorRectTransform.anchoredPosition = DirectionToClosestEnemy * 150f;
            ClosestEnemyIndicatorRectTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(DirectionToClosestEnemy));

            float DistanceToClosestEnemy = Vector3.Distance(TargetEnemy.transform.position, MainCamera.transform.position);
            ClosestEnemyIndicatorRectTransform.gameObject.SetActive(DistanceToClosestEnemy > MainCamera.orthographicSize * 1.5f);
        }
        else
        {
            ClosestEnemyIndicatorRectTransform.gameObject.SetActive(false);
        }

    }

    private void SetMessageText(string Message)
    {
        WaveMessageText.SetText(Message);
    }

    private void SetWaveNumberText(string Text)
    {
        WaveNumberText.SetText(Text);
    }
}
