define(["webRequest", "utils"], function (webRequest, utils) {
    return function () {
        var self = this;

        self.username = ko.observable("");
        self.password = ko.observable("");
        self.isValid = ko.observable(true);
          

        self.login = function () {
            var usernameRegex = /^[a-zA-Z0-9]+$/;
                self.isValid(true);
                if (self.username() == "") {
                    self.isValid (false);
                    utils.showError("Usarname cannot be left empty");
                }
                else {
                    if (!self.username().match(usernameRegex)) {
                        self.isValid (false);
                        utils.showError("Usarname is not a valid type");
                    }
                }
                if (self.password() == "") {
                    self.isValid (false);
                    utils.showError("Password cannot be left empty");
                }
               

                if (self.isValid()) {
                    webRequest.post("Api/Membership/Login", JSON.stringify({ username: self.username(), password: self.password() }), function (result) {
                        if (result) {
                            utils.showSuccess("Successfully logged in.");
                            window.location.href = "#/Home"
                        }
                        else {
                            utils.showError("Wrong Username / Password");
                        }
                    });
                }
            }
        self.register = function () {
            window.location.href = "#/Register"   
        }
    }
});