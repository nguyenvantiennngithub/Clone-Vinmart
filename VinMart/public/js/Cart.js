function htmlButtonAdded(count) {
    return `
        <div class="product-container-btn">
            <div class="product-couter cart-quantity" data-type="decrease"><i class="fa-solid fa-minus"></i></div>
            <span class="product-count cart-value">${count}</span>
            <div class="product-couter cart-quantity" data-type="increase"><i class="fa-solid fa-plus"></i></div>
        </div>
    `
}

function htmlButtonAddNew() {
    return `
        <button class="product-add cart-add">Thêm vào giỏ</button>
    `
}

function htmlCartItem(cartItem) {
    return `
        <div class="header-dialog-cart-item" data-id="${cartItem.Product.Id}">
            <div class="header-dialog-cart-item-logo">
                <img src="${cartItem.Product.MediaURL}" class="header-dialog-cart-item-logo-element" />
            </div>
            <div class="header-dialog-cart-item-content">
                <a href="#" class="header-dialog-account-item-content-displayname">${cartItem.Product.DisplayName}</a>
                <span class="header-dialog-account-item-content-unit">DVT: Chai</span>
                <div class="header-dialog-cart-item-content-container">
                    <span class="header-dialog-cart-item-content-count">x ${cartItem.Count}</span>

                    <span class="header-dialog-cart-item-content-price">${cartItem.Product.Price.toLocaleString('it-IT')}₫</span>
                </div>
            </div>
        </div>
    `
}

function htmlNoCart() {
    return `
        <div class="header-dialog-cart-item-no-cart" id="no-cart">
            <div class="header-dialog-cart-empty">
                <img src="/public/images/no-cart.png"/>
            </div>
        </div>
    `
}
function htmlCountCart(value) {
    return `Giỏ hàng (${value})`;
}

function handleAddToCart() {
    $(document).on('click', '.cart-add', function () {
        var _this = this;
        var data = {
            idProduct: $(_this).closest('.cart-item').data("id"),
        }
        ajaxAddCart(_this, data, callbackAddToCart)
    })
}

function ajaxAddCart(_this, data, callback) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(data),
        url: "/Cart/Add",
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            var totalPrice = calcTotalPrice();
            callback && callback(res, _this, totalPrice);
            updateCartDialog("Add", res, totalPrice);
        }
    })
}


function handleUpdateCart(callbackUpdate, callbackDelete) {
    $(document).on('click', '.cart-quantity', function () {
        var _this = this;

        var value = parseInt($(this).parent().find('.cart-value').text());
        value = $(this).data('type') == "increase" ? ++value : --value;
        $(this).parent().find('.cart-value').text(value);

        var data = {
            idProduct: $(_this).closest('.cart-item').data("id"),
            value: value,
        }
        console.log(data)
        if (value > 0) {
            ajaxUpdateCart(data, _this, callbackUpdate)
        } else if (value == 0) {
            ajaxDeleteCart(data, _this, callbackDelete)
        } else {
            alert("Error about change value");
        }
    })
}
function ajaxUpdateCart(data, _this, callback) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(data),
        url: "/Cart/Update",
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            //callbackUpdateCart(data);
            var totalPrice = calcTotalPrice();
            updateCartDialog("Update", data, totalPrice);
            callback && callback(data, _this, totalPrice)

        }
    })
}

function handleDeleteCart(callbackDelete) {
    $('.cart-delete').on('click', function () {
        var _this = this;
        var data = {
            idProduct: $(_this).closest('.cart-item').data("id"),
        }
        ajaxDeleteCart(data, _this, callbackDelete);
    })
}
function ajaxDeleteCart(data, _this, callback) {
    $.ajax({
        type: "POST",
        url: `/Cart/Delete/${data.idProduct}`,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            //callbackUpdateCart(data);
            var totalPrice = calcTotalPrice();
            callback && callback(data, _this, totalPrice)
            updateCartDialog("Delete", data, totalPrice);
        }
    })
}

function updateCartDialog(type, data, totalPrice) {
    if (type == "Update") {
        //update count item in cart
        var cartItemContainer = $(`.header-dialog-cart-item[data-id=${data.idProduct}]`);
        cartItemContainer.find('.header-dialog-cart-item-content-count').text(`x ${data.value}`);
    } else if (type == "Delete") {
        //delete item in cart
        var cartItemContainer = $(`.header-dialog-cart-item[data-id=${data.idProduct}]`);
        cartItemContainer.remove();
        if ($('.header-dialog-cart-item').length == 0) {
            $('#cart-dialog-list').append(htmlNoCart())
        }
    } else if (type == "Add") {
        //append new cart
        console.log(data)
        $('.header-dialog-cart-list').append(htmlCartItem(data.data))
    } else if (type == "Append") {
        var container = $(`.header-dialog-cart-item[data-id=${data.idProduct}]`);
        var count = Number(container.find('.header-dialog-cart-item-content-count').text().replace('x ', ''))
        
        var cartItemContainer = $(`.header-dialog-cart-item[data-id=${data.idProduct}]`);
        cartItemContainer.find('.header-dialog-cart-item-content-count').text(`x ${data.value + count}`);
    }
    
    var cartItem = $('.header-dialog-cart-item');
    cartItemPrice = cartItem.find('.header-dialog-cart-item-content-price')

    //Change count cart in header
    $('#cart-text').text(htmlCountCart(cartItem.length))
    //Change total price in cart dialog
    $('#cart-total-price').text(`${totalPrice.toLocaleString('it-IT')}₫`)
    //change count cart in cart dialog
    $('#cart-total-count').text(`Có tổng cộng ${cartItem.length} sản phẩm`)
}

function htmlNoCartIndex() {
    return `
        <div class="cart-empty">
            <div class="cart-empty-container">
                <img src="/public/images/no-cart.png" />
            </div>
        </div>
    `
}

function handleDeleteAllCart(callback) {

    $('#cart-delete-all').click(function () {
        var _this = this
        swal({
            title: "Are you sure?",
            text: "Bạn có muốn xóa tất cả giỏ hàng không?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                ajaxDeleteAllCart(_this, callback);
            }
            
        });
    })
}
function calcTotalPrice() {
    var totalPrice = 0;
    var cartItem = $('.header-dialog-cart-item');
    cartItemPrice = cartItem.find('.header-dialog-cart-item-content-price')
    //calc total price
    cartItemPrice.each(function () {
        var count = Number($(this).parent().find('.header-dialog-cart-item-content-count').text().replace('x ', ''))
        var price = Number($(this).text().replace(/[^0-9]+/g, ""));
        totalPrice += (price * count);
    })
    return totalPrice;
}


function ajaxDeleteAllCart(_this, callback) {
    $.ajax({
        type: "POST",
        url: `/Cart/DeleteAll`,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            //callbackUpdateCart(data);
            //var totalPrice = updateCartDialog("Delete", data);
            callback && callback()
            updateCartDialog();

        }
    })
}



function handleAddOrUpdateCart(callback) {
    $('#cart-add-or-update').on('click', function () {
        var _this = this;
        var data = {
            idProduct: $(_this).data("id"),
            value: Number($('.detail-product-item-value-value').text()),
        }
        console.log(data);
        ajaxAddOrUpdateCart(_this, data, callback)
    })
}

function ajaxAddOrUpdateCart(_this, data, callback) {
    $.ajax({
        type: "POST",
        url: `/Cart/AddUpdate`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            console.log(res);
            var totalPrice = calcTotalPrice();
            callback && callback(res, data, _this, totalPrice)
        }
    })
}