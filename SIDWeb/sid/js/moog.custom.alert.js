// Custom jQuery Alert Dialogs Plugin
// Customizado por moogriento.
// Se toma como base a :
//
// Cory S.N. LaViska
// A Beautiful Site (http://abeautifulsite.net/)
//
// Uso:
//		jcAlert = function (titulo, mensaje, callback)
//		jcConfirm = function (titulo, mensaje, callback)
// 
// Licencia:a
// 
// Este plugin está licenciado bajo GNU General Public License
//
(function ($) {

    $.cAlert = {

        relativePath: '',                   // relative path of app
        verticalOffset: -75,                // vertical offset of the dialog from center screen, in pixels
        horizontalOffset: 0,                // horizontal offset of the dialog from center screen, in pixels/
        repositionOnResize: true,           // re-centers the dialog on window resize
        overlayOpacity: .50,                // transparency level of overlay
        overlayColor: '#000000',               // base color of overlay

        // Public methods
        alert: function (titulo, mensaje, despedida, tipo, callback) {
            $.cAlert._showAlert(titulo, mensaje, despedida, tipo, function (result) {
                if (callback) callback(result);
            });
        },

        confirm: function (titulo, mensaje, despedida, callback) {
            $.cAlert._showConfirm(titulo, mensaje, despedida, function (result) {
                if (callback) callback(result);
            });
        },

        _showAlert: function (titulo, mensaje, callback) {
            $.cAlert._hide();
            $.cAlert._overlay('show');

            $("BODY").append(
		        '<div id="divAlertContainer" class="alertMainContainer">' +
                    '<div id="divTextContainer" class="alertTextContainer">' +
                        '<div id="divTitulo" class="alertTitulo"></div>' +
                        '<div id="divMensaje" class="alertMensaje"></div>' +
                        '<div id="divButtonContainer" class="alertButtonContainer">' +
                            '<input id="btnAlertAceptar" type="button" value="Aceptar" class="button" />' +
                        '</div>' +
                    '</div>' +
                    '<div style="clear: both;"></div>' +
                '</div>');

            $("#divAlertContainer").css({
                position: 'fixed',
                zIndex: 99999,
                padding: 0,
                margin: 0
            });

            $("#divTitulo").html(titulo);
            $("#divMensaje").html(mensaje);

            $.cAlert._reposition();
            $.cAlert._maintainPosition(true);

            $("#btnAlertAceptar").click(function () {
                $.cAlert._hide();
                callback(true);
            });
        },

        _showConfirm: function (titulo, mensaje, callback) {
            $.cAlert._hide();
            $.cAlert._overlay('show');

            $("BODY").append(
		        '<div id="divAlertContainer" class="alertMainContainer">' +
                    '<div id="divTextContainer" class="alertTextContainer">' +
                    '<div id="divTitulo" class="alertTitulo"></div>' +
                    '<div id="divMensaje" class="alertMensaje"></div>' +
                    '<div id="divButtonContainer" class="alertButtonContainer">' +
                        '<input id="btnAlertNo" type="button" value="Cancelar" class="alt-button" />&nbsp;' +
                        '<input id="btnAlertSi" type="button" value="Aceptar" class="button" />' +
                    '</div>' +
                '</div>' +
                '<div style="clear: both;"></div>' +
            '</div>');

            $("#divAlertContainer").css({
                position: 'fixed',
                zIndex: 99999,
                padding: 0,
                margin: 0
            });

            $("#divTitulo").html(titulo);
            $("#divMensaje").html(mensaje);

            $.cAlert._reposition();
            $.cAlert._maintainPosition(true);

            $("#btnAlertSi").click(function () {
                $.cAlert._hide();
                callback(true);
            });
            $("#btnAlertNo").click(function () {
                $.cAlert._hide();
                callback(false);
            });
        },

        _hide: function () {
            $("#divAlertContainer").remove();
            $.cAlert._overlay('hide');
            $.cAlert._maintainPosition(false);
        },

        _overlay: function (status) {
            switch (status) {
                case 'show':
                    $.cAlert._overlay('hide');
                    $("BODY").append('<div id="popup_overlay"></div>');
                    $("#popup_overlay").css({
                        position: 'absolute',
                        zIndex: 99998,
                        top: '0px',
                        left: '0px',
                        width: '100%',
                        height: $(document).height(),
                        background: $.cAlert.overlayColor,
                        opacity: $.cAlert.overlayOpacity,
                        filter: 'alpha(opacity=50)'
                    });
                    break;
                case 'hide':
                    $("#popup_overlay").remove();
                    break;
            }
        },

        _reposition: function () {
            var top = (($(window).height() / 2) - ($("#divAlertContainer").outerHeight() / 2)) + $.cAlert.verticalOffset;
            var left = (($(window).width() / 2) - ($("#divAlertContainer").outerWidth() / 2)) + $.cAlert.horizontalOffset;
            if (top < 0) top = 0;
            if (left < 0) left = 0;

            $("#divAlertContainer").css({
                top: top + 'px',
                left: left + 'px'
            });
            $("#popup_overlay").height($(document).height());
        },

        _maintainPosition: function (status) {
            if ($.cAlert.repositionOnResize) {
                switch (status) {
                    case true:
                        $(window).bind('resize', $.cAlert._reposition);
                        break;
                    case false:
                        $(window).unbind('resize', $.cAlert._reposition);
                        break;
                }
            }
        }
    }

    jcAlert = function (titulo, mensaje, callback) {
        $.cAlert.alert(titulo, mensaje, callback);
    };

    jcConfirm = function (titulo, mensaje, callback) {
        $.cAlert.confirm(titulo, mensaje, callback);
    };

})(jQuery);