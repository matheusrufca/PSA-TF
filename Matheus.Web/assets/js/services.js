angular.module('app')
	.constant("apiSettings", {
		serviceUrl: 'http://localhost:22233/api'
	})
	.factory('carService', function ($http, $q, apiSettings, toastr) {
		var self = {}, service = {};

		self.errorMsg = 'Ops, ocorreu um erro';

		service.get = function () {
			var df = $q.defer(), serviceUrl;

			serviceUrl = apiSettings.serviceUrl + '/cars';

			$http.get(serviceUrl).then(function (response) {
				if (!response.data) { df.reject({}); return; }

				df.resolve(response.data.data);
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
				if (!response.data) { df.reject({}); return; }

				df.resolve(response.data.data);
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
				if (!result) { df.reject({}); return; }

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
				if (!result) { df.reject({}); return; }

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

				if (!result) { df.reject({}); return; }
				df.resolve(response.data.data);

				toastr.success(result.statusMessage);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
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
				if (!response.data) { df.reject({}); return; }

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
				if (!response.data) { df.reject({}); return; }

				df.resolve(response.data.data);
			}, function (err) {
				df.reject(err); // reject promise

				toastr.error(err.message || self.errorMsg);
			});

			return df.promise;
		};

		return service;
	});

