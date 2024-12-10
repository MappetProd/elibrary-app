console.log($("#genre-select-book-param").attr("value"))
let value = $("#genre-select-book-param").attr("value")
$("#genre-select-book-param").val(value).trigger('chosen:updated')
//$("#genre-select-book-param").trigger('chosen:updated')