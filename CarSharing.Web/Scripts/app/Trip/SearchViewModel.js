define(["webRequest", "utils", "Location/LocationModel", "Trip/TripModel"], function (webRequest, utils, LocationModel, TripModel) {
    return function () {
        var self = this;
        self.username = ko.observable("");
        self.depdate = ko.observable("");
        self.esthour = ko.observable("");
        self.hours = ko.observableArray(["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"]);
        self.hour = ko.observable("");
        self.maxprice = ko.observable("");
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
        self.trips = ko.observableArray([]);

        self.search = function () {
            self.trips.removeAll();
            var departureLocationId = (self.departureDistrict() == undefined) ? self.departureProvince() : self.departureDistrict();
            var arrivalLocationId = (self.arrivalDistrict() == undefined) ? self.arrivalProvince() : self.arrivalDistrict();
            var seat = (self.seatnum() != undefined) ? self.seatnum : -1;
            var numRegex = /^[0-9]+$/;
            var dateRegex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            var usernameRegex = /^[a-zA-Z0-9]+$/;
            self.isValid(true);
            if (self.depdate() != "" && !self.depdate().match(dateRegex)) {
                self.isValid(false);
                utils.showError("Departure date is not in a valid form!");
            }
            if (self.maxprice() != "" && !self.maxprice().match(numRegex)) {
                self.isValid(false);
                utils.showError("Price is not in a valid form!");
            }
            if (self.username() != "" && !self.username().match(usernameRegex)) {
                self.isValid(false);
                utils.showError("User name should contain only letters and digits!");
            }
            if (self.isValid()) {
                webRequest.get("Api/Trip/Search", { "username": self.username(), "depDate": self.depdate(), "estHour": self.esthour(), "hour": self.hour(), "price": self.maxprice(), "seatNum": seat, "arrLocId": arrivalLocationId, "depLocId": departureLocationId }, function (result) {
                    if (result == null || result.length == 0) {
                        return;
                    }
                    var trips = result;
                    for (var i = 0; i < trips.length; i++) {
                        var tripModel = new TripModel(trips[i]);
                        self.trips.push(tripModel);
                    }
                });
            }
        }
        self.back = function () {
            webRequest.get("Api/Membership/IsLoggedIn", null, function (result) {
                if (!result)
                    window.location.href = "#/";
                else
                    window.location.href = "#/Home"
            });
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

