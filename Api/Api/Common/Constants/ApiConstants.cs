using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Constants
{
    public class ApiConstants
    {
        public static class StatusCode
        {
            public const int Success200 = 200;
            public const int Success201 = 201;
            public const int Valid210 = 210;
            public const int Valid211 = 211;
            public const int Valid213 = 213;
            public const int NoPermision = 222;
            public const int Error400 = 400;
            public const int Error401 = 401;
            public const int Error404 = 404;
            public const int Error422 = 422;
            public const int Error500 = 500;
            public const int Error600 = 600;
        }

        public static class MessageResource
        {
            #region common
            public const string ACCTION_SUCCESS = "Thành công!";
            public const string EXCEPTION_UNKNOWN = "Lỗi ngoại lệ chưa xác định!";
            public const string ADD_SUCCESS = "Thêm mới thành công!";
            public const string UPDATE_SUCCESS = "Sửa thông tin thành công!";
            public const string UNAUTHORIZE = "Lỗi xác thực!";
            public const string DELETE_SUCCESS = "Xóa bản ghi thành công!";

            public static string MISS_DATA_MESSAGE = "Thông tin không đủ!";  //error_code = 210
            public static string BAD_REQUEST_MESSAGE = "Lỗi sai dữ liệu!";  //error_code = 400
            public static string NOT_FOUND_VIEW_MESSAGE = "Không tìm thấy mục cần xem!"; //error_code = 404
            public static string NOT_FOUND_UPDATE_MESSAGE = "Không tìm thấy mục cần sửa!"; //error_code = 404
            public static string NOT_FOUND_DELETE_MESSAGE = "Không tìm thấy mục cần xóa!"; //error_code = 404
            public static string ERROR_EXIST_MESSAGE = "Mục này đã tồn tại!";   //error_code = 211

            public static string ERROR_400_MESSAGE = "Có lỗi xảy ra. Xin vui lòng thử lại sau!";    //error_code = 400
            public static string ERROR_500_MESSAGE = "Hệ thống xảy ra lỗi. Xin vui lòng thử lại sau!";

            public const string NOT_FOUND = "Không tìm thấy!";
            public const string INVALID = "Không hợp lệ!";
            #endregion

            #region check role
            public const string NOPERMISION_VIEW_MESSAGE = "Bạn không có quyền xem dữ liệu tới mục này!";    //error_code = 222
            public const string NOPERMISION_UPDATE_MESSAGE = "Bạn không có quyền cập nhật mục này!";   //error_code = 222
            public const string NOPERMISION_CREATE_MESSAGE = "Bạn không có quyền thêm mới mục này!";   //error_code = 222
            public const string NOPERMISION_DELETE_MESSAGE = "Bạn không có quyền xoá mục này!";   //error_code = 222
            public const string NOPERMISION_ACCEPT_MESSAGE = "Bạn không có quyền duyệt đơn đăng ký!";
            public const string NOPERMISION_ACTION_MESSAGE = "Bạn không có quyền thực hiện thao tác này!";
            #endregion

            #region "user"
            public const string USER_NOT_FOUND = "Người dùng không tồn tại!";
            #endregion
        }
    }
}
