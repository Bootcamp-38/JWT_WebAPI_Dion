function Login() {
    var UserRoleVM = new Object();
    UserRoleVM.UserEmail = $('#email').val();
    UserRoleVM.UserPassword = $('#password').val();
    //debugger;
    $.ajax({
        type: "Post",
        url: '/Accounts/Get/',
        data: UserRoleVM
    }).then((result) => {
        if (result.result == 'Redirect') {
            window.location = result.url;
        }
    }).catch((error) => {
        console.log(error);
    });

}
