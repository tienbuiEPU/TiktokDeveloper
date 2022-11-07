using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Constants
{
    public class AppConstants
    {
        #region "typeAttribute"
        public static class TypeAttributeCode
        {
            public const string TypeProject = "PROJECT_TYPE";
            public const string AreaProject = "AREAPROJECT";
            public const string TypePidPackage = "PIDPACKAGE_TYPE";
            public const string TypeReportPidPackage = "TYPE_PIDPACKAGE_REPORT";
            public const string StatusPidPackage = "CONTRACT_BIDPACKAGE_STATUS";
            public const string StatusProject = "PROJECT_STATUS";
            public const string TypeContract = "CONTRACT_TYPE";
        }
        #endregion

        #region "typeAttributeItem"
        public static class TypeAttributeItemCode
        {
            public const string WaittingProcess = "REL";
        }
        #endregion
    }
}
