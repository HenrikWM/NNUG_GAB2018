function tryLoadHttpResource(id, url, onSuccess, onError, retryCount) {
    var attempsLimit = 8;

    if (retryCount >= attempsLimit) {
        console.log("tryLoadHttpResource: failed loading '" + id + "' from: " + url);
        onError(id, url);

        return;
    }

    if (retryCount === undefined)
        retryCount = 1;

    var interval = 1000 * Math.pow(2, retryCount);

    $.ajax(url)
        .success(function (data) {
            onSuccess(id, url, data);
        })
        .error(function () {
            setTimeout(
                function () {
                    retryCount += 1;
                    tryLoadHttpResource(id, url, onSuccess, onError, retryCount);
                },
                interval
            );
        });
}
