let btnImg = new Map()
btnImg.set(true, '/img/btn/empty-cart.png')
btnImg.set(false, '/img/btn/add-to-cart-btn.png')

async function initCartActionButtons() {
    let test = await fetch('https://localhost:7146/Cart/GetBooksInCart').then(response =>
        response.json().then(data => ({
            data: data,
            status: response.status,
            ok: response.ok
        }))
    );


    if (test.ok) {
        for (var i = 0; i < test.data.length; i++) {
            var catalogItemThatInCart = $("#catalog").find(`[data-id='${test.data[i]}']`)[0]
            $(catalogItemThatInCart).attr("data-isincart", true)
            var catalogItemImg = $(catalogItemThatInCart).find(".cart-action-img")[0]
            $(catalogItemImg).attr('src', btnImg.get(true))
        }
    }
}


$(".cart-action-btn").on("click", async function () {
    var onClickedPrintedBook = $(this).parent().parent()
    var printedBookId = onClickedPrintedBook.data("id")
    var isBookInCart = onClickedPrintedBook.data("isincart")

    let formData = new FormData();
    formData.append('printedBookId', printedBookId);

    let response
    if (!isBookInCart) {
        response = await fetch("https://localhost:7146/Cart/AddItem", {
            method: 'PUT',
            body: formData
        })
    }
    else {
        response = await fetch("https://localhost:7146/Cart/RemoveItemByPrintedBook", {
            method: 'DELETE',
            body: formData
        })
    }

    if (response.ok) {
        isBookInCart = !isBookInCart
        onClickedPrintedBook.data('isincart', isBookInCart)
        $(this).find(".cart-action-img").attr('src', btnImg.get(isBookInCart))
    }
})

initCartActionButtons()
