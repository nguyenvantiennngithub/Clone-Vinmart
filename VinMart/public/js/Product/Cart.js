function callbackAddToCart(data, _this) {
    if (data.status == false) {
        $('#modalNotLoggedIn').modal("show");
    } else if (data.status == true) {
        //delete no cart item if has
        $('#no-cart').remove();
        //change new button
        $(_this).after(htmlButtonAdded(1));
        $(_this).remove();
    }
}

function callbackUpdateCart(data) {
    //update cart
}

function callbackDeleteCart(data, _this) {
    //change button
    var parent = $(_this).parent();
    parent.after(htmlButtonAddNew());
    parent.remove();
}




//==============================================CALL BACK==============================================
$(document).ready(function () {
    handleAddToCart();
    handleUpdateCart(callbackUpdateCart, callbackDeleteCart);
})