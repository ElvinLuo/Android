using System;

namespace WindowsFormsApplication.Comparison
{
    public class ComparisonMethod
    {
        public static bool CompareUpdateDate(object left, object right)
        {
            DateTime leftValue = System.Convert.ToDateTime(left.ToString());
            DateTime rightValue = System.Convert.ToDateTime(right.ToString());

            if (leftValue <= rightValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
