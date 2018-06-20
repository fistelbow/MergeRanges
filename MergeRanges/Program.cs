using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MergeRanges
{
    /// <summary>
    /// MergeRanges console application to merge a collection of integer intervals, which represent
    /// value ranges, into the most compact yet equivalent set of ranges covering the same intervals.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to run workflow. Loads a JSON file containing integer intervals with Start and Stop values.
        /// </summary>
        /// <param name="args">Expecting optional string arg[0] that is a path to a valid JSON file. 
        /// If not provided, defaults to a project data file.</param>
        static void Main(string[] args)
        {
            string path = ((args.Length == 0) ? @".\SampleRanges3.json" : args[0]); // Default JSON files in $(OutDir)
            RangeCollection coll = new RangeCollection();

            if (File.Exists(path))
            {
                using (Stream stream = File.OpenRead(path))
                {
                    var serializer = new DataContractJsonSerializer(typeof(RangeCollection));
                    coll = (RangeCollection)serializer.ReadObject(stream);
                }

                List<Tuple<int, int>> tuples = new List<Tuple<int, int>>(); 

                foreach (Range range in coll.Ranges)
                {
                    bool startIsInt;
                    bool stopIsInt;

                    startIsInt = Int32.TryParse(range.Start, out int start); // Confirm each value is an integer
                    stopIsInt = Int32.TryParse(range.Stop, out int stop);

                    if (startIsInt && stopIsInt)
                    {
                        tuples.Add(Tuple.Create(start, stop));
                    }
                }

                RangeList rangeList = new RangeList(tuples);
                rangeList.PrintTuples("Unsorted");
                rangeList.Sort();
                rangeList.PrintTuples("Sorted");
                rangeList.Merge();
                rangeList.PrintTuples("Merged");
            }
        }


        [DataContract]
        class Range
        {
            [DataMember(Name = "Start")] public string Start { get; set; }
            [DataMember(Name = "Stop")] public string Stop { get; set; }
        }

        [DataContract]
        class RangeCollection
        {
            [DataMember(Name = "Ranges")] public IEnumerable<Range> Ranges { get; set; }
        }
    }
}
