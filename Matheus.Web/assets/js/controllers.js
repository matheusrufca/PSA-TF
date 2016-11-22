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
				$state.go('.child');
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

		function range(min, max, step) {
			var output = [];
			step = step || 1;

			for (var i = min; i <= max; i += step) {
				output.push(i);
			}
			return output;
		}
	});
