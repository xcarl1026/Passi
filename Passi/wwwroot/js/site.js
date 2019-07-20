// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//global value for query data 
var searchQuery;

/*function searchADUser() {
    searchQuery = $('#searchQuery').val();
    var url = 'UserInfoGrid/' + $('#searchQuery').val();
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#UserInfo').html(result);
            $('#resetpw').on('click', ResetPassword);
            $('#unlockAccBtn').on('click', UnlockAcc)
        })
}*/
//Function for buttons in the side navbar in Directory page
function SearchADUser(btnID) {
    searchQuery = btnID;
    var url = 'UserInfoGrid/' + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#UserInfo').html(result);
            $('#resetpw').on('click', LaunchResetModal);
            $('#unlockAccBtn').on('click', LaunchUnlockModal)
        })
}

//Functions for button in the side navbar in GroupDirectory page
function SearchADGroup(btnID) {
    searchQuery = btnID;
    var url = '/GroupsInfo/' + searchQuery;
    fetch(url)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#GroupsInfo').html(result);
        })
}

//Function for search bar in Directory page
$("#searchUserList").on("keyup", FilterUserList);
function FilterUserList() {
    var value = $(this).val().toLowerCase();
    $("#UserList :button").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

//Function for search bad in GroupDirectory page
$("#searchGroupList").on("keyup", FilterGroupList);
function FilterGroupList() {
    var value = $(this).val().toLowerCase();
    $("#GroupList :button").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

//Btn Action for Group Members in Directory/UserInfo page
function GetGroupMembers(btnID) {
    searchQuery = btnID;
    if (searchQuery == "Domain Users") {
        alert("Default group all accounts are a part of.");
    }
    else
    {
        var url = 'GroupDirectory?=' + searchQuery;
        location.replace(url);
    }
    
}

//Sets display properties for Unlock Modal
function LaunchUnlockModal() {
    $('#UnlockModal').css('display', 'block');
    $('#closeUnlockModalBtn').on('click', function close() { $('#UnlockModal').css('display', 'none'); $('#modalUnlockStatus').html("Press OK to send the unlock command."); });
    $('#xUnlockModalBtn').on('click', function close() { $('#UnlockModal').css('display', 'none'); $('#modalUnlockStatus').html("Press OK to send the unlock command."); });
}

//Sets display properties for Reset Modal
function LaunchResetModal() {
    $('#resetModal').css('display', 'block');
    $('#xModalBtn').on('click', function close() { $('#resetModal').css('display', 'none'); });
    $('#closeModalBtn').on('click', function close() { $('#modalPWResetStatus').html(""); $('#resetModal').css('display', 'none'); });
}

//Submit action for OK button in Reset Modal
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

//Submit action for OK button in Unlock Modal
$('#unlockAccForm').submit(function (event) {
    let formData = new FormData(document.forms[1]);
    formData.append('searchQuery', searchQuery);
    fetch('/UserInfoGrid?handler=UnlockAccount', {
        method: 'post',
        body: new URLSearchParams(formData)
    })
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            $('#modalUnlockStatus').html(result);
        })
    event.preventDefault();
});

//Button action for group members in GroupDirectory/info page
function GetGroupMemberInfo(objectType, btnID) {
    var url;
    if (objectType == "user") {
        url = "Directory?=" + btnID;
        location.replace(url);
    } else {
        url = "GroupDirectory?=" + btnID;
        location.replace(url);
    }

}