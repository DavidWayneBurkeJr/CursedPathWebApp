﻿(function ($) {
    function Song() {
        var $this = this;

        function initilizeModel() {
            $("#modal-action-song").on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () {
            initilizeModel();
        };
    }
    $(function () {
        var self = new Song();
        self.init();
    });
}(jQuery));