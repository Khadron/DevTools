// Guids.cs
// MUST match guids.h
using System;

namespace KongQiang.DevTools
{
    static class GuidList
    {
        public const string guidDevToolsPkgString = "4933e828-bd8d-4653-9e51-dbc5419794dd";
        public const string guidDevToolsCmdSetString = "cc9cc61b-bd82-405c-9207-8f18dd458c09";

        public static readonly Guid guidDevToolsCmdSet = new Guid(guidDevToolsCmdSetString);
    };
}