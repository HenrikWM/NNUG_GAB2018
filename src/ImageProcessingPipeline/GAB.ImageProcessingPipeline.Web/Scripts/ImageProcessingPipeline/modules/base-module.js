var baseModule = (function () {
    var onPollError = function (sectionClassName, id, url) {
        $("." + sectionClassName + " .panel").toggleClass("panel-error");
        $("." + sectionClassName + " .image-loading").hide();
    };

    var onPollSuccess = function (sectionClassName) {
        $("." + sectionClassName + " .panel").toggleClass("panel-default");
        $("." + sectionClassName + " .panel").toggleClass("panel-success");
        
        $("." + sectionClassName + " .image-loading").hide();
    };

    var startPollingForResource = function (sectionClassName) {
        $("." + sectionClassName).show();

        $("." + sectionClassName + " .image-loading").show();
    };

    var reset = function (sectionClassName) {
        $("." + sectionClassName + " panel").toggleClass("panel-success");
        $("." + sectionClassName + " panel").toggleClass("panel-default");
        $("." + sectionClassName).hide();
    };

    return {
        onPollError: onPollError,
        onPollSuccess: onPollSuccess,
        startPollingForResource: startPollingForResource,
        reset: reset
    }
})();
