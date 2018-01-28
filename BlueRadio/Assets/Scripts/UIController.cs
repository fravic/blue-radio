
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

  const float GAME_TIME = 60 * 3;

  public Text moneyLabel;
  public Text blueScoreLabel;
  public Text redScoreLabel;
  public Text timerLabel;
  public GameObject blueBar;
  public GameObject redBar;
  public Button aggressiveUnitBtn;
  public Button constructionUnitBtn;

  float gameStartTime;

  public void Start() {
    aggressiveUnitBtn.onClick.AddListener(AddAggressiveUnit);
    constructionUnitBtn.onClick.AddListener(AddConstructionUnit);
  }

  public void Update() {
    // Only enable buttons if we have enough money
    if (PlayerManager.Instance.money < PlayerManager.UNIT_COST) {
      aggressiveUnitBtn.interactable = false;
      constructionUnitBtn.interactable = false;
    }

    // Update money indicator
    moneyLabel.text = "$" + PlayerManager.Instance.money;

    // Update influence indicators
    int blueInf = GameManager.Instance.BlueInfluence;
    int redInf = GameManager.Instance.RedInfluence;
    int totalInf = GameManager.Instance.housesList.Count;

    blueScoreLabel.text = blueInf + "";
    redScoreLabel.text = redInf + "";
    blueBar.transform.localScale = new Vector3(blueInf / (float)totalInf, 1, 1);
    redBar.transform.localScale = new Vector3(redInf / (float)totalInf, 1, 1);

    // Update timer
    float timeLeft = Mathf.Max(GAME_TIME - (Time.time - gameStartTime), 0);
    string minutesStr = Mathf.FloorToInt(timeLeft / 60) + "";
    int seconds = Mathf.FloorToInt(timeLeft % 60);
    string secondsStr = seconds < 10 ? "0" + seconds : seconds + "";
    timerLabel.text = minutesStr + ":" + secondsStr;
  }

  private void AddAggressiveUnit() {
    PlayerManager.Instance.SpawnAggressiveUnit();
  }

  private void AddConstructionUnit() {
    PlayerManager.Instance.SpawnConstructionUnit();
  }
}