(function () {
    'use strict';
    angular
        .module("GithubSearchTool")
        .directive('gitPagingDirective', PagingDirective);

    function PagingDirective() {

        return {
            templateUrl: '/Templates/PagingTemplate.html'
        };
    }
})();