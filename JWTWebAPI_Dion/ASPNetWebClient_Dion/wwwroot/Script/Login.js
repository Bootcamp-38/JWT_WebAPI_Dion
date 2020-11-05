function Login() {
    var User = new Object();
    User.Email = $('#email').val();
    User.Password = $('#password').val();
    debugger;
    $.ajax({
        type: "Post",
        url: '/Home/Get/',
        data: User
    }).then((result) => {
        debugger;
        console.log(result);
    }).catch((error) => {
        console.log(error);
    });
}
