using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] Button endTurnBtn;
    [SerializeField] TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGO;

    private void Start()
    {
        endTurnBtn.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }
    private void UpdateTurnText()
    {
        int turnNumber = TurnSystem.Instance.GetTurnNumber();
        turnNumberText.text = "TURN " + turnNumber;
    }    

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGO.SetActive(!TurnSystem.Instance.IsOnPlayerTurn());
    }

    private void UpdateEndTurnButtonVisibility()
    {
        endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsOnPlayerTurn());
    }
}
