using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rc_3_2 : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int index;
		public bool MyTurn;
		public string Script;
		public string Who;
	}
}

