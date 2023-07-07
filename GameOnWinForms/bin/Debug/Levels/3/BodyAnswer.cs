using SecurityFramework;

namespace SystemController
{
    public class Body : Base
    {
	public void Start(int pass)
        {
	    door1.TryOpen(pass);
	}
    }
}