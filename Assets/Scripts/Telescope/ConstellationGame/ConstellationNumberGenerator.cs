using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationNumberGenerator : NumberGenerator
{
    //public ConstellationManager constellationManager;

    public override void MakeRoomForNewNumbers()
    {
        //GameObject[] gos = GameObject.FindGameObjectsWithTag("question");
        //print("Found " + gos.Length + "questions");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("star");
        foreach (GameObject g in gos)
        {
            EquationManager em = g.GetComponent<EquationManager>();
            em.MakeRoom();
        }
    }

    public override void OptimizeDifficulty(EquationManager.UpdateStrategy strat)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("star");

        foreach (GameObject g in gos)
        {
            EquationManager em = g.GetComponent<EquationManager>();
            //print("Going to optimize");
            em.OptimizeDifficulty(strat);
        }
    }
}
