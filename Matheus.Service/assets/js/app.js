angular.module('app', ['ui.router', 'ui.bootstrap'])
.config(function ($stateProvider, $urlRouterProvider) {

	$urlRouterProvider.otherwise('/');

	$stateProvider
        .state('index', {
        	abstract: true,
        	//url: '/',
        	views: {
        		'@': {
        			templateUrl: 'assets/views/dashboard.html',
        			controller: 'DashboardController'
        		},
        		//'top@index': { templateUrl: 'tpl.top.html', },
        		//'left@index': { templateUrl: 'tpl.left.html', },
        		'main': { templateUrl: 'assets/views/dashboard.main.html' },
        	},
        })
	//.state('cars', {
	//	parent: 'index',
	//	url: '/cars/list',
	//	templateUrl: 'assets/views/cars.list.html',
	//	controller: 'CarsController'
	//})
	//.state('cars.detail', {
	//	url: '/cars/:id',
	//	views: {
	//		'detail@index': {
	//			templateUrl: 'assets/views/cars.detail.html',
	//			controller: 'CarsController'
	//		}
	//	},
	//});
})
.controller('DashboardController', function ($scope, $stateParams) {

})
.controller('CarsController', function ($scope, $stateParams) {
	$scope.id = $stateParams.id;
})
.controller('FuelSupplyController', function ($scope, $stateParams) {
	$scope.id = $stateParams.id;
});