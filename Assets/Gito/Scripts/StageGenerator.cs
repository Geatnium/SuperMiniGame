using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundPrfab;
    private GameObject[] grounds;
    private EnemyGenerator[] enemyGenerators;
    int num = 3;
    int currentNum = 0;

    private float _stageSpeed = 0.0f;
    public float stageSpeed
    {
        set { _stageSpeed = value; }
    }

    public void Init()
    {
        grounds = new GameObject[num];
        enemyGenerators = new EnemyGenerator[num];
        for(int i = 0; i < num; i++)
        {
            grounds[i] = Instantiate(groundPrfab, transform);
            grounds[i].transform.position = new Vector3(i * 20.0f, 0.0f, 0.0f);
            enemyGenerators[i] = grounds[i].GetComponent<EnemyGenerator>();
        }
        enemyGenerators[0].NoneEnemy();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < num; i++)
        {
            grounds[i].transform.position -= new Vector3(_stageSpeed, 0.0f, 0.0f) * Time.fixedDeltaTime;
        }
        if (grounds[currentNum].transform.position.x <= -20f)
        {
            grounds[currentNum].transform.position = new Vector3(20.0f * (num - 1), 0.0f, 0.0f);
            enemyGenerators[currentNum].Generate();
            currentNum++;
            if (currentNum == num)
            {
                currentNum = 0;
            }
        }
    }
}
