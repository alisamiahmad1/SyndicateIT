
function selectNavigation() {
    var $li = $(this);
    if ($li.hasClass('active').toString() == 'true') {
        if ($li.hasClass('open').toString() == 'true') {
            $li.removeClass('open');
            $li.find('ul').hide();
        } else {

            $li.addClass('open');
            $li.find('ul').show();
        }
    }
    else {
        $('#nav li').each(function () {
            var $li = $(this);
            $li.removeClass('open');
            $li.removeClass('active');
            $li.find('ul').hide();
        });
        $li.addClass('active open');
        $li.find('ul').show();
    }
}

function selectProfile() {
    var $li = $(".nav-profile");
    if ($li.hasClass('open').toString() == 'true') {
        $li.removeClass('open');
    } else {
        $li.addClass('open');
    }
}
function toggleBar() 
{
    ClearCurrentOpen();
    UpdateCurrentNav();
};
function ClearCurrentOpen() {
    $('#nav li').each(function () {
        var $li = $(this);
        $li.removeClass('open');
        $li.find('ul').hide();
    });
}
function UpdateCurrentNav() {
    if ($("#app").attr('class') == "app") {
        $('#app').addClass('nav-collapsed-min');
    } else {
        $('#app').removeClass('nav-collapsed-min');
    }
}