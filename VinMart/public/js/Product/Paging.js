function htmlButtonAdd(remainProductLength, displayNameBigCategory) {
    return `
        Xem thêm
        ${remainProductLength}
        sản phẩm
        <span style="font-weight: 700; margin-right: 8px;">${displayNameBigCategory}</span>
        <i class="fa-solid fa-angle-down"></i>
    `
}
function htmlButtonAddChecked(cart, product) {
    var productInCart = cart.find(item => item.IdProduct == product.Id)
    if (!cart) {
        return htmlButtonAddNew();
    } else if (productInCart) {
        return htmlButtonAdded(productInCart.Count);
    } else {
        return htmlButtonAddNew();
    }
}

function htmlProductItem(cart, product) {
    function htmlFakePrice() {
        if (product.SalePrice == product.Price) {
            return `<p class="product-fakeprice"></p>`
        }
        return `<p class="product-fakeprice">${product.SalePrice.toLocaleString('it-IT')}₫</p>`
    }
    
    return `
        <div class="col-lg-3">
            <div class="product-item" data-id="${product.Id}">
                <div class="product-image">
                    <img class="product-image-element" src="${product.MediaURL}" />
                    <div class="product-sale">-13%</div>
                </div>
                <a href="#" class="product-name">${product.DisplayName}</a>
                <p class="product-unit">DVT: Chai</p>

                <p class="product-price">${product.Price.toLocaleString('it-IT')}₫</p>
                ${htmlFakePrice()}
                ${htmlButtonAddChecked(cart, product)}
            </div>
        </div>
    `
}

function htmlSpinner() {
    return `
        <div class="spinner-container">
            <span class="glyphicon glyphicon-refresh spinning"></span>
        </div>
    `
}

function ajaxGetAllCart(products, currentContainer) {
    $.ajax({
        url: `/Cart/AllCart`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "GET",
        success: function (data) {
            console.log(data);
            var cart = data.status ? data.data : null;
            renderListProductItem(cart, products, currentContainer)
        },
        error: function (a, b) {
            console.log(a, b)
        }
    })
}

function successGetMoreProduct(data, currentContainer, pageIncreased) {
    currentContainer.data("page", pageIncreased);
    const productMore = currentContainer.find('.product-more');
    if (pageIncreased * 8 >= data.Count) {
        productMore.remove();
    } else {
        const remainProductLength = data.Count - (((pageIncreased - 1) * 8) + data.Products.length)
        productMore.html(htmlButtonAdd(remainProductLength, data.DisplayName))
    }
}

function renderListProductItem(cart, products, currentContainer) {
    var html = products.map(function (product) {
        return htmlProductItem(cart, product);
    })
    currentContainer.find('.row').append(html);
}

function handleGetMoreProduct() {
    $('.product-more').on('click', function () {
        const currentContainer = $(this).closest('.product-category-container');
        var pageIncreased = currentContainer.data("page") + 1;
        var idBigCategory = currentContainer.data("bigcategory");
        currentContainer.find('.more').prepend(htmlSpinner());
        $.ajax({
            url: `/Product/GetProductByBigCateogryAndPage?bigCategory=${idBigCategory}&page=${pageIncreased}`,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            success: function ({ data }) {
                successGetMoreProduct(data, currentContainer, pageIncreased);
                //renderListProductItem(data.Products, currentContainer);
                ajaxGetAllCart(data.Products, currentContainer);//to render list product
                currentContainer.find('.spinner-container').remove();
            },
            error: function (a, b) {
                console.log(a, b)
            }
        })
    })
}


$(document).ready(function () {
    handleGetMoreProduct();
})