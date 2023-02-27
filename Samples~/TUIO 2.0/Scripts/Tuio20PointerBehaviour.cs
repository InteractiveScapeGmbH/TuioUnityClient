using TuioNet.Tuio20;

public class Tuio20PointerBehaviour : Tuio20ComponentBehaviour
{
    private Tuio20Pointer _tuio20Pointer;

    public void Initialize(Tuio20Pointer tuio20Pointer)
    {
        _tuio20Pointer = tuio20Pointer;
        _tuio20Component = tuio20Pointer;
    }
}