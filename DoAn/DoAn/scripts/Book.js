function ValidationBook()
{
    var mail = $('#Ten_Sach').val();
    $.ajax({
        url: '/Book/IsAlreadyExist?Ten_Sach=' + mail,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (result) {
                $('#valid_book').text('This book is existed');
                $('#valid_book').show();
                $('#regist').hide();

        },
        error: function () {
            $('#valid_book').hide();
            $('#regist').show();
        }
    });
}


function Notification()
{
    alert("You need to login to buy this product");
}


function find_book()
{
    var name = $('#Search_Book').val();
    var book = $('#content_book');
    book.html('');
    if (name != null || name != '')
    {
        $.ajax({
            url: '/Book/FindBook?name=' + name,
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                list = '';
                $.each(data, function (key, item) {
                    list += '<tr class="center-text">'
                    + '<th>' + item.ten_sach + '</th>'
                    + '<th>' + item.tac_gia + '</th>'
                    + '<th>' + item.nha_xuat_ban + '</th>'
                    + '<th>' + item.the_loai + '</th>'
                    + '<th>' + item.gia_ban + '&#163;' + '</th>'
                    + '<th>'
                    + '<a class="btn-black" href="/Book/BookDetail/' + item.id_sach + '">View</a> '
                    + '<a class="btn-black" href="/Book/EditBook/' + item.id_sach + '">Edit</a> '
                    + '<a class="btn-black" href="/Book/DeleteBook/' + item.id_sach + '">Delete</a>'
                    + '</th>'
                    + '</tr>';

                })
                book.append(list);
                $('#Search_Book').val('');
            },
            error: function () {
                $('#Search_Book').val('');
                book.append('<tr><td  style="color:red">Not Found This Book</td></tr>');
            }

        })
    }
    
}


function buy_product(id)
{
    var tmp = parseInt(id);
    $.ajax({
        url: '/Cart/Add_Cart?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) { alert('add to cart') },
        error: function(){ alert('not find')}
    })
}


function book_manger(page)
{
    $.ajax({
        url: '/Book/GetAllBook?page=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            list = '';
            $.each(data, function (key, item) {
                list += '<tr class="center-text">'
                + '<th>' + item.ten_sach + '</th>'
                + '<th>' + item.tac_gia + '</th>'
                + '<th>' + item.nha_xuat_ban + '</th>'
                + '<th>' + item.the_loai + '</th>'
                + '<th>' + item.gia_ban + '&#163;'+ '</td>'
                + '<th>'
                + '<a class="btn-black" href="/Book/BookDetail/' + item.id_sach + '">View</a> '
                + '<a class="btn-black" href="/Book/EditBook/' + item.id_sach + '">Edit</a> '
                + '<a class="btn-black" href="/Book/DeleteBook/' + item.id_sach + '">Delete</a>'
                + '</th>'
                + '</tr>';
            })
            $('#table_book_manger tbody').html(list);
        }
    })
}


function load_book(page)
{
    var login = $('#login').val();
    $.ajax({
        url: '/Book/GetAllBook?page=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            var list = '';
            $.each(data, function (key, item) {
                list += '<div class="col-2 center-text spacing-col" >'
                + '<img src="../UploadFiles/' + item.hinh + '" alt="Alternate Text" height="300" width="250" />'
                + '<div class="title-book">'
                + '<a href="../Book/BookDetail/' + item.id_sach + '" class="title">' + item.ten_sach + '</a>'
                + '</div>'
                + '<div class="title-book">'
                + '<span class="span-price">' + item.gia_ban + ' &#163; ' + '</span><br />';
                if (login == null || login == '')
                {
                    list += '<button class="btn-black" style="margin-top: 5px" onclick="Notification()">Buy</button>';
                }
                else{
                    list += 
                '<button class="btn-black" style="margin-top:10px;color:white" onclick="buy_product(' + item.id_sach + ')" >Buy</button>';
                }
                list += ' </div></div>';
            })
            $('#row_book').html(list);
        }
    })
}


function get_book_by_genre(name)
{
    var login = $('#login').val();
    var book = $('#row_book');
    book.html(' ');
    $.ajax({
        url: '/Book/GetBookByGenre?name=' + name,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            var list = '';
            $.each(data, function (key, item) {
                list += '<div class="col-2 center-text spacing-col" >'
                + '<img src="../UploadFiles/' + item.hinh + '" alt="Alternate Text" height="300" width="250" />'
                + '<div class="title-book">'
                + '<a href="../Book/BookDetail/' + item.id_sach + '" class="title">' + item.ten_sach + '</a>'
                + '</div>'
                + '<div class="title-book">'
                + '<span class="span-price">' + item.gia_ban + ' &#163; ' + '</span><br />';
                if (login == null || login == '') {
                    list += '<button class="btn-black" style="margin-top: 5px" onclick="Notification()">Buy</button>';
                }
                else {
                    list +=
                '<button class="btn-black" style="margin-top:10px;color:white" onclick="buy_product(' + item.id_sach + ')" >Buy</button>';
                }
                list += ' </div></div>';
                
            })
            book.append(list);
        },
        error: function () {
            alert('Not Found Book');
        }

    })
}


function validation_create_genre()
{
    var name = $('#create_genre').val();
    $.ajax({
        url: '/Book/ValidGenre?Name=' + name,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid_create_genre').text('This genre is existed.');
            $('#valid_create_genre').show();
            $('#btn_create_genre').hide();
        },
        error: function () {
            $('#valid_create_genre').hide();
            $('#btn_create_genre').show();
        }
    })
}


function validation_update_genre() {
    var name = $('#txt_update_genre').val();
    $.ajax({
        url: '/Book/ValidGenre?Name=' + name,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid_update_genre').text('This genre is existed.');
            $('#valid_update_genre').show();
            $('#btn_update_genre').hide();
        },
        error: function () {
            $('#valid_update_genre').hide();
            $('#btn_update_genre').show();
        }
    })
}


function get_genre(name) {
    var id = parseInt(name);
    $.ajax({
        url: '/Book/GetdGenre?ID=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#id_customer').val(data.id);
            $('#txt_update_genre').val(data.genre);
            $('#modal_update_genre').show();
        },
        error: function () {
            alert('Not Found This Genre');
        }
    })
}

