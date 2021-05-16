using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Models.Attributes
{
    public class TVPAttribute : Attribute
    {
        public TVPAttribute(int tvpOrder, bool isForeignKey = false, Type propertyOverride = null)
        {
            TvpOrder = tvpOrder;
            IsForeignKey = isForeignKey;
            PropertyOverride = propertyOverride;
        }

        public Type PropertyOverride { get; set; }

        public bool IsForeignKey { get; set; }

        public int TvpOrder { get; set; }
    }
}
