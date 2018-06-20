using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeRanges
{
    /// <summary>
    /// RangeList class encapsulates functionality to load, sort and merge a list of intervals.
    /// </summary>
    public class RangeList
    {
        public List<Tuple<int, int>> Tuples { get; set; }

        /// <summary>
        /// Constructor from input list of tuples
        /// </summary>
        /// <param name="tuples"></param>
        public RangeList(List<Tuple<int, int>> tuples)
        {
            this.Tuples = tuples;
        }

        /// <summary>
        /// Constructor from an array of tuples
        /// </summary>
        /// <param name="tuples"></param>
        public RangeList(Tuple<int, int>[] tuples)
        {
            this.Tuples = tuples.ToList();
        }

        /// <summary>
        /// Constructor from a 2D array of integers
        /// </summary>
        /// <param name="tuples"></param>
        public RangeList(int[,] tuples)
        {
            List<Tuple<int, int>> convert = new List<Tuple<int, int>>();

            for (int i = 0; i < tuples.Length; i++)
            {
                int item1 = tuples[i,0];
                int item2 = tuples[i,1];
                Tuple<int, int> tuple = new Tuple<int, int>(item1, item2);
                convert.Add(tuple);
            }

            this.Tuples = convert;
        }

        /// <summary>
        /// Wrapper method to expose RangeList sorting.
        /// </summary>
        public void Sort()
        {
            Tuples.Sort();
        }

        /// <summary>
        /// Core logic for merge algorithm. Iterates through a sorted RangeList, comparing Start value
        /// of the current tuple with the Stop value of the previous tuple. If greater, it regards
        /// the prevous tuple as non-overlapping and adds it to an output list. If less or equal, it
        /// merges previous with current, using the Start from previous and the greater of the two
        /// Stop values from previous and current.
        /// </summary>
        public void Merge()
        {
            Tuple<int, int> lastTuple = Tuples.FirstOrDefault();
            List<Tuple<int, int>> merged = new List<Tuple<int, int>>();

            foreach (Tuple<int, int> currTuple in Tuples)
            {
                if (currTuple.Item1 > lastTuple.Item2)
                {
                    merged.Add(lastTuple);
                    lastTuple = currTuple;
                }
                else
                {
                    lastTuple = Tuple.Create(lastTuple.Item1, Math.Max(lastTuple.Item2, currTuple.Item2));
                }
            }

            merged.Add(lastTuple);
            Tuples = merged;
        }

        /// <summary>
        /// Writes list of tuples to the console.
        /// </summary>
        /// <param name="prefix"></param>
        public void PrintTuples(string prefix)
        {
            foreach (Tuple<int, int> tuple in Tuples)
            {
                Console.WriteLine("{0} Range start: {1}, Range stop: {2}", prefix, tuple.Item1, tuple.Item2);
            }

            Console.ReadLine();
        }
        
    }
}
