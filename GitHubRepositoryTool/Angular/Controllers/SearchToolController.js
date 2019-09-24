(function () {
    'use strict';
    angular
        .module("GithubSearchTool")
        .controller('SearchToolController', ['$http', 'PagingService', SearchToolController]);

    function SearchToolController($http, PagingService) {


        //#region Init
        var ctr = this;

        ctr.isLoaded = false;
        ctr.PagingService = PagingService;
        ctr.pagingObject = {};
        ctr.repositoryItems;
        ctr.SetPage = SetPage;
        ctr.SearchForGitRepository = SearchForGitRepository;
        ctr.SaveReposotory = SaveReposotory;
        ctr.gitHubRepositories;


        angular.element(document).ready(function () {
            ctr.isLoaded = true;
        });

        //#endregion

        //#region functions

        //search for github reposotory via api
        function SearchForGitRepository(keyword) {
            ctr.isLoaded = false;
            var url = "https://api.github.com/search/repositories?q=" + keyword;
            $http.get(url)
                .then(ParseGitRepositoryResponse, RequestError);
        }

        //save repository items into controller object 
        function ParseGitRepositoryResponse(response) {
            //Request succeed
            if (response.status === 200) {
                ctr.gitHubRepositories = response.data.items;
                SetPage(1);
            }
            //Error handing
            else {
                Notification("fail", "request error");
            }
            ctr.isLoaded = true;
        }

        function RequestError(e) {
            Notification("fail", "status : " + e.status + ", message :" + e.statusText);
            ctr.isLoaded = true;
        }

        //save repository in server session.
        function SaveReposotory(repository) {
            $http({
                method: 'POST',
                url: '/GitHubSearch/SaveBookMark',
                data: { "repository": JSON.stringify(repository) },
                headers: { 'Content-Type': 'application/json;' },
                dataType: "json"
            }).then(SaveReposotoryResult);

        }

        function SaveReposotoryResult(response) {
            //Request succeed
            if (response.status === 200) {
                if (response.data === "ok") {
                    Notification("success", "repository successfully saved");
                }
                else {
                    Notification("fail", response.data);
                }
            }
            //Error handing
            else {
                Notification("fail", "request error");
            }
            ctr.isLoaded = true;
        }

        function Notification(type, message) {
            var str;
            if (type === "success") {
                str = ' <div class="alert alert-success" style="padding:5px;margin:0px"><strong> Success! </strong >' + message + '<button type = "button" class="close" data-dismiss="alert" aria-label="Close" > <span aria-hidden="true">&times;</span> </button > </div >';
            }
            if (type === "fail") {
                str = ' <div class="alert alert-danger" style="padding:5px;margin:0px"><strong> Failed! </strong>' + message + '<button type = "button" class="close" data-dismiss="alert" aria-label="Close" > <span aria-hidden="true">&times;</span> </button > </div >';
            }
            var newAlert = $(str);
            newAlert.appendTo('#notifications');

            setTimeout(function () {
                newAlert.fadeTo(500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 4000);
        }

        //#endregion

        //#region Paging

        //switch between repository pages
        function SetPage(page) {
            if (page < 1 || (ctr.pagingObject.totalPages !== undefined && page > ctr.pagingObject.totalPages)) {
                return;
            }

            // get paging object from service
            ctr.pagingObject = ctr.PagingService.GetPager(ctr.gitHubRepositories.length, page, 12);

            // get current page of items
            ctr.repositoryItems = ctr.gitHubRepositories.slice(ctr.pagingObject.startIndex, ctr.pagingObject.endIndex + 1);
        }

        //#endregion

    }
})();