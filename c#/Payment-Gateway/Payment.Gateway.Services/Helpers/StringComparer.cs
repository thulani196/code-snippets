namespace Payment.Gateway.Logic.Helpers
{
    using System.Collections.Generic;

    public class StringComparer : IComparer<string>
    {
        ///<summary>Compare method using Ordinal comparison</summary>
        ///<param name="a"> The first string in the comparison.</param>
        ///<param name="b"> The second string in the comparison.</param>
        ///<returns>An int containing the result of the comparison.</returns>
        public int Compare(string a, string b)
        {
            // Return if we are comparing the same object or one of the 
            // objects is null, since we don't need to go any further.
            if (a == b) return 0;
            if (string.IsNullOrEmpty(a)) return -1;
            if (string.IsNullOrEmpty(b)) return 1;

            // Get the CompareInfo object to use for comparing
            System.Globalization.CompareInfo myComparer = System.Globalization.CompareInfo.GetCompareInfo("en-US");
            // Compare using an Ordinal Comparison.
            return myComparer.Compare(a, b, System.Globalization.CompareOptions.Ordinal);
        }
    }
}
