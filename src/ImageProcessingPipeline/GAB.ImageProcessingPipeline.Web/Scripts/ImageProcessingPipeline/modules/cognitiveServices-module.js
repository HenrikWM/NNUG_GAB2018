var cognitiveServicesModule = (function () {
    var sectionClassName = "cognitiveServices-section";
    var dataContainerId = "CognitiveServicesData";

    var onPollError = function (id, url) {
        console.log("CognitiveServices-module: error");
        baseModule.onPollError(sectionClassName, id, url);
    };

    var getFilteredData = function (data, filter) {
        var items = data.filter(filter);
        if (items.length === 0)
            return null;

        return items.map(e => e.Name).join(", ");
    }

    var onPollSuccess = function (id, url, data) {
        console.log("CognitiveServices-module: success");
        baseModule.onPollSuccess(sectionClassName);

        if ($.isEmptyObject(data)) {
            $("#" + id).append("<li><p><b>No data from Cognitive Services.</b></p></li>");
            return;
        }
        
        var tags = getFilteredData(data.Tags, function (element) { return element.Confidence > 0.8; });
        if (tags !== null) {
            $("#" + id).append("<li><p><b>Tags</b>: " + tags + "</p></li>");
        }

        var categories = getFilteredData(data.Categories, function (element) { return element.Score > 0.6; });
        if (categories !== null) {
            $("#" + id).append("<li><p><b>Categories</b>: " + categories + "</p></li>");
        }
    };
    
    var startPollingForResource = function (fileName) {
        console.log("CognitiveServices-module: start polling for " + fileName);
        baseModule.startPollingForResource(sectionClassName);

        tryLoadHttpResource(
            dataContainerId,
            window.imagePipelineApp.config.CognitiveServicesContainerUrl + fileName + ".json",
            onPollSuccess,
            onPollError);
    };

    var reset = function () {
        console.log("CognitiveServices-module: reset");
        baseModule.reset(sectionClassName);

        $("#" + dataContainerId).empty();
    };

    return {
        startPollingForResource: startPollingForResource,
        reset: reset
    };
})();
