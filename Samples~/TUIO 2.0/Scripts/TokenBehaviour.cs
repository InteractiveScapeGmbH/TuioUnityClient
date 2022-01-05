using Tuio.Tuio20;

public class TokenBehaviour : Tuio20ComponentBehaviour
{
    private Tuio20Token _token;

    public void Initialize(Tuio20Token token)
    {
        _token = token;
        _component = token;
    }
}