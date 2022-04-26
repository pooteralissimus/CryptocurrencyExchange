﻿
$(".changes24").each(function () { // select the color for percent of price changing
    var percent = $(this).text()
    if (parseFloat($(this).text()) >= 0) {
        $(this).addClass("percent_green")
        $(this).text("+"+percent+"%")
    } else if (parseFloat($(this).text()) == 0) {
        $(this).addClass("percent_green")
        $(this).text("+0%")
    } else {
        $(this).addClass("percent_red")
        $(this).text(percent+"%")
    }
});


$(function () {   // calculate price after input a value

    $('#calc-input').on('input', function (e) {
        var price = +$("#coinPriceSpan").text()
        var quantity = +$('#calc-input').val()
        if (quantity < 0) {
            alert("you cant buy less then 0 coins");
            $('#calc-input').val(0);
            $('#totalPrice').val(0);
            return;
        }
        var total = +price * quantity
        total = total.toFixed(2)
        $('#totalPrice').text("$" + total);
    });

});

