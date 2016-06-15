(function(){
    'use strict';

    function EventDetailsController($routeParams, sports, games) {
        var vm = this;
        vm.name = $routeParams.name;
        vm.eventKey = $routeParams.eventKey;
        sports.getSports()
            .then(function(sports) {
                vm.sports = sports;
            });

        vm.getGames = function (all) {
            games.getGamesByEventName(vm.name, vm.eventKey, all)
                .then(function(games){
                    vm.games = games;
                });
        };

        vm.getGames();
    }

    angular.module('myApp.controllers')
        .controller('EventDetailsController', ['$routeParams', 'sports', 'games', EventDetailsController]);
}());