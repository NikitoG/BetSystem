(function(){
    'use strict';

    function SportDetailsController($routeParams, sports, games) {
        var vm = this;
        vm.name = $routeParams.name;
        sports.getSports()
            .then(function(sports) {
                vm.sports = sports;
            });

        vm.getGames = function (all) {
            games.getGamesBySportName(vm.name, all)
                .then(function(games){
                    vm.games = games;
                });
        };

        vm.getGames();
    }

    angular.module('myApp.controllers')
        .controller('SportDetailsController', ['$routeParams', 'sports', 'games', SportDetailsController])
}());