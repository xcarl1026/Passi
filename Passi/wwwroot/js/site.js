// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var searchQuery;
$('#searchADUser').on('click', searchADUser);


function searchADUser() {
    searchQuery = $('#searchQuery').val();
    var url = 'UserInfoGrid/' + $('#searchQuery').val();
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#UserInfo').html(result);
            $('#resetpw').on('click', ResetPassword);
            $('#unlockacc').on('click', UnlockAcc)
        })
}

function SearchADUser2(btnID) {
    searchQuery = btnID;
    var url = 'UserInfoGrid/' + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#UserInfo').html(result);
            $('#resetpw').on('click', ResetPassword);
            $('#unlockacc').on('click', UnlockAcc)
        })
}

function SearchADGroup(btnID) {
    searchQuery = btnID;
    var url = 'GroupsInfo/' + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#GroupsInfo').html(result);
        })
}

$("#searchInput").on("keyup", FilterList);
function FilterList() {
    var value = $(this).val().toLowerCase();
    $("#UserList :button").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

$("#searchInput2").on("keyup", FilterList);
function FilterList() {
    var value = $(this).val().toLowerCase();
    $("#GroupList :button").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}


function GetGroupMembers(btnID) {
    searchQuery = btnID;
    var url = 'GroupsInfo/' + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#UserInfo').html(result);
        })
}


function ResetPassword() {
    $('#resetModal').css('display', 'block');
    $('#xModalBtn').on('click', function close() { $('#resetModal').css('display', 'none'); });
    $('#closeModalBtn').on('click', function close() { $('#modalPWResetStatus').html(""); $('#resetModal').css('display', 'none'); });
}

function UnlockAcc() {
    var url = '/UserInfoGrid?handler=UnlockAccount' + searchQuery;
    fetch(url, {
        method: 'post'
    })
        .then(() => {
            $('#UnlockModal').css('display', 'block');
            $('#xUnlockModalBtn').on('click', function close() { $('#UnlockModal').css('display', 'none'); });
            $('#closeUnlockModalBtn').on('click', function close() { $('#UnlockModal').css('display', 'none'); });
        })

}

$('#pwResetForm').submit(function (event) {
    let formData = new FormData(document.forms[0]);
    formData.append('searchQuery', searchQuery);
    fetch('/UserInfoGrid?handler=ResetPassword', {
        method: 'post',
        body: new URLSearchParams(formData)
    })
        .then((response) => {
            return response.text();
            //alert('Posted using Fetch');
        })
        .then((result) => {
            //var html = result.html();
            $('#modalPWResetStatus').html(result);
        })
    event.preventDefault();
});






/* var resetModal;
document.getElementById('searchADUser').addEventListener('click', () => {
    searchQuery = document.getElementById("searchQuery").value;
    var page = '/UserInfoGrid/'
    var url = page + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            document.getElementById('grid').innerHTML = result;
            document.getElementById('resetpw').addEventListener('click', () => {
                resetModal = document.getElementById('resetModal');
                resetModal.style.display = "block";
                document.getElementById("xModalBtn").addEventListener('click', () => {
                    resetModal.style.display = "none";
                });
                document.getElementById("closeModalBtn").addEventListener('click', () => {
                    resetModal.style.display = "none";
                });

            });
        });
});*/

/*document.forms[0].onsubmit = () => {
   let formData = new FormData(document.forms[0]);
   fetch('/UserInfoGrid?handler=ResetPassword', {
       method: 'post',
       body: new URLSearchParams(formData)
   })
       .then((response) => {
           response.text();
           alert('Posted using Fetch');
       })
       .then((result) => {
       //document.getElementById('grid').innerHTML = result;
   });
   return false;
   */
