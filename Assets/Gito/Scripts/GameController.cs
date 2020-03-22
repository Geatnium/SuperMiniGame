using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private PlayerDeath playerDeath;
    private StageGenerator stageGenerator;

    private bool isPlaying = false;
    private bool isEnd = false;

    [SerializeField] private float initSpeed = 5.0f;
    private float speedUpTime = 0.0f;
    [SerializeField] private float speedUpInterval = 1.0f;
    [SerializeField] private float speedUpScale = 0.05f;
    private float timeScale = 1.0f;

    [SerializeField] private GameObject startScreen, scoreScreen, gameOverScreen, gameOver_restart;
    [SerializeField] private Text scoreNumText, gameOverScoreText;
    [SerializeField] private int scoreUpBase = 10;
    private float score = 0;
    private float endTime = 0;

    private void Awake()
    {
        Application.targetFrameRate = 24;
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject stage = GameObject.FindWithTag("Stage");
        playerAnimation = player.GetComponent<PlayerAnimation>();
        playerController = player.GetComponent<PlayerController>();
        playerDeath = player.GetComponent<PlayerDeath>();
        stageGenerator = stage.GetComponent<StageGenerator>();
        stageGenerator.Init();

        playerController.enabled = false;
    }

    private void Update()
    {
        if (isEnd) {
            endTime += Time.deltaTime;
            if(endTime > 2.0f)
            {
                gameOver_restart.SetActive(true);
                if (Input.GetMouseButtonUp(0))
                {
                    Restart();
                }
            }
            return;
        }
        if (!isPlaying)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameStart();
            }
            return;
        }

        speedUpTime += Time.deltaTime;
        if(speedUpTime >= speedUpInterval)
        {
            timeScale += speedUpScale;
            playerAnimation.animationSpeed = timeScale;
            stageGenerator.stageSpeed = initSpeed * timeScale;
            speedUpTime = 0.0f;
        }

        score += scoreUpBase * timeScale * Time.deltaTime;
        scoreNumText.text = ((int)score).ToString();
        
    }

    public void GameStart()
    {
        isPlaying = true;
        playerController.enabled = true;
        stageGenerator.stageSpeed = initSpeed;
        startScreen.SetActive(false);
        scoreScreen.SetActive(true);
    }

    public void GameEnd()
    {
        playerAnimation.DoDeath();
        playerDeath.enabled = false;
        playerController.enabled = false;
        playerAnimation.enabled = false;
        stageGenerator.stageSpeed = 0.0f;
        isPlaying = false;
        isEnd = true;
        scoreScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScoreText.text = ((int)score).ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
