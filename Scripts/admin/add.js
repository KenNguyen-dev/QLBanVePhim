function onInput_PhongChieu() {
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
function errorImg() {
    var link = "/Content/Images/default-thumbnail.jpg";
    document.getElementById("previews_image").src = link;
}