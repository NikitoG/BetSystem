(function () {
    'use strict';

    function HomeController(statistics) {
        var vm = this;

        statistics.getStats()
            .then(function(stats) {
                vm.stats = stats;
            })
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['statistics', HomeController]);
}());