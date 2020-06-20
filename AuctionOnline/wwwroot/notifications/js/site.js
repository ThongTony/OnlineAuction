/// <reference path="../lib/signalr/browser/signalr.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalRHub").build();

    connection.start();

    connection.on("refreshNotifications", function () {
        loadData();
    });

    loadData();

    function loadData() {
        var tr = '';

        $.ajax({
            url: '/NotifiedExpiredItem/GetExpiredItems',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    //tr = tr + `<tr style="width:90px;">
                    //    <td style="width:90px;">${v.itemId}</td>
                    //    <td style="width:90px;">${v.currentDate}</td>
                    //    <td style="width:90px;">${v.isExpired}</td>
                    //    <td style="width:90px;">${v.isSeen}</td>
                    //    <td style="width:140px;"><a href="/Item/Details/${v.itemId}">See Expired Item</a></td>
                    //</tr>`;

                    tr = tr + `<li style="font-size:12.5px; margin-left: -20px; margin-right: 8px; float:left;">
                            <a href="/Item/Details/${v.itemId}" role="button" tabindex="0">
                                <div>
                                    <div style="float:left;margin-right:10px;">
                                        <img src="https://scontent.xx.fbcdn.net/v/t1.0-1/cp0/p48x48/55738269_2813143815370024_4176931368388788224_n.jpg?_nc_cat=106&amp;_nc_sid=dbb9e7&amp;_nc_ohc=r1_aiKlQr2oAX9slP3O&amp;_nc_ad=z-m&amp;_nc_cid=0&amp;_nc_ht=scontent.xx&amp;oh=0b4355eaa6036f3654a35daeac226554&amp;oe=5F0C85E2">
                                    </div>
                                    <div>
                                        <span>
                                            <span>Nước hoa Charme Bình Dương</span>
                                            <span> checked in at </span>
                                            <span>CALLA Cosmetics</span>
                                            <span>: "1. Nước hoa Charme..."</span>
                                        </span>
                                        <div>
                                            <div style="float:left;">
                                                <img src="https://www.facebook.com/rsrc.php/v3/yT/r/-IWvKsOGqat.png">
                                            </div>
                                            <div>
                                                <span>
                                                    <abbr aria-label="10 minutes ago" minimize="true" title="Tuesday, 16 June 2020 at 22:00" data-utime="1592319605" data-minimize="true">
                                                        <span class="timestampContent">10m - ${v.currentDate}</span>
                                                    </abbr>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>`;

                });

                $("#notificationBody").html(tr);
                $("#notification_count").html(result.length);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});