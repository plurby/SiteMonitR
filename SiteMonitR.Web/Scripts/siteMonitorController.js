/// <reference path="jquery-1.8.0.js" />
/// <reference path="jquery.signalR-0.5.3.js" />
/// <reference path="knockout-2.1.0.js" />
$(function () {

    $('#noSitesToMonitorMessage').hide();

    function SiteStatusItem(u, s, t) {
        var self = this;
        self.url = u;
        self.cssClass = ko.observable(s);
        self.siteStatus = ko.observable(t);
    }

    function GridViewModel(sites) {
        var self = this;
        self.items = ko.observableArray(sites);
    }

    function controller() {
        var self = this;
        self.model = new GridViewModel([]);
        self.connection = $.hubConnection();
        self.connection.logging = true;
        self.siteMonitorHub = self.connection.createProxy("SiteMonitR");

        this.updateSite = function (url, cssClass, siteStatus) {
            self.model.items().forEach(function (n) {
                if (n.url == url) {
                    n.cssClass(cssClass);
                    n.siteStatus(siteStatus);
                }
            });
        };

        self.addSite = function (url) {
            if ($('.site[data-url="' + url + '"]').length == 0) {
                var site = new SiteStatusItem(url, 'btn-warning', 'Waiting');
                self.model.items.push(site);
            }
        };

        self.updateSiteStatus = function (monitorUpdate) {
            if (monitorUpdate.Result == true) {
                self.updateSite(monitorUpdate.Url, 'btn-success', 'Up');
            }
            else {
                self.updateSite(monitorUpdate.Url, 'btn-danger', 'Down');
            }
        };

        self.toggleSpinner = function (isVisible) {
            if (isVisible == true)
                $('#spin').show();
            if (isVisible != true)
                $('#spin').hide();
        };

        self.toggleGrid = function () {
            if ($('.site').length == 0) {
                $('#noSitesToMonitorMessage').show();
                $('#sites').hide();
            }
            else {
                $('#noSitesToMonitorMessage').hide();
                $('#sites').show();
            }
        }
    }

    var c = new controller();

    c.siteMonitorHub
        .on('notifySiteStatus', function (monitorUpdate) {
            c.updateSiteStatus(monitorUpdate);
            c.toggleSpinner(false);
        })
        .on('notifySiteRemoved', function (url) {
            $('.site[data-url="' + url + '"]').remove();
            c.toggleGrid();
            c.toggleSpinner(false);
        })
        .on('notifySiteAdded', function (url) {
            $('#siteUrl').val('http://');
            $('#siteUrl').focus();
            c.toggleSpinner(false);
            c.toggleGrid();
        })
        .on('checkingSite', function (url) {
            c.toggleSpinner(false);
            c.updateSite(url, 'btn-info', 'Checking');
        })
        .on('sitesObtained', function (sites) {
            $(sites).each(function (i, site) {
                c.addSite(site);
            });
            c.toggleSpinner(false);
            c.toggleGrid();
        })
        .on('serviceIsUp', function () {
            c.toggleSpinner(true);
            c.siteMonitorHub.invoke('getSiteList');
        });;

    $('#addSite').click(function () {
        var u = $('#siteUrl').val();
        c.addSite(u);
        c.toggleSpinner(true);
        c.siteMonitorHub.invoke('addSite', u);
    });

    $('.removeSite').live('click', function () {
        c.toggleSpinner(true);
        var url = $(this).data('url');

        $('.site[data-url="' + url + '"]').fadeOut('fast', function () {
            $('.site[data-url="' + url + '"]').remove();
        });

        c.siteMonitorHub.invoke('removeSite', url);
    });

    c.connection.start().done(function () {
        c.toggleSpinner(true);
        c.siteMonitorHub.invoke('getSiteList');
    });

    ko.applyBindings(c.model);

    $('#siteUrl').val('http://');
});