$(document).ready(function () {
    $("#mySearch").on("keyup", function () {
        var gt = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(gt) > -1)
        });
    });
});