using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Machine : MonoBehaviour
{

    public enum Machine
    {
        Stop,NormalSpeed,FastSpeed
    }

    public Machine _machine;

    public float defaultSpeed,hourPerDay = 24f;
    [SerializeField] float deltaTime;

    public int Day, Week, Month, Year;


    private void Update()
    {
        switch (_machine)
        {
            case Machine.Stop:
                defaultSpeed = 0;
                break;
            case Machine.NormalSpeed:
                defaultSpeed = 1;
                break;
            case Machine.FastSpeed:
                defaultSpeed = 2;
                break;
        }

        Calender();

    }

    void Calender()
    {

        deltaTime += (defaultSpeed * Time.deltaTime);

        if ( deltaTime >= hourPerDay) { deltaTime = 0; Day++; }
        if( Day > 7) { Day = 0; Week++; }
        if(Week > 4) { Week = 0; Month++; }
        if(Month > 12) { Month = 0; Year++; }
    }


}
