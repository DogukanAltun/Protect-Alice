using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button DestroyDoor;
    public Button BuildDoor;
    public Button CollapseButton;
    public Button RevealButton;
    public Button BrotherHideButton;
    public Button PlayerHideButton;
    public GameObject GameOverPanel;
    public GameObject NextLevelPanel;
    public Text Score;
    public int levelIndex;
    public bool Stopped;
    public DoorController controller;
    public Littlebrother brother;
    public PlayerController player;
    public static CanvasManager instance;

    private void Awake()
    {
        instance = this;
        levelIndex = PlayerPrefs.GetInt("LevelIndex");
    }
    private void Start()
    {
        controller = FindObjectOfType<DoorController>();
        brother = FindObjectOfType<Littlebrother>();
    }
    public void SetButton()
    {
        if(controller.closestDoor.GetComponent<PartedDoor>().DoorStatus.doorStatus == Enum.DoorStatus.Builted)
        {
            DestroyDoor.gameObject.SetActive(true);
            BuildDoor.gameObject.SetActive(false);
        }
        else
        {
            DestroyDoor.gameObject.SetActive(false);
            BuildDoor.gameObject.SetActive(true);
        }
        if(!brother.brotherhiding && !player.playerHiding)
        {
          BrotherHideButton.gameObject.SetActive(true);
          PlayerHideButton.gameObject.SetActive(false);
          CollapseButton.gameObject.SetActive(false);
          RevealButton.gameObject.SetActive(false);
        } 
        else if(brother.brotherhiding && !player.playerHiding)
        {
          BrotherHideButton.gameObject.SetActive(false);
          PlayerHideButton.gameObject.SetActive(true);
          CollapseButton.gameObject.SetActive(true);
          RevealButton.gameObject.SetActive(false);
        }
        else if (brother.brotherhiding && player.playerHiding)
        {
            BrotherHideButton.gameObject.SetActive(false);
            PlayerHideButton.gameObject.SetActive(false);
            CollapseButton.gameObject.SetActive(true);
            RevealButton.gameObject.SetActive(true);
        }
    }

    public void GameOverCanvas()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        PlayerPrefs.SetInt("LevelIndex", levelIndex);
    }

    public void NextLevelCanvas()
    {
        Time.timeScale = 0;
        NextLevelPanel.SetActive(true);
        levelIndex++;
        PlayerPrefs.SetInt("LevelIndex", levelIndex);
    }

    public void LoadScene()
    {
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        NextLevelPanel.SetActive(false);
        SceneManager.LoadScene(1);
        Stopped = true;
    }

}
