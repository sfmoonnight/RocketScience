using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNumGenerator : NumberGenerator
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LateUpdate()
    {
        if (!initialized)
        {
            OptimizeDifficulty(EquationManager.UpdateStrategy.overwrite);
            initialized = true;
        }
    }
}
