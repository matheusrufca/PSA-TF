angular.module('app')
  .constant("apiSettings", {
  	serviceUrl: 'http://localhost:22233/api'
  })
  .factory('carService', function ($http, $q, apiSettings) {
  	var self = {}, service = {};

  	service.get = function () {
  		var df = $q.defer(), serviceUrl;

  		serviceUrl = apiSettings.serviceUrl + '/cars';

  		$http.get(serviceUrl).then(function (response) {
  			if (!response.data) { df.reject({}); return; }

  			df.resolve(response.data.data);
  		}, function (err) {
  			df.reject(err); // reject promise
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
  			if (!response.data) { df.reject({}); return; }

  			df.resolve(response.data.data);
  		}, function (err) {
  			df.reject(err); // reject promise
  		});

  		return df.promise;
  	};

  	service.edit = function (item_id, item) {
  		var df = $q.defer(), serviceUrl;

  		serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id;

  		$http.put(serviceUrl, item).then(function (response) {
  			if (!response.data) { df.reject({}); return; }

  			df.resolve(response.data.data);
  		}, function (err) {
  			df.reject(err); // reject promise
  		});

  		return df.promise;
  	};

  	service.remove = function (item_id, item) {
  		var df = $q.defer(), serviceUrl;

  		serviceUrl = apiSettings.serviceUrl + '/cars/' + item_id;

  		$http.delete(serviceUrl).then(function (response) {
  			if (!response.data) { df.reject({}); return; }

  			df.resolve(response.data.data);
  		}, function (err) {
  			df.reject(err); // reject promise
  		});

  		return df.promise;
  	};

  	return service;
  });



angular.module('app')
	.filter('licencePlate', function () {
		return function (licencePlate) {
			var output = '';

			if (!typeof licencePlate == 'string') { return licencePlate; }
			if (!licencePlate || licencePlate.length < 7) { return licencePlate; }

			output = [licencePlate.substr(0, 3), "-", licencePlate.substr(3)].join('');
			return output;
		};
	});
