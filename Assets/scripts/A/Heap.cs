using System;

public class Heap<T> where T : IHeapItem<T>
{

    T[] totalItems;
    int recentItemCount;

    public Heap(int maxHeapSize)
    {
        totalItems = new T[maxHeapSize];
    }

    public T RemoveFirst()
    {
        T firstItem = totalItems[0];
        recentItemCount--;
        totalItems[0] = totalItems[recentItemCount];
        totalItems[0].HeapIndex = 0;
        SortDesc(totalItems[0]);
        return firstItem;
    }


    public void Add(T itemT)
    {
        itemT.HeapIndex = recentItemCount;
        totalItems[recentItemCount] = itemT;
        SortAsc(itemT);
        recentItemCount++;
    }


    public bool Contains(T itemT)
    {
        return Equals(totalItems[itemT.HeapIndex], itemT);
    }


    public void UpdateItem(T itemT)
    {
        SortAsc(itemT);

    }


    public int Count
    {
        get
        {
            return recentItemCount;
        }
    }


    void SortDesc(T itemT)
    {
        while(true)
        {
            int leftChildIndex = itemT.HeapIndex * 2 + 1;
            int rightChildIndex = itemT.HeapIndex * 2 + 2;
            int tmpIndex = 0;

            if (leftChildIndex < recentItemCount)
            {
                tmpIndex = leftChildIndex;

                if (rightChildIndex < recentItemCount)
                {
                    if (totalItems[leftChildIndex].CompareTo(totalItems[rightChildIndex]) < 0)
                    {
                        tmpIndex = rightChildIndex;
                    }
                }

                if (itemT.CompareTo(totalItems[tmpIndex]) < 0)
                {
                    Swap(itemT, totalItems[tmpIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void Swap(T itemTA, T itemTB)
    {
        totalItems[itemTA.HeapIndex] = itemTB;
        totalItems[itemTB.HeapIndex] = itemTA;

        int itemTAIndex = itemTA.HeapIndex;
        itemTA.HeapIndex = itemTB.HeapIndex;
        itemTB.HeapIndex = itemTAIndex;
    }

    void SortAsc(T itemT)
    {
        int rootIndex = (itemT.HeapIndex - 1) / 2;
        while(true)
        {
            T rootItem = totalItems[rootIndex];
            if (itemT.CompareTo(rootItem) > 0)
            {
                Swap(itemT, rootItem);
            }
            else
                break;
        }
        rootIndex = (itemT.HeapIndex - 1) / 2;
    }

   
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}