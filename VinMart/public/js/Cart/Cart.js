
function callbackUpdateCart(data, _this, totalPrice) {
    $('#cart-temp-price').text(`${totalPrice.toLocaleString('it-IT')}₫`)
    $('#cart-price').text(`${totalPrice.toLocaleString('it-IT')}₫`)
}

function callbackDeleteCart(data, _this, totalPrice) {
    $(_this).closest('.cart-item').remove();
    $('#cart-temp-price').text(`${totalPrice.toLocaleString('it-IT')}₫`)
    $('#cart-price').text(`${totalPrice.toLocaleString('it-IT')}₫`)
}


function callbackDeleteAllCart() {
    //remove all product and add image no cart
    $('.cart-info-product').remove();
    $('.cart-info').append(htmlNoCartIndex())

    //remove all cart at dialog cart header
    $('.header-dialog-cart-item').remove();
    $('#cart-dialog-list').append(htmlNoCart());

    //set price = 0;
    $('#cart-temp-price').text(`${"0".toLocaleString('it-IT')} ₫`);
    $('#cart-price').text(`${"0".toLocaleString('it-IT')} ₫`);
    swal("Xóa thành công", {
        icon: "success",
    });
}



$(document).ready(function () {
    handleUpdateCart(callbackUpdateCart, callbackDeleteCart)
    handleDeleteCart(callbackDeleteCart)
    handleDeleteAllCart(callbackDeleteAllCart);
})