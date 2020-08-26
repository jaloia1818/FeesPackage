// Copyright (c) 2007-2013 by Intuit Inc.
// All rights reserved
// Usage governed by the QuickBooks SDK Developer's License Agreement

using System;

namespace FeesPackage.Session_Framework
{
    public class QBException : Exception
    {
        private QBException() { }

        public QBException(string sMsg)
            : base(sMsg)
        {
        }

        public override string ToString()
        {
            return base.Message;
        }

    }
}
