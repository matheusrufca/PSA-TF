angular.module('app')
	.filter('licencePlate',
		function () {
			return function (licencePlate) {
				var output = '';

				if (!typeof licencePlate == 'string') {
					return licencePlate;
				}
				if (!licencePlate || licencePlate.length < 7) {
					return licencePlate;
				}

				output = [licencePlate.substr(0, 3), "-", licencePlate.substr(3)].join('');
				return output;
			};
		});