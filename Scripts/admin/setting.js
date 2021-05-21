function add_NewDDP() {
    document.getElementById("btnAdd-DDP").style.display = "none";

    var table = document.getElementById("table-DDP");

    var row = table.insertRow(0);

    var cell0 = row.insertCell(0);
    var cell1 = row.insertCell(1);
    var cell2 = row.insertCell(2);
    var cell3 = row.insertCell(3);
    var cell4 = row.insertCell(4);

    cell0.innerHTML = document.getElementById("form_id_ddp").innerHTML;
    cell1.innerHTML = document.getElementById("form_ten_ddp").innerHTML;
    cell2.innerHTML = document.getElementById("form_phuthu_ddp").innerHTML;
    cell3.innerHTML = document.getElementById("form_submit_ddp").innerHTML;
    cell4.innerHTML = document.getElementById("form_cancel_ddp").innerHTML;
}

function add_NewLP() {
    document.getElementById("btnAdd-LP").style.display = "none";

    var table = document.getElementById("table-LP");

    var row = table.insertRow(0);

    var cell0 = row.insertCell(0);
    var cell1 = row.insertCell(1);
    var cell2 = row.insertCell(2);
    var cell3 = row.insertCell(3);

    cell0.innerHTML = document.getElementById("form_id_lp").innerHTML;
    cell1.innerHTML = document.getElementById("form_ten_lp").innerHTML;
    cell2.innerHTML = document.getElementById("form_submit_lp").innerHTML;
    cell3.innerHTML = document.getElementById("form_cancel_lp").innerHTML;
}