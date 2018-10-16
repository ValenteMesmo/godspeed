namespace Godspeed
{
    public class MemberValue<T>
    {
        private T currentValue;
        private T previousValue;

        public MemberValue(T value)
        {
            previousValue = currentValue = value;
        }

        public T GetValue() => currentValue;
        public T GetPreviousValue() => previousValue;
        public void SetValue(T value)
        {
            previousValue = currentValue;
            currentValue = value;
        }
    }
}
