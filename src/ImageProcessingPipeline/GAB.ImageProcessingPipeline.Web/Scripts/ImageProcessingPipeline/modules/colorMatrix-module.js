var colorMatrixModule = (function () {
    var sectionClassName = "colormatrix-section";
    var dataContainerId1 = "image-colormatrix-grey-lg";
    var dataContainerId2 = "image-colormatrix-grey-md";
    var dataContainerId3 = "image-colormatrix-grey-sm";

    var onPollError = function (id, url) {
        console.log("ColorMatrix-module: error");
        baseModule.onPollError(sectionClassName, id, url);
    };

    var onPollSuccess = function (id, url) {
        console.log("ColorMatrix-module: success");
        baseModule.onPollSuccess(sectionClassName);

        setImage(id, url);
    };
    
    var startPollingForResource = function (fileName) {
        console.log("ColorMatrix-module: start polling for " + fileName);
        baseModule.startPollingForResource(sectionClassName);

        tryLoadHttpResource(
            dataContainerId1,
            window.imagePipelineApp.config.LargeColorMatrixContainerUrl + "grayscale-" + fileName,
            onPollSuccess,
            onPollError);

        tryLoadHttpResource(
            dataContainerId2,
            window.imagePipelineApp.config.MediumColorMatrixContainerUrl + "grayscale-" + fileName,
            onPollSuccess,
            onPollError);

        tryLoadHttpResource(
            dataContainerId3,
            window.imagePipelineApp.config.SmallColorMatrixContainerUrl + "grayscale-" + fileName,
            onPollSuccess,
            onPollError);
    };

    var reset = function () {
        console.log("ColorMatrix-module: reset");
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
