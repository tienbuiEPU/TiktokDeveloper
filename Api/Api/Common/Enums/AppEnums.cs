using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Enums
{
    public class AppEnums
    {
        public enum EntityStatus
        {
            /// <summary>
            /// All
            /// </summary>
            NA = 0,
            NORMAL = 1,
            OK = 2,
            NOT_OK = 3,
            SAP_DELETED = 9,
            TEMP = 10,
            LOCK = 98,
            DELETED = 99,
        }

        public enum CacheDataTypes
        {
            ByteArray = 0, // Can convert to byte array
            Json = 1
        }

        public enum Action
        {
            VIEW = 0,
            CREATE = 1,
            UPDATE = 2,
            DELETED = 3,
            IMPORT = 4,
            EXPORT = 5,
            PRINT = 6,
            EDIT_ANOTHER_USER = 7,
            MENU = 8
        }

        public enum TypeFunction    // Phân quyền chức năng với người dùng và nhóm quyền
        {
            FUNCTION_USER = 1, // Người dùng - Chức năng
            FUNCTION_ROLE = 2,    // Nhóm quyền - Chức năng
        }
    }
}
