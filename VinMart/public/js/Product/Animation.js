//handle turn on/off overlay when hover in menu
function handleToggleDialogMenu() {
    //when hover in menu will turn on 
    $('.navigation-item.navigation-item-menu').hover(function () {
        $('#overlay').show();
    })

    //when hover in overlay will turn off
    $('#overlay').hover(function () {
        $('#overlay').hide();
    })
}

//handle at banner when click on dot or arrow buttons it will change banner
function handleChangeBanner() {
    const bannerLength = $('.row.banner-item').length - 1;
    console.log(bannerLength);
    //when click on dot buttons
    $(document).on('click', '.dot', function () {
        const show = $(this).data('goto');
        $('#banner').attr('data-show', show)
    })
    //when click on arrow decrease
    $('#decrease').click(function () {
        const show = $('#banner').attr('data-show');
        if (show == 0) {
            $('#banner').attr('data-show', bannerLength);
        } else {
            $('#banner').attr('data-show', show - 1)
        }
    })
    //when click on arrow increase
    $('#increase').click(function () {
        const show = $('#banner').attr('data-show');
        if (show == bannerLength) {
            $('#banner').attr('data-show', 0);
        } else {
            $('#banner').attr('data-show', parseInt(show) + 1)
        }
    })
}

$(function () {
    handleChangeBanner();
    handleToggleDialogMenu();
});