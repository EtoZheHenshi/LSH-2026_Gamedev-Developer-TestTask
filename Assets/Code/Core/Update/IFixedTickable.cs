namespace Code.Core.Update
{
    public interface IFixedTickable
    {
        public void FixedTick(float fixedDeltaTime);
    }
}