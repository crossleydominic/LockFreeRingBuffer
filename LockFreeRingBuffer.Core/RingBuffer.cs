using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockFreeRingBuffer.Core
{
    public class RingBuffer<T> : IProducerBuffer<T>, IConsumerBuffer<T>
    {
        private int _head;
        private int _tail;
        private int _size;
        private T[] _elements;

        public RingBuffer(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Size must be greater than zero", "size");
            }
            
            _head = 0;
            _tail = 0;
            _size = size + 1; //Append an empty slot for empty/full detection
            _elements = new T[_size]; 
        }

        public bool Push(T element)
        {
            int currentTail = _tail;    //No fence required for tail because only 1 thread is writing
            int nextTail = Increment(currentTail);

            Fences.EmitAcquireFence(); //Acquire for _head

            if(nextTail == _head)
            {
                return false;
            }

            _elements[currentTail] = element;
            _tail = nextTail;

            Fences.EmitReleaseFence(); //Release for _tail

            return true;
        }

        private int Increment(int currentTail)
        {
            return (currentTail + 1) % _size;
        }

        public bool Pop(ref T element)
        {
            int currentHead = _head; //No fence required for head because only 1 thread is writing

            Fences.EmitAcquireFence(); //Acquire for _tail

            if(currentHead == _tail)
            {
                return false;
            }

            element = _elements[currentHead];
            _head = Increment(currentHead);

            Fences.EmitReleaseFence(); //Release for _head

            return true;
        }
    }
}
