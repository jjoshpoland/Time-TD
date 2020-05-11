using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TD.Towers;
using TD.Managers;

public class TowerMenu : MonoBehaviour
{

    public Button UpgradeButton1;
    public Button UpgradeButton2;

    public Button SellButton;

    Tower tower;
    Turret Turret1;
    Turret Turret2;

    public void Init(Tower tower)
    {
        this.tower = tower;
        this.Turret1 = tower.GetUpgradePath(0);
        this.Turret2 = tower.GetUpgradePath(1);

        UpgradeButton1.GetComponentInChildren<Text>().text = Turret1.name;
        UpgradeButton2.GetComponentInChildren<Text>().text = Turret2.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Sell()
    {
        ClockManager.instance.AddTime(tower.Cost / 2f);
        tower.Sell();
        Destroy(gameObject);
    }
}
