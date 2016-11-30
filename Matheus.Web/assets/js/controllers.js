angular.module('app')
	.controller('DashboardController', function ($scope, $stateParams, carService) {
	})
	.controller('CarListController', function ($scope, $state, $stateParams, $filter, moment, carService, carList) {
		var self = {};

		self.init = function () {
			if (!carList) {
				$scope.getCars();
			}
		};

		$scope.cars = carList || [];

		$scope.getCars = function () {

			function success(result) {
				if (!angular.isArray(result)) {
					return;
				}

				$scope.cars = result;
			};

			function error(err) { };


			carService.get().then(success, error);
		};

		$scope.remove = function (item) {
			function success(result) {
				$scope.cars.splice(item, 1);
			};

			function error(err) { };

			carService.remove(item.id).then(success, error);
		};

		$scope.filterStartDate = moment().subtract(6, 'months');
		$scope.filterEndDate = moment();




		self.init();
	})
	.controller('CarDetailController', function ($scope, $state, $stateParams, carService, car) {
		var self = {};

		self.init = function () {
			if (!car) {
				$scope.getDetail($stateParams.id);
			}
		};


		$scope.car = car || {};


		$scope.getDetail = function (item_id) {
			if (!item_id) {
				return;
			}

			function success(result) {
				if (!result) {
					return;
				}
				$scope.car = angular.copy(result);
			};

			function error(err) { };

			carService.getDetail(item_id).then(success, error);
		};

		$scope.save = function () {
			var car, pictureFile = $scope.car.pictureFile;

			car = angular.extend($scope.car, {
				picture: pictureFile ? formatBase64Image(pictureFile) : null
			});

			function success(result) {
				if (!result) {
					return;
				}

				$scope.car = angular.copy(result);
				$state.go('cars.list');
				//$state.go('.list', {}, { reload: true });
			};

			function error(err) { };

			carService.save(car).then(success, error);
		};

		$scope.remove = function () {
			function success(result) {
				$state.go('cars.list');
				//$state.go('.list', {}, { reload: true });
			};

			function error(err) { };

			carService.remove($scope.car.id).then(success, error);
		};


		$scope.resetOdometer = function () {
			if ($scope.car.id) {
				$scope.car.odometer.currentDistance = 0;
				$scope.save();
			}
		};

		$scope.getYears = function () {
			var currentYear = new Date().getFullYear();

			return range(currentYear - 100, currentYear).reverse();
		};

		$scope.showSaveForm = function () {
			return !$state.includes('cars.detail.supply.add');
		};

		$scope.showAddFuelSupplies = function () {
			return $state.includes('cars.detail') && !$state.includes('cars.detail.supply.add');
		};

		$scope.showListFuelSupplies = function () {
			return $state.includes('cars.detail');
		};


		$scope.$watch('car.pictureFile', function (newValue, oldValue) {
			//if (newValue == oldValue) { return; }
			//if (!newValue) { return; }
			var pic = formatBase64Image(newValue) || placeholder;

			$scope.car.picture = pic;
		});

		function formatBase64Image(picture) {
			if (!picture) { return; }

			return ['data:', picture.filetype, ';base64,', picture.base64].join('');
		};

		function range(min, max, step) {
			var output = [];
			step = step || 1;

			for (var i = min; i <= max; i += step) {
				output.push(i);
			}
			return output;
		};

		self.init();
	})
	.controller('AddSupplyController', function ($scope, $state, $stateParams, carService, fuelTypeService, fuelTypesList) {
		var self = {};

		$scope.car = $stateParams.car;

		$scope.fuelSupply = {};

		$scope.fuelTypes = fuelTypesList || [];


		$scope.addSupply = function () {
			var carId = $stateParams.id;
			var newItem = angular.copy($scope.fuelSupply);

			function success(result) {
				if (!result) {
					return;
				}

				$scope.car = angular.copy(result);
				$state.go('cars.detail', {}, { reload: true });
			};

			function error(err) { };

			carService.addFuelSupply(carId, newItem).then(success, error);
		};


		$scope.getTotalPrice = function () {
			if (!$scope.fuelSupply.fuelType) {
				return;
			}
			if (!$scope.fuelSupply.fuelQuantity) {
				return;
			}

			return $scope.fuelSupply.fuelQuantity * $scope.fuelSupply.fuelType.price;
		};


		self.init = function () {
			if (!fuelTypesList) {
				self.getFuelTypes();
			}
		};

		self.getFuelTypes = function () {
			function success(result) {
				$scope.fuelTypes = angular.copy(result);
			};

			function error(err) { };

			fuelTypeService.get().then(success, error);

		};

		self.init();
	})

	.controller('FuelSupplyListController', function ($scope, $state, $stateParams, fuelSupplyList) {

		$scope.fuelSupplies = fuelSupplyList || [];


		$scope.getDistanceTravelled = function (item) {
			var total = 0;

			return total;
		};

		$scope.getTotal = function (items, prop) {
			var total = 0;

			for (var i in items) {
				var item = items[i];
				if (!item.hasOwnProperty(prop)) { return; }
				total += item[prop];
			};

			return total;
		};

		$scope.getAverage = function (items, prop) {
			var total = 0;

			if (!items || !items.length) { return total; }

			for (var i in items) {
				var item = items[i];
				if (!item.hasOwnProperty(prop)) { return; }
				total += item[prop];
			};

			return total / items.length;
		};

	});


angular.module('app')
	.filter('dateRange',
		function ($linq, moment) {
			return function (items, startDate, endDate) {
				var output, sDate, eDate;

				output = [];
				sDate = startDate ? moment(startDate) : moment(0);
				eDate = endDate ? moment(endDate) : moment();

				if (!sDate.isValid() || !eDate.isValid()) {
					console.warn('Invalid date format.');
					return items || [];
				}

				angular.forEach(items, function (item, i) {
					var car, fS = [];

					//car = angular.copy(item);
					fS = $linq.Enumerable()
						.From(item.fuelSupplies || [])
						.Where(function (x) {
							var fueledDate = moment(x.fueledAt);

							if (!fueledDate.isSameOrAfter(sDate) || !fueledDate.isSameOrBefore(eDate)) {
								return false;
							}
							return true;
						});

					//angular.forEach(item.fuelSupplies)

					item.fuelSupplies = fS.ToArray();

					output.push(item);
				});

				return output;
			};
		})

.filter('searchCar', function (S) {
	function _contain(car, search) {
		var plate, model, manufacturer, plateFormatted, searchFields;

		if (!car) { return; }

		search = S((search || '').toUpperCase()).latinise().s;
		plate = car.licencePlate;
		plateFormatted = [plate.substr(0, 3), plate.substr(3)].join('-');
		model = car.model;
		manufacturer = car.manufacturer;

		searchFields = [plate, plateFormatted, model, manufacturer].join().toUpperCase();

		return S(searchFields).latinise().contains(search);
	};

	return function (items, searchText) {
		var output = [];

		if (!searchText) {
			return items;
		}

		searchText = searchText.toUpperCase();

		angular.forEach(items, function (item, i) {
			if (_contain(item, searchText)) {
				output.push(item);
			}
		});

		return output;
	};
})

.filter('searchCarPlate', function (S) {
	function _contain(car, search) {
		var plate, plateFormatted, searchFields;

		if (!car) { return; }

		search = S((search || '').toUpperCase()).latinise().s;
		plate = car.licencePlate;
		plateFormatted = [plate.substr(0, 3), plate.substr(3)].join('-');

		searchFields = [plate, plateFormatted].join().toUpperCase();

		return S(searchFields).latinise().contains(search);
	};

	return function (items, searchText) {
		var output = [];

		if (!searchText) {
			return items;
		}

		searchText = searchText.toUpperCase();

		angular.forEach(items, function (item, i) {
			if (_contain(item.carSupplied, searchText)) {
				output.push(item);
			}
		});

		return output;
	};
});