function ClickHandler() {
    _this = this;
    this.init = function () {
        if ($('#Status').text() == "") {
            $('div[position]').click(function () {
                if ($(this).text() == "") {
                    $.post(
                        '/Game/Game/' + $("#GameId").data("id"),
                        { 'position': $(this).attr('position') },
                        function (data) {
                            $('#game-field').html(data);
                            _this.init();
                        });
                }
            });
        }
    }
}

var clickHandler = null;
$().ready(function () {
    clickHandler = new ClickHandler();
    clickHandler.init();
});