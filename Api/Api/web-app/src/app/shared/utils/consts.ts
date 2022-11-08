import { STColumn, STColumnTag, STColumnTagValue } from '@delon/abc/st';

//Pattern
export const Only_Text_Pattern = '^[a-zA-Z ]*$';
export const Only_Number_Pattern = '^(0|[1-9][0-9]*)$';

//Loại nhân viên
export const TypeEmployees = {
  1: 'Nhân viên',
  2: 'Bác sĩ',
  3: 'Kỹ thuật viên'
};

//Khu vực
export const TypeArea = {
  1: 'K1',
  2: 'K2',
  3: 'K3'
};

//Giới tính
export const TypeSex = {
  1: 'Nam',
  2: 'Nữ',
  3: 'Khác'
};

//Tuyến bệnh viện
export const TuyenBv = {
  1: 'Trung ương',
  2: 'Tỉnh/TP',
  3: 'Quận/Huyện',
  4: 'Phường/Xã'
};

//Trạng thái đăng ký của đơn đăng ký khám chữa bệnh
export const TrangThaiDk: STColumnTag = {
  1: { text: 'Chờ khám', color: 'orange' },
  2: { text: 'Đã khám', color: '#108ee9' },
  3: { text: 'Nhập viện nội trú', color: '#2db7f5' },
  4: { text: 'Chuyển viện', color: '#f50' },
  5: { text: 'Nhập viện ngoại trú', color: '#2db7f5' },
  6: { text: 'Đang khám', color: 'orange' },
  7: { text: 'Chờ cận lâm sàng', color: 'orange' },
  8: { text: 'Bệnh nhân không khám', color: '#f50' },
  9: { text: 'Bác sĩ hủy khám', color: '#f50' },
  10: { text: 'Thanh toán ra viện', color: '#108ee9' },
  11: { text: 'Ra viện', color: '#108ee9' }
};

//Đối tượng bệnh nhân
export const DoiTuongBn = {
  1: 'BHYT',
  2: 'Viện phí',
  //3: 'Khác'
};

// Loại nhóm dịch vụ
export const LoaiNhomDvThuoc = {
  1: 'Dịch vụ',
  2: 'Thuốc'
}

// Tuyến CMKT
export const TuyenCmkt = {
  1: 'Tuyến 1',
  2: 'Tuyến 2',
  3: 'Tuyến 3',
  4: 'Tuyến 4',
  5: 'Chưa phân tuyến'
}

// Hạng bệnh viện
export const HangBv = {
  1: 'Hạng đặc biệt',
  2: 'Hạng 1',
  3: 'Hạng 2',
  4: 'Hạng 3',
  5: 'Hạng 4',
  6: 'Chưa xếp hạng'
}

// Tuyến đăng ký
export const TuyenDk = {
  1: 'Đúng tuyến',
  2: 'Đa tuyến đúng tuyến'
}
// Loại khoa kho
export const LoaiKhoaKho = {
  1: 'Khoa',
  2: 'Phòng khám',
  3: 'Giường',
  4: 'Kho',
  5: 'Tủ trực',
}
// Loại Phẫu thuật thủ thuật
export const LoaiPttt = {
  1: 'Loại 1',
  2: 'Loại 2',
  3: 'Loại 3',
  4: 'Loại đặc biệt',
}


export const LoaiSucKhoe =
{
  1: 'A',
  2: 'B1',
  3: 'B2',
  4: 'C',
  5: 'D'
}
// Mũ Bảo hiểm
export const MuBh = {
  1: 'Có đội mũ',
  2: 'Không đội mũ'
}
