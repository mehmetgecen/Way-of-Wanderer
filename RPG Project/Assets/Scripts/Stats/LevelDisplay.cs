using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using RPG.Stats;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    BaseStats baseStats;
    private void Awake()
    {
        baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = baseStats.GetLevel().ToString();
    }
}
