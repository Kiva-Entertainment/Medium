public class Rock : Unit {
	public Rock(int team) : base(name: "Rock",
	                              type: "Rock",
	                              hpMax: 100,
	                              hpCur: 100,
	                              spMax: 0,
	                              spCur: 0,
	                              mvMax: 0,
	                              mvCur: 0,
	                              jump: 0.0f,
	                              strength: 0,
	                              toughness: 12,
	                              intelligence: 0,
	                              willpower: 0,
	                              regen: 0.0f,
	                              team: 0)
	{
		// No skills :(
	}
}