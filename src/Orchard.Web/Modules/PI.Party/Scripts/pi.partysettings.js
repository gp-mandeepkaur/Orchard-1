function PI_PartySettings() {
    this.PIConnectionStringSettings = function () {

        if ($('#hdnPassword').val()) {
            $('#txtPassword').removeAttr("required");
        }       

        var SaveMessage = (function () {

            //Creating the demo window
            function _createWindow() {

                $('#saveMessageWindow').jqxWindow({
                    showCloseButton: true, maxHeight: 400, maxWidth: 700, minHeight: 100, minWidth: 200, height: 150, width: 300, isModal: true, modalOpacity: 0.3,
                    initContent: function () {
                        $('#saveMessageWindow').jqxWindow('focus');
                    }
                });
            };

            function _settings() {
                var winHeight = $(window).height();
                var winWidth = $(window).width();
                var posX = (winWidth / 2) - ($('#saveMessageWindow').width() / 2) + $(window).scrollLeft();
                var posY = (winHeight / 2) - ($('#saveMessageWindow').height() / 2) + $(window).scrollTop();

                //KEEP CENTERED
                $('#saveMessageWindow').jqxWindow({ position: { x: posX, y: posY }, height: 150, width: 300 });

            }

            return {
                init: function () {
                    //Adding jqxWindow
                    _createWindow();
                    _settings();

                }
            };
        }());

        $('form').submit(function (e) {
            e.preventDefault();
            $("#jqxLoader").jqxLoader({ width: 200, height: 60, imagePosition: 'center', text: 'Testing connection' });
            var form = this;
            $("#jqxLoader").jqxLoader('open');
            $.ajax({
                url: '../../PI.Party/Admin/ValidateConnectionSettings',
                type: 'POST',
                data: $('form').serialize(),
                dataType: "html",
                success: function (data) {
                    data = JSON.parse(data);
                    if (data) {
                        form.submit();
                    }
                    else {
                        SaveMessage.init();
                        var html = "<ul class='ulError'>";
                        html += "<li>Party Connection settings are not valid.</li>";
                        html += "</ul>";

                        $('#saveMessageWindowContent').html(html);
                        $('#saveMessageWindow').jqxWindow('open');
                    }
                    $('#jqxLoader').jqxLoader('close');
                },
                error: function () {
                    $('#jqxLoader').jqxLoader('close');
                }
            });
        });
    }
}