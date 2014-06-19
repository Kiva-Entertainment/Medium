/// <summary>
/// Position of a thing, 2 dimensional
/// Does not have y value
/// </summary>
public class loc {
	public int x { get; set; }
	public int z { get; set; }
	
	public loc () {
		x = z = 0;
	}
	
	public loc (int x, int z) {
		this.x = x;
		this.z = z;
	}

	public loc plus (loc l) {
		return new loc (x + l.x, z + l.z);
	}
}
