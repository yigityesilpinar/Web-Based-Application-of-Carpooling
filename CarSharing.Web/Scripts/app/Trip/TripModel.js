define(function () {
    return function (model) {
        var self = this;
        self.id = model.tripId;
        self.departDate = model.DepartDate;
        self.username = model.user.username;
        self.seatNumber = model.availableSeatNumber;
        self.price = model.price;
        self.departLocation = model.departLocation.name;
        self.arrivalLocation = model.arrivalLocation.name;
        self.estimatedMin = (model.estimatedMin != undefined) ? model.estimatedMin : "00";
        self.estimatedHour = model.estimatedHour;
        self.depMin = (model.depMin != undefined) ? model.depMin : "00";
        self.depHour = model.depHour;
    }
});