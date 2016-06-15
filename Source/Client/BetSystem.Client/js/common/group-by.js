(function () {
    'use strict';

    function groupBy() {
        return _.memoize(function (items, field) {
                return _.groupBy(items, field);
            }
        );
    }

    angular.module('myApp.filters')
        .filter('groupBy', [groupBy])
}());