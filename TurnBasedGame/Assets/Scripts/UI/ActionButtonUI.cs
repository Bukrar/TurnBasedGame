using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private ActionBase actionBase;
    public void SetBaseAction(ActionBase actionBase)
    {
        this.actionBase = actionBase;
        textMeshPro.text = actionBase.GetActionName().ToUpper();

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(actionBase);
        });
    }

    public void UpdateSelectedVisual()
    {
        ActionBase selectAactionBase = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectAactionBase == actionBase);
    }
}
