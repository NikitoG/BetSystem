(function () {
    'use strict';

    function SportController(sports) {
        var vm = this;

        sports.getSports()
            .then(function(sports) {
                vm.sports = sports;
            })
    }

    angular.module('myApp.controllers')
        .controller('SportController', ['sports', SportController]);
}());