var originalModule = (function () {
    var sectionClassName = "original-section";
    var dataContainerId = "OriginalImage";

    var onPollError = function (id, url) {
        console.log("Original-module: error");
        baseModule.onPollError(sectionClassName, id, url);
    };

    var onPollSuccess = function (id, url) {
        console.log("Original-module: success");
        baseModule.onPollSuccess(sectionClassName);
        
        setImage(id, url);
    };

    var startPollingForResource = function (fileName) {
        console.log("Original-module: start polling for " + fileName);
        baseModule.startPollingForResource(sectionClassName);

        tryLoadHttpResource(
            dataContainerId,
            window.imagePipelineApp.config.OriginalsContainerUrl + fileName,
            onPollSuccess,
            onPollError);
    };

    var reset = function () {
        console.log("Original-module: reset");
        baseModule.reset(sectionClassName);
        
        setImage(dataContainerId, "");
    };

    return {
        startPollingForResource: startPollingForResource,
        reset: reset
    };
})();
