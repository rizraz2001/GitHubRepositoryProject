(function () {
    'use strict';
    angular
        .module("GithubSearchTool")
        .controller('BookmarkController', ['$http', 'PagingService', BookmarkController]);

    function BookmarkController($http, PagingService) {

        var ctr = this;
        ctr.PagingService = PagingService;
        ctr.pagingObject = {};
        ctr.repositoryItems;
        ctr.gitHubRepositories = [];
        ctr.SetPage = SetPage;

        angular.element(document).ready(function () {
            //Get all bookmarks that saved in the session on page load
            GetBookmarks();
        });

        function GetBookmarks() {
            var url = "/GitHubSearch/GetAllBookMarks";
            $http.get(url)
                .then(ParseGitRepositoryResponse, RequestError);
        }

        //save repository items into controller object 
        function ParseGitRepositoryResponse(response) {
            //Request succeed
            if (response.status === 200) {
                if (response.data !== "") {
                    for (var item of response.data.Data) {
                        ctr.gitHubRepositories.push(JSON.parse(item));
                    }
                }
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