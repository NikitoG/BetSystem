(function () {
    'use strict';

    function config($routeProvider) {
        var PARTIALS_PREFIX = 'views/partials/';
        var CONTROLLER_AS_VIEW_MODEL = 'vm';

        $routeProvider
            .when('/', {
                templateUrl: PARTIALS_PREFIX +  'home/home.html',
                controller: 'HomeController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/sports', {
                templateUrl: PARTIALS_PREFIX +  'sports/sports.html',
                controller: 'SportController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/sports/:name', {
                templateUrl: PARTIALS_PREFIX +  'games/sport-details.html',
                controller: 'SportDetailsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/sports/:name/:eventKey', {
                templateUrl: PARTIALS_PREFIX +  'games/sport-details.html',
                controller: 'EventDetailsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/match/:id', {
                templateUrl: PARTIALS_PREFIX +  'games/match-details.html',
                controller: 'MatchController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/register', {
                templateUrl: PARTIALS_PREFIX + 'identity/register.html',
                controller: 'SignUpCtrl'
            })
            .otherwise({ redirectTo: '/' });
    }

    angular.module('myApp.services', []);
    angular.module('myApp.filters', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'myApp.controllers', 'myApp.filters']).
        config(['$routeProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', 'http://localhost:30774');
}());