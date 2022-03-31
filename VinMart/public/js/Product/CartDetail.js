function callbackAddOrUpdate(res, data, _this, totalPrice) {
    console.log(res, data)
    if (!res.status) {
        $('#modalNotLoggedIn').modal("show");
    } else {
        var totalPrice = calcTotalPrice();
        if (res.isAdd) {
            console.log(res)
            updateCartDialog("Add", res, totalPrice);
        } else {
            updateCartDialog("Append", data, totalPrice);
        }
    }
}

function handleChangeValue() {
    $('.detail-product-item-value-quantity').on('click', function () {
        var value = Number($('.detail-product-item-value-value').text());
        value = $(this).data('type') === 'increase' ? ++value : --value;
        if (value > 0) {
            $('.detail-product-item-value-value').text(value);
        } else {
            return;
        }
    })
}


$(document).ready(function () {
    handleAddOrUpdateCart(callbackAddOrUpdate);
    handleChangeValue()
})

