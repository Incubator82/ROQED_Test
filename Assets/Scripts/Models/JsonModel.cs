using System;
using System.Collections.Generic;
[Serializable]
public class JsonModel
{
    public JsonModel()
    {
        LeftParititionBillets = new List<LeftParititionBillets>();
        RigthParititionBillets = new List<RigthParititionBillets>();
    }
    public List<LeftParititionBillets> LeftParititionBillets;
    public List<RigthParititionBillets> RigthParititionBillets;
}
[Serializable]
public class LeftParititionBillets
{
    public string Text;
    public int Number;
}

[Serializable]
public class RigthParititionBillets
{
    public string Text;
    public int Number;
}
