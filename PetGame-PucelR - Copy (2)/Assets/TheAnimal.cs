using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAnimal
{
    private string name;
    private int fulness;
    private int energy;
    private int happiness;
    

    public TheAnimal(int fullnessStat, int energyStat, int happinessStat)
    {
        fulness = fullnessStat;
        energy = energyStat;
        happiness = happinessStat;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Energy
    {
        get { return energy; }
        set { energy = value; }
    }
    public int Fullness
    {
        get { return fulness; }
        set { fulness = value; }
    }
    public int Happiness
    {
        get { return happiness; }
        set { happiness = value; }
    }

}
