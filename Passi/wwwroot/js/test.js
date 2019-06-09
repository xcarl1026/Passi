
document.getElementById('load').addEventListener('click', () => {
    var searchQuery = document.getElementById("searchQuery").value;
    var page = '/Directory?handler=Search/'
    var url = page + searchQuery;
    fetch(url)
   
        

    var page2 = '/UserInfoGrid/'
    fetch(page2)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            document.getElementById('grid').innerHTML = result;
            document.getElementById('resetpw').addEventListener('click', () => {
                var resetModal = document.getElementById('resetModal');
                resetModal.style.display = "block";
                document.getElementById("xModalBtn").addEventListener('click', () => {
                    resetModal.style.display = "none";
                });
                document.getElementById("closeModalBtn").addEventListener('click', () => {
                    resetModal.style.display = "none";

                });
            });
        });
});

document.getElementById('resetpwsave').addEventListener('click', () => {
    $.ajax({
        type: "POST",
        url: "/Directory?handler=SearchADUser",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            alert('you good');
        },
        failure: function () {
            alert('nope');
        }
    });
});