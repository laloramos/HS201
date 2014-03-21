$(function () {
    $("ul.nav li").removeClass("active");

    // Get the current URL
    var currentId = window.location.pathname.replace("/","") + "Link";

    var currentLi = $("#" + ((currentId === "Link") ? "HomeLink" : currentId));

    // Then add your class

    if (currentLi != undefined)
        currentLi.addClass("active");
});