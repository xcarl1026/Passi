

document.getElementById('load').addEventListener('click', () => {
    var searchQuery = document.getElementById("searchQuery").value;
    var page = '/UserInfoGrid/'
    var url = page + searchQuery;
    fetch(url)
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

document.getElementById("searchQuery").addEventListener("keyup", () => {
    if (event.keyCode == 13) {
        event.preventDefault();
        document.getElementById("load").click();
    }
});

