namespace LockFreeRingBuffer.Core
{
    public interface IProducerBuffer<T>
    {
        bool Push(T element);
    }
}