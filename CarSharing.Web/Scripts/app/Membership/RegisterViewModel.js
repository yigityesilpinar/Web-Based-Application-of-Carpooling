define(["webRequest", "utils"], function (webRequest, utils) {
    return function () {
        var self = this;
        self.name = ko.observable("");
        self.lastname = ko.observable("");
        self.email = ko.observable("");
        self.username = ko.observable("");
        self.password = ko.observable("");
        self.repassword = ko.observable("");
        self.gender = ko.observable("");
        self.age = ko.observable("");
        self.genders = ko.observableArray(["Male", "Female"]);
        self.isValid = ko.observable(true);
        self.isAvailable = ko.observable(true);


        self.register = function () {
            var emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var usernameRegex = /^[a-zA-Z0-9]+$/;
            var nameRegex = /^[a-zA-Z]+$/;
            var numRegex = /^[0-9]{1,2}$/;
            self.isValid(true);        
            if (self.name() == "") {  
                self.isValid(false);
                utils.showError("Please enter your name");
            }
            else {
                if (!self.name().match(nameRegex)) {
                    self.isValid(false);
                    utils.showError("Name should contain only letters");
                }
            }

            if (self.lastname() == "") {
                self.isValid(false);
                utils.showError("Please enter your last name");
            }
            else {
                if (!self.lastname().match(nameRegex)) {
                    self.isValid(false);
                    utils.showError("Lastname should contain only letters");
                }
            }
            if (self.gender() == undefined) {
                self.isValid(false);
                utils.showError("Please select your gender");
            }
            if (self.username() == "") {
                self.isValid(false);
                utils.showError("Usarname cannot be left empty");
            }
            if (!self.username() == "" && !self.username().match(usernameRegex)) {
                self.isValid(false);
                utils.showError("Usarname is not a valid type");
            }
            if (self.email() == "") { 
                self.isValid(false);
                utils.showError("Email cannot be left empty");
            }
            if (!self.email() == "" && !self.email().match(emailRegex)) {
                self.isValid(false);
                utils.showError("Email is not valid a type");
            }
            if (self.password() == "") {
                self.isValid(false);
                utils.showError("Password cannot be left empty");
            }
            if (self.password() != "" && self.repassword() == "") {
                self.isValid(false);
                utils.showError("Please confirm your password");
            }
            if (self.password() != "" && self.repassword() != "" && self.repassword() != self.password()) {
                self.isValid(false);
                utils.showError("Password confirmation does not match");
            }

            if (self.age() == "") {
                self.isValid(false);
                utils.showError("Please enter your age");
            }
            else {
                if (!self.age().match(numRegex)) {
                    self.isValid(false);
                    utils.showError("Please enter your age in valid form");
                }
            }

            if (self.isValid()) { 
                self.isAvailable(true);
                webRequest.postSync("Api/Membership/CheckUserName", JSON.stringify({ username: self.username(), }), function (checkUser) {
                    if (!checkUser) {
                        utils.showError("Username is already taken !");
                        self.isAvailable(false);
                    }
                });
                if (self.isAvailable())
                    {
                    webRequest.postSync("Api/Membership/CheckMail", JSON.stringify({ email: self.email() }), function (checkMail) {
                    if (!checkMail){
                        utils.showError("Email is already taken !");
                        self.isAvailable(false);
                    }
                    });

                }
                if (self.isAvailable()) {
                    webRequest.postSync("Api/Membership/Register", JSON.stringify({ name: self.name(), lastname: self.lastname(), email: self.email(), username: self.username(), password: self.password(), repassword: self.repassword(), gender: self.gender(), age: self.age(), }), function (result) {
                        if (result) {
                            utils.showSuccess("Successfully registered to the sytsem.");
                            window.location.href = "#/"
                        }
                        else {
                            utils.showError("Something went wrong!");
                        }
                    });
                }

            }
        }
        self.back = function () {
            window.location.href = "#/"
        }
    }
});