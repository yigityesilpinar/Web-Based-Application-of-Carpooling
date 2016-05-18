define(function () {
    return function (model) {
        var self = this;

        self.id = model.locationId;
        self.name = model.name;
        self.parentId = model.parentId;
        self.lat = model.lat;
        self.lon = model.lon;
    }
});