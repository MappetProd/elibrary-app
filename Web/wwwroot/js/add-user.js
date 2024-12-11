$('#add-user-form').ajaxForm({
        url: 'https://localhost:7146/Register/CreateUser',
        success: function (response) {
                alert("Читатель успешно зарегистрирован")
                location.reload()    
        },
    error: function (response) {
            console.log(response)
            alert("Произошла ошибка при регистрации читателя")
        }
})