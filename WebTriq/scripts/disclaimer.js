var cookieName = 'ret_not';

function setDisclaimerCookie() {
    $.cookie(cookieName, 1, { path: '/' });
}

function removeDisclaimerCookie() {
    $.cookie(cookieName, null);
}

function isDisclaimerCookieSet() {
    return ($.cookie(cookieName) === '1');
}

function showDisclaimerDlg() {
    $('#myModal').modal('show');
}