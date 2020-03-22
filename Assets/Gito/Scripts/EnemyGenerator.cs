using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject _enemy;

    private GameObject enemy
    {
        get
        {
            return _enemy == null ? _enemy = transform.GetChild(1).gameObject : _enemy;
        }
    }

    public void Generate()
    {
        int r = RandomWeigt(new float[] { 0.5f, 0.3f, 0.2f });
        switch (r)
        {
            case 0:
                enemy.SetActive(true);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                enemy.transform.localPosition = new Vector3(Random.Range(11.0f, 19.0f), 0f, 0f);
                break;
            case 1:
                enemy.SetActive(false);
                transform.localScale = new Vector3(0.85f, 1.0f, 1.0f);
                break;
            case 2:
                enemy.SetActive(false);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            default:
                break;
        }
    }

    public void NoneEnemy()
    {
        enemy.SetActive(false);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private int RandomWeigt(float[] weights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            float r = Random.value;
            if (r < weights[i])
            {
                return i;
            }
            if (i == weights.Length - 1)
            {
                i = 0;
                continue;
            }
        }
        return 0;
    }
}
