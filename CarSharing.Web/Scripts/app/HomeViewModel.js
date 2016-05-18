define(["webRequest", "utils"], function (webRequest, utils) {
    return function () {

        webRequest.get("Api/Membership/IsLoggedIn", null, function (result) {
            if (!result) {
                utils.showError("You have to login!");
                window.location.href = "#/";
            }
            else {
                document.getElementById("home").style.display = "";
            }
        });

        var self = this;
        self.add = function () {
            window.location.href = "#/Home/Add"
        }
        self.search = function () {
            window.location.href = "#/Home/Search"
        }
     
        self.logout = function () {
            webRequest.post("Api/Membership/Logout",null, function (result) {
                window.location.href = "#/";
            });
            }
           
    }
});