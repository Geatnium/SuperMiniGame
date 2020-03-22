using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    private GameController gameController { get { return GameObject.FindWithTag("GameController").GetComponent<GameController>(); } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameController.GameEnd();
        }
    }

    private void Update()
    {
        if (transform.position.y < -5.0f)
        {
            gameController.GameEnd();
        }
    }
}
