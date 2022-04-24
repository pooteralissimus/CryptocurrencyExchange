
$(".row-crypto-table__percent_red").each(function () {
    var changes24h = $($(this).contents()[1]).text();
    if (parseFloat(changes24h) >= 0) {
        $(this).removeClass("row-crypto-table__percent_red");
        $(this).addClass("row-crypto-table__percent_green");
    }
})