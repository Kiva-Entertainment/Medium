public class Craft : BasicSkill {
	
	public override int getCost () { return 0; }
	public override string getName () { return "Dash"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("run");
		new Rock (team: 0).deploy (locs [0]);
	}
	
	public override Loc[] getRange (bool onlyValid) {
		return BasicRange.self(actor).ToArray();
	}
	
}
