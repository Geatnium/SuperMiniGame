using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimation.DoJump();
        }
    }
}
