angular.module('app', ['ui.router'])
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
			'main@index': { templateUrl: 'assets/views/_main.html', },
		},
	})
	.state('dashboard', {
		parent: 'index',
		url: '/dashboard',
		templateUrl: 'assets/views/cars/cars.list.html',
		controller: 'CarListController'
	})
	//.state('dashboard.content', {
	//	url: '/:id',
	//	views: {
	//		'content@index': {
	//			templateUrl: 'assets/views/content.html',
	//			controller: 'DetailCtrl'
	//		}
	//	},
	//})

	.state('cars', {
		parent: 'dashboard',
		url: '/cars',
		views: {
			'content@index': {
				templateUrl: 'assets/views/cars/cars.list.html',
				controller: 'CarListController'
			}
		},
	})
	.state('cars.detail', {
		parent: 'cars',
		url: '/:id',
		views: {
			'content@index': {
				templateUrl: 'assets/views/cars/cars.detail.html',
				controller: 'CarDetailController'
			}
		},
	});
});
