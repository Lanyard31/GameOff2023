using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearGrab : MonoBehaviour
{
    public ScrapCounter scrapCounter;
    //public int gearCounter = 0;

    public void IncrementGearCounter()
    {
        scrapCounter.UpdateScrapText(1);
    }
}
