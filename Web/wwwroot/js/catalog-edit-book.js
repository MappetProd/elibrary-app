$(".edit-book-btn").on("click", function () {
    let onClickedBookId = $(this).parent().parent().find(".catalog-item-info-zone").data("id")
    let url = "/EditBook/Index?" + new URLSearchParams({ bookId: onClickedBookId }).toString()
    //console.log(url)
    fetch(url)
})

$(".delete-printed-book-btn").on("click", function () {
    let onClickedBookId = $(this).parent().parent().find(".catalog-item-info-zone").data("id")
    let formData = new FormData();
    formData.append('bookId', onClickedBookId);

    let response = await fetch("/EditBook/RemoveBook", {
        method: 'DELETE',
        body: formData
    })

    if (response.ok) {
        $(this).parent().remove()
    }
    else {
        alert("Что-то пошло не так!")
    }
})

