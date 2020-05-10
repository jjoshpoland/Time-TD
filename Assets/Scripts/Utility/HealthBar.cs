using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health health;
    [SerializeField]
    Slider bar;
    [SerializeField]
    Canvas canvas;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
        GetComponent<FollowObject>().followTarget = transform.parent.gameObject;
        GetComponent<FollowObject>().OffsetY = 4;
        
        canvas.GetComponent<LookAtCamera>().cameraToLookAt = Camera.main;
        transform.parent = null;
        health.OnTakeDamage.AddListener(UpdateHealthBar);
        health.OnDie.AddListener(Die);
        health.OnDelete.AddListener(Die);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthBar()
    {
        bar.value = (float)health.HP / (float)health.MaxHP;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
