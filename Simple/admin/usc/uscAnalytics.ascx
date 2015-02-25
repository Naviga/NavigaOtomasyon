<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscAnalytics.ascx.cs" Inherits="Ws.admin.usc.uscAnalytics" %>
<style>
    #view-selector table
    {
        float: left;
    }
    #timeline,#sessions,#toppages {
        min-height: 350px;
        max-height: 350px;
        overflow: auto;
        overflow-x: hidden;
        overflow-y: auto;
    }
</style>
<!-- Step 1: Create the containing elements. -->
<div class="row">
    <div class="large-12">

        <div class="row">
            <div class="panel large-12">
                <section id="auth-button" class="right"></section>
                <section id="view-selector"></section>
                <div class="clear"></div>
            </div>
            <div class="panel large-4 columns">
                <section id="timeline"></section>
            </div>
            <div class="panel large-4 columns">
                <section id="sessions"></section>
            </div>
            <div class="panel large-4 columns">
                <section id="toppages"></section>
            </div>
        </div>
    </div>
</div>

<!-- Step 2: Load the library. -->

<script>
    (function (w, d, s, g, js, fjs) {
        g = w.gapi || (w.gapi = {}); g.analytics = { q: [], ready: function (cb) { this.q.push(cb) } };
        js = d.createElement(s); fjs = d.getElementsByTagName(s)[0];
        js.src = 'https://apis.google.com/js/platform.js';
        fjs.parentNode.insertBefore(js, fjs); js.onload = function () { g.load('analytics') };
    }(window, document, 'script'));
</script>

<script>
    gapi.analytics.ready(function () {

        // Step 3: Authorize the user.

        var CLIENT_ID = '150298425145-0qste6f0idqpm1jsrp320b547a7spr08.apps.googleusercontent.com'; //localhost:61180
        //var CLIENT_ID = '150298425145-6qj9bf2khn0t7pbu3lbr4ikmageec2p8.apps.googleusercontent.com'; //demo.finexmedia.com
        //var CLIENT_ID = '150298425145-jdksufche8e34mga5mmcptc7b7q7j9uv.apps.googleusercontent.com'; //finexmedia.com

        gapi.analytics.auth.authorize({
            container: 'auth-button',
            clientid: CLIENT_ID,
        });

        // Step 4: Create the view selector.

        var viewSelector = new gapi.analytics.ViewSelector({
            container: 'view-selector'
        });

        // Step 5: Create the timeline chart.

        var timeline = new gapi.analytics.googleCharts.DataChart({
            reportType: 'ga',
            query: {
                'dimensions': 'ga:date',
                'metrics': 'ga:sessions',
                'start-date': '30daysAgo',
                'end-date': 'yesterday',
            },
            chart: {
                type: 'LINE',
                container: 'timeline',
                options: {
                    width: '100%'
                }
            }
        });

        var sessions = new gapi.analytics.googleCharts.DataChart({
            reportType: 'ga',
            query: {
                'dimensions': 'ga:country',
                'metrics': 'ga:sessions'
            },
            chart: {
                type: 'GEO',
                container: 'sessions',
                options: {
                    width: '100%'
                }
            }
        });

        var topPages = new gapi.analytics.googleCharts.DataChart({
            reportType: 'ga',
            query: {
                'dimensions': 'ga:pagePath',
                'metrics': 'ga:sessions',
                'sort': '-ga:sessions',
                'maxResults': '8'
            },
            chart: {
                type: 'TABLE',
                container: 'toppages',
                options: {
                    width: '100%'
                }
            }
        });
        //  <ga-chart
        //  title="Top pages"
        //  type="TABLE"
        //  metrics="ga:sessions"
        //  dimensions="ga:pagePath"
        //  sort="-ga:sessions"
        //  maxResults="8">
        //</ga-chart>

        //  <section id="charts">
        //<ga-chart
        //  title="Timeline"
        //  type="LINE"
        //  metrics="ga:sessions"
        //  dimensions="ga:date">
        //</ga-chart>

        //<ga-chart
        //  title="Sessions (by country)"
        //  type="GEO"
        //  metrics="ga:sessions"
        //  dimensions="ga:country">
        //</ga-chart>

        // Step 6: Hook up the components to work together.

        gapi.analytics.auth.on('success', function (response) {
            viewSelector.execute();
        });

        viewSelector.on('change', function (ids) {
            var newIds = {
                query: {
                    ids: ids
                }
            }
            timeline.set(newIds).execute();
            sessions.set(newIds).execute();
            topPages.set(newIds).execute();
        });
    });
</script>
