angular.module('app', ['ngAnimate', 'ui.router', 'ui.router.default', 'ui.bootstrap', 'toastr'])
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
				controller: 'CarListController'
			})
			.state('cars', {
				abstract: true,
				parent: 'dashboard',
				url: '/cars',
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
				}
			})
			.state('cars.detail', {
				parent: 'cars',
				url: '/:id',
				views: {
					'content@index': {
						templateUrl: 'assets/views/cars/cars.detail.html',
						controller: 'CarDetailController'
					}
				}
			})


			.state('fuelSupplies', {
				abstract: true,
				parent: 'dashboard',
				url: '/fuelSupplies',
				views: {
					'content@index': {
						templateUrl: 'assets/views/fuelSupplies/fuelSupplies.list.html',
						controller: 'FuelSupplyListController'
					}
				}
			})
			.state('fuelSupplies.list', {
				parent: 'fuelSupplies',
				url: '/',
				views: {
					'content@index': {
						templateUrl: 'assets/views/fuelSupplies/fuelSupplies.list.html',
						controller: 'FuelSupplyListController'
					}
				}
			})
			.state('fuelSupplies.detail', {
				parent: 'fuelSupplies',
				url: '/:id',
				views: {
					'content@index': {
						templateUrl: 'assets/views/fuelSupplies/fuelSupplies.detail.html',
						controller: 'FuelSupplyDetailController'
					}
				}
			});
	});
