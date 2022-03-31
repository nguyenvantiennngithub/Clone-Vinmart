function handleValidInput() {
    $('.login-input-element').on('blur', function () {
        if (!$(this).val().trim()) {
            $(this).addClass('invalid')
        } else {
            $(this).remove('invalid')
        }
    })

    $('.login-input-element').on('keydown', function () {
        $(this).removeClass('invalid')
    })
}

function handleSubmit() {
    //check input before submit
    $('#login-form').on('submit', function (e) {
        $('.login-input-element').each(function () {
            if (!$(this).val().trim()) {
                $(this).addClass('invalid')
                e.preventDefault();
            }
        })
    })
}


$(function () {
    handleValidInput();
    handleSubmit();
})