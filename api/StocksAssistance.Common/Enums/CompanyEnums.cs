using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.Enums
{
    public enum TagType
    {
        [Description("Factor")]
        Factor = 0,
        [Description("Strategy")]
        Strategy = 1
    }

    public enum AttributeType
    {
        [Description("Yahoo Symbol")]
        YahooSymbol = 0
    }

    public enum LogType
    {
        [Description("Created")]
        Created = 0,
        [Description("Updated")]
        Updated = 1
    }
}
