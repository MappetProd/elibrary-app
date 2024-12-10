

$("#genres").on("focus", function () {
    $(this).autocomplete({
        source: "/AddBook/GetSuggestedGenres",
        select: function (event, ui) {
                event.preventDefault();
                var selectedObj = ui.item;
                $("${item.val}").appendTo("#genres")
            }
    })
})
