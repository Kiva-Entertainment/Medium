
// THIS IS SCAFFOLDING

/// <summary>
/// Generate various basic types of units.
/// TODO remove once unit saving/loading exists
/// </summary>
public static class Generator {

	public static Unit getSoldier () {
		return new Unit(name: "Worchester the S0ldier");
	}

	public static Unit getGolem (int team = 0) {
		Unit result = new Unit (name: "Golem",
								type: "Golem",
								hpMax: 120,
								hpCur: 120,
								spMax: 80,
								spCur: 0,
		                        mvMax: 4,
		                        mvCur: 4,
		                        jump: 0.4f,
		                        strength: 120,
		                        toughness: 140,
		                        intelligence: 60,
		                        willpower: 80,
		                        regen: 0.12f,
		                        team: team);
		result.addSkill (new Punch().setActor(result));
		result.addSkill (new Dash().setActor(result));

		return result;
	}

	public static Unit getBase (int team = 0) {
		Unit result = new Unit (name: "Base",
		                        type: "Base",
		                        hpMax: 120,
		                        hpCur: 120,
		                        spMax: 0,
		                        spCur: 0,
		                        mvMax: 0,
		                        mvCur: 0,
		                        jump: 0.0f,
		                        strength: 0,
		                        toughness: 10,
		                        intelligence: 0,
		                        willpower: 0,
		                        regen: 0.0f,
		                        team: team);
		result.addSkill (new Deploy().setActor(result));
		//result.addSkill (new Dash().setActor(result));
		
		return result;
	}










}
