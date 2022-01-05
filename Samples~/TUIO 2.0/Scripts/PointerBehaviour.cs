using Tuio.Tuio20;

public class PointerBehaviour : Tuio20ComponentBehaviour
{
    private Tuio20Pointer _pointer;

    public void Initialize(Tuio20Pointer pointer)
    {
        _pointer = pointer;
        _component = pointer;
    }
}