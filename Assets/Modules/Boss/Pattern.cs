using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatternType : int
{
    FireMagic = 0,
    Attack,
    Defense,
    FireBreath
    
}
public class Pattern
{
    public PatternType type;
    public int value;
}

public class BossState
{
    public string name;
    public List<Pattern> patterns = new List<Pattern>();
}
