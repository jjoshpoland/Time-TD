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
    public Text TowerTitle;

    public Button SellButton;

    Tower tower;
    Turret Turret1;
    Turret Turret2;

    

    public void Init(Tower tower)
    {
        this.tower = tower;
        this.Turret1 = tower.GetUpgradePath(0);
        this.Turret2 = tower.GetUpgradePath(1);

        TowerTitle.text = tower.name;

        if(Turret2 == null)
        {
            Destroy(UpgradeButton2.gameObject);
        }
        else
        {
            Text towerText = UpgradeButton2.GetComponentInChildren<Text>();
            towerText.text = Turret2.name + " (" + Turret2.Cost + ")";
            towerText.color = Turret2.DamageColor;
        }

        if(Turret1 == null)
        {
            Destroy(UpgradeButton1.gameObject);
        }
        else
        {
            UpgradeButton1.GetComponentInChildren<Text>().text = Turret1.name + " (" + Turret1.Cost + ")";
        }

        
        

        
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

    public void Upgrade(int index)
    {
        if(index == 0 && Turret1 != null)
        {
            ClockManager.instance.RemoveTime(tower.GetUpgradePath(index).Cost);
            tower.Upgrade(index);
            
        }
        else if(index == 1 && Turret2 != null)
        {
            ClockManager.instance.RemoveTime(tower.GetUpgradePath(index).Cost);
            tower.Upgrade(index);
            
        }
        
        Destroy(gameObject);
    }
}
