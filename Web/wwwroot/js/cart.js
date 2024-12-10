$(".delete-cart-item-btn").on("click", async function () {
    let removingCartItem = $(this).parent()
    let onClickedCartItemId = $(removingCartItem).data("id")
    let formData = new FormData();
    formData.append('cartItemId', onClickedCartItemId);

    let response = await fetch("Cart/RemoveItemByCartItemId", {
        method: 'DELETE',
        body: formData
    })

    if (response.ok) {
        $(removingCartItem).remove()
    }
    else {
        alert("Что-то пошло не так!")
    }

    if (isCartEmpty()) {
        renderEmptyCartMessage()
    }
})

function isCartEmpty() {
    let test = $('#cart').children().length
    if (test == 0) {
        return true
    }
    return false
}

function renderEmptyCartMessage() {
    $('#cart').append('<p>В корзине ничего нет!</p>')
    $('#create-application-btn').remove()
}

if (isCartEmpty()) {
    renderEmptyCartMessage()
}

$("#create-application-btn").on("click", async function () {
    let response = await fetch("https://localhost:7146/ReaderApplication/CreateApplication", {
        method: 'PUT'
    })

    if (response.ok) {
        $("#cart").empty()
    }
    else {
        alert("Что-то пошло не так!")
    }

    if (isCartEmpty()) {
        renderEmptyCartMessage()
    }
})