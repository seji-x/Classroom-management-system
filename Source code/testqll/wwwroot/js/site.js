// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.


//Học sinh
function openModalHS(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalHS').modal('show');
        $('#MaHS').text(id)
        $('#txtMaHS').attr('readonly', true);
        var item = null;
        for (var i = 0; i < dataHS.length; i++) {
            if (id == dataHS[i].MaHs) {
                item = dataHS[i];
                $('#selML').val(item.MaLop);
                break;
            }
            console.log(dataHS[i]);
        }
        console.log(item);
        $('#txtMaHS').val(item.MaHs);
        $('#txtTenHS').val(item.TenHs);
        $('#txtNS').val(item.NgaySinh.slice(0, 10));
        $('#selGT').val(item.GioiTinh);
        $('#txtDiaChi').val(item.DiaChi);
        $('#txtSDT').val(item.Sdt);
        $('#txtEmail').val(item.Email);
        $('#txtMaLop').val(item.MaLop);
    }
    else {
        $('#DivModalHS').modal('show');
        $('#MaHS').text("[Thêm mới]")
        $('#txtMaHS').attr('readonly', false);
        $('#txtMaHS').val("");
        $('#txtTenHS').val("");
        $('#txtNS').val("");
        $('#selGT').val("");
        $('#txtDiaChi').val("");
        $('#txtSDT').val("");
        $('#txtEmail').val("");
        $('#txtMaLop').val("");
    }

}
function saveHS() {
    if ($("#MaHS").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaHs = $('#txtMaHS').val();
        item.TenHs = $('#txtTenHS').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        item.MaLop = parseInt($('#selML').val());
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/HocSinh?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { hs: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.hs;
                    console.log(obj);
                    item.MaHs = obj.maHs;
                    alert("Thêm mới thành công !!");
                    dataHS.push(item);
                    $("#MaHS").text(obj.maHs);
                    var htmlStr = '<tr id="trLop_' + obj.maHs + '">';
                    htmlStr += '<td>' + obj.maHs + '</td>';
                    htmlStr += '<td>' + obj.tenHs + '</td>';
                    htmlStr += '<td>' + obj.gioiTinh + '</td>';
                    htmlStr += '<td>' + obj.ngaySinh + '</td>';
                    htmlStr += '<td>' + obj.diaChi + '</td>';
                    htmlStr += '<td>' + obj.sdt + '</td>';
                    htmlStr += '<td>' + obj.email + '</td>';
                    htmlStr += '<td>' + obj.maLop + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalHS(' + "'" + obj.maHs + "'" + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteHS(' + "'" + obj.maHs + "'" + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableHS").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        console.log("cập nhập");
        var item = {};
        item.MaHs = $('#MaHS').text();
        item.TenHs = $('#txtTenHS').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        item.MaLop = parseInt($('#selML').val());
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/HocSinh?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { hs: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trHS_" + item.MaHs + " td:eq(1)").html(item.TenHs)
                    $("#trHS_" + item.MaHs + " td:eq(2)").html(item.GioiTinh)
                    $("#trHS_" + item.MaHs + " td:eq(3)").html(item.NgaySinh)
                    $("#trHS_" + item.MaHs + " td:eq(4)").html(item.DiaChi)
                    $("#trHS_" + item.MaHs + " td:eq(5)").html(item.Sdt)
                    $("#trHS_" + item.MaHs + " td:eq(6)").html(item.Email)
                    $("#trHS_" + item.MaHs + " td:eq(7)").html(item.MaLop)
                    for (var i = 0; i < dataHS.length; i++) {
                        if (item.MaLop == dataHS[i].MaHs) {
                            dataHS[i] = item;
                            console.log(dataHS[i])
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteHS(id) {
    if (confirm("Bạn có muốn xoá học sinh này không?") == true) {

    $.ajax({
        type: "POST",
        url: "/HocSinh?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { maHS: id },
        dataType: "json",
        success: function (res) {
            console.log(res)
            if (res.success) {
                alert("Xoá thành công !!");
                $("#trHS_" + id).remove();
                console.log('trHS_' + id);
                var i = 0;
                console.item;
                for (i = 0; i < dataHS.length; i++) {
                    if (id == dataHS[i].MaHs) {
                        break;
                    }
                }
                dataHS.splice(i, 1);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
    }
}
//Lớp
function openModal(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModal').modal('show');
        $('#MaLop').text(id);
        var item = null;
        for (var i = 0; i < dataLop.length; i++) {
            if (id == dataLop[i].MaLop) {
                item = dataLop[i];
                break;
            }
        }
        $('#txtTenLop').val(item.TenLop);
        $('#txtPhongHoc').val(item.PhongHoc);
        $('#txtMoTa').val(item.MoTa);
        $('#selTT').val(item.TrangThai);
    }
    //Thêm mới
    else {
        $('#DivModal').modal('show');
        $('#MaLop').text("[Thêm mới]");
        $('#txtTenLop').val("");
        $('#txtPhongHoc').val("");
        $('#txtMoTa').val("");
        $('#txtTT').val("");
    }
}
function saveLop() {
    //console.log($("#MaLop").text());
    if ($("#MaLop").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenLop = $('#txtTenLop').val();
        item.PhongHoc = $('#txtPhongHoc').val();
        item.MoTa = $('#txtMoTa').val();
        item.TrangThai = $('#selTT').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/Lop?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { lop: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    var obj = res.lop;
                    item.MaLop = obj.maLop;
                    alert("Thêm mới thành công !!");
                    dataLop.push(item);
                    $("#MaLop").text(obj.maLop);
                    var htmlStr = '<tr id="trLop_' + obj.maLop + '">';
                    htmlStr += '<td>' + obj.maLop + '</td>';
                    htmlStr += '<td>' + obj.tenLop + '</td>';
                    htmlStr += '<td>' + obj.phongHoc + '</td>';
                    htmlStr += '<td>' + obj.moTa + '</td>';
                    htmlStr += '<td>' + obj.trangThai + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModal(' + obj.maLop + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteLop(' + obj.maLop + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableLop").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
                alert("Trùng dữ liệu, vui lòng kiểm tra lại!")
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaLop = parseInt($('#MaLop').text());
        item.TenLop = $('#txtTenLop').val();
        item.PhongHoc = $('#txtPhongHoc').val();
        item.MoTa = $('#txtMoTa').val();
        item.TrangThai = $('#selTT').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/Lop?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { lop: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trLop_" + item.MaLop + " td:eq(1)").html(item.TenLop);
                    $("#trLop_" + item.MaLop + " td:eq(2)").html(item.PhongHoc)
                    $("#trLop_" + item.MaLop + " td:eq(3)").html(item.MoTa)
                    $("#trLop_" + item.MaLop + " td:eq(4)").html(item.TrangThai)
                    for (var i = 0; i < dataLop.length; i++) {
                        if (item.MaLop == dataLop[i].MaLop) {
                            dataLop[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteLop(id) {
    if (confirm("Bạn có muốn xoá lớp này không?") == true) {
    $.ajax({
            type: "POST",
            url: "/Lop?handler=Delete",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { maLop: id },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Xoá thành công !!");
                    $("#trLop_" + id).remove();
                    var i = 0;
                    for (i = 0; i < dataLop.length; i++) {
                        if (id == dataLop[i].MaLop) {
                            console.log(i);
                            break;
                        }
                    }
                    dataLop.splice(i, 1);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }
    
}
//Giáo viên
function openModalGV(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalGV').modal('show');
        $('#MaGV').text(id);
        $('#txtMaGV').attr('readonly', true);
        var item = null;
        for (var i = 0; i < dataGV.length; i++) {
            if (id == dataGV[i].MaGv) {
                item = dataGV[i];
                break;
            }
        }
        $('#txtMaGV').val(item.MaGv);
        $('#txtTenGV').val(item.TenGv);
        $('#txtNS').val(item.NgaySinh.slice(0, 10));
        $('#selGT').val(item.GioiTinh);
        $('#txtDiaChi').val(item.DiaChi);
        $('#txtSDT').val(item.Sdt);
        $('#txtEmail').val(item.Email);
        $('#txtCN').val(item.ChuyenNganh);
        $('#txtTDCM').val(item.TrinhDoChuyenMon);
    }
    else {
        $('#DivModalGV').modal('show');
        $('#MaGV').text("[Thêm mới]")
        $('#txtMaGV').attr('readonly', false);
        $('#txtMaGV').val("");
        $('#txtTenGV').val("");
        $('#txtNS').val("");
        $('#selGT').val("");
        $('#txtDiaChi').val("");
        $('#txtSDT').val("");
        $('#txtEmail').val("");
        $('#txtCN').val("");
        $('#txtTDCM').val("");
    }

}
function saveGV() {
    if ($("#MaGV").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaGv = $('#txtMaGV').val();
        item.TenGv = $('#txtTenGV').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        item.ChuyenNganh = $('#txtCN').val();
        item.TrinhDoChuyenMon = $('#txtTDCM').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/GiaoVien?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { gv: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.gv;
                    console.log(obj);
                    item.MaGv = obj.maGv;
                    alert("Thêm mới thành công !!");
                    dataGV.push(item);
                    $("#MaGV").text(obj.maGv);
                    var htmlStr = '<tr id="trGV_' + obj.maGv + '">';
                    htmlStr += '<td>' + obj.maGv + '</td>';
                    htmlStr += '<td>' + obj.tenGv + '</td>';
                    htmlStr += '<td>' + obj.gioiTinh + '</td>';
                    htmlStr += '<td>' + obj.ngaySinh.slice(0, 10) + '</td>';
                    htmlStr += '<td>' + obj.diaChi + '</td>';
                    htmlStr += '<td>' + obj.sdt + '</td>';
                    htmlStr += '<td>' + obj.email + '</td>';
                    htmlStr += '<td>' + obj.chuyenNganh + '</td>';
                    htmlStr += '<td>' + obj.trinhDoChuyenMon + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalGV(' + "'" + obj.maGv + "'" + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteGV(' + "'" + obj.maGv + "'" + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableGV").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhập
        var item = {};
        item.MaGv = $('#txtMaGV').val();
        item.TenGv = $('#txtTenGV').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        item.ChuyenNganh = $('#txtCN').val();
        item.TrinhDoChuyenMon = $('#txtTDCM').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/GiaoVien?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { gv: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trGV_" + item.MaGv + " td:eq(1)").html(item.TenGv)
                    $("#trGV_" + item.MaGv + " td:eq(2)").html(item.GioiTinh)
                    $("#trGV_" + item.MaGv + " td:eq(3)").html(item.NgaySinh.slice(0, 10))
                    $("#trGV_" + item.MaGv + " td:eq(4)").html(item.DiaChi)
                    $("#trGV_" + item.MaGv + " td:eq(5)").html(item.Sdt)
                    $("#trGV_" + item.MaGv + " td:eq(6)").html(item.Email)
                    $("#trGV_" + item.MaGv + " td:eq(7)").html(item.ChuyenNganh)
                    $("#trGV_" + item.MaGv + " td:eq(8)").html(item.TrinhDoChuyenMon)
                    for (var i = 0; i < dataGV.length; i++) {
                        if (item.MaGv == dataGV[i].MaGv) {
                            dataGV[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteGV(id) {
    if (confirm("bạn có muốn xoá giáo viên này không") == true) {
        $.ajax({
                type: "POST",
                url: "/GiaoVien?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maGV: id },
                dataType: "json",
                success: function (res) {
                    console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        $("#trGV_" + id).remove();
                        var i = 0;
                        console.item;
                        for (i = 0; i < dataGV.length; i++) {
                            if (id == dataGV[i].MaGv) {
                                break;
                            }
                        }
                        dataGv.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    alert("Giáo viên còn đang dạy, vui lòng kiểm tra lại!");
                }

            });
    }
    
}
//Môn học
function openModalMH(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalMH').modal('show');
        $('#MaMH').text(id)
        var item = null;
        for (var i = 0; i < dataMH.length; i++) {
            if (id == dataMH[i].MaMh) {
                item = dataMH[i];
                break;
            }
        }
        $('#txtTenMH').val(item.TenMh);
    }
    else {
        $('#DivModalMH').modal('show');
        $('#MaMH').text("[Thêm mới]")
        $('#txtTenMH').val("");
    }

}
function saveMH() {
    if ($('#MaMH').text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenMh = $('#txtTenMH').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/MonHoc?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { mh: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.mh;
                    console.log(obj);
                    item.MaMh = obj.maMh;
                    alert("Thêm mới thành công !!");
                    dataMH.push(item);
                    $("#MaMH").text(obj.maMh);
                    var htmlStr = '<tr id="trMH_' + obj.maMh + '">';
                    htmlStr += '<td>' + obj.maMh + '</td>';
                    htmlStr += '<td>' + obj.tenMh + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalMH(' + obj.maMh + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteMH(' + obj.maMh + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';
                    $("#tableMH").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaMh = parseInt($('#MaMH').text());
        item.TenMh = $('#txtTenMH').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/MonHoc?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { mh: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trMH_" + item.MaMh + " td:eq(1)").html(item.TenMh)
                    for (var i = 0; i < dataMH.length; i++) {
                        if (item.MaMh == dataMH[i].MaMh) {
                            dataMH[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteMH(id) {
    if (confirm("Bạn có muốn xoá môn học này không?") == true) {
        $.ajax({
                type: "POST",
                url: "/MonHoc?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maMH: id },
                dataType: "json",
                success: function (res) {
                    console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        $("#trMH_" + id).remove();
                        var i = 0;
                        console.item;
                        for (i = 0; i < dataMH.length; i++) {
                            if (id == dataMH[i].MaMh) {
                                break;
                            }
                        }
                        dataMH.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    alert("Môn này còn được dạy ở lớp, vui lòng kiểm tra lại");
                }
            });
    }
    
}
//Khoá học
function openModalKH(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalKH').modal('show');
        $('#MaKH').text(id)
        var item = null;
        for (var i = 0; i < dataKH.length; i++) {
            if (id == dataKH[i].MaKh) {
                item = dataKH[i];
                break;
            }
        }
        console.log(item);
        $('#txtTenKH').val(item.TenKh);
        $('#txtNBD').val(item.NgayBatDau.slice(0, 10));
        $('#txtNKT').val(item.NgayKetThuc.slice(0, 10));
    }
    else {
        $('#DivModalKH').modal('show');
        $('#MaKH').text("[Thêm mới]")
        $('#txtTenKH').val("");
        $('#txtNBD').val("");
        $('#txtNKT').val("");
    }

}
function saveKH() {
    if ($("#MaKH").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenKh = $('#txtTenKH').val();
        item.NgayBatDau = $('#txtNBD').val();
        item.NgayKetThuc = $('#txtNKT').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/KhoaHoc?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { kh: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.kh;
                    console.log(obj);
                    item.MaKh = obj.maKh;
                    alert("Thêm mới thành công !!");
                    dataKH.push(item);
                    $("#MaKH").text(obj.maKh);
                    var htmlStr = '<tr id="trKH_' + obj.maKh + '">';
                    htmlStr += '<td>' + obj.maKh + '</td>';
                    htmlStr += '<td>' + obj.tenKh + '</td>';
                    htmlStr += '<td>' + obj.ngayBatDau + '</td>';
                    htmlStr += '<td>' + obj.ngayKetThuc + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalKH(' + obj.maKh + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteKH(' + obj.maKh + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableKH").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        console.log("cập nhập");
        var item = {};
        item.MaKh = parseInt($('#MaKH').text());
        item.TenKh = $('#txtTenKH').val();
        item.NgayBatDau = $('#txtNBD').val();
        item.NgayKetThuc = $('#txtNKT').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/KhoaHoc?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { kh: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trKH_" + item.MaKh + " td:eq(1)").html(item.TenKh)
                    $("#trKH_" + item.MaKh + " td:eq(2)").html(item.NgayBatDau)
                    $("#trKH_" + item.MaKh + " td:eq(3)").html(item.NgayKetThuc)
                    for (var i = 0; i < dataKH.length; i++) {
                        if (item.MaKh == dataKH[i].MaKh) {
                            dataKH[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteKH(id) {
    if (confirm("Bạn có muốn xoá khoá học này không") == true) {
        $.ajax({
                type: "POST",
                url: "/KhoaHoc?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maKH: id },
                dataType: "json",
                success: function (res) {
                    console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        $("#trKH_" + id).remove();
                        var i = 0;
                        console.item;
                        for (i = 0; i < dataKH.length; i++) {
                            if (id == dataKH[i].MaKh) {
                                break;
                            }
                        }
                        dataKH.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    alert("Không thể xoá khoá học này. Khoá học này đã bắt đầu hoặc đã kết thúc."); 
                }

            });
    }
    
}
//Admin
function openModalAd(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalAd').modal('show');
        $('#MaAd').text(id)
        $('#txtMaAd').attr('readonly', true);
        var item = null;
        for (var i = 0; i < dataAd.length; i++) {
            if (id == dataAd[i].MaAdmin) {
                item = dataAd[i];
                break;
            }
        }
        $('#txtMaAd').val(item.MaAdmin);
        $('#txtTenAd').val(item.TenAdmin);
        $('#txtNS').val(item.NgaySinh.slice(0, 10));
        $('#selGT').val(item.GioiTinh);
        $('#txtDiaChi').val(item.DiaChi);
        $('#txtSDT').val(item.Sdt);
        $('#txtEmail').val(item.Email);
    }
    else {
        $('#DivModalAd').modal('show');
        $('#MaAd').text("[Thêm mới]");
        $('#txtMaAd').attr('readonly', false);
        $('#txtMaAd').val("");
        $('#txtTenAd').val("");
        $('#txtNS').val("");
        $('#selGT').val("");
        $('#txtDiaChi').val("")
        $('#txtSDT').val("");
        $('#txtEmail').val("")
    }
}
function saveAd() {
    if ($("#MaAd").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaAdmin = $('#txtMaAd').val();
        item.TenAdmin = $('#txtTenAd').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/Admin?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { ad: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.ad;
                    console.log(obj);
                    item.MaAdmin = obj.maAdmin;
                    alert("Thêm mới thành công !!");
                    dataAd.push(item);
                    $("#MaAd").text(obj.maAdmin);
                    var htmlStr = '<tr id="trAd_' + obj.maAdmin + '">';
                    htmlStr += '<td>' + obj.maAdmin + '</td>';
                    htmlStr += '<td>' + obj.tenAdmin + '</td>';
                    htmlStr += '<td>' + obj.gioiTinh + '</td>';
                    htmlStr += '<td>' + obj.ngaySinh + '</td>';
                    htmlStr += '<td>' + obj.diaChi + '</td>';
                    htmlStr += '<td>' + obj.sdt + '</td>';
                    htmlStr += '<td>' + obj.email + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalAd(' + "'" + obj.maAdmin + "'" + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteAd(' + "'" + obj.maAdmin + "'" + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableAd").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhập
        var item = {};
        item.MaAdmin = $('#MaAd').text();
        item.TenAdmin = $('#txtTenAd').val();
        item.GioiTinh = $('#selGT').val();
        item.NgaySinh = $('#txtNS').val();
        item.DiaChi = $('#txtDiaChi').val();
        item.Sdt = parseFloat($('#txtSDT').val());
        item.Email = $('#txtEmail').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/Admin?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { ad: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trAd_" + item.MaAdmin + " td:eq(1)").html(item.TenAdmin);
                    $("#trAd_" + item.MaAdmin + " td:eq(2)").html(item.GioiTinh);
                    $("#trAd_" + item.MaAdmin + " td:eq(3)").html(item.NgaySinh);
                    $("#trAd_" + item.MaAdmin + " td:eq(4)").html(item.DiaChi);
                    $("#trAd_" + item.MaAdmin + " td:eq(5)").html(item.Sdt);
                    $("#trAd_" + item.MaAdmin + " td:eq(6)").html(item.Email);
                    for (var i = 0; i < dataAd.length; i++) {
                        if (item.MaAdmin == dataAd[i].MaAdmin) {
                            dataAd[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteAd(id) {
    $.ajax({
        type: "POST",
        url: "/Admin?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { maAd: id },
        dataType: "json",
        success: function (res) {
            console.log(res)
            if (res.success) {
                alert("Xoá thành công !!");
                $("#trAd_" + id).remove();
                var i = 0;
                console.item;
                for (i = 0; i < dataAd.length; i++) {
                    if (id == dataAd[i].MaAdmin) {
                        break;
                    }
                }
                dataAd.splice(i, 1);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
}
//Tài khoản Admin
function openModalTKA(id) {
    if (id != null) {
        //Cập nhập
        $('#txtTDN').attr('readonly', true);
        $('#txtMaAd').attr('readonly', true);
        $('#DivModalTKA').modal('show');
        $('#MaTk').text(id);
        var item = null;
        for (var i = 0; i < dataTKA.length; i++) {
            if (id == dataTKA[i].MaTk) {
                item = dataTKA[i];
                break;
            }
        }
        $('#txtTDN').val(item.TenDangNhap);
        $('#txtMK').val(item.MatKhau);
        $('#txtMaAd').val(item.MaAdmin);
        $('#txtMaAd').val(item.MaAdmin);
    }
    //Thêm mới
    else {
        $('#DivModalTKA').modal('show');
        $('#MaTk').text("[Thêm mới]");
        $('#txtTDN').val("");
        $('#txtMK').val("");
        $('#txtMaAd').val("");
        $('#txtTDN').attr('readonly', false);
        $('#txtMaAd').attr('readonly', false);

    }
}
function saveTKA() {
    if ($("#MaTk").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaAdmin = $('#txtMaAd').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanAdmin?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tka: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    var obj = res.tka;
                    item.MaTk = obj.maTk;
                    alert("Thêm mới thành công !!");
                    dataTKA.push(item);
                    $("#MaTk").text(obj.maTk);
                    var htmlStr = '<tr id="trTKA_' + obj.maTk + '">';
                    htmlStr += '<td>' + obj.maTk + '</td>';
                    htmlStr += '<td>' + obj.tenDangNhap + '</td>';
                    htmlStr += '<td>' + obj.matKhau + '</td>';
                    htmlStr += '<td>' + obj.maAdmin + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalTKA(' + obj.maTk + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteTKA(' + obj.maTk + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableTKA").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaTk = parseInt($('#MaTk').text());
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaAdmin = $('#txtMaAd').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanAdmin?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tka: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trTKA_" + item.MaTk + " td:eq(2)").html(item.MatKhau)
                    for (var i = 0; i < dataTKA.length; i++) {
                        if (item.MaTk == dataTKA[i].MaTk) {
                            dataTKA[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteTKA(id) {
    $.ajax({
        type: "POST",
        url: "/TaiKhoanAdmin?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { maTKA: id },
        dataType: "json",
        success: function (res) {
            console.log(res)
            if (res.success) {
                alert("Xoá thành công !!");
                $("#trTKA_" + id).remove();
                var i = 0;
                for (i = 0; i < dataTKA.length; i++) {
                    if (id == dataTKA[i].MaTk) {
                        break;
                    }
                }
                dataTKA.splice(i, 1);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
}
//Tài khoản học sinh
function openModalTKHS(id) {
    if (id != null) {
        //Cập nhập
        $('#txtTDN').attr('readonly', true);
        $('#txtMaHS').attr('readonly', true);
        $('#DivModalTKHS').modal('show');
        $('#MaTKHS').text(id);
        var item = null;
        for (var i = 0; i < dataTKHS.length; i++) {
            if (id == dataTKHS[i].MaTk) {
                item = dataTKHS[i];
                break;
            }
        }
        $('#txtTDN').val(item.TenDangNhap);
        $('#txtMK').val(item.MatKhau);
        $('#txtMaHS').val(item.MaHs);
    }
    //Thêm mới
    else {
        $('#DivModalTKHS').modal('show');
        $('#MaTKHS').text("[Thêm mới]");
        $('#txtTDN').val("");
        $('#txtMK').val("");
        $('#txtMaHS').val("");
        $('#txtTDN').attr('readonly', false);
        $('#txtMaHS').attr('readonly', false);

    }
}
function saveTKHS() {
    if ($("#MaTKHS").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaHs = $('#txtMaHS').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanHS?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkhs: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    var obj = res.tkhs;
                    item.MaTk = obj.maTk;
                    alert("Thêm mới thành công !!");
                    dataTKHS.push(item);
                    $("#MaTk").text(obj.maTk);
                    var htmlStr = '<tr id="trTKHS_' + obj.maTk + '">';
                    htmlStr += '<td>' + obj.maTk + '</td>';
                    htmlStr += '<td>' + obj.tenDangNhap + '</td>';
                    htmlStr += '<td>' + obj.matKhau + '</td>';
                    htmlStr += '<td>' + obj.maHs + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalTKHS(' + obj.maTk + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteTKHS(' + obj.maTk + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableTKHS").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaTk = parseInt($('#MaTKHS').text());
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaHs = $('txtMaHS').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanHS?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkhs: str },
            dataType: "json",
            success: function (res) {
                //console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trTKHS_" + item.MaTk + " td:eq(2)").html(item.MatKhau)
                    for (var i = 0; i < dataTKHS.length; i++) {
                        if (item.MaTk == dataTKHS[i].MaTk) {
                            dataTKHS[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteTKHS(id) {
    $.ajax({
        type: "POST",
        url: "/TaiKhoanHS?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { maTKHS: id },
        dataType: "json",
        success: function (res) {
            //console.log(res)
            if (res.success) {
                alert("Xoá thành công !!");
                $("#trTKHS_" + id).remove();
                var i = 0;
                for (i = 0; i < dataTKHS.length; i++) {
                    if (id == dataTKHS[i].MaTk) {
                        break;
                    }
                }
                dataTKHS.splice(i, 1);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
}
//Tài khoản giáo viên
function openModalTKGV(id) {
    if (id != null) {
        //Cập nhập
        $('#txtTDN').attr('readonly', true);
        $('#txtMaGV').attr('readonly', true);
        $('#DivModalTKGV').modal('show');
        $('#MaTKGV').text(id);
        var item = null;
        for (var i = 0; i < dataTKGV.length; i++) {
            if (id == dataTKGV[i].MaTk) {
                item = dataTKGV[i];
                break;
            }
        }
        $('#txtTDN').val(item.TenDangNhap);
        $('#txtMK').val(item.MatKhau);
        $('#txtMaGV').val(item.MaGv);
    }
    //Thêm mới
    else {
        $('#DivModalTKGV').modal('show');
        $('#MaTKGV').text("[Thêm mới]");
        $('#txtTDN').val("");
        $('#txtMK').val("");
        $('#txtMaGV').val("");
        $('#txtTDN').attr('readonly', false);
        $('#txtMaGV').attr('readonly', false);

    }
}
function saveTKGV() {
    if ($("#MaTKGV").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaGv = $('#txtMaGV').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanGV?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkgv: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    var obj = res.tkgv;
                    item.MaTk = obj.maTk;
                    alert("Thêm mới thành công !!");
                    dataTKGV.push(item);
                    $("#MaTk").text(obj.maTk);
                    var htmlStr = '<tr id="trTKGV_' + obj.maTk + '">';
                    htmlStr += '<td>' + obj.maTk + '</td>';
                    htmlStr += '<td>' + obj.tenDangNhap + '</td>';
                    htmlStr += '<td>' + obj.matKhau + '</td>';
                    htmlStr += '<td>' + obj.maGv + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalTKGV(' + obj.maTk + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteTKGV(' + obj.maTk + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';

                    $("#tableTKGV").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaTk = parseInt($('#MaTKGV').text());
        item.TenDangNhap = $('#txtTDN').val();
        item.MatKhau = $('#txtMK').val();
        item.MaGv = $('#txtMaGV').val();
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/TaiKhoanGV?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkgv: str },
            dataType: "json",
            success: function (res) {
                //console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trTKGV_" + item.MaTk + " td:eq(2)").html(item.MatKhau);
                    for (var i = 0; i < dataTKGV.length; i++) {
                        if (item.MaTk == dataTKGV[i].MaTk) {
                            dataTKGV[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteTKGV(id) {
    $.ajax({
        type: "POST",
        url: "/TaiKhoanGV?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { maTKGV: id },
        dataType: "json",
        success: function (res) {
            //console.log(res)
            if (res.success) {
                alert("Xoá thành công !!");
                $("#trTKGV_" + id).remove();
                var i = 0;
                for (i = 0; i < dataTKGV.length; i++) {
                    if (id == dataTKGV[i].MaTk) {
                        break;
                    }
                }
                dataTKGV.splice(i, 1);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
}
//Điểm
function openModalD(idMH, idHS, idKh) {
    if (idMH != null || idHS != null || idKh != null) {
        //Cập nhập
        $('#DivModalD').modal('show');
        $('#MaMH').text("Môn học " + idMH);
        $('#MaHS').text("học sinh " + idHS);
        var item = null;
        for (var i = 0; i < dataD.length; i++) {
            if (idMH == dataD[i].MaMh && idHS == dataD[i].MaHs) {
                item = dataD[i];
                break;
            }
        }
        console.log(item);  
        $('#txtMaMH').val(item.MaMh);
        $('#txtMaHS').val(item.MaHs);
        $('#txtDiem').val(item.Diem);
        $('#txtMaKH').val(item.MaKh);
        $('#txtMaMH').attr('readonly', true);
        $('#txtMaHS').attr('readonly', true);
    }
    else {
        $('#DivModalD').modal('show');
        $('#MaMH').text("[Thêm mới]");
        $('#MaHS').text("");
        $('#txtMaMH').val("");
        $('#txtMaHS').val("");
        $('#txtDiem').val("");
        $('#txtMaKH').val("");
        $('#txtMaMH').attr('readonly', false);
        $('#txtMaHS').attr('readonly', false);
    }

}
function saveD() {
    if ($('#MaMH').text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaMh = parseInt($('#txtMaMH').val());
        item.MaHs = $('#txtMaHS').val();
        item.Diem = parseFloat($('#txtDiem').val());
        item.MaKh = parseInt($('#txtMaKH').val());
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/Diem?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { d: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.d;
                    console.log(obj);
                    alert("Thêm mới thành công !!");
                    dataD.push(item);
                    $("#MaMH").text("mã môn " + obj.maMh);
                    $("#MaHS").text("học sinh " + obj.maHs);
                    var htmlStr = '<tr id="trD_' + obj.maMh + obj.maHs + obj.maKh +'">';
                    htmlStr += '<td>' + obj.maKh + '</td>';
                    htmlStr += '<td>' + obj.maMh + '</td>';
                    htmlStr += '<td>' + obj.maHs + '</td>';
                    htmlStr += '<td>' + obj.diem + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalD(' + obj.maMh + ", '" + obj.maHs + "'" + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteD(' + obj.maMh + ", '" + obj.maHs + "'" + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';
                    $("#tableD").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaMh = parseInt($('#txtMaMH').val());
        item.MaHs = $('#txtMaHS').val();
        item.Diem = parseFloat($('#txtDiem').val());
        item.MaKh = parseInt($('#txtMaKH').val());
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/Diem?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { d: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trD_" + item.MaMh + item.MaHs + item.MaKh + " td:eq(3)").html(item.Diem);
                    $("#trD_" + item.MaMh + item.MaHs + item.MaKh + " td:eq(0)").html(item.MaKh);
                    console.log(item.Diem);
                    for (var i = 0; i < dataD.length; i++) {
                        if (item.MaMh == dataD[i].MaMh && item.MaHs == dataD[i].maHS) {
                            dataD[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteD(idMH, idHS, idKh) {
    if (confirm("Bạn có muốn xoá điểm này không?") == true) {
        $.ajax({
                type: "POST",
                url: "/Diem?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maMH: idMH, maHS: idHS },
                dataType: "json",
                success: function (res) {
                    //console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        $("#trD_" + idMH + idHS + idKh).remove();
                        var i = 0;
                        for (i = 0; i < dataD.length; i++) {
                            if (idMH == dataD[i].MaMh && idHS == dataD[i].MaHs) {
                                break;
                            }
                        }
                        dataD.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }

            });
    }
    
}
//Thời khoá biểu
function openModalTKB(id) {
    if (id != null) {
        //Cập nhập
        $('#DivModalTKB').modal('show');
        $('#MaTKB').text(id)
        var item = null;
        for (var i = 0; i < dataTKB.length; i++) {
            if (id == dataTKB[i].MaTkb) {
                item = dataTKB[i];
                break;
            }
        }
        console.log(item);
        $('#txtMaTKB').val(item.MaTkb);
        $('#selMaKH').val(item.MaKh);
        $('#selMaKH').attr('disabled', true);
        $('#txtMaTKB').attr('disabled', true);
        if (item.TrangThai == true) {
            $('#selTT').val("true");
        }
        else {
            $('#selTT').val("false");
        }
    }
    else {
        $('#DivModalTKB').modal('show');
        $('#MaTKB').text("[Thêm mới]");
        $('#txtMaTKB').val("");
        $('#selMaKH').val("");
        $('#selMaKH').attr('disabled', false);
        $('#txtMaTKB').attr('disabled', true);
        $('#selTT').val("");

    }

}
function saveTKB() {
    if ($('#MaTKB').text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaKh = parseInt($('#selMaKH').val());

        if ($('#selTT').val() == true) {
            item.TrangThai = true;
        }
        else {
            item.TrangThai = false;
        }
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/TKB?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkb: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.tkb;
                    console.log(obj);
                    item.MaTkb = obj.maTkb;
                    alert("Thêm mới thành công !!");
                    dataTKB.push(item);
                    $("#MaTKB").text(obj.maTkb);
                    var htmlStr = '<tr id="trTKB_' + obj.maTkb + '">';
                    htmlStr += '<td>' + obj.maTkb + '</td>';
                    htmlStr += '<td>' + obj.maKh + '</td>';
                    htmlStr += '<td>' + obj.trangThai + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalTKB(' + obj.maTkb + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="openModalTKB(' + obj.maTkb + ');" class="btn btn-outline-primary btn-sm">';
                    htmlStr += '<a class="nav - link text - dark chitiet"  asp-area="" asp-page=" / index">chi tiết</a></button>';
                    htmlStr += '<button type="button" onclick="deleteTKB(' + obj.maTkb + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';
                    $("#tableTKB").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Thêm thất bại !!");
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.MaTkb = parseInt($('#txtMaTKB').val());
        item.MaKh = parseInt($('#selMaKH').val());
        if ($('#selTT').val() == true) {
            item.TrangThai = true;
        }
        else {
            item.TrangThai = false;
        }
        var str = JSON.stringify(item);
        //console.log(str);
        $.ajax({
            type: "POST",
            url: "/TKB?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkb: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    $("#trTKB_" + item.MaTkb + " td:eq(1)").html(item.MaKh)
                    for (var i = 0; i < dataTKB.length; i++) {
                        if (item.MaTkb == dataTKB[i].MaTkb) {
                            dataTKB[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteTKB(id) {
    if (confirm("Bạn có muốn xoá thời khoá biểu này không?") == true) {
        $.ajax({
                type: "POST",
                url: "/TKB?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maTKB: id },
                dataType: "json",
                success: function (res) {
                    console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        $("#trTKB_" + id).remove();
                        var i = 0;
                        console.item;
                        for (i = 0; i < dataTKB.length; i++) {
                            if (id == dataTKB[i].MaTkb) {
                                break;
                            }
                        }
                        dataTKB.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });
    }
    
}
//Chi tiết thời khoá biểu
function openModalTKBCT(idLop, thu, tiet) {
    if (idLop != null && thu != null && tiet != null) {
        //Cập nhập
        $('#DivModalTKBCT').modal('show');
        $('#MaLop').text("lớp: "+idLop);
        $('#Thu').text(", thứ: "+ thu);
        $('#Tiet').text(", tiết: "+tiet);
        var item = null;
        for (var i = 0; i < dataTKBCT.length; i++) {
            if (idLop == dataTKBCT[i].Malop && thu == dataTKBCT[i].Thu && tiet == dataTKBCT[i].Tiet) {
                item = dataTKBCT[i];
                break;
            }
        }
        console.log(item);
        $('#txtMaTKB').val(item.MaTkb);                          
        $('#txtMaLop').val(item.Malop);
        $('#txtThu').val(item.Thu);
        $('#txtTiet').val(item.Tiet);
        $('#txtMH').val(item.MaMh);
        $('#txtGV').val(item.MaGv);
        $('#txtMaLop').attr('readonly', true);
        $('#txtThu').attr('readonly', true);
        $('#txtTiet').attr('readonly', true);
    }
    //Thêm mới
    else {
        $('#DivModalTKBCT').modal('show');
        $('#MaLop').text("[Thêm mới]");
        $('#Thu').text("");
        $('#Tiet').text("");
        $('#txtMaTKB').val("");
        $('#txtMaLop').val("");
        $('#txtThu').val("");
        $('#txtTiet').val("");
        $('#txtMH').val("");
        $('#txtGV').val("");;
        $('#txtMaLop').attr('readonly', false);
        $('#txtThu').attr('readonly', false);
        $('#txtTiet').attr('readonly', false);
    }
}
function saveTKBCT() {
    //console.log($("#MaLop").text());
    if ($("#MaLop").text() == "[Thêm mới]") {
        //Thêm mới
        var item = {};
        item.MaTkb = parseInt($('#txtMaTKB').val());
        item.Malop = parseInt($('#txtMaLop').val());
        item.Thu = parseInt($('#txtThu').val());
        item.Tiet = parseInt($('#txtTiet').val());
        item.MaMh = parseInt($('#txtMH').val());
        item.MaGv = $('#txtGV').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/ChiTietTKB?handler=Add",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkbct: str },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    var obj = res.tkbct;
                    alert("Thêm mới thành công !!");
                    dataTKBCT.push(item);
                    $("#MaLop").text(obj.maLop);
                    $("#Thu").text(obj.thu);
                    $("#Tiet").text(obj.tiet);
                    console.log(obj);
                    var htmlStr = '<tr id="trTKBCT_' + obj.malop + +obj.thu + obj.tiet +'">';
                    htmlStr += '<td>' + obj.maTkb + '</td>';
                    htmlStr += '<td>' + obj.thu + '</td>';
                    htmlStr += '<td>' + obj.tiet + '</td>';
                    htmlStr += '<td>' + obj.malop + '</td>';
                    htmlStr += '<td>' + obj.maMh + '</td>';
                    htmlStr += '<td>' + obj.maGv + '</td>';
                    htmlStr += '<td>' + ' <button type="button" onclick="openModalTKBCT(' + obj.malop + ", " + obj.thu + ", " + obj.tiet + ');" class="btn btn-outline-primary btn-sm">Sửa</button>';
                    htmlStr += '<button type="button" onclick="deleteTKBCT(' + obj.malop + ", " + obj.thu + ", " + obj.tiet + ');" class="btn btn-outline-danger btn-sm">xoá</button>' + '</td></tr>';
                    $("#tableTKBCT").append(htmlStr);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }
    else {
        //Cập nhật
        var item = {};
        item.Malop = parseInt($('#txtMaLop').val());
        console.log(item.MaLop);
        item.Thu = parseInt($('#txtThu').val());
        item.Tiet = parseInt($('#txtTiet').val());
        item.MaTkb = parseInt($('#txtMaTKB').val());
        item.MaMh = parseInt($('#txtMH').val());
        item.MaGv = $('#txtGV').val();
        var str = JSON.stringify(item);
        console.log(str);
        $.ajax({
            type: "POST",
            url: "/ChiTietTKB?handler=Update",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { tkbct: str },
            dataType: "json",
            success: function (res) {
                console.log(res)
                if (res.success) {
                    alert("Cập nhập thành công !!");
                    console.log("trTKBCT_" + item.Malop + item.Thu + item.Tiet);
                    $("#trTKBCT_" + item.Malop + "_" + item.Thu + "_" + item.Tiet + " td:eq(0)").html(item.MaTkb);
                    $("#trTKBCT_" + item.Malop + "_" + item.Thu + "_" + item.Tiet + " td:eq(4)").html(item.MaMh);
                    $("#trTKBCT_" + item.Malop + "_" + item.Thu + "_" + item.Tiet + " td:eq(5)").html(item.MaGv);
                    for (var i = 0; i < dataTKBCT.length; i++) {
                        if (item.Malop == dataTKBCT[i].Malop && item.Thu == dataTKBCT[i].Thu &&
                            item.Tiet == dataTKBCT[i].Tiet) {
                            dataTKBCT[i] = item;
                            break;
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    }

}
function deleteTKBCT(idLop, thu, tiet) {
    if (confirm("Bạn có muốn xoá thời khoá biểu này không?") == true) {
        $.ajax({
                type: "POST",
                url: "/ChiTietTKB?handler=Delete",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { maLop: idLop, thu: thu, tiet: tiet },
                dataType: "json",
                success: function (res) {
                    console.log(res)
                    if (res.success) {
                        alert("Xoá thành công !!");
                        console.log("trTKBCT_" + idLop + "_" + thu + "_" + tiet);
                        $("#trTKBCT_" + idLop + "_" + thu + "_" + tiet).remove();
                        var i = 0;
                        for (i = 0; i < dataTKBCT.length; i++) {
                            if (idLop == dataTKBCT[i].MaLop && thu == dataTKBCT[i].Thu && tiet == dataTKBCT[i].Tiet) {
                                break;
                            }
                        }
                        dataTKBCT.splice(i, 1);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }

            });
    }
    
}
//đóng modal
function closeModal() {
    $('#DivModal').modal('hide');
    $('#DivModalHS').modal('hide');
    $('#DivModalGV').modal('hide');
    $('#DivModalMH').modal('hide');
    $('#DivModalKH').modal('hide');
    $('#DivModalAd').modal('hide');
    $('#DivModalTKA').modal('hide');
    $('#DivModalTKHS').modal('hide');
    $('#DivModalTKGV').modal('hide');
    $('#DivModalD').modal('hide');
    $('#DivModalTKB').modal('hide');
    $('#DivModalTKBCT').modal('hide');
}

//Pagination
function goPre(user) {
    if (curPage == 1)
        alert('Bạn đang ở trang đầu');
    else{
        curPage -= 1;
        getDataPage(curPage, user);
    }
}
function goNext(user) {
    if (curPage == totPage)
        alert('Bạn đang ở trang cuối');
    else {
        curPage += 1;
        getDataPage(curPage, user)
    }
}
function getDataPage(page, user) {
    var filter = {
        Page: page,
        Size: 10
    };
    var str = JSON.stringify(filter);
    $.ajax({
        type: "POST",
        url: "/" + user + "?handler=List",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { filter: str },
        dataType: "json",
        success: function (res) {
            console.log(res)
            if (res.success) {
                console.log(res.data)
                var dt = res.data;
                totPage = dt.totalPage;
                $("#tbodyDT").html("");
                if (user == 'HocSinh') {
                    dataHS1 = dt.data;
                    $('#hsTemplate').tmpl(dataHS1).appendTo("#tbodyDT")
                }
                if (user == "GiaoVien") {
                    dataGV1 = dt.data;
                    $('#gvTemplate').tmpl(dataGV1).appendTo("#tbodyDT")
                }
                if (user == "Admin") {
                    dataAd1 = dt.data;
                    $('#adTemplate').tmpl(dataAd1).appendTo("#tbodyDT")
                }
                if (user == "MonHoc") {
                    dataMH1 = dt.data;
                    $('#mhTemplate').tmpl(dataMH1).appendTo("#tbodyDT")
                }
                if (user == "Diem") {
                    dataD1 = dt.data;
                    $('#diemTemplate').tmpl(dataD1).appendTo("#tbodyDT")
                }
                if (user == "ChiTietTKB") {
                    console.log(dt.data);
                    dataTKBCT1 = dt.data;
                    console.log(dataTKBCT1);
                    $('#tkbTemplate').tmpl(dataTKBCT1).appendTo("#tbodyDT")
                }
                $('#spanCurrentPage').text(page);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }

    });
}

