﻿function onInput_PhongChieu() {
    var id = document.getElementById("id").value;
    document.getElementById("previews_id").innerHTML = "ID " + id;
}

function onInput_SuatChieu() {
    var id = document.getElementById("id").value;
    document.getElementById("previews_id").innerHTML = "ID Suất Chiếu: " + id;

    var phimVal = document.getElementById("phim_id");
    var phim = phimVal.options[phimVal.selectedIndex].text;
    document.getElementById("previews_phim").innerHTML = "" + phim;
    var link = document.getElementById("image").options[phimVal.selectedIndex].text;
    document.getElementById("previews_image").src = link;

    var gio_bat_dau = document.getElementById("gio_bat_dau").value;
    var gio_ket_thuc = document.getElementById("gio_ket_thuc").value;
    var ngay_chieu = document.getElementById("ngay_chieu").value;
    document.getElementById("previews_time").innerHTML = gio_bat_dau + " - " + gio_ket_thuc;
    document.getElementById("previews_date").innerHTML = "Ngày chiếu: " + ngay_chieu;

    var ddpVal = document.getElementById("dinh_danh_phim_id");
    var ddp = ddpVal.options[ddpVal.selectedIndex].text;
    document.getElementById("previews_ddphim").innerHTML = "Định dạng phim: " + ddp;

    var pcVal = document.getElementById("phong_chieu_id");
    var pc = pcVal.options[pcVal.selectedIndex].text;
    document.getElementById("previews_pc").innerHTML = "ID Phòng Chiếu: " + pc;
}

function onInput_NV(type) {
    switch (type) {
        case "ten":
            var ten = document.getElementById("ho_ten").value;
            document.getElementById("previews_ten").innerHTML = "" + ten;
            break;
        case "vt":
            var vtVal = document.getElementById("vai_tro_id");
            var vt = vtVal.options[vtVal.selectedIndex].text;
            document.getElementById("previews_vaitro").innerHTML = "" + vt;
            break;
        case "gt":
            var gtVal = document.getElementById("gioi_tinh");
            var gt = gtVal.options[gtVal.selectedIndex].text;
            document.getElementById("previews_gioitinh").innerHTML = "Giới tính: " + gt;
            break;
        case "email":
            var email = document.getElementById("email").value;
            document.getElementById("previews_email").innerHTML = "Email: " + email;
            break;
        case "diachi":
            var diachi = document.getElementById("dia_chi").value;
            document.getElementById("previews_diachi").innerHTML = "Địa chỉ: " + diachi;
            break;
        case "sdt":
            var sdt = document.getElementById("so_dien_thoai").value;
            document.getElementById("previews_sdt").innerHTML = "SĐT: " + sdt;
            break;
        case "cmnd":
            var cmnd = document.getElementById("so_cmnd").value;
            document.getElementById("previews_cmnd").innerHTML = "CMND: " + cmnd;
            break;
        case "mk":
            var mk = document.getElementById("mat_khau").value;
            document.getElementById("previews_mk").innerHTML = "Mật khẩu: <password>" + mk + "<password>";
            break;
        default:
            break;
    }
}

function onInput_Phim(type) {
    switch (type) {
        case "ten":
            var ten = document.getElementById("ten").value;
            document.getElementById("previews_ten").innerHTML = "" + ten;
            break;
        case "loai":
            var loaiVal = document.getElementById("loai_phim_id");
            var loai = loaiVal.options[loaiVal.selectedIndex].text;
            document.getElementById("previews_loai").innerHTML = "Loại phim: " + loai;
            break;
        case "hinh":
            var link = document.getElementById("hinh_anh").value;
            document.getElementById("previews_hinh").src = link;
            break;
        case "id":
            var id = document.getElementById("id").value;
            document.getElementById("previews_id").innerHTML = "" + id;
            break;
        case "thoiluong":
            var val = document.getElementById("thoi_luong").value;
            document.getElementById("previews_thoiluong").innerHTML = "Thời lượng: " + val + " phút";
            break;
        case "ghtuoi":
            var val = document.getElementById("gioi_han_tuoi").value;
            document.getElementById("previews_ghtuoi").innerHTML = "Giới hạn tuổi: " + val;
            break;
        case "ngcongchieu":
            var val = document.getElementById("ngay_cong_chieu").value;
            document.getElementById("previews_ngcongchieu").innerHTML = "Ngày công chiếu: " + val;
            break;
        case "ngonngu":
            var val = document.getElementById("ngon_ngu").value;
            document.getElementById("previews_ngonngu").innerHTML = "Ngôn ngữ: " + val;
            break;
        case "dv":
            var val = document.getElementById("dien_vien").value;
            document.getElementById("previews_dv").innerHTML = "Diễn viên chính: " + val;
            break;
        case "quocgia":
            var val = document.getElementById("quoc_gia").value;
            document.getElementById("previews_quocgia").innerHTML = "Quốc gia: " + val;
            break;
        case "nsx":
            var val = document.getElementById("nha_san_xuat").value;
            document.getElementById("previews_nsx").innerHTML = "Nhà sản xuất: " + val;
            break;
        case "tt":
            var val = document.getElementById("tom_tat").value;
            document.getElementById("previews_tt").innerHTML = "Tóm tắt: " + val;
            break;
        case "trth":
            var val = document.getElementById("trang_thai").value;
            document.getElementById("previews_trth").innerHTML = "Trạng thái: " + val;
            break;
        default:
            break;
    }
}

function errorImg() {
    var link = "/Content/Images/default-thumbnail.jpg";
    document.getElementById("previews_image").src = link;
}