using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using interval2;

namespace UnitTestInterval
{
    [TestClass]
    public class NormalBehavior1
    {
        [TestMethod]
        public void NormalBehavior()
        {
            List<Interval<int>> MyList = new List<Interval<int>>(4);
            MyList.Add(new Interval<int>(1, 17));
            MyList.Add(new Interval<int>(21, 30));
            MyList.Add(new Interval<int>(40, 50));

            Interval<int> MyInterval;

            try
            {
                MyInterval = new Interval<int>(15, 25);
                List<Interval<int>> NewList = new List<Interval<int>>(MyInterval.Insert(MyList));

                String ActualResult = new String('\0', 0);
                String ExpectedResult = new String('\0', 0);

                ExpectedResult = "[1, 30], [40, 50], ";

                foreach (var l in NewList)
                {
                    ActualResult += "[" + l.Min.ToString() + ", " + l.Max + "], ";
                }

                Assert.AreEqual(ExpectedResult, ActualResult, "New interval is not inserted correctly");
            }
            catch (ArithmeticException e)
            {
                StringAssert.Contains(e.Message, Interval<int>.MaxLessThanMinMessage);
            }
        }
    }

    [TestClass]
    public class TwoIntervalsWithOverallPoint1
    {
        [TestMethod]
        public void TwoIntervalsWithOverallPoint()
        {
            List<Interval<int>> MyList = new List<Interval<int>>(1);
            MyList.Add(new Interval<int>(17, 25));

            Interval<int> MyInterval;

            try
            {
                MyInterval = new Interval<int>(1, 17);
                List<Interval<int>> NewList = new List<Interval<int>>(MyInterval.Insert(MyList));

                String ActualResult = new String('\0', 0);
                String ExpectedResult = new String('\0', 0);

                ExpectedResult = "[1, 17], [17, 25], ";

                foreach (var l in NewList)
                {
                    ActualResult += "[" + l.Min.ToString() + ", " + l.Max + "], ";
                }

                Assert.AreEqual(ExpectedResult, ActualResult, "New interval is not inserted correctly");
            }
            catch (ArithmeticException e)
            {
                StringAssert.Contains(e.Message, Interval<int>.MaxLessThanMinMessage);
            }
        }
    }

        [TestClass]
    public class MaxLessThanMin1
    {

        [TestMethod]
            public void MaxLessThanMin()
        {
            List<Interval<int>> MyList = new List<Interval<int>>(4);
            MyList.Add(new Interval<int>(1, 17));
            MyList.Add(new Interval<int>(21, 30));
            MyList.Add(new Interval<int>(40, 50));

            foreach (var l in MyList)
            {
                Console.Write("[{0}, {1}], ", l.Min, l.Max);
            }

            Console.WriteLine();

            Interval<int> MyInterval;

            try
            {
                MyInterval = new Interval<int>(25, 15);
                List<Interval<int>> NewList = new List<Interval<int>>(MyInterval.Insert(MyList));

                foreach (var l in NewList)
                {
                    Console.Write("[{0}, {1}], ", l.Min, l.Max);
                }

                Console.WriteLine();
            }
            catch(ArithmeticException e)
            {
                StringAssert.Contains(e.Message, Interval<int>.MaxLessThanMinMessage);
            }
        }

        [TestClass]
        public class IntervalPoint1
        {

            [TestMethod]
            public void IntervalPoint()
            {
                try
                {
                    Interval<int> MyInterval = new Interval<int>(2, 2);                    
                }
                catch (ArithmeticException e)
                {
                    StringAssert.Contains(e.Message, Interval<int>.MaxLessThanMinMessage);
                }
            }
        }

    }
}
