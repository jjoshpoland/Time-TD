using System.Collections;
using System.Collections.Generic;
using TD.Managers;
using UnityEngine;
using UnityEngine.UI;

public class WaveList : MonoBehaviour
{
    Wave[] waves;
    public RectTransform waveInfoPrefab;
    public RectTransform mobTextPrefab;

    RectTransform[] waveInfos;

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

        WaveInfo.GetComponentInChildren<Text>().text = waves[order].name;

        waveInfos[order] = WaveInfo;

        List<MobQuantity> mobs = waves[order].Mobs;

        for (int i = 0; i < mobs.Count; i++)
        {
            float mobTextHeight = mobTextPrefab.rect.height;
            RectTransform mobText = Instantiate(mobTextPrefab, WaveInfo);

            mobText.anchoredPosition = new Vector2(81, ((-mobTextHeight) * (i + 1)) - 10f);

            //get text component, assign string value


            mobText.GetComponent<Text>().text = mobs[i].Quantity + "x " + mobs[i].Mob.name;
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
