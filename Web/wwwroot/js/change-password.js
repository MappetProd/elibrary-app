document.getElementById('passwordChangeForm').addEventListener('submit', async function (event) {
    event.preventDefault();
    const oldPassword = document.getElementById('oldPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (newPassword !== confirmPassword) {
        alert('Новый пароль и его подтверждение не совпадают.');
    } else {
        let formData = new FormData();
        formData.append("old_password", oldPassword);
        formData.append("new_password", newPassword);
        formData.append("new_password_confirm", confirmPassword);
        let response = await fetch("Change", {
            method: 'POST',
            body: formData
        })

        if (response.redirected) {
            alert('Пароль успешно изменён.');
        }
        else {
            alert('Что-то пошло не так!');
        }
    }
});