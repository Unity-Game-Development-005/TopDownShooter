
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // create a singleton for the game controller
    public static GameController gameController;


    public Image panel;

    public TMP_Text player1ScoreText;

    public TMP_Text player2ScoreText;

    public TMP_Text gameOverText;

    public TMP_Text startGameText;



    public const int AMMO = 1;


    public const int AMMO_PICKUP = 1;

    public const int MAXIMUM_AMMO = 6;


    public const int HEALTH_PICKUP = 1;

    public const int MAXIMUM_HEALTH = 10;


    public const int DAMAGE = 1;

    public const int MAXIMUM_DAMAGE = 10;

    public const int IS_DEAD = 0;


    [HideInInspector] public bool gameOver;



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
        GameOver();
    }


    private void Update()
    {
        WaitForSpaceBar();
    }


    private void WaitForSpaceBar()
    {
        if (gameOver)
        {
            // check to see if we press the space bar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // if we do
                // clear the game over elements
                gameOverText.gameObject.SetActive(false);

                startGameText.gameObject.SetActive(false);

                panel.gameObject.SetActive(false);

                RestartGame();
            }
        }
    }


    private void RestartGame()
    {
        // reset player 1
        Player1Controller.player1Controller.Initialise();

        // reset player 2
        Player2Controller.player2Controller.Initialise();

        UpdatePlayerScores();

        // destroy all spawned health pickups
        SpawnController.spawnController.DestroySpawnedObjects();


        // clear the game over flag
        gameOver = false;

        // and start the game
        Time.timeScale = 1;
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
        Player1Controller.player1Controller.player1Score++;

        UpdatePlayerScores();
    }


    public void UpdatePlayer2Score()
    {
        Player2Controller.player2Controller.player2Score++;

        UpdatePlayerScores();
    }


    private void UpdatePlayerScores()
    {
        player1ScoreText.text = Player1Controller.player1Controller.player1Score.ToString();

        player2ScoreText.text = Player2Controller.player2Controller.player2Score.ToString();
    }


} // end of class
