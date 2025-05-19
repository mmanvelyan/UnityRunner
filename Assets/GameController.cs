using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController playerController;
    private RoadGenerator roadGenerator;
    public Canvas canvasMenu;
    public Canvas canvasCreator;
    public Canvas canvasRestart;
    public Text score;

    void Start()
    {
        canvasMenu.gameObject.SetActive(true);
        canvasCreator.gameObject.SetActive(false);
        canvasRestart.gameObject.SetActive(false);
        playerController = FindFirstObjectByType<PlayerController>();
        roadGenerator = GetComponent<RoadGenerator>();
        playerController.StopPlayer();
        roadGenerator.StopLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (roadGenerator.running)
        {
            score.text = (int.Parse(score.text) + 1).ToString();
        }
    }

    public void StartGame()
    {
        score.text = "0";
        canvasMenu.gameObject.SetActive(false);
        canvasRestart.gameObject.SetActive(false);
        playerController.StartPlayer();
        roadGenerator.StartLevel();
    }

    public void StopGame()
    {
        canvasRestart.gameObject.SetActive(true);
        playerController.StopPlayer();
        roadGenerator.StopLevel();
    }

    public void showCreator()
    {
        canvasMenu.gameObject.SetActive(false);
        canvasCreator.gameObject.SetActive(true);
    }

    public void hideCreator()
    {
        canvasMenu.gameObject.SetActive(true);
        canvasCreator.gameObject.SetActive(false);
    }
}
