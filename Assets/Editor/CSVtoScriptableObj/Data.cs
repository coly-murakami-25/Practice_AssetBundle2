using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
// ScriptableObject化するマスタ
public class Data : ScriptableObject { 
  public List<Param> param = new List<Param>();
 
  [System.SerializableAttribute]
  public class Param {
    public int      intValue;
    public float    floatValue;
    public string   stringValue;
  }
}