(function(){
    'use strict';

    function games(data){
        function getGamesBySportName(name, all) {
            if(all) {
                return data.get('api/sports/' + name + '?all=true');
            }
            return data.get('api/sports/' + name);
        }


        function getGamesByEventName(name, eventKey, all) {
            if(all) {
                return data.get('api/events/' + eventKey + '?all=true');
            }
            return data.get('api/events/' + eventKey );
        }

        function matchDetails(id) {
            return data.get('api/match/' + id);
        }

        return {
            getGamesBySportName : getGamesBySportName,
            getGamesByEventName : getGamesByEventName,
            matchDetails: matchDetails
        }
    }

    angular.module('myApp.services')
        .factory('games', ['data', games])
}());