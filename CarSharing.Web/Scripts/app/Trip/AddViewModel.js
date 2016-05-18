define(["webRequest", "utils", "Location/LocationModel"], function (webRequest, utils, LocationModel) {
    return function () {

        webRequest.get("Api/Membership/IsLoggedIn", null, function (result) {
            if (!result) {
                utils.showError("You have to login!");
                window.location.href = "#/";
            }
            else {
                document.getElementById("add").style.display = "";
            }
        });

        var self = this;

        self.depdate = ko.observable("");
        self.esthour = ko.observable("");
        self.hours = ko.observableArray(["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"]);
        self.mins = ko.observableArray(["00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55"]);
        self.estmin = ko.observable("");
        self.hour = ko.observable("");
        self.min = ko.observable("");
        self.price = ko.observable("");
        self.seatnum = ko.observable("");
        self.seatnums = ko.observableArray(["1", "2", "3", "4"]);
        self.departureProvince = ko.observable();
        self.arrivalProvince = ko.observable();
        self.departureDistrict = ko.observable();
        self.arrivalDistrict = ko.observable();
        self.provinces = ko.observableArray([]);
        self.departureDistricts = ko.observableArray([]);
        self.arrivalDistricts = ko.observableArray([]);
        self.isValid = ko.observable(true);
        self.add = function () {
            var departureLocationId = (self.departureDistrict() == undefined) ? self.departureProvince() : self.departureDistrict();
            var arrivalLocationId = (self.arrivalDistrict() == undefined) ? self.arrivalProvince() : self.arrivalDistrict();
            var numRegex = /^[0-9]+$/;
            var dateRegex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            self.isValid(true);
            if (self.depdate() == "") {
                self.isValid (false);
                utils.showError("Please enter the departure date");
            }
            else {
                if (!self.depdate().match(dateRegex)) {
                    self.isValid (false);
                    utils.showError("Departure date is not in a valid form!");
                }
            }
            if (self.seatnum() == undefined) {
                self.isValid (false);
                utils.showError("Please select the seat number");
            }
            if (self.price() == "") {
                self.isValid (false);
                utils.showError("Please enter the price for the trip");
            }
            else {
                if (!self.price().match(numRegex)) {
                    self.isValid (false);
                    utils.showError("Price is not in a valid form!");
                }
            }
            if (self.isValid()) {
                webRequest.post("Api/Trip/Add", JSON.stringify({ depdate: self.depdate(), price: self.price(), seatnum: self.seatnum(), depLocId: departureLocationId, arrLocId: arrivalLocationId, hour: self.hour(), min: self.min(), esthour: self.esthour(), estmin: self.estmin() }), function (result) {
                    if (result) {
                        utils.showSuccess("Trip successfully added.");
                        window.location.href = "#/Home"
                    }
                    else {
                        utils.showError("Something went wrong!");
                    }
                });
            }

        }

        self.back = function () {
            window.location.href = "#/Home"
        }

        self.getProvinces = function () {
            webRequest.get("Api/Location/GetProvinces", null, function (result) {
                if (result == null || result.length == 0) {
                    return;
                }
                var provinces = result;
                for (var i = 0; i < provinces.length; i++) {
                    var locationModel = new LocationModel(provinces[i]);
                    self.provinces.push(locationModel);
                }
            });
        }

        self.getDepartureDistricts = function () {
            webRequest.get("Api/Location/GetDistrictsByProvince", { "provinceId": self.departureProvince() }, function (result) {
                if (result == null || result.length == 0) {
                    return;
                }
                var districts = result;
                for (var i = 0; i < districts.length; i++) {
                    var locationModel = new LocationModel(districts[i]);
                    self.departureDistricts.push(locationModel);
                }
            });
        }

        self.getArrivalDistricts = function () {
            webRequest.get("Api/Location/GetDistrictsByProvince", { "provinceId": self.arrivalProvince() }, function (result) {
                if (result == null || result.length == 0) {
                    return;
                }
                var districts = result;
                for (var i = 0; i < districts.length; i++) {
                    var locationModel = new LocationModel(districts[i]);
                    self.arrivalDistricts.push(locationModel);
                }
            });
        }

        self.onDepartureProvinceChange = function () {
            self.departureDistricts.removeAll();
            if (self.departureProvince() == undefined) {
                return;
            }
            self.getDepartureDistricts();
        }

        self.onArrivalProvinceChange = function () {
            self.arrivalDistricts.removeAll();
            if (self.arrivalProvince() == undefined) {
                return;
            }
            self.getArrivalDistricts();
        }
    }
});