// -----------------------------------------------------------------------
// <copyright file="RestrictionItem.cs" company="Expedia, Inc.">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RestrictionItem : IComparable
    {
        public int indexInConfigItemList;
        public int indexInConfigValues;

        public RestrictionItem(int i, int j)
        {
            indexInConfigItemList = i;
            indexInConfigValues = j;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            RestrictionItem that = obj as RestrictionItem;
            if (that == null)
                return false;

            return (this.indexInConfigItemList == that.indexInConfigItemList &&
                this.indexInConfigValues == that.indexInConfigValues);
        }

        public int CompareTo(object obj)
        {
            RestrictionItem that = obj as RestrictionItem;

            int hBit =
                (this.indexInConfigItemList == that.indexInConfigItemList) ? 0 :
                ((this.indexInConfigItemList < that.indexInConfigItemList) ? -1 : 1) * 2;
            int lBit =
                (this.indexInConfigValues == that.indexInConfigValues) ? 0 :
                (this.indexInConfigValues < that.indexInConfigValues) ? -1 : 1;

            return hBit + lBit;
        }

        public override int GetHashCode()
        {
            return indexInConfigItemList ^ indexInConfigValues;
        }
    }

}
