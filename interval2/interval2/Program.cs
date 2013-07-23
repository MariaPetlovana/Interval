using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace interval2
{
    public class Interval<T> : IEquatable<Interval<T>> where T : IComparable<T>
    {
        public const string MaxLessThanMinMessage = "Max is less than min";
        
        protected T m_min;
        protected T m_max;

        protected static int MySort(Interval<T> a, Interval<T> b)
        {
            return a.m_min.CompareTo(b.m_min);
        }

        public bool Equals(Interval<T> MyInterval)
        {
            return MyInterval.m_min.CompareTo(m_min) == 0 && MyInterval.m_max.CompareTo(m_max) == 0;
        }
        
        //constructor
        public Interval(T min, T max)
        {
            if (min.CompareTo(max) >= 0)
            {
                throw new ArithmeticException(MaxLessThanMinMessage);
            }
            this.m_min = min;
            this.m_max = max;
        }

        //getters
        public T Min
        {
            get
            {
                return m_min;
            }
        }
        
        public T Max
        {
            get
            {
                return m_max;
            }
        }

        //iscontains a value within an interval
        public bool IsContains(T value)
        {
            return (value.CompareTo(m_min) >= 0 && value.CompareTo(m_max) <= 0);
        }

        //iscontains an interval within an interval
        public bool IsContains(Interval<T> MyInterval)
        {
            return (IsContains(MyInterval.m_min) && IsContains(MyInterval.m_max));
        }

        //isintersection between 2 intervals
        public bool IsIntersection(Interval<T> MyInterval)
        {
            return (m_min.CompareTo(MyInterval.m_min) > 0 ? m_min : MyInterval.m_min).CompareTo(m_max.CompareTo(MyInterval.m_max) < 0 ? m_max : MyInterval.m_max) < 0; //<=
        }

        //isintersection of sequence of intervals
        public bool IsIntersection(IEnumerable<Interval<T>> Intervalable)
        {
            IEnumerator<Interval<T>> Intervalator = Intervalable.GetEnumerator();
            while (Intervalator.MoveNext())
            {
                if (IsIntersection(Intervalator.Current))
                    return true;
            }
            return false;
        }

        //returns new interval - intersection of 2 intervals
        public Interval<T> Intersect(Interval<T> MyInterval)
        {
            T MyMin = m_min.CompareTo(MyInterval.m_min) > 0 ? m_min : MyInterval.m_min;
            T MyMax = m_max.CompareTo(MyInterval.m_max) < 0 ? m_max : MyInterval.m_max;
            return MyMin.CompareTo(MyMax) < 0 ? new Interval<T>(MyMin, MyMax) : null; //<=
        }

        //returns new interval - created from merge of 2 2 intervals
        public Interval<T> WidedInterval(Interval<T> MyInterval)
        {
            T MyMin = m_min.CompareTo(MyInterval.m_min) < 0 ? m_min : MyInterval.m_min;
            T MyMax = m_max.CompareTo(MyInterval.m_max) > 0 ? m_max : MyInterval.m_max;
            return new Interval<T>(MyMin, MyMax);
        }     

        //if isintersection returns intersect
        public Interval<T> Combine(Interval<T> MyInterval)
        {
            return IsIntersection(MyInterval) ? WidedInterval(MyInterval) : null;
        }

        public bool IsInList(List<Interval<T>> MySequence)
        {
            for (int i = 0; i < MySequence.Count; ++i)
            {
                if (Equals(MySequence[i]))
                { 
                    return true;
                }
            }
            return false;
        }

        //returns new sequence of intervals
        public List<Interval<T> > Insert(List<Interval<T> > MySequence)
        {
            MySequence.Add(this);
            MySequence.Sort(MySort);
            Interval<T> CurrentInterval = this;
            List<Interval<T> > ResultSequence = new List<Interval<T> >(MySequence.Count);
            bool CanCombine = false;

            for (int i = 0; i < MySequence.Count; ++i)
            {
                CanCombine = CurrentInterval.IsIntersection(MySequence[i]);
                if (CanCombine)
                {
                    CurrentInterval = CurrentInterval.Combine(MySequence[i]);
                }
                else
                {
                    ResultSequence.Add(CurrentInterval);
                    CurrentInterval = MySequence[i];
                    CanCombine = true;
                }
            }
            if (CanCombine)
            {
                ResultSequence.Add(CurrentInterval); 
            }

            return ResultSequence;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {            
            List<Interval<int>> MyList = new List<Interval<int>>(4);
            MyList.Add(new Interval<int>(1, 17));
            MyList.Add(new Interval<int>(21, 30));
            MyList.Add(new Interval<int>(40, 50));

            foreach (var l in MyList)
            {
                Console.Write("[{0}, {1}], ", l.Min.ToString(), l.Max);
            }

            Console.WriteLine();

            Interval<int> MyInterval;

            MyInterval = new Interval<int>(15, 25);
            List<Interval<int>> NewList = new List<Interval<int>>(MyInterval.Insert(MyList));

            foreach (var l in NewList)
            {
                Console.Write("[{0}, {1}], ", l.Min.ToString(), l.Max);
            }

            Console.WriteLine();
                
            Console.WriteLine("Hello");
            Console.ReadKey();
        }
    }
}
