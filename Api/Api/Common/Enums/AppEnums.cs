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

        public enum ShowLogLevel
        {
            Default = 0,
            Production = 1,
            Stacktrace = 2
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

        public enum TypeUserProject    // Phân quyền người dùng với đơn vị và dự án
        {
            USER_UNIT = 1, // Người dùng - Đơn vị
            USER_PROJECT = 2,    // Người dùng - Dự án
            USER_CONTRACT= 3,    // Người dùng - Hợp đồng
        }

        public enum TypeTaskPlan    // loại kế hoạch
        {
            PROJECT_PLAN = 1, // tiến độ thi công
            CONTRACT_PLAN = 2,    // kế hoạch thi công
        }

        public enum TypeMonitoringUser    // quyền giám sát
        {
            MANAGER_MAX = 0,    // quản lý cấp cao
            MANAGER = 1,    // quản lý
            MONITORING = 2, // giám sát thi công
        }

        public enum ContractStatus    // trang thái duyệt phiếu
        {
            INIT = 49, // Chuẩn bị triển khai
            DEPLOYING = 50,    // Đang triển khai
            ACCEPTED = 51,    // Đã nghiệm thu
            PAYMENTED = 52,    // Đã quyết toán
            PAUSE = 53,    // Tạm dừng
            END = 60,    // Kết thúc
        }

        public enum TypeChangeContract    // điều chỉnh hợp đồng, loại phụ lục hợp đồng
        {
            CHANGE_VALUE_PRICE = 1,    // Thay đổi giá trị HĐ (khối lượng, đơn giá) hạ tầng or Thay thế giá trị cao tầng
            CHANGE_CONTRACT_NEW = 2, // Phát sinh công việc ngoài hợp đồng hạ tầng or tăng giảm giá trị của cao tầng
            CHANGE_PLAN_EDIT = 3, // Thay đổi tiến độ hạ tầng
            CHANGE_ALL_EDIT = 4, // Tổng hợp các loại trên hạ tầng => Phát sinh tăng giảm +thay đổi tiến độ
            CHANGE_CONTENT = 5, // Thay đổi nội dung hợp đồng cao tầng
            CHANGE_OTHOR = 6, // Phụ lục khác cao tầng
        }

        public enum TypeContract    // loại hợp đồng
        {
            CONTRACT = 1, // Hợp đồng chính         RelatedId=null thì là Hợp đồng chính, RelatedId!=null hợp đồng chi tiết của hợp đồng nguyên tắc
            CONTRACT_CHILD = 2,    // Phụ lục hợp đồng
            CONTRACT_SUB_CONTRACTOR = 3, // Hợp đồng nhà thầu phụ
            CONTRACT_PRINCIPLE = 4,   // Hợp đồng nguyên tắc
            CONTRACT_PRINCIPLE_DETAIL = 5 //Hợp đồng chi tiết của hợp đồng nguyên tắc
        }

        public enum TypePlan    // kế hoạch theo
        {
            WEEK = 1, // tuần
            MONTH = 2,    // tháng
        }

        public enum TypeAction    // loại hành động
        {
            ACTION = 1, // Hành động
            NOTIFICATION = 2, // Thông báo
            WARNING = 3         //Cảnh báo
        }

        public enum FileType
        {
            IMAGE = 1,
            FILE = 2
        }

        public enum TypeDepartment    // loại phong ban
        {
            NGHIEP_VU = 1, // Phong nghiệp vụ
            BINH_THUONG = 2, // Phòng bình thường
        }

        public enum TypeAttactmentItem //Loại file đính kèm
        {
            CONTRACTOR = 1,         //File đính kèm của Nhà thầu
            OWNER = 2,               //File đính kèm của Đơn vị chủ quản
            ATTACTMENT_FOLDER = 3,   //File đính kèm của thư mục file
            CONTRACT = 4  ,              //File đính kèm của hợp đồng, phụ lục hợp đồng, hợp đồng thầu phụ
            ONEDRIVE_FILE = 5           //File đính kèm của hình ảnh video thi công
        }

        public enum TypeAttactmentFolder
        {
            THUMUC_FILE_DINHKEM_HOPDONG = 1,           //Thư mục file đính kèm của Hợp đồng
            THUTUC_PHAPLI = 2,                  //Thủ tục pháp lý của dự án
            HOSO_THIETKE = 3,                   //Hồ sơ thiết kế của dự án
            MATBANG = 4,                        // Tài liệu mặt bằng của dự án
            CHUTRUONG_CHIDAO = 5,                // Chủ trương chỉ đạo của dự án
            THUMUC_FILE_DINHKEM_DUAN = 6,           //Thư mục file đính kèm của Dự án
            THUMUC_FILE_DINHKEM_PHIEUDENGHI = 7,     //Thư mục file đính kèm của phiếu đề nghị thanh toán
            THUMUC_FILE_DINHKEM_GOITHAU = 8,        //Thư mục file đính kèm của gói thầu
            DANH_SACH_CONGVIEC_VIEC_GOI_THAU = 9    //Danh sách công việc của gói thầu
        }

        public enum TypeDevice //loại device login
        {
            WEB = 1,         
            IOS = 2,
            ANDROID = 3
        }

        //public enum TypeContractChild
        //{
        //    REPLACE_VALUE = 1,          //Thay thế giá trị hợp đồng
        //    INCREASE_VALUE = 2,         //Tăng giá trị hợp đồng
        //    REDUCE_VALUE = 3,           //Giảm giá trị hợp đồng
        //    REPLACE_CONTENT = 4,        //Thay đổi nội dung hợp đồng
        //    OTHER = 5                   //Khác
        //}
        public enum TypeCash    // loại phiếu
        {
            CASH_ADVANCE = 1, // phiếu đề nghị tạm ứng
            CASH = 2, // phiếu đề nghị thanh toán
            CASH_END = 3, // phiếu đề nghị quyết toán
            CASH_ITEM = 4,    // phiếu thanh toán
            UNDEFINED = 5       //Không xác định
        }

        public enum TypeCashAdvance    // loại phiếu tạm ứng
        {
            TU_HD = 1, // Tạm ứng hợp đồng
            TU_VL = 2 // Tạm ứng vật liệu
        }

        public enum TypeWarranty    // loại bảo hành phiếu quyết toán
        {
            GIU_TIEN = 1, // Giữ lại tiền bảo hành
            BL_BH = 2 // Bảo lãnh bảo hành
        }

        public enum ActionTargetType    // hành động
        {
            PROJECT = 1, // Dự án
            BIDPACKAGE = 2,    // Gói thầu
            CONTRACT = 3,    // Gói thầu
            UNIT = 4,    // Đơn vị
            USER = 5,    // Người dùng
            CONTRACT_CHILD = 6,    // Phụ lục
            CATEGORY = 7,    // Khối lượng
            PLAN = 8,    // Kế hoạch
            MONITORING_USER = 9, //Phân quyền giám sát
            MONITORING_HSTK = 10, //Hồ sơ thiết kế
            MONITORING_LSTC = 11, //Lịch sử thi công
            MONITORING_HAVD = 12, //Hình ảnh - Video
            MONITORING_KLTC = 13, //Khối lượng thi công
            MONITORING_KLNT = 14, //Khối lượng nghiệm thu
            MONITORING_KKVM = 14, //Khó khăn vướng mắc
            MONITORING_YKQL = 14, //Ý kiến chủ đầu tư
            MONITORING_HSHC = 14, //Hồ sơ hoàn công
        }

        public enum TypeValue    // giá trị thi công
        {
            PLAN = 1,    // kế hoạch
            REAL = 2, // thực tế
            ACCEPTANCE = 3, // nghiệm thu
            PAY = 4, // thanh toán
        }

        public enum MonitoringType   // loại danh mục giám sát
        {
            GSHSTC = 1, // Hồ sơ thi công
            GSLSTC = 2,    // Lịch sử thi công
            GSHAVD = 3,    // Hình ảnh - Video
            GSKLTCTT = 4,    // Khối lượng thi công thực tế
            GSKLTCNT = 5,    // Khối lượng thi công nghiệm thu
            GSKKVM = 6,    // Khó khăn vướng mắc
            GSYKCDT = 7,    // Ý kiến chủ đầu tư
            GSHSHC = 8,    // Hồ sơ hoàn công
            GSCBCHD = 9,    // Cảnh báo chập hợp đồng
        }

        public enum TypeValueCategory  //Loại giá trị
        {
            VALUE_PLAN = 1,   //giá trị kế hoạch
            VALUE_REAL = 2    //giá trị thực hiện
        }

        public enum TypeMonitoringContract // loại giám sát thi công hợp đồng
        {
            DESIGN_PROFILE = 1,
            CONTRUCTION_DIARY = 2,
            IMAGE_VIDEO = 3,
            DIFFICULTY = 4,
            INVESTOR_OPINIONc = 5,
            COMPLETED_PROFILE = 6,
            WARNING_CONTRACT = 7,        //Các cảnh báo hợp đồng chậm tiến độ,
            VOUMNE_CONSTRUCTION = 8,         // khối lượng thực hiện
            VOUMNE_CONSTRUCTION_ACCEPTANCE = 9  // khối luongj nghiệm thu
        }

        public enum TypeWarningContract // loại cảnh báo hợp đồng
        {
            UPDATE_IMAGE_VIDEO_CONSTRUCTION = 1, // cập nhật hình ảnh thi công
            UPDATE_VOLUME_CONSTRUCTION = 2,  // cập nhật khối lượng thực tế thi công
            UPDATE_VOLUME_CONSTRUCTION_ACCEPTANCE = 6,  // cập nhật khối lượng nghiệm thu
            UPDATE_DIARY_CONSTRUCTION = 3, // cập nhật nhật ký thi công
            UPDATE_PAYMENT = 4, // thanh toán
            UPDATE_CASH = 7, //bảo lãnh bảo hành
            PROCESS_CONTRACT = 5
        }

        public enum ProcessStatusDifficulty // trạng thái xử lí khó khăn vướng mắc
        {
            PROGRESSING = 1,
            FINISHED = 10
        }

        public enum TypeFCM
        {
            NEW_MESSAGE = 1, //tin nhắn
            NEW_EMAIL = 2, //email
            NEW_REPLY = 3, //trả lời
            NEW_COMMENT = 4, //bình luận
            NEW_KKVM = 5 //khó khăn vướng mắc
        }

        public enum TypeAcceptCash    // trang thái duyệt phiếu đề nghị tạm ứng, thanh toán, quyết toán
        {
            INIT = 1, // chưa duyệt
            ACCEPT = 2,    // đã duyệt
            NOT_ACCEPT = 3,    // không duyệt
        }

        public enum TypePaymentStatus    // trang thái thanh toán
        {
            INIT = 1, // chưa thanh toán
            FULL = 2,    // đã thanh toán hết
            NOT_ENOUGH = 3,    // chưa thanh toán hết
            NOT_PAYMENT = 4     // không thanh toán
        }

        public enum TypePaymentMethod    // phương thức thanh toán
        {
            DIRECT = 1, // Bằng tiền mặt
            ONLINE = 2,    // Online
            OTHER = 3,    // Khác
        }
        public enum TypeFile    // loại file
        {
            DOCUMENTS = 1, // file văn bản
            VIDEO = 2,    // file video
            AUDIO = 3,    // file âm thanh
            ELECTRONIC_BOOKS = 4,    // file sách điện tử
            IMAGES = 5,    // file hình ảnh
            ARCHIVES = 6,    // file nén
            OTHER = 7           //Loại khác
        }

        public enum ProgressReportType    // Loại báo cáo tiến độ của hợp đồng
        {
            MPP = 1,                // Báo cáo tiến độ bằng file Mpp
            AMOUNT_OF_WORK = 2,    // Báo cáo theo khối lượng công việc
        }

        public enum ConversationType        //Loại mapping hội thoại
        {
            TASK_PLAN_PROJECT = 1,           //Tổng tiến độ
            KKVM = 2,                            //Khó khăn vướng mắc
            PAYMENT = 3                     //Trao đổi trên các phiếu đề nghị tạm ứng, thanh toán, quyết toán
        }

        //Trạng thái công việc của gói thầu
        public enum BidpackageJobStatus
        {
            PROCESSING = 1,             // Đang xử lý
            OUT_OF_DATE = 2,            // Quá hạn
            COMPLETE = 3                // Hoàn thành
        }

        //loại template send mail
        public enum EmailTypes
        {
            DEFAULT = 0,
            REGISTRATION_USER = 1, 
            FORGOT_PASSWORD = 2,
        }

        //Dạng hợp đồng
        public enum ContractChannel
        {
            CDT_TT = 1,     //Chủ đầu tư - Tổng thầu
            TT_TP = 2       //Tổng thầu - thầu phụ
        }
    }
}
