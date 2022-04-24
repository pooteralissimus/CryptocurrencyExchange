////$(".row-crypto-table__percent_red").each(function () {
////    var changes24h = $($(this).contents()[1]).text();
////    if (parseFloat(changes24h) >= 0) {
////        $(this).removeClass("row-crypto-table__percent_red")
////        $(this).addClass("row-crypto-table__percent_green")
////        $(this).text("+" + changes24h + "%")
////    }
////    else {
////        $(this).text("-" + changes24h + "%")
////    }
////})

//$(".changes24").each(function (i)) {
//    $(this).addClass("percent_green")
//

$(".changes24").each(function () {
    var percent = $(this).text()
    if (parseFloat($(this).text()) >= 0) {
        $(this).addClass("percent_green")
        $(this).text("+"+percent+"%")
    } else if (parseFloat($(this).text()) == 0) {
        $(this).text("+0%")
    } else {
        $(this).addClass("percent_red")
        $(this).text(percent+"%")
    }
});