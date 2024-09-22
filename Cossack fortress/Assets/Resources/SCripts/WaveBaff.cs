using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBaff
{
    public float MaxHitPointOn {  get; set; }
    public float DamageOn { get; set; }
    public float SpeedOn { get; set; }
    public int NumberOfEnemies { get; set; }

    public WaveBaff(float hitPoint, float damage, float speed, int numberOfEnemies)
    {
        MaxHitPointOn = hitPoint;
        DamageOn = damage;
        SpeedOn = speed;
        NumberOfEnemies = numberOfEnemies;
    }
}
