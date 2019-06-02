function get_lock_customer(page)
{
    $.ajax({
        url: '/Home/List_Lock_User?page=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (result) {
            var list = '';
            $.each(result, function (key, item) {
                list += '<tr class="center-text">'
                 + '<th>' + item.last_name + '</th>'
                 + '<th>' + item.first_name + '</th>'
                 + '<th>' + item.email + '</th>'
                 + '<th>' + item.role + '</th>'
                 + '<th>'
                 + '<a class="btn-black" href="/Customer/UnLockCustomer/' + item.id_customer + '">Restore</a> '
                 + '</th>'
                 + '</tr>';
            });
            $('#table_customer tbody').html(list);
         }
            
    });
}

function get_lock_book(page)
{
    $.ajax({
        url: '/Home/List_Lock_Book?page=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            list = '';
            $.each(data, function (key, item) {
                list += '<tr class="center-text">'
                + '<th>' + item.ten_sach + '</th>'
                + '<th>' + item.tac_gia + '</th>'
                + '<th>' + item.the_loai + '</th>'
                + '<th>' + item.gia_ban + '&#163;' + '</td>'
                + '<th>'
                + '<a class="btn-black" href="/Book/Unlock_Book/' + item.id_sach + '">Restore</a> '
                + '</th>'
                + '</tr>';
            })
            $('#table_book tbody').html(list);
        }
    })
}