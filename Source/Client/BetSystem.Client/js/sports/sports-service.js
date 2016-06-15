(function(){
    'use strict';

    function sports(data) {
        function getSports() {
            return data.get('api/sports');
        }

        return {
            getSports: getSports
        }
    }

    angular.module('myApp.services')
        .factory('sports', ['data', sports])
}());