$(".confirm-btn").on("click", async function () {
    let onResolvedApplication = $(this).parent().parent()
    let onResolvedApplicationId = $(onResolvedApplication).data("id")

    let formData = new FormData();
    formData.append('applicationId', onResolvedApplicationId);

    let response = await fetch("https://localhost:7146/ModerateApplication/Resolve", {
        method: 'POST',
        body: formData
    })

    if (response.ok) {
        location.reload()
    }
    else {
        alert("Не удалось принять заявку!")
    }
})


$(".close-btn").on("click", async function () {
    let onResolvedApplication = $(this).parent().parent()
    let onResolvedApplicationId = $(onResolvedApplication).data("id")

    let formData = new FormData();
    formData.append('applicationId', onResolvedApplicationId);

    let response = await fetch("https://localhost:7146/ModerateApplication/End", {
        method: 'POST',
        body: formData
    })

    if (response.ok) {
        location.reload()
    }
    else {
        alert("Не удалось завершить заявку!")
    }
})