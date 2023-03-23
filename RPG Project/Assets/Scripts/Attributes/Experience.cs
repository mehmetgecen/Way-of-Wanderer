using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private float experiencePoints = 0;

    public void GainExperience(float experience)
    {
        experiencePoints += experience;
    }
}
