var exifModule = (function () {
    var sectionClassName = "exif-section";
    var dataContainerId = "ExifData";

    var onPollError = function (id, url) {
        console.log("EXIF-module: error");
        baseModule.onPollError(sectionClassName, id, url);
    };

    var onPollSuccess = function (id, url, data) {
        console.log("EXIF-module: success");
        baseModule.onPollSuccess(sectionClassName);

        if ($.isEmptyObject(data)) {
            $("#" + id).append("<li><p><b>No EXIF data found.</b></p></li>");
        } else {
            $.each(data, function (key, dataItem) {
                $("#" + id).append("<li><p><b>" + key + "</b>: " + dataItem + "</p></li>");
            });
        }
    };
    
    var startPollingForResource = function (fileName) {
        console.log("EXIF-module: start polling for " + fileName);
        baseModule.startPollingForResource(sectionClassName);

        tryLoadHttpResource(
            dataContainerId,
            window.imagePipelineApp.config.ExifContainerUrl + fileName + ".json",
            onPollSuccess,
            onPollError);
    };
    
    var reset = function () {
        console.log("EXIF-module: reset");
        baseModule.reset(sectionClassName);
        
        $("#" + dataContainerId).empty();
    };

    return {
        startPollingForResource: startPollingForResource,
        reset: reset
    };
})();
