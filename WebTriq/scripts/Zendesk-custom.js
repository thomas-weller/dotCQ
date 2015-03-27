var hdr = '' +
    '<div id="top">' +
        '<div id="header">' +
            '<div id="top-right">' +
                '<span id="menu_login"><a id="link_login">login</a></span>' +
                '<span id="menu_signup"> | <a id="link_signup">sign up</a></span>' +
                '<span id="menu_profile"><a id="link_profile">profile</a></span>' +
                '<span id="menu_logout"> | <a id="link_logout">logout</a></span>' +
            '</div>' +
        '</div>' +
        '<div id="gboard_header">' +
            '<div class="inner">' +
                '<div id="logo">	' +
                    '<a class="brand" href="http://www.dotcq.com" style="width: 46px; height: 46px"><img src="https://s3-eu-west-1.amazonaws.com/dotcq-projectzero/WebContent/img/logo.png" alt="logo" style="position: absolute; margin-left: 24px;margin-top: -6px; width: 46px; height: 46px"></a>' +
                '</div>' +
                '<div id="q-main-menu">' +
                    '<div class="menu-main-menu-container">' +
                        '<ul id="menu-main-menu" class="menu">' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home">Home</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/concept">Concept</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/faq">FAQ</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/calculator/index">Calculator</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/sample">Sample</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/survey">Survey</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/Forums">Forums</a></li>' +
                            '<li class="menu-item"><a href="http://www.dotcq.com/home/legal">Legal</a></li>' +
                        '</ul>' +
                    '</div>' +
                '</div>' +
            '</div>' +
        '</div>' +
    '</div>';

$j(function () {
    ensureRedirectToForums();

    $j('#top').replaceWith(hdr);

    if (currentUser.isAnonymous === true) {
        $j("#menu_logout").hide();
        $j("#menu_profile").hide();
    } else {
        $j("#menu_login").hide();
        $j("#menu_signup").hide();

        $j("#link_profile").text(currentUser.name);
        $j("#link_profile").attr('href', 'https://dotcq.zendesk.com/users/' + currentUser.id + '?return_to=https://dotcq.zendesk.com/forums');
    }

    setLinks();
});

function ensureRedirectToForums() {
    if (currentUser.isAnonymous === true || !(currentUser.isAgent === true || currentUser.isAdmin === true)) {
        var currentUrl = $j(location).attr('href').toLowerCase();
        if (currentUrl == 'https://dotcq.zendesk.com/home') {
            window.location.replace('https://dotcq.zendesk.com/forums');
        }
    }
}

function setLinks() {
    var currentUrl = $j(location).attr('href').toLowerCase();
    var returnParam = null;

    if (!(currentUser.isAgent === true || currentUser.isAdmin === true) ||
        currentUrl.search('?return_to=') == -1) {
        returnParam = '?return_to=' + currentUrl;
    }
    if (currentUser.isAnonymous === true) {
        $j("#link_login").attr('href', 'https://dotcq.zendesk.com/login' + returnParam);
        $j("#link_signup").attr('href', 'https://dotcq.zendesk.com/registration' + returnParam);
    } else {
        $j("#link_profile").attr('href', 'https://dotcq.zendesk.com/users/' + currentUser.id + returnParam);
        $j("#link_logout").attr('href', 'https://dotcq.zendesk.com/access/logout' + returnParam);
    }
}