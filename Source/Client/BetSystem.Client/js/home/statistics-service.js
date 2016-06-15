(function(){
    'use strict';

    function statistics(data) {
        function getStats() {
            return data.get('api/stats');
        }

        return {
            getStats: getStats
        }
    }

    angular.module('myApp.services')
        .factory('statistics', ['data', statistics])
}());