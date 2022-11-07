using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Constants
{
    public class ProductConstant
    {
        public static class ContractConstant
        {
            public static class ProcessStatusCode
            {
                public const string PREPARE_START = "REL";
                public const string PROCESSING = "CB_PROCESSING";
                public const string CANCEL = "CB_CANCEL";
                public const string PAUSE = "LKD";
                public const string FINISHED = "CB_FINISHED";
            }
        }
    }
}
