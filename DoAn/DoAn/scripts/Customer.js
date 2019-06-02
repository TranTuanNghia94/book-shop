

//function GetCustomer(id)
//{
//        $.ajax({
//            url: '/Customer/GetCustomer/' + id,
//            type: 'GET',
//            contentType: "application/json;charset=UTF-8",
//            dataType: "json",
//            success: function (data) {
//                $('#ID').val(data.id_customer);
//                $('#Email').val(data.email);
//                $('#First_Name').val(data.first_name);
//                $('#Last_Name').val(data.last_name);
//                $('#Password').val(data.password);
//                $('#Address').val(data.address);
//                $('#Phone').val(data.phone);
//                $('#Gender').val(data.gender);
//            }
//        });
//}

function Get_Bill(page)
{
    $.ajax({
        url: '/Customer/GetBill?page=' + page,
        type: 'GET',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            list = '';
            $.each(data, function (key, item) {
                list += '<tr class="center-text">'
                + '<th>' + item.Item1.date_create + '</th>'
                + '<th>' + item.Item2.ten_sach + '</th>'
                + '<th>' + item.Item2.so_luong + '</th>'
                + '<th>' + item.Item2.don_gia + '</th>'
                + '<th>' + item.Item2.thanh_tien + '</th>'
                + '</tr>';
            })
            $('#table_bill tbody').html(list);
        }
    })
}


function Get_Record(page)
{
    $.ajax({
        url: '/Home/Record_User?page=' + page,
        type: 'GET',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            list = '';
            $.each(data, function (key, item) {

                list += '<tr>'
                + '<th>' + item.username + '</th>'
                + '<th>' + item.action + '</th>'
                + '<th>"' + Date(item.date_action) + '"</th>'
                + '</tr>';
            })
            $('#table_record tbody').html(list);
        }
    })
}


function SearchCustomer()
{
    var Email = $('#searchMail').val();
    var customer = $('#contentCustomer');
    customer.html('');
    if (Email != null)
    {
        $.ajax({
            url: '/Customer/Search?Email=' + Email,
            type: 'POST',
            contentType: 'json',
            success: function (data) {
                if (data.length == 0) {
                    
                    $('#searchMail').val('');
                    customer.append('<tr><td  style="color:red">not found this mail</td></tr>');
                }
                else {
                    
                    $.each(data, function (key, value) {
                        var dt = '<tr>'
                        + '<td>' + value.first_name + '</td>'
                        + '<td>' + value.email + '</td>'
                        + '<td>' + value.flag + '</td>'
                        + '<td>' + value.role + '</td>';
                        if (value.role != 'admin' && value.flag == false) {
                            dt += '<td> <a class="btn-black" href="/Customer/Customer_Info/' + value.id_customer + '">View</a> '
                            + '<a class="btn-black" onclick="FindCustomer(' + value.id_customer + ')" >Edit</a> '
                            + ' <a class="btn-black" onClick="DeleteCustomer(' + value.id_customer + ')" >Delete</a></td>'
                            + '</tr>';
                        }
                        customer.append(dt);
                    });
                    $('#searchMail').val('');
                   
                }
            },
            error: function () {
                $('#searchMail').val('');
                customer.append('<tr><td  style="color:red">not found this mail</td></tr>');
            }
        });
    }
    else if(Email == null || Email == '')
    {
        LoadCustomer();
    }
}


function FindCustomer(id)
{
    $.ajax({
        url: '/Customer/Staff/' + id,
        type: 'GET',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            $('#id_customer').val(data.id_customer);
            $('#email_customer').val(data.email);
            $('#firstname_customer').val(data.first_name);
            $('#lastname_customer').val(data.last_name);
            $('#role_customer').val(data.role);
            $('#createstaff').show();
        }
    });

}


function AddCustomer() {
    $.ajax({
        type: "POST",
        url: '/Customer/Register',
        dataType: "htm",
        contentType: "application/json; charset=urf-8",
        data: JSON.stringify({
                Email: $('#Email').val(),
                First_Name: $('#First_Name').val(),
                Last_Name: $('#Last_Name').val(),
                Password: $('#Password').val(),
                Address: $('#Address').val(),
                Phone: $('#Phone').val(),
                Day: $('#Day').val(),
                Month: $('#Month').val(),
                Year: $('#Year').val(),
                Gender: $('#Gender').val()
        }),
        success: 
            $('#createstaff').modal('hide')

            
    });
}


function UnLocked(Id)
{
    var check_unlock = confirm('DO WANT TO UNLOCK THIS ACCOUNT ?');
    if(check_unlock)
    {
        $.ajax({
            type: "POST",
            url: '/Customer/UnLockCustomer/' + Id,
            dataType: "html",
            contentType: "application/json; charset=urf-8",
            success: function (result) {
                alert('Unlock Successful');
                LoadCustomer();
            }
        });
    }
}


function DeleteCustomer(Id) {
    var check_delete = confirm('DO WANT TO DELETE THIS ACCOUNT ?');
    if (check_delete) {
        $.ajax({
            type: "POST",
            url: '/Customer/DeleteCustomer/' + Id,
            dataType: "html",
            contentType: "application/json; charset=urf-8",
            success: function (result) {
                alert('Delete Successful');
                LoadCustomer();
            }
        });
    }
}


function CreateStaff()
{
    $.ajax({
        url: '/Customer/CreateStaff',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=urf-8",
        data: JSON.stringify({
            Id: $('#id_customer').val(),
            Role: $('#role_customer').val()
        }),
        success: function () {
            alert("update success");
            LoadCustomer();
        }
    });
}


function LoadCustomer(page) {
    $.ajax({
        url: '/Customer/ShowCustomer?page_number=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (result) {
            var list = '';
            $.each(result, function (key, item) {
                list += '<tr>'
                 + '<td>' + item.first_name + '</td>'
                 + '<td>' + item.email + '</td>'
                 + '<td>' + item.flag + '</td>'
                 + '<td>' + item.role + '</td>';
                if (item.role != 'admin' && item.flag == false)
                {
                    list += '<td> <a class="btn-black" href="/Customer/Customer_Info/' + item.id_customer + '">View</a> '
                    + '<a id="edit" class="btn-black" onclick="FindCustomer(' + item.id_customer + ')" >Edit</a> '
                    + ' <a class="btn-black" onClick="DeleteCustomer(' + item.id_customer + ')" >Delete</a></td>'
                    + '</tr>';
                }
                else if (item.role == 'admin') {
                    list += '<td></td>';
                }

                 
            });
            $('#tblCustomer tbody').html(list);
        }
    });
}


function ValidationCustomer()
{
    var mail = $('#Email').val();
        $.ajax({
            url: '/Customer/IsAlreadySigned?Email=' + mail,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json; charset=urf-8",
            success: function (result) {
                if (result != null) {
                    $('#valid_mail').text('This mail is existed');
                    $('#valid_mail').show();
                    $('#register').hide();
                }
            },
            error: function () {
                $('#valid_mail').hide();
                $('#register').show();
            }
        });

}


function ExistCustomer() {
    var mail = $('#Mail').val();
    $.ajax({
        url: '/Customer/IsAlreadySigned?Email=' + mail,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (result) {
            if (result != null) {
                $('#valid').text('This mail is existed');
                $('#valid').show();
                $('#update').hide();
                return true;
            }
        },
        error: function () {
            $('#valid').hide();
            $('#update').show();
        }
    });

}

