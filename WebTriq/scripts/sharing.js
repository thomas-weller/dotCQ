function doShare(name) {

    var href = 'www.dotcq.com';
    var title = '.CQ';

    if (name == 'email') {
        sendLocalMail();
    } else if (name == 'gmail') {
        sendGmail();
    } else if (name == 'bookmark') {
        bookmark(title, href);
    } else {
        shareDlg(name, title, href);
    }
    return false;
}

function getBaseURL() {
    return location.protocol + "//" + location.hostname +
      (location.port && ":" + location.port) + "/";
}
function shareDlg(name, title, href) {
    var url;
    var width = 500;
    var height = 400;

    switch (name) {
        case 'facebook':
            url = 'https://www.facebook.com/sharer/sharer.php?s=100' +
                      '&p[url]=' + href +
                      '&p[title]=' + title +
                      '&p[summary]=' + $('#social_summary').val() +
                      '&p[images][0]=' + $('#fb_image').val();
            break;
        case 'delicious':
            height = 500;
            url = 'http://delicious.com/save?v=5&noui&jump=close' +
                      '&url=' + href +
                      '&title=' + title;
            break;
        case 'twitter':
            height = 350;
            url = 'http://twitter.com/share' +
                      '?url=' + href +
                      '&counturl=' + href +
                      '&text=' + $('#twitter_text').val() +
                      '&hashtags=dotcq';
            break;
        case 'gplus':
            url = 'https://plus.google.com/share' +
                      '?url=' + href;
            break;
        case 'linkedin':
            height = 440;
            url = 'http://www.linkedin.com/shareArticle?mini=true' +
                      '&url=' + href +
                      '&title=' + title +
                      '&summary=' + $('#social_summary').val();
            break;
        default:
            return;
    }

    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;

    var win = window.open(url, 'share', 'width=' + width + ',height=' + height);
    win.moveTo(left, top);
}

function bookmark(title, url) {
    try {
        if (window.sidebar) {
            window.sidebar.addPanel(title, url, "");
        } else if (document.all) {
            window.external.AddFavorite(url, title);
        } else if ((window.opera && window.print) || window.chrome) {
            throw '';
        }
    } catch(e) {
        alert('Your browser does not support adding bookmarks programmatically.\n\n' +
                'Press Ctrl+D (or Command+D for Macs) instead.');
    }
}

function sendGmail() {
    var str = 'http://mail.google.com/mail/?view=cm&fs=1' +
            '&su=' + $('#mail_subject').val() +
            '&body=' + $('#mail_body').val() +
            '&ui=1';

    window.open(str);
}

function sendLocalMail() {
    var str = 'mailto:' +
            '?subject=' + $('#mail_subject').val() +
            '&body=' + $('#mail_body').val();

    window.location.href = str;

}
