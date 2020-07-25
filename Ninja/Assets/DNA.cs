using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    List<int> Genes = new List<int>();
    int dnalength = 0;
    int maxvalue = 0;

    public DNA(int l, int v)
    {
        dnalength = l;
        maxvalue = v;
        Setrandom();
    }
    public void Setrandom()
    {
        Genes.Clear();
        for (int i = 0; i < dnalength; i++)
        {
            Genes.Add(Random.Range(-maxvalue, maxvalue)); //Because force can have positive and negative values
        }
    }

    public void setint(int pos, int value)
    {
        Genes[pos] = value;
    }

    public void combinedna(DNA dna1, DNA dna2)
    {
        for(int i=0;i<dnalength;i++)
        {
            Genes[i] = Random.Range(0, 10) > 5 ? dna1.Genes[i] : dna2.Genes[i];
        }
    }

    public void Mutate()
    {
        Genes[Random.Range(0, dnalength)] = Random.Range(-maxvalue, maxvalue);
    }

    public int getgene(int pos)
    {
        return Genes[pos];
    }
}
