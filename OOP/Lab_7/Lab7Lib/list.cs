using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab7Lib
{
    public class List : IEnumerable<int>
    {
        private Box head;
        private int size;

        public List()
        {
            head = null;
            size = 0;
        }

        public int this[int index]
        {
            get { return GetElementByIndex(index); }
        }

        public int GetSize()
        {
            return size;
        }

        public void AddElementAfterHead(int value)
        {
            Box newBox = new Box(value);
            if (head == null)
            {
                head = newBox;
            }
            else
            {
                newBox.Next = head.Next;
                head.Next = newBox;
            }
            size++;
        }

        public void DeleteElementByIndex(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            if (index == 0)
            {
                head = head.Next;
            }
            else
            {
                Box current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                current.Next = current.Next.Next;
            }
            size--;
        }

        public int GetElementByIndex(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            Box current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Value;
        }

        /// <summary>
        /// Returns the first element in the list that is greater than the specified value.
        /// </summary>
        public int ReturnFirstElementBiggerThan(int value)
        {
            Box current = head;
            while (current != null)
            {
                if (current.Value > value)
                {
                    return current.Value;
                }
                current = current.Next;
            }
            throw new InvalidOperationException("No element bigger than the specified value was found.");
        }

        /// <summary>
        /// Returns the sum of all elements in the list that are less than the specified value.
        /// </summary>
        public int FindSumOfElementsLessThan(int value)
        {
            int sum = 0;
            Box current = head;
            while (current != null)
            {
                if (current.Value < value)
                {
                    sum += current.Value;
                }
                current = current.Next;
            }
            return sum;
        }

        /// <summary>
        /// Returns new list containing only the elements that are greater than the specified value.
        /// </summary>
        public List ReturnListOfElementsGreaterThan(int value)
        {
            List resultList = new List();
            Box current = head;
            while (current != null)
            {
                if (current.Value > value)
                {
                    resultList.AddElementAfterHead(current.Value);
                }
                current = current.Next;
            }
            return resultList;
        }

        public Box FindBiggest()
        {
            if (head == null)
            {
                return null;
            }

            // Find the biggest element
            Box MaxNode = head;
            Box current = head.Next;
            while (current != null)
            {
                if (current.Value > MaxNode.Value)
                {
                    MaxNode = current;
                }
                current = current.Next;
            }

            return MaxNode;
        }

        /// <summary>
        /// Deletes all elements in the list that are less than the bigest element in the list.
        /// </summary>
        public void DeleteElementsLessThanBiggest()
        {
            Box MaxNode = FindBiggest();

            MaxNode.Next = null;

            int newSize = 0;
            Box current = head;
            while (current != null)
            {
                newSize++;
                current = current.Next;
            }

            this.size = newSize;
        }
        
        public IEnumerator<int> GetEnumerator()
        {
            Box current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
