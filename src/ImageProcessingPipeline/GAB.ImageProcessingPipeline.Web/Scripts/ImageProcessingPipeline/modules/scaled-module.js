var scalingModule = (function () {
    var sectionClassName = "scaled-section";
    var dataContainerId1 = "image-scaled-lg";
    var dataContainerId2 = "image-scaled-md";
    var dataContainerId3 = "image-scaled-sm";

    var onPollError = function (id, url) {
        console.log("Scaled-module: error");
        baseModule.onPollError(sectionClassName);
    };

    var onPollSuccess = function(id, url) {
        console.log("Scaled-module: success");
        baseModule.onPollSuccess(sectionClassName);

        setImage(id, url);
    };

    var startPollingForResource = function (fileName) {
        console.log("Scaled-module: start polling for " + fileName);
        baseModule.startPollingForResource(sectionClassName);

        tryLoadHttpResource(
            dataContainerId1,
            window.imagePipelineApp.config.LargeImagesContainerUrl + fileName,
            onPollSuccess,
            onPollError);

        tryLoadHttpResource(
            dataContainerId2,
            window.imagePipelineApp.config.MediumImagesContainerUrl + fileName,
            onPollSuccess,
            onPollError);

        tryLoadHttpResource(
            dataContainerId3,
            window.imagePipelineApp.config.SmallImagesContainerUrl + fileName,
            onPollSuccess,
            onPollError);
    };

    var reset = function () {
        console.log("Scaled-module: reset");
        baseModule.reset(sectionClassName);

        setImage(dataContainerId1, window.imagePipelineApp.loaderGifUrl);
        setImage(dataContainerId2, window.imagePipelineApp.loaderGifUrl);
        setImage(dataContainerId3, window.imagePipelineApp.loaderGifUrl);
    };
    
    return {
        startPollingForResource: startPollingForResource,
        reset: reset
    };
})();
