angular.module('app')
	.constant("apiSettings", {
		serviceUrl: 'http://localhost:22233/api'
	})


	.factory('Car', function ($linq, moment) {
		function Car(data) {
			if (data) {
				this.setData(data);
			}
		};

		Car.prototype = {
			setData: function (data) {
				angular.extend(this, data);
			},
			getFuelAverage: function () {
				var fuelSupplies = this.fuelSupplies || [];
				var totalFuel = 0, totalDistance = 0;

				angular.forEach(fuelSupplies, function (item, i) {
					totalFuel += item.fuelQuantity;
					totalDistance += item.distanceTravelled;
				});

				return totalDistance / totalFuel;
			},
			getCostAverage: function () {
				var fuelSupplies = this.fuelSupplies || [];
				var totalPrice = 0, totalDistance = 0;

				angular.forEach(fuelSupplies, function (item, i) {
					totalPrice += item.totalPrice;
					totalDistance += item.distanceTravelled;
				});

				return totalDistance / totalPrice;
			}
		};

		return Car;
	})


	.factory('carService', function ($http, $q, apiSettings, toastr, Car) {
		var self = {}, service = {};

		self.errorMsg = 'Ops, ocorreu um erro';

		service.get = function () {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars';

			$http.get(serviceUrl).then(function (response) {
				if (!isOk(response.status)) { df.reject({}); return; }

				var mappedData = (response.data.data || []).map(function (x) {
					return new Car(x);
				});


				df.resolve(mappedData);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		service.getDetail = function (item_id) {
			var df = $q.defer(), serviceUrl;
			serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id;

			$http.get(serviceUrl).then(function (response) {
				if (!isOk(response.status)) { df.reject({}); return; }


				df.resolve(new Car(response.data.data || {}));
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};


		service.save = function (item) {
			if (item.id) {
				return service.edit(item.id, item);
			} else {
				return service.add(item);
			}
		};

		service.add = function (item) {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars';

			$http.post(serviceUrl, item).then(function (response) {
				var result = response.data;
				if (!isOk(response.status)) { df.reject({}); return; }

				df.resolve(result.data);
				toastr.success(result.statusMessage);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		service.edit = function (item_id, item) {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id;

			$http.put(serviceUrl, item).then(function (response) {
				var result = response.data;
				if (!isOk(response.status)) { df.reject({}); return; }

				df.resolve(result.data);
				toastr.success(result.statusMessage);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		service.addFuelSupply = function (item_id, item) {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id + '/add_fuel_suppy';

			$http.put(serviceUrl, item).then(function (response) {
				var result = response.data;
				if (!result) { df.reject({}); return; }

				df.resolve(result.data);
				toastr.success(result.statusMessage);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		service.remove = function (item_id) {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id;

			$http.delete(serviceUrl).then(function (response) {
				var result = response.data;

				if (!isOk(response.status)) { df.reject({}); return; }
				df.resolve(response.data.data);

				toastr.success(result.statusMessage);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		function isOk(statusCode) {
			return statusCode >= 200 && statusCode < 300;
		};

		return service;
	})
	.factory('fuelSupplyService', function ($http, $q, apiSettings, toastr) {
		var self = {}, service = {};

		self.errorMsg = 'Ops, ocorreu um erro';

		service.get = function () {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/fuelSupplies';

			$http.get(serviceUrl).then(function (response) {
				if (!isOk(response.status)) { df.reject({}); return; }

				df.resolve(response.data.data);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		return service;
	})
	.factory('fuelTypeService', function ($http, $q, apiSettings, toastr) {
		var self = {}, service = {};

		self.apiServiceUrl = apiSettings.serviceUrl + '/fuelTypes/';
		self.errorMsg = 'Ops, ocorreu um erro';

		service.get = function () {
			var df = $q.defer();

			$http.get(self.apiServiceUrl).then(function (response) {
				if (!isOk(response.status)) { df.reject({}); return; }

				df.resolve(response.data.data);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		return service;
	});

