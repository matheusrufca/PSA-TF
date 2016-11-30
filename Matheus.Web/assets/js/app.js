angular.module('app', ['ngAnimate', 'ui.router', 'ui.router.default', 'ui.bootstrap', 'angular.filter', 'toastr', 'datatables', 'angularMoment', 'angular-linq'])
	.config(function ($stateProvider, $urlRouterProvider) {

		$urlRouterProvider.otherwise('/dashboard');

		$stateProvider
			.state('index', {
				abstract: true,
				//url: '/',
				views: {
					'@': {
						//templateUrl: 'assets/views/layout.html',
						//controller: 'DashboardController'

					},
					'sidebar@index': { templateUrl: 'assets/views/_sidebar.html', },
					'main@index': { templateUrl: 'assets/views/_main.html', }
				}
			})
			.state('dashboard', {
				parent: 'index',
				url: '/dashboard',
				templateUrl: 'assets/views/cars/cars.list.html',
				controller: 'CarListController',
				resolve: {
					carList: function ($stateParams, carService) {
						return carService.get().then(function (result) {
							return result;
						});
					}
				}
			})

			.state('cars', {
				abstract: true,
				parent: 'dashboard',
				url: '/cars',
				data: {
					pageTitle: 'Carros'
				},
				views: {
					'content@index': {
						templateUrl: 'assets/views/cars/cars.list.html',
						controller: 'CarListController'
					}
				}
			})
			.state('cars.list', {
				parent: 'cars',
				url: '/',
				views: {
					'content@index': {
						templateUrl: 'assets/views/cars/cars.list.html',
						controller: 'CarListController'
					}
				},
				resolve: {
					carList: function ($stateParams, carService) {
						return carService.get().then(function (result) {
							return result;
						});
					}
				}
			})
			.state('cars.add', {
				parent: 'cars',
				url: '/add',
				views: {
					'content@index': {
						templateUrl: 'assets/views/cars/cars.detail.html',
						controller: 'CarDetailController'
					}
				},
				resolve: {
					car: function () { return null; }
				}
			})
			.state('cars.detail', {
				parent: 'cars',
				url: '/edit/:id',
				views: {
					'content@index': {
						templateUrl: 'assets/views/cars/cars.detail.html',
						controller: 'CarDetailController'
					},
					'supplies': {
						templateUrl: 'assets/views/cars/cars.supply.html',
						controller: 'FuelSupplyListController'
					}
				},
				resolve: {
					car: function ($stateParams, carService) {
						return carService.getDetail($stateParams.id).then(function (result) {
							return result;
						});
					}
				}
			})
			.state('cars.detail.supply', {
				parent: 'cars.detail',
				abstract: true,
				'default': '.list',
				views: {
					'supplies@cars.detail': {
						templateUrl: 'assets/views/cars/cars.supply.html'
					}
				}
			})
			.state('cars.detail.supply.list', {
				parent: 'cars.detail.supply',
				url: '',
				views: {
					'@cars.detail.supply': {
						templateUrl: 'assets/views/cars/cars.supply.list.html'
					}
				}
			})
			.state('cars.detail.supply.add', {
				parent: 'cars.detail.supply',
				url: '/add-supply',
				views: {
					'@cars.detail.supply': {
						templateUrl: 'assets/views/cars/cars.supply.add.html',
						controller: 'AddSupplyController'
					}
				},
				resolve: {
					fuelTypesList: function (fuelTypeService) {
						return fuelTypeService.get().then(function (result) {
							return result;
						});
					}
				}
			})



			.state('supplies', {
				abstract: true,
				parent: 'dashboard',
				url: '/supplies',
				views: {
					'content@index': {
						templateUrl: 'assets/views/supplies/supplies.list.html',
						controller: 'FuelSupplyListController'
					}
				},
				data: {
					pageTitle: 'Abastecimentos'
				}
			})
			.state('supplies.list', {
				parent: 'supplies',
				url: '/',
				resolve: {
					fuelSupplyList: function (fuelSupplyService) {
						return fuelSupplyService.get().then(function (result) {
							return result;
						});
					}
				},
				views: {
					'content@index': {
						templateUrl: 'assets/views/supplies/supplies.list.html',
						controller: 'FuelSupplyListController'
					}
				}
			})
			.state('supplies.detail', {
				parent: 'supplies',
				url: '/:id',
				views: {
					'content@index': {
						templateUrl: 'assets/views/supplies/supplies.detail.html',
						controller: 'FuelSupplyDetailController'
					}
				}
			});
	})
	.run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams) {
		$rootScope.$state = $state;
		$rootScope.$stateParams = $stateParams;
	}]);
