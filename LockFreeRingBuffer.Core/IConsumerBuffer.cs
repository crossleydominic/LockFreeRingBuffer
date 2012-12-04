namespace LockFreeRingBuffer.Core
{
    public interface IConsumerBuffer<T>
    {
        bool Pop(ref T element);
    }
}