angular.module('app')
	.controller('DashboardController', function ($scope, $stateParams, carService) {
	})
	.controller('ListCtrl', function () { })
	.controller('DetailCtrl', function ($scope, $stateParams) {
		$scope.id = $stateParams.id;
	})


	.controller('CarListController', function ($scope, $state, $stateParams, carService) {
		var self = {};

		self.init = function () {
			$scope.getCars();
		};

		$scope.cars = [];

		$scope.getCars = function () {

			function success(result) {
				if (!angular.isArray(result)) { return; }

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


		self.init();
	})
	.controller('CarDetailController', function ($scope, $state, $stateParams, carService) {
		var self = {};

		$scope.id = $stateParams.id;
		$scope.car = {};



		$scope.$watch('id', function (newValue, oldValue) {
			$scope.getDetail($scope.id);
		});

		$scope.cars = [];
		$scope.car = {};

		$scope.getDetail = function (item_id) {
			if (!item_id) { return; }

			function success(result) {
				if (!result) { return; }
				$scope.car = angular.copy(result);
			};

			function error(err) { };

			carService.getDetail(item_id).then(success, error);
		};

		$scope.save = function () {
			function success(result) {
				if (!result) { return; }

				$scope.car = angular.copy(result);
			};

			function error(err) { };

			carService.save($scope.car).then(success, error);
		};

		$scope.remove = function () {
			function success(result) {
				$state.go('^');
			};

			function error(err) { };

			carService.remove($scope.car.id).then(success, error);
		};

		$scope.addSupply = function () {
			$state.go('cars.addSupply', { id: $scope.car.id, car: $scope.car });
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

		function range(min, max, step) {
			var output = [];
			step = step || 1;

			for (var i = min; i <= max; i += step) {
				output.push(i);
			}
			return output;
		}
	})


	.controller('FuelSupplyListController', function ($scope, $state, $stateParams, fuelSupplyService, suppliesData) {
		var self = {};

		$scope.fuelSupplies = suppliesData || [];

		self.init = function () {
			//$scope.getFuelSupplies();
		};


		$scope.getFuelSupplies = function () {
			function success(result) {
				$scope.fuelSupplies = angular.copy(result);
			};

			function error(err) { };

			fuelSupplyService.get().then(success, error);
		};


		self.init();
	})

	.controller('AddSupplyController', function ($scope, $state, $stateParams, carService, fuelTypeService) {
		var self = {};

		$scope.car = $stateParams.car;

		$scope.fuelSupply = {};

		$scope.fuelTypes = [];


		$scope.getTotalPrice = function () {
			if (!$scope.fuelSupply.fuelType) { return; }
			if (!$scope.fuelSupply.fuelQuantity) { return; }

			return $scope.fuelSupply.fuelQuantity * $scope.fuelSupply.fuelType.price;
		};

		self.init = function () {
			self.getFuelTypes();
		};


		$scope.save = function () {
			function success(result) {
				if (!result) { return; }

				$scope.fuelSupply = angular.copy(result);
			};

			function error(err) { };

			var newItem = angular.copy($scope.fuelSupply);




			carService.save(newItem).then(success, error);
		};

		self.getFuelTypes = function () {
			function success(result) {
				$scope.fuelTypes = angular.copy(result);
			};

			function error(err) { };

			fuelTypeService.get().then(success, error);

		};

		self.init();
	});
