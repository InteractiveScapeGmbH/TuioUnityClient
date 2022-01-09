using Tuio.Tuio20;

public class Tuio20TokenBehaviour : Tuio20ComponentBehaviour
{
    private Tuio20Token _tuio20Token;

    public void Initialize(Tuio20Token tuio20Token)
    {
        _tuio20Token = tuio20Token;
        _tuio20Component = tuio20Token;
    }
}