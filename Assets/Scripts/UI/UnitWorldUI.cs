using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionChanged += Unit_OnAnyActionChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    #region Events
    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }

    private void Unit_OnAnyActionChanged(object sender, System.EventArgs e)
    {
        UpdateActionPointsText();
    }
    #endregion


    #region Update UI Method
    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHeathNomalized();
    }
    #endregion
}
