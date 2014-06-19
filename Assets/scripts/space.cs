
public class Space {
	public bool isHole { get; set; }
	public int height { get; set; }

	public Space () {
		isHole = true;
		height = 0;
	}

	public Space (int height) {
		isHole = false;
		this.height = height;
	}
}
