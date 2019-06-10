
var userInfoGridDiv = document.getElementById("userInfoGrid");
userInfoGridDiv.style.display = "none";

var searchQuery;
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
        document.getElementById("searchADUser").click();
    }
});

/* document.getElementById('resetPWSave').addEventListener('click', () => {
     postData('/Directory?handler=Test/', { answer: 42 })
         .then(data => console.log(JSON.stringify(data))) // JSON-string from `response.json()` call
         .catch(error => console.error(error));
 });

function postData(url = '', data = {}) {
// Default options are marked with *
 return fetch(url, {
     method: 'POST', // *GET, POST, PUT, DELETE, etc.
     mode: 'cors', // no-cors, cors, *same-origin
     cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
     credentials: 'same-origin', // include, *same-origin, omit
     headers: {
         'Content-Type': 'application/json',
         // 'Content-Type': 'application/x-www-form-urlencoded',
     },
     redirect: 'follow', // manual, *follow, error
     referrer: 'no-referrer', // no-referrer, *client
     body: JSON.stringify(data), // body data type must match "Content-Type" header
 })
 .then(response => response.json()); // parses JSON response into native Javascript objects
 }*/
/*document.getElementById('resetPWSave').addEventListener('click', () => {
   //var input = $('#resetPWIn').val();
      $.ajax({
          type: "POST",
          url: "/Directory?handler=Test/",
          beforeSend: function (xhr) {
              xhr.setRequestHeader("XSRF-TOKEN",
                  $('input:hidden[name="__RequestVerificationToken"]').val());
          },
          success: function (response) {
              alert('you good');
          },
          failure: function (response) {
              alert('nope');
          }
      });
  });*/


/*document.getElementById('resetPWSave').addEventListener('click', () => {
    var item1 = $('#resetPWIn').val();
    $.ajax({
        type: "POST",
        url: "/UserInfoGrid?handler=Test/" + searchQuery,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify({
            Item1: item1
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert('you good');
        },
        failure: function (response) {
            alert('nope');
        }
    });
});*/

/*document.getElementById('resetPWSave').addEventListener('click', () => {
    $.ajax({
        type: "POST",
        url: "/UserInfoGrid?handler=ChangePassword/" + searchQuery + "/" + document.getElementById('resetPWIn').value,
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
});*/


/*var userInfoGridDiv = document.getElementById("userInfoGrid");
userInfoGridDiv.style.display = "none";


document.forms[0].onsubmit = () => {
    let formData = new FormData(document.forms[0]);
    fetch('/Directory?handler=SearchADUser', {
        method: 'post',
        body: new URLSearchParams(formData)
    })
        .then(() => {
            alert('Posted using Fetch');
            fetch('/Directory')
                .then((response) => {
                    return response.text();
                })
                .then((result) => {
                    userInfoGridDiv.innerHTML = result;
                    userInfoGridDiv.style.display = "block";
            });
    });
    return false;

};

document.getElementById("searchQuery").addEventListener("keyup", () => {
    if (event.keyCode == 13) {
        event.preventDefault();
        document.getElementById("searchADUser").click();
    }
});*/


/*document.forms[0].onsubmit = () => {
             let formData = new FormData(document.forms[0]);
             fetch('/Directory?handler=SearchADUser', {
                 method: 'post',
                 body: new URLSearchParams(formData)
             })
                 .then((response) => {
                     response.text();
                     alert('Posted using Fetch');
                 })
                 .then((result) => {
                 document.getElementById('grid').innerHTML = result;
             });
             return false;
         };*/


document.forms[0].onsubmit = () => {
    let formData = new FormData(document.forms[0]);
    fetch('/UserInfoGrid?handler=SearchADUser', {
        method: 'post',
        body: new URLSearchParams(formData)
    })
        .then((response) => {
            response.text();
            alert('Posted using Fetch');
        })
        .then((result) => {
            document.getElementById('grid').innerHTML = result;
        });
    return false;
};