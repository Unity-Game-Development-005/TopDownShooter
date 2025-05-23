
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // create a single for the game controller
    public static GameController gameController;


    public Image panel;

    public TMP_Text playerOneScoreText;

    public TMP_Text playerTwoScoreText;

    public TMP_Text gameOverText;

    public TMP_Text startGameText;


    public int playerOneScore;

    public int playerTwoScore;




    public bool gameOver;


    public const int AMMO = 1;

    public const int AMMO_PICKUP = 1;

    public const int DAMAGE = 1;

    public const int MAXIMUM_DAMAGE = 10;

    public const int HEALTH_PICKUP = 1;

    public const int MAXIMUM_HEALTH = 10;

    public const int IS_DEAD = 0;



    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
        }

        else if (gameController != this)
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        Initialise();

        GameOver();
    }


    private void Update()
    {
        WaitForSpaceBar();
    }


    private void WaitForSpaceBar()
    {
        // if we press the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // clear the game over elements
            gameOverText.gameObject.SetActive(false);

            startGameText.gameObject.SetActive(false);

            panel.gameObject.SetActive(false);

            gameOver = false;

            // and start the game
            Time.timeScale = 1;
        }
    }


    private void Initialise()
    {
        gameOver = true;

        playerOneScore = 0;

        playerTwoScore = 0;
    }


    public void GameOver()
    {
        // pause the game play
        Time.timeScale = 0;

        // set game over flag
        gameOver = true;

        // set the game over elements
        gameOverText.gameObject.SetActive(true);

        startGameText.gameObject.SetActive(true);

        panel.gameObject.SetActive(true);
    }


    public void UpdatePlayer1Score()
    {
        playerOneScore++;

        playerOneScoreText.text = playerOneScore.ToString();
    }


    public void UpdatePlayer2Score()
    {
        playerTwoScore++;

        playerTwoScoreText.text = playerTwoScore.ToString();
    }


} // end of class
