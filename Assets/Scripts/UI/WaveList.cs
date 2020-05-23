using System.Collections;
using System.Collections.Generic;
using TD.AI;
using TD.Managers;
using UnityEngine;
using UnityEngine.UI;

public class WaveList : MonoBehaviour
{
    Wave[] waves;
    public RectTransform waveInfoPrefab;
    public RectTransform mobTextPrefab;

    RectTransform[] waveInfos;

    public Image YellowShield;
    public Image RedShield;
    public Image GreyShield;

    private void Start()
    {
        LevelManager manager = GetComponentInParent<LevelManager>();
        waves = manager.GetWaves();

        waveInfos = new RectTransform[waves.Length];
        for (int i = 0; i < waves.Length; i++)
        {
            CreateWaveInfo(i);
        }
    }

    void CreateWaveInfo(int order)
    {
        float height = waveInfoPrefab.rect.height;

        RectTransform WaveInfo = Instantiate(waveInfoPrefab, transform);

        WaveInfo.anchoredPosition = new Vector2(0, (height) * (order + .5f));

        WaveInfo.GetComponentInChildren<Text>().text = "Wave " + (order + 1).ToString();

        waveInfos[order] = WaveInfo;

        List<MobQuantity> mobs = waves[order].Mobs;

        for (int i = 0; i < mobs.Count; i++)
        {
            float mobTextHeight = mobTextPrefab.rect.height;
            RectTransform mobText = Instantiate(mobTextPrefab, WaveInfo);

            mobText.anchoredPosition = new Vector2(81, ((-mobTextHeight) * (i + 1)) - 10f);

            //get text component, assign string value

            Text targetText = mobText.GetComponent<Text>();
            targetText.text = mobs[i].Quantity + "x " + mobs[i].Mob.name;

            DamageType[] immunities = mobs[i].Mob.GetComponent<Health>().immunities;

            if(immunities.Length > 0)
            {
                for (int immunity = 0; immunity < immunities.Length; immunity++)
                {
                    Image icon = null;
                    if(immunities[immunity] == DamageType.Explosive)
                    {
                        icon = Instantiate(YellowShield, mobText.transform);
                        
                    }
                    else if(immunities[immunity] == DamageType.Kinetic)
                    {
                        icon = Instantiate(GreyShield, mobText.transform);
                    }
                    else if(immunities[immunity] == DamageType.Thermal)
                    {
                        icon = Instantiate(RedShield, mobText.transform);
                    }

                    if(icon != null)
                    {
                        icon.rectTransform.anchoredPosition = new Vector3((-icon.rectTransform.rect.width * 4.25f) + (icon.rectTransform.rect.width * immunity), -icon.rectTransform.rect.height, 0);
                    }
                    
                }
            }
            
        }
    }

    public void ClearWave(int wave)
    {
        if(wave < 0)
        {
            Debug.LogError("Invalid wave index of " + wave);
            return;
        }

        if(wave >= waveInfos.Length || waveInfos.Length <= 0)
        {
            Debug.LogWarning("Cannot clear wave info for a wave not stored");
            return;
        }

        Destroy(waveInfos[wave].gameObject);

        if(wave + 1 < waveInfos.Length)
        {
            int newOrder = 0;
            for (int i = wave + 1; i < waveInfos.Length; i++)
            {
                waveInfos[i].anchoredPosition = new Vector2(0, (waveInfoPrefab.rect.height) * (newOrder + .5f));
                newOrder++;
            }
        }
    }
}
