using System;
using RPG.Attributes;
using RPG.Combat;
using RPG.Stats;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        private GameObject _player;
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            GetComponentInChildren<TextMeshProUGUI>().text = _player.GetComponent<BaseStats>().GetStat(Stat.Damage).ToString();
        }
    }
}

