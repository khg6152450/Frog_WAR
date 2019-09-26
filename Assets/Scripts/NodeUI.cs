using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui1;
    public GameObject ui2;
    public GameObject ui3;
    public GameObject ui = null;
    public bool level2 = false;
    public bool level3 = false;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;
    // target은 선택된 노드 위치를 말함

    private Transform playerTarget;

    public Transform partToRotate;
    public float turnSpeed = 10f;

    void Start()
    {
        if(level2 == true)
        {
            ui = ui2;
        } else if(level3 == true)
        {
            ui = ui3;
        } else
        {
            ui = ui1;
        }
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();
        
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true); // 노드 클릭 시 UI 보이게
    }

    public void Hide()
    {
        ui.SetActive(false); // 노드 클릭 시 UI 안 보이게
    }

    public void Upgrade()
    {
        Debug.Log("upgrade 클릭1");
        target.UpgradeTurret();
        Debug.Log("upgrade 클릭3");
        BuildManager.instance.DeselectNode();
        Debug.Log("upgrade 클릭4");
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
