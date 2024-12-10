let authors_last_index = 0
$("#add-author-into-form-btn").on("click", function () {
    addAuthorSubForm()
})

function addAuthorSubForm() {
    $('#authors').prepend(
        `<div class="book-author">
        <label> Имя</label>
        <input placeholder="Александр" type="text" name="authors[${authors_last_index}].name" required>
        <label>Фамилия</label>
        <input placeholder="Пушкин" type="text" name="authors[${authors_last_index}].surname" required>
        <label>Отчество (при наличии)</label>
        <input placeholder="Сергеевич" type="text" name="authors[${authors_last_index}].patronymic">
    </div>`)
    authors_last_index += 1
}