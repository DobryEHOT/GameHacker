using SecurityFramework;

namespace SystemController
{
    public class Body : Base
    {
	public void Start(int pass)
        {
	    if(pass == password)
	    {
		door1.Open();
	    }
            else
            {
                door1.Closed();
            }
	}
    }
}