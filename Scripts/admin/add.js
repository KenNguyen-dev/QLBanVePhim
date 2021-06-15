function onInput_PhongChieu() {
    var id = document.getElementById("id").value;
    document.getElementById("previews_id").innerHTML = "ID " + id;
}

function onInput_SuatChieu(type) {
    switch (type) {
        case "id":
            var id = document.getElementById("id").value;
            document.getElementById("previews_id").innerHTML = "ID Suất Chiếu: " + id;
            break;
        case "phim":
            var phimVal = document.getElementById("phim_id");
            var phim = phimVal.options[phimVal.selectedIndex].text;
            document.getElementById("previews_phim").innerHTML = "" + phim;
            var link = document.getElementById("image").options[phimVal.selectedIndex].text;
            document.getElementById("previews_image").src = link;
            break;
        case "gio":
            var gio_bat_dau = document.getElementById("gio_bat_dau").value;
            var gio_ket_thuc = document.getElementById("gio_ket_thuc").value;
            document.getElementById("previews_time").innerHTML = gio_bat_dau + " - " + gio_ket_thuc;
            break;
        case "ngay":
            var ngay_chieu = document.getElementById("ngay_chieu").value;
            const date = new Date(ngay_chieu);
            const options = { year: 'numeric', month: 'numeric', day: 'numeric' };
            document.getElementById("previews_date").innerHTML = "Ngày chiếu: " + date.toLocaleDateString('vi-VN', options);
            break;
        case "dinhdang":
            var ddpVal = document.getElementById("dinh_danh_phim_id");
            var ddp = ddpVal.options[ddpVal.selectedIndex].text;
            document.getElementById("previews_ddphim").innerHTML = "Định dạng phim: " + ddp;
            break;
        case "phong":
            var pcVal = document.getElementById("phong_chieu_id");
            var pc = pcVal.options[pcVal.selectedIndex].text;
            document.getElementById("previews_pc").innerHTML = "ID Phòng Chiếu: " + pc;
            break;
        default:
            break;
    }
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
            const date = new Date(val);
            const options = { year: 'numeric', month: 'numeric', day: 'numeric' };
            document.getElementById("previews_ngcongchieu").innerHTML = "Ngày công chiếu: " + date.toLocaleDateString('vi-VN', options);
            break;
        case "ngonngu":
            var val = document.getElementById("ngon_ngu").value;
            document.getElementById("previews_ngonngu").innerHTML = "Ngôn ngữ: " + val;
            break;
        case "trailer":
            var val = document.getElementById("trailer").value;
            document.getElementById("previews_trailer").innerHTML = "Trailer: " + val;
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
            var trthVal = document.getElementById("trang_thai");
            var trth = trthVal.options[trthVal.selectedIndex].text;
            document.getElementById("previews_trth").innerHTML = "Trạng thái: " + trth;
            break;
        default:
            break;
    }
}

function errorImg() {
    var link = "/Content/Images/default-thumbnail.jpg";
    document.getElementById("previews_image").src = link;
}

function onInput_DoAn(type) {
    switch (type) {
        case "id":
            var val = document.getElementById("do_an_id").value;
            document.getElementById("previews_id").innerHTML = "ID:" + val;
            break;
        case "ten":
            var val = document.getElementById("do_an_ten").value;
            document.getElementById("previews_ten").innerHTML = "" + val;
            break;
        case "gia":
            var val = document.getElementById("don_gia").value;
            document.getElementById("previews_gia").innerHTML = "Đơn Giá: " + val + " VND";
            break;
        case "hinh":
            var val = document.getElementById("do_an_hinh_anh").value;
            document.getElementById("previews_hinh").src = val;
            break;
        case "loai":
            var Val = document.getElementById("do_an_loai_do_an_id");
            var v = Val.options[Val.selectedIndex].text;
            document.getElementById("previews_loai").innerHTML = "Loại Đồ Ăn: " + v;
            break;
        case "size":
            var Val = document.getElementById("kich_co_do_an_id");
            var v = Val.options[Val.selectedIndex].text;
            document.getElementById("previews_size").innerHTML = "Kích Cỡ Đồ Ăn: " + v;
            break;
        default:
            break;
    }
}
function onInput_DoAn2(type) {
    switch (type) {
        case "ten":
            var val = document.getElementById("ten").value;
            document.getElementById("previews_ten").innerHTML = "" + val;
            document.getElementById("submitBtn").disabled = false;
            break;
        case "hinh":
            var val = document.getElementById("hinh_anh").value;
            document.getElementById("previews_hinh").src = val;
            document.getElementById("submitBtn").disabled = false;
            break;
        case "loai":
            document.getElementById("submitBtn").disabled = false;
            break;
        default:
            break;
    }
}

function onInput_HoaDon(type) {
    switch (type) {
        case "da":
            var daVal = document.getElementById("do_an_chi_tiet_id");
            var price = document.getElementById("price").options[daVal.selectedIndex].text;
            var sl = document.getElementById("so_luong").value;
            document.getElementById("tongtien").innerHTML = parseInt(price) * parseInt(sl);
            break;
        case "sl":
            var daVal = document.getElementById("do_an_chi_tiet_id");
            var price = document.getElementById("price").options[daVal.selectedIndex].text;
            var sl = document.getElementById("so_luong").value;
            if (parseInt(sl) < 1) {
                document.getElementById("so_luong").value = 1;
                sl = 1;
            }
            if (parseInt(sl) > 99) {
                document.getElementById("so_luong").value = 99;
                sl = 99;
            }
            document.getElementById("tongtien").innerHTML = parseInt(price) * parseInt(sl);
            break;
        default:
            break;
    }
}