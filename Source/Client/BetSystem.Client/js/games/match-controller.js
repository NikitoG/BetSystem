(function(){
    'use strict';

    function MatchController($routeParams, sports, games) {
        var vm = this;
        vm.gameId = $routeParams.id;

        sports.getSports()
            .then(function(sports) {
                vm.sports = sports;
            });

        games.matchDetails(vm.gameId)
            .then(function(game) {
                vm.game = game;
            });
    }

    angular.module('myApp.controllers')
        .controller('MatchController', ['$routeParams', 'sports', 'games', MatchController]);
}());