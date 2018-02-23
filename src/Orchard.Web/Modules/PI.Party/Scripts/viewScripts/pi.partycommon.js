$("#jqxLoader").jqxLoader({ width: 200, height: 60, imagePosition: 'center' });

function ShowMessage(msg) {
    createPopupWindow('#messageWindow').init();
    $('#messageWindow').show();
    $('#messageWindowContent').html(msg === undefined ? "Some error occured.Please Refresh the page or contact support if error persists." : msg);
    $('#messageWindow').jqxWindow('open');
}


function createPopupWindow(windowId, popupHeight, popupWidth) {
    if (typeof (popupHeight) === 'undefined') popupHeight = 'auto';
    if (typeof (popupWidth) === 'undefined') popupWidth = 'auto';
    //Creating the demo window
    function _createWindow() {
        $(windowId).jqxWindow({
            showCloseButton: true, isModal: true,
            maxHeight: 700, maxWidth: 700, minHeight: 150, minWidth: 300,
            height: popupHeight, width: popupWidth, modalOpacity: 0.3, draggable: true, resizable: true,
            initContent: function () {
                $(windowId).jqxWindow('focus');
            }
        });
    };
    function _settings() {
        var winHeight = $(window).height();
        var winWidth = $(window).width();
        var posX = (winWidth / 2) - ($(windowId).width() / 2) + $(window).scrollLeft();
        var posY = (winHeight / 2) - ($(windowId).height() / 2) + $(window).scrollTop();
        //KEEP CENTERED
        $(windowId).jqxWindow({ position: { x: posX, y: posY } });
    }

    return {
        init: function () {
            //Adding jqxWindow
            _createWindow();
            _settings();
        }
    };
}


(function ($) {
    $.httpPost = function (options) {
        // Extend our default options with those provided.
        // Note that the first argument to extend is an empty
        // object – this is to keep from overriding our "defaults" object.
        var opts = $.extend({}, $.httpPost.defaults, options);
        // Ajax plugin implementation code
        CallToServer(opts);
    };
    $.httpGet = function (options) {
        // Extend our default options with those provided.
        // Note that the first argument to extend is an empty
        // object – this is to keep from overriding our "defaults" object.
        var opts = $.extend({}, $.httpGet.defaults, options);
        // Ajax plugin implementation code
        CallToServer(opts);
    };
    // httpPost Plugin defaults – added as a property on our plugin function.
    $.httpPost.defaults = {
        type: 'POST',
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        processData: true,
        loader: true,
        data: null,
        success: null,
        error: null,
        complete: null
    };
    // httpPost Plugin defaults – added as a property on our plugin function.
    $.httpGet.defaults = {
        type: 'GET',
        dataType: "json",
        loader: true,
        data: null,
        success: null,
        error: null,
        complete: null
    };

    function CallToServer(opts) {
        $.ajax({
            beforeSend: function () {
                if (opts.loader)
                    $('#jqxLoader').jqxLoader('open');
            },
            url: opts.url,
            dataType: opts.dataType,
            type: opts.type,
            data: opts.data,
            processData: opts.processData,
            contentType: opts.contentType,
            success: function (data) {
                // check success callback
                if (opts.success != null && typeof opts.success == 'function') {
                    opts.success(data);
                }
                //else {
                //    console.log("secound argumnt (successcallback) is not a function");
                //    throw "successcallback must be a function";
                //    return;
                //}
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // check error callback
                if (opts.error != null && typeof opts.error == 'function') {
                    opts.error({ jqXHR: jqXHR, textStatus: textStatus, errorThrown: errorThrown });
                }
                ShowMessage();
            },
            complete: function () {
                if (opts.loader)
                    $('#jqxLoader').jqxLoader('close');
                // check complete callback
                if (opts.complete != null && typeof opts.complete == 'function') {
                    opts.complete();
                }
            }
        });
    }
}(jQuery));