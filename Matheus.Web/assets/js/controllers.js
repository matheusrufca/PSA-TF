angular.module('app')
	.controller('DashboardController', function($scope, $stateParams, carService) {
		})
	.controller('CarListController', function($scope, $state, $stateParams, carService, carList) {
			var self = {};

			self.init = function() {
				if (!carList) {
					$scope.getCars();
				}
			};

			$scope.cars = carList || [];

			$scope.getCars = function() {

				function success(result) {
					if (!angular.isArray(result)) {
						return;
					}

					$scope.cars = result;
				};

				function error(err) {};


				carService.get().then(success, error);
			};

			$scope.remove = function(item) {
				function success(result) {
					$scope.cars.splice(item, 1);
				};

				function error(err) {};

				carService.remove(item.id).then(success, error);
			};


			self.init();
		})
	.controller('CarDetailController', function($scope, $state, $stateParams, carService, car) {
			var self = {};

			self.init = function() {
				if (!car) {
					$scope.getDetail($stateParams.id);
				}
			};


			$scope.car = car || {};


			$scope.getDetail = function(item_id) {
				if (!item_id) {
					return;
				}

				function success(result) {
					if (!result) {
						return;
					}
					$scope.car = angular.copy(result);
				};

				function error(err) {};

				carService.getDetail(item_id).then(success, error);
			};

			$scope.save = function() {
				function success(result) {
					if (!result) {
						return;
					}

					$scope.car = angular.copy(result);
					$state.go('cars.list');
					//$state.go('.list', {}, { reload: true });
				};

				function error(err) {};

				carService.save($scope.car).then(success, error);
			};

			$scope.remove = function() {
				function success(result) {
					$state.go('cars.list');
					//$state.go('.list', {}, { reload: true });
				};

				function error(err) {};

				carService.remove($scope.car.id).then(success, error);
			};


			$scope.resetOdometer = function() {
				if ($scope.car.id) {
					$scope.car.odometer.currentDistance = 0;
					$scope.save();
				}
			};

			$scope.getYears = function() {
				var currentYear = new Date().getFullYear();

				return range(currentYear - 100, currentYear).reverse();
			};

			$scope.showSaveForm = function() {
				return !$state.includes('cars.detail.supply.add');
			};

			$scope.showAddFuelSupplies = function() {
				return $state.includes('cars.detail') && !$state.includes('cars.detail.supply.add');
			};

			$scope.showListFuelSupplies = function() {
				return $state.includes('cars.detail');
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
	.controller('AddSupplyController', function($scope, $state, $stateParams, carService, fuelTypeService, fuelTypesList) {
			var self = {};

			$scope.car = $stateParams.car;

			$scope.fuelSupply = {};

			$scope.fuelTypes = fuelTypesList || [];


			$scope.addSupply = function() {
				var carId = $stateParams.id;
				var newItem = angular.copy($scope.fuelSupply);

				function success(result) {
					if (!result) {
						return;
					}

					$scope.car = angular.copy(result);
					$state.go('cars.detail', {}, { reload: true });
				};

				function error(err) {};

				carService.addFuelSupply(carId, newItem).then(success, error);
			};


			$scope.getTotalPrice = function() {
				if (!$scope.fuelSupply.fuelType) {
					return;
				}
				if (!$scope.fuelSupply.fuelQuantity) {
					return;
				}

				return $scope.fuelSupply.fuelQuantity * $scope.fuelSupply.fuelType.price;
			};


			self.init = function() {
				if (!fuelTypesList) {
					self.getFuelTypes();
				}
			};

			self.getFuelTypes = function() {
				function success(result) {
					$scope.fuelTypes = angular.copy(result);
				};

				function error(err) {};

				fuelTypeService.get().then(success, error);

			};

			self.init();
		})

	.controller('FuelSupplyListController', function($scope, $state, $stateParams, fuelSupplyList) {

		$scope.fuelSupplies = fuelSupplyList || [];
	});
